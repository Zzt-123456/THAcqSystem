using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZZT.MTHProject
{
    /// <summary>
    /// 程序入口类：Modbus TCP 多通道温湿度监控系统的启动点。
    /// 负责初始化应用程序、弹出登录窗口、并根据登录结果决定是否进入主界面。
    /// 使用场景：整个 WinForms 程序由 .NET 框架在启动时调用 Main 方法运行。
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// 启动流程：1.启用可视化样式 -> 2.显示登录窗体 -> 3.登录成功则运行主窗体，否则退出应用。
        /// [STAThread] 特性保证 COM 组件（含部分 WinForms 剪贴板/拖拽功能）在单线程单元中运行。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //启用 Windows 系统的视觉样式（控件外观跟随系统主题）
            Application.EnableVisualStyles();
            //设置控件文本默认使用 GDI+ 渲染（兼容旧版 .NET Framework 行为）
            Application.SetCompatibleTextRenderingDefault(false);

            //创建登录窗体实例
            FrmLogin frmLogin = new FrmLogin();
            //将登录窗体置顶显示，避免用户在登录前误操作其它窗口
            frmLogin.TopMost = true;

            //以模态方式（ShowDialog）显示登录窗体，等待用户完成登录
            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                //登录成功，启动主窗体并进入消息循环（此时主线程阻塞，直到 FrmMain 关闭）
                Application.Run(new FrmMain());
            }
            else
            {
                //登录失败或用户取消，直接退出整个应用程序
                Application.Exit();
            }
        }
    }
}
