using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZZT.MTHBLL;
using ZZT.MTHModels;

namespace ZZT.MTHProject
{
    public partial class FrmLogin : System.Windows.Forms.Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        //BLL业务逻辑层对象，用于调用登录验证等业务方法
        private SysAdminManage sysAdminManage = new SysAdminManage();

        /// <summary>
        /// 登录按钮：执行登录流程
        /// 流程：验证输入非空 → 封装SysAdmin对象 → 调用BLL校验 → 校验成功存入全局变量
        /// </summary>
        private void btn_Login_Click(object sender, EventArgs e)
        {
            //验证数据：用户名不能为空
            if (this.txt_LoginName.Text.Trim().Length == 0)
            {
                new FrmMsgBoxWithoutAck("请填写登录用户名！", "登录提示").ShowDialog();
                this.txt_LoginName.Focus();
                return;
            }

            //验证数据：密码不能为空
            if (this.txt_Pwd.Text.Trim().Length == 0)
            {
                new FrmMsgBoxWithoutAck("请填写登录用户密码！", "登录提示").ShowDialog();
                this.txt_Pwd.Focus();
                return;
            }

            //封装对象：将输入的用户名密码封装为SysAdmin实体供BLL使用
            SysAdmin sysAdmin = new SysAdmin()
            {
                LoginName = this.txt_LoginName.Text.Trim(),
                LoginPwd = this.txt_Pwd.Text.Trim()
            };

            //调用BLL进行登录校验，返回null表示登录失败
            sysAdmin = sysAdminManage.AdminLogin(sysAdmin);
            if (sysAdmin == null)
            {
                new FrmMsgBoxWithoutAck("用户名或密码不正确！", "登录提示").ShowDialog();
                this.txt_LoginName.Focus();
                return;
            }
            
            
            //登录成功：设置对话框返回值为OK，主程序据此进入主界面
            this.DialogResult = DialogResult.OK;

            //存储登录用户信息到全局变量，供后续界面鉴权使用
            CommonMethods.CurrentAdmin = sysAdmin;

            return;
            
        }

        #region 无边框拖动
        //由于本窗体为无边框样式，通过鼠标在面板上的按下/移动事件实现窗体拖动

        //记录鼠标按下时的相对坐标
        private Point mPoint;

        //鼠标按下：记录按下时的相对坐标作为拖动基准点
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }

        //鼠标移动：按住左键时根据偏移量移动窗体位置
        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }
        #endregion

        /// <summary>
        /// 用户名输入框按键事件：按下回车键时触发登录（等同于点击登录按钮）
        /// </summary>
        private void txt_LoginName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.btn_Login_Click(null, null);
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 启用软键盘

        /// <summary>
        /// 启动Windows系统软键盘（osk.exe）
        /// 处理流程：
        /// 1. 检查osk.exe是否存在
        /// 2. 启动进程后循环等待窗口创建（FindWindow查找"屏幕键盘"窗口句柄）
        /// 3. 获取屏幕尺寸，计算底部居中位置
        /// 4. 调用MoveWindow定位软键盘，并SetForegroundWindow置前
        /// </summary>
        private void StartKeyBoard()
        {
            //打开软键盘
            try
            {
                //检查系统目录下是否存在osk.exe可执行文件
                if (!System.IO.File.Exists(Environment.SystemDirectory + "\\osk.exe"))
                {
                    MessageBox.Show("软件盘可执行文件不存在！");
                    return;
                }

                //启动osk.exe软键盘进程
                softKey = System.Diagnostics.Process.Start(Environment.SystemDirectory + "\\osk.exe");
                // 上面的语句在打开软键盘后，系统还没用立刻把软键盘的窗口创建出来了。所以下面的代码用循环来查询窗口是否创建，只有创建了窗口
                // FindWindow才能找到窗口句柄，才可以移动窗口的位置和设置窗口的大小。这里是关键。
                //循环等待软键盘窗口创建完成，拿到窗口句柄后才能移动/设置窗口
                IntPtr intptr = IntPtr.Zero;
                while (IntPtr.Zero == intptr)
                {
                    System.Threading.Thread.Sleep(100);
                    intptr = FindWindow(null, "屏幕键盘");
                }


                // 获取屏幕尺寸
                int iActulaWidth = Screen.PrimaryScreen.Bounds.Width;
                int iActulaHeight = Screen.PrimaryScreen.Bounds.Height;


                // 设置软键盘的显示位置，底部居中（宽度1000，高度300）
                int posX = (iActulaWidth - 1000) / 2;
                int posY = (iActulaHeight - 300);


                //设定键盘显示位置：移动到屏幕底部居中
                MoveWindow(intptr, posX, posY, 1000, 300, true);


                //设置软键盘到前端显示
                SetForegroundWindow(intptr);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // 申明要使用的dll和api
        //根据窗口类名/标题查找窗口句柄
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        //移动并设置窗口位置和大小
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(System.IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);


        //将指定窗口置为前台
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);


        //软键盘进程对象，用于后续控制
        private System.Diagnostics.Process softKey;

        #endregion

        private void txt_LoginName_DoubleClick(object sender, EventArgs e)
        {
            StartKeyBoard();
        }
    }
}
