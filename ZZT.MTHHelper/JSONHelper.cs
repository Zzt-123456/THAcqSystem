using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZT.MTHHelper
{
    /// <summary>
    /// JSON 帮助类
    /// ==================================================================
    /// 用途：封装 Newtonsoft.Json 的序列化与反序列化能力，对外提供简洁的静态方法。
    /// 核心职责：
    ///   1. 将任意实体对象（如 RecipeInfo 配方对象）序列化为 JSON 字符串，便于持久化存储或网络传输；
    ///   2. 将 JSON 字符串反序列化为指定类型的实体对象，便于从配置文件中恢复对象状态。
    /// 使用场景：
    ///   - 本项目中主要用于配方数据（RecipeInfo）的保存与读取；
    ///   - 任何需要对象 ↔ JSON 字符串互相转换的场景均可直接调用。
    /// 设计说明：
    ///   - 所有方法均为静态方法，无需实例化即可使用；
    ///   - 内部使用 try-catch 包裹，发生异常时返回默认值（空字符串或 default(T)），避免因序列化失败导致程序崩溃。
    /// </summary>
    public class JSONHelper
    {
        /// <summary>
        /// 使用 Newton 方式将实体对象转换成 JSON 字符串
        /// </summary>
        /// <typeparam name="T">对象类型（任意可序列化的类型）</typeparam>
        /// <param name="x">对象</param>
        /// <returns>字符串（成功返回 JSON 文本，失败返回 string.Empty）</returns>
        public static string EntityToJSON<T>(T x)
        {
            // 默认返回空字符串，便于调用方判断是否序列化成功
            string result = string.Empty;

            try
            {
                // 调用 Newtonsoft.Json 进行序列化，自动处理公共属性、嵌套对象、集合等
                result = Newtonsoft.Json.JsonConvert.SerializeObject(x);
            }
            catch (Exception)
            {
                // 出现异常（如循环引用、不支持类型等）时返回空字符串，保证调用方不会因异常中断业务流程
                result = string.Empty;
            }
            return result;

        }

        /// <summary>
        /// 使用 Newton 方式将 JSON 字符串转换成实体类
        /// </summary>
        /// <typeparam name="T">对象类型（目标实体类型）</typeparam>
        /// <param name="json">字符串（JSON 格式文本）</param>
        /// <returns>对象（成功返回反序列化后的实体，失败返回 default(T)）</returns>
        public static T JSONToEntity<T>(string json)
        {
            // 默认值：引用类型为 null，值类型为 0/false，作为反序列化失败时的安全回退
            T t = default(T);
            try
            {
                // 通过 typeof(T) 指定目标类型，JsonConvert 会按类型的公共属性进行字段映射
                t = (T)JsonConvert.DeserializeObject(json, typeof(T));
            }
            catch (Exception)
            {
                // 反序列化失败（如 JSON 格式错误、字段类型不匹配）时返回默认值，避免上层崩溃
                t = default(T);
            }

            return t;
        }
    }
}
