using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZZT.MTHHelper
{
    /// <summary>
    /// DataGridView 辅助类
    /// ==================================================================
    /// 用途：为 WinForms 中的 DataGridView 控件提供常用的 UI 美化与绘制辅助方法。
    /// 核心职责：
    ///   1. 在行头自动绘制连续行号（DataGridView 默认不显示行号）；
    ///   2. 绘制控件外边框（默认 DataGridView 边框样式有限，可自定义颜色）；
    ///   3. 设置奇偶行交替颜色、默认背景色、网格线颜色，提升数据可读性。
    /// 使用场景：
    ///   - 在温湿度监控主界面的多通道数据表格中，通过订阅 RowPostPaint / Paint 事件回调本类方法；
    ///   - 通常在窗体加载时调用 DgvStyle 统一所有表格的视觉风格。
    /// 设计说明：
    ///   - 所有方法均为静态方法，调用方式简单，无需维护实例状态；
    ///   - DgvRowPostPaint 内部对异常做了 MessageBox 提示，避免绘制失败导致界面空白而无反馈。
    /// </summary>
    public class DataGridViewHelper
    {
        /// <summary>
        /// 为 DataGridView 添加行号
        /// 使用方式：在 DataGridView 的 RowPostPaint 事件中调用本方法。
        /// </summary>
        /// <param name="dgv">dgv 控件</param>
        /// <param name="e">dgv 参数（RowPostPaint 事件参数，包含行索引、行边界等绘制信息）</param>
        public static void DgvRowPostPaint(DataGridView dgv, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                // 使用行头默认前景色作为行号画刷颜色，保持与系统主题一致
                SolidBrush solidBrush = new SolidBrush(dgv.RowHeadersDefaultCellStyle.ForeColor);

                // 行号从 1 开始显示（用户视角），而 e.RowIndex 是 0 基索引
                string lineNo = (e.RowIndex + 1).ToString();

                // 启用 ClearType 文本渲染，使行号字体边缘更平滑清晰
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                // 设置行号在行头单元格内水平、垂直双向居中显示
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                // 在行头矩形区域内绘制行号文本：
                //   X、Y 来自行边界（RowBounds.Location）
                //   宽度取行头宽度（RowHeadersWidth），高度取行模板高度（RowTemplate.Height）
                e.Graphics.DrawString(lineNo, e.InheritedRowStyle.Font, solidBrush, new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgv.RowHeadersWidth, dgv.RowTemplate.Height), sf);
            }
            catch (Exception ex)
            {
                // 绘制失败时弹窗提示，便于开发者定位问题（例如行头宽度被设为 0、字体为空等）
                MessageBox.Show("添加行号时发生错误，错误信息：" + ex.Message, "操作失败");
            }
        }

        /// <summary>
        /// 为 DataGridView 绘制边框
        /// 使用方式：在 DataGridView 的 Paint 事件中调用本方法，可自定义边框颜色。
        /// </summary>
        /// <param name="dgv">dgv 控件</param>
        /// <param name="e">dgv 参数（Paint 事件参数，提供 Graphics 对象）</param>
        /// <param name="borderColor">边框颜色</param>
        public static void DgvRowPaint(DataGridView dgv, PaintEventArgs e, Color borderColor)
        {
            // 注意：宽高各减 1，避免绘制超出控件边界导致边框被裁剪而显示不全
            e.Graphics.DrawRectangle(new Pen(borderColor), new Rectangle(0, 0, dgv.Width - 1, dgv.Height - 1));
        }

        /// <summary>
        /// 奇偶换色
        /// ==================================================================
        /// 通过设置奇数行与默认行不同的背景色，实现"斑马线"效果，提升长表格的可读性。
        /// 同时禁用了选中行的背景色变化（保持视觉稳定，避免监控数据时频繁闪烁）。
        /// </summary>
        /// <param name="dgv">dgv 控件</param>
        /// <param name="defaultBackColor">默认背景色（偶数行）</param>
        /// <param name="alternatingBackColor">奇数行背景色</param>
        /// <param name="gridColor">数据网格线颜色</param>
        public static void DgvStyle(DataGridView dgv, Color defaultBackColor, Color alternatingBackColor, Color gridColor)
        {
            // 奇数行的背景色（含选中背景色同步设置，避免选中时颜色跳变）
            dgv.AlternatingRowsDefaultCellStyle.BackColor = alternatingBackColor;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = alternatingBackColor;

            // 默认的行样式（偶数行）
            dgv.RowsDefaultCellStyle.BackColor = defaultBackColor;
            dgv.RowsDefaultCellStyle.SelectionBackColor = defaultBackColor;

            // 行头单元格也采用相同背景色，保证整行视觉统一
            dgv.RowHeadersDefaultCellStyle.BackColor = defaultBackColor;
            dgv.RowHeadersDefaultCellStyle.SelectionBackColor = defaultBackColor;

            // 数据网格线颜色（单元格之间的分隔线）
            dgv.GridColor = gridColor;
        }
    }
}
