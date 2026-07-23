using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZT.MTHModels
{
    /// <summary>
    /// 通信设备实体类
    /// 封装一台Modbus TCP设备的连接参数、通信组集合、变量实时值字典，
    /// 并提供报警检测与触发机制。是整个应用通信与数据管理的核心数据结构。
    /// </summary>
    public class Device
    {
        /// <summary>
        /// 设备IP地址
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// 端口号（Modbus TCP默认502）
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 当前配方名称（从INI读取，标识当前生效的工艺配方）
        /// </summary>
        public string CurrentRecipe {  get; set; }

        /// <summary>
        /// 通信组集合：每个Group定义一段连续的Modbus存储区及其下挂的变量列表
        /// </summary>
        public List<Group> GroupList { get; set; }


        /// <summary>
        /// 通信状态标志位：true=已连接，false=未连接/断线
        /// 通信线程据此决定是读取数据还是尝试重连
        /// </summary>
        public bool IsConnected { get; set; }

        /// <summary>
        /// 重连时间（毫秒）：断线后重连前的等待间隔，避免频繁重连
        /// 默认10毫秒
        /// </summary>
        public int ReConnectTime { get; set; } = 10;

        /// <summary>
        /// 重连标志位：false=尚未首次连接，true=已尝试过首次连接（后续断线均视为重连）
        /// 用于区分首次连接与重连，输出不同的日志提示
        /// </summary>
        public bool ReConnectSign { get; set; }

        /// <summary>
        /// 所有变量的键值对字典：键=变量名称(VarName)，值=变量的实时值(object)
        /// 通信线程解析出变量值后通过UpdateVariable写入此字典，界面层通过索引器this[varName]读取
        /// </summary>
        public Dictionary<string, object> CurrentValue = new Dictionary<string, object>();

        /// <summary>
        /// 定义报警触发与消除事件
        /// 参数1：true=报警触发，false=报警消除
        /// 参数2：发生报警的Variable对象
        /// 在CheckAlarm检测到上升沿/下降沿时由本类内部触发，也可通过RaiseAlarm外部触发
        /// </summary>
        public event Action<bool, Variable> AlarmTrigEvent;

        /// <summary>
        /// 外部触发报警事件（用于本地比较报警等场景）
        /// 提供给外部代码主动触发报警的入口，绕过CheckAlarm的自动检测
        /// </summary>
        /// <param name="ackType">true=触发报警，false=消除报警</param>
        /// <param name="variable">报警变量</param>
        public void RaiseAlarm(bool ackType, Variable variable)
        {
            AlarmTrigEvent?.Invoke(ackType, variable);
        }

        /// <summary>
        /// 更新变量值到CurrentValue字典，并触发报警检测
        /// 已存在则更新，不存在则新增
        /// </summary>
        /// <param name="variable">待更新的变量（携带最新VarValue）</param>
        public void UpdateVariable(Variable variable)
        {
            //变量已存在则更新值，否则新增键值对
            if (CurrentValue.ContainsKey(variable.VarName))
            {
                CurrentValue[variable.VarName] = variable.VarValue;
            }
            else
            {
                CurrentValue.Add(variable.VarName, variable.VarValue);
            }
            //报警检测：根据新值与缓存值的边沿变化判断是否触发或消除报警
            CheckAlarm(variable);
        }

        /// <summary>
        /// 报警检测：基于上升沿(PosAlarm)和下降沿(NegAlarm)配置进行边沿检测
        /// 原理：通过缓存上一次的布尔值(PosCacheValue/NegCacheValue)，与当前值比较，
        ///   - 上升沿报警：false→true 触发报警，true→false 消除报警
        ///   - 下降沿报警：true→false 触发报警，false→true 消除报警
        /// 缓存值的作用：保存上一次状态以识别状态跳变（边沿），避免每次都报警
        /// </summary>
        /// <param name="variable">待检测的变量</param>
        private void CheckAlarm(Variable variable)
        {
            //上升沿报警检测：仅当变量配置了PosAlarm时执行
            if (variable.PosAlarm)
            {
                //将变量值转换为布尔（兼容True/true/1等多种形式）
                string strValue = variable.VarValue.ToString();
                bool currentValue = strValue == "True" || strValue == "true" || strValue == "1";

                //上升沿：上一次为false且本次为true → 报警触发
                if (variable.PosCacheValue == false && currentValue == true)
                {
                    //检测到了报警触发
                    AlarmTrigEvent?.Invoke(true, variable);
                }

                //下降恢复：上一次为true且本次为false → 报警消除
                if (variable.PosCacheValue == true && currentValue == false)
                {
                    //检测到了报警消除
                    AlarmTrigEvent?.Invoke(false, variable);
                }

                //更新缓存值为当前值，供下次比较
                variable.PosCacheValue = currentValue;
            }

            //下降沿报警检测：仅当变量配置了NegAlarm时执行
            if (variable.NegAlarm)
            {
                //将变量值转换为布尔
                string strValue = variable.VarValue.ToString();
                bool currentValue = strValue == "True" || strValue == "true" || strValue == "1";

                //下降沿：上一次为true且本次为false → 报警触发（与上升沿相反）
                if (variable.NegCacheValue == true && currentValue == false)
                {
                    //检测到了报警触发
                    AlarmTrigEvent?.Invoke(true, variable);
                }

                //上升恢复：上一次为false且本次为true → 报警消除
                if (variable.NegCacheValue == false && currentValue == true)
                {
                    //检测到了报警消除
                    AlarmTrigEvent?.Invoke(false, variable);
                }

                //更新缓存值为当前值，供下次比较
                variable.NegCacheValue = currentValue;
            }
        }

        /// <summary>
        /// 索引器，通过变量名获取变量实时值
        /// 用法：Device["模块1温度"] 返回object，需调用方按需转换
        /// 未找到返回null
        /// </summary>
        /// <param name="key">变量名称</param>
        /// <returns>变量值，未找到返回null</returns>
        public object this[string key]
        {
            get
            {
                if (CurrentValue.ContainsKey(key))
                {
                    return CurrentValue[key];
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
