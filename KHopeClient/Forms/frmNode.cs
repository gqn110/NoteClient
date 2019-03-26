using KHopeClient.IService;
using KHopeClient.Model;
using KHopeClient.Service;
using LayeredSkin.DirectUI;
using LayeredSkin.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KHopeClient
{
    public partial class frmNode : LayeredForm
    {
        KHNode _KHNode;
        public frmNode(KHNode node)
        {
            InitializeComponent();
            _KHNode = node;

            Rectangle workAreaRectangle = Screen.GetWorkingArea(this);
            if (_KHNode.Width != 0 && _KHNode.Height != 0)
                this.Size = new Size(_KHNode.Width, _KHNode.Height);

            if (_KHNode.LocationX != 0 && _KHNode.LocationY != 0)
                this.Location = new Point(_KHNode.LocationX, _KHNode.LocationY);

            DuiTextBox txtBox = this.lblContent.DUIControls[0] as DuiTextBox;
            if (txtBox != null)
            {
                if (!string.IsNullOrEmpty(node.Content))
                    txtBox.Text = node.Content;
                else
                    txtBox.Focus();

                FontStyle fontStyle = FontStyle.Italic;
                if (node.Bold == 1)
                {
                    fontStyle = FontStyle.Bold;
                }
                txtBox.Font = new System.Drawing.Font(node.FontName, node.FontSize, fontStyle, GraphicsUnit.Point);
                txtBox.ForeColor = Color.FromName(node.FontColor);
                txtBox.FocusedChanged += TxtBox_FocusedChanged;
            }
        }

        private void TxtBox_FocusedChanged(object sender, EventArgs e)
        {
            try
            {
                string txt = (sender as DuiTextBox).Text;
                if (!this._KHNode.Content.Equals(txt))
                {
                    this._KHNode.Width = this.Size.Width;
                    this._KHNode.Height = this.Size.Height;
                    this._KHNode.LocationX = this.Location.X;
                    this._KHNode.LocationY = this.Location.Y;
                    this._KHNode.Content = txt;
                    this._KHNode.UpdateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    new KHNodeService().Update(this._KHNode);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WriteLogErr(ex.Message);
            }
        }

        #region 加载
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNode_Load(object sender, EventArgs e)
        {
            this.lbtHeader.Cursor = Cursors.SizeAll;

            // this.lpnlAutoUp.Visible = false;
            //设置参数
            this.InitSetting();
        }
        #endregion

        #region 设置
        /// <summary>
        /// 初始化设置
        /// </summary>
        public void InitSetting()
        {
            InitFontName(this._KHNode.FontName);
            InitBold(this._KHNode.Bold);
            InitFontSize(this._KHNode.FontSize);
            InitFontColor(this._KHNode.FontColor);
        }

        /// <summary>
        /// 加载字体
        /// </summary>
        /// <param name="selName"></param>
        private void InitFontName(string selName)
        {
            if (string.IsNullOrEmpty(selName))
                selName = "微软雅黑";
            List<string> lst = GetFontNameList();

            ToolStripMenuItem menuItem = null;
            foreach (var item in lst)
            {
                menuItem = new System.Windows.Forms.ToolStripMenuItem();
                menuItem.Name = "FontNameMenuItem" + item;
                menuItem.Size = new System.Drawing.Size(124, 22);
                menuItem.Text = item;
                menuItem.Tag = item;
                if (selName == item)
                {
                    menuItem.Checked = true;
                    menuItem.CheckState = System.Windows.Forms.CheckState.Checked;
                }
                menuItem.Click += MenuItem0_Click;
                this.tsmName.DropDownItems.Add(menuItem);
            }
        }
        /// <summary>
        /// 字体选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem0_Click(object sender, EventArgs e)
        {
            try
            {
                string sel = (sender as ToolStripMenuItem).Tag.ToString();
                if (this._KHNode.FontName != sel)
                {
                    this._KHNode.FontName = sel;
                    this._KHNode.UpdateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    new KHNodeService().Update(this._KHNode);
                    //改变选中对象
                    ToolStripMenuItem item = null;
                    for (int i = 0; i < this.tsmName.DropDownItems.Count; i++)
                    {
                        item = this.tsmName.DropDownItems[i] as ToolStripMenuItem;
                        if (item.Name == "FontNameMenuItem" + sel)
                        {
                            item.Checked = true;
                            item.CheckState = System.Windows.Forms.CheckState.Checked;
                        }
                        else
                        {
                            item.Checked = false;
                            item.CheckState = System.Windows.Forms.CheckState.Unchecked;
                        }
                    }
                    //设置字体
                    DuiTextBox txtBox = this.lblContent.DUIControls[0] as DuiTextBox;
                    if (txtBox != null)
                    {
                        FontStyle fontStyle = FontStyle.Italic;
                        if (this._KHNode.Bold == 1)
                            fontStyle = FontStyle.Bold;
                        txtBox.Font = new System.Drawing.Font(this._KHNode.FontName, 18F, fontStyle, GraphicsUnit.Point);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WriteLogErr(ex.Message);
            }
        }

        /// <summary>
        /// 加载字体
        /// </summary>
        /// <param name="selName"></param>
        private void InitBold(int bold)
        {
            List<string> lst = GetBoldList();
            string selName = bold == 1 ? "加粗" : "不加粗";
            ToolStripMenuItem menuItem = null;
            foreach (var item in lst)
            {
                menuItem = new System.Windows.Forms.ToolStripMenuItem();
                menuItem.Name = "BoldMenuItem" + item;
                menuItem.Size = new System.Drawing.Size(124, 22);
                menuItem.Text = item;
                menuItem.Tag = item;
                if (selName == item)
                {
                    menuItem.Checked = true;
                    menuItem.CheckState = System.Windows.Forms.CheckState.Checked;
                }
                menuItem.Click += MenuItem_Click;
                this.tsmBold.DropDownItems.Add(menuItem);
            }
        }

        /// <summary>
        /// 加粗选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string selText = (sender as ToolStripMenuItem).Tag.ToString();
                int sel = selText == "加粗" ? 1 : 0;
                if (sel != this._KHNode.Bold)
                {
                    this._KHNode.Bold = sel;
                    this._KHNode.UpdateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    new KHNodeService().Update(this._KHNode);
                    //改变选中对象
                    ToolStripMenuItem item = null;
                    for (int i = 0; i < this.tsmBold.DropDownItems.Count; i++)
                    {
                        item = this.tsmBold.DropDownItems[i] as ToolStripMenuItem;
                        if (item.Name == "BoldMenuItem" + selText)
                        {
                            item.Checked = true;
                            item.CheckState = System.Windows.Forms.CheckState.Checked;
                        }
                        else
                        {
                            item.Checked = false;
                            item.CheckState = System.Windows.Forms.CheckState.Unchecked;
                        }
                    }
                    //设置
                    DuiTextBox txtBox = this.lblContent.DUIControls[0] as DuiTextBox;
                    if (txtBox != null)
                    {
                        FontStyle fontStyle = this._KHNode.Bold == 1 ? FontStyle.Bold : FontStyle.Italic;
                        txtBox.Font = new System.Drawing.Font(this._KHNode.FontName, 18F, fontStyle, GraphicsUnit.Point);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WriteLogErr(ex.Message);
            }
        }

        /// <summary>
        /// 加载字体
        /// </summary>
        /// <param name="selName"></param>
        private void InitFontSize(int selSize)
        {
            List<int> lst = GetSizeList();

            ToolStripMenuItem menuItem = null;
            foreach (var item in lst)
            {
                menuItem = new System.Windows.Forms.ToolStripMenuItem();
                menuItem.Name = "FontSizeMenuItem" + item;
                menuItem.Size = new System.Drawing.Size(124, 22);
                menuItem.Text = item.ToString();
                menuItem.Tag = item;
                if (selSize == item)
                {
                    menuItem.Checked = true;
                    menuItem.CheckState = System.Windows.Forms.CheckState.Checked;
                }
                menuItem.Click += MenuItem2_Click;
                this.tsmSize.DropDownItems.Add(menuItem);
            }
        }

        /// <summary>
        /// 字体选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                int sel = Convert.ToInt32((sender as ToolStripMenuItem).Tag.ToString());
                if (this._KHNode.FontSize != sel)
                {
                    this._KHNode.FontSize = sel;
                    this._KHNode.UpdateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    new KHNodeService().Update(this._KHNode);

                    //改变选中对象
                    ToolStripMenuItem item = null;
                    for (int i = 0; i < this.tsmSize.DropDownItems.Count; i++)
                    {
                        item = this.tsmSize.DropDownItems[i] as ToolStripMenuItem;
                        if (item.Name == "FontSizeMenuItem" + sel)
                        {
                            item.Checked = true;
                            item.CheckState = System.Windows.Forms.CheckState.Checked;
                        }
                        else
                        {
                            item.Checked = false;
                            item.CheckState = System.Windows.Forms.CheckState.Unchecked;
                        }
                    }

                    //设置
                    DuiTextBox txtBox = this.lblContent.DUIControls[0] as DuiTextBox;
                    if (txtBox != null)
                    {
                        FontStyle fontStyle = this._KHNode.Bold == 1 ? FontStyle.Bold : FontStyle.Italic;
                        txtBox.Font = new System.Drawing.Font(this._KHNode.FontName, sel, fontStyle, GraphicsUnit.Point);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WriteLogErr(ex.Message);
            }
        }

        /// <summary>
        /// 加载颜色
        /// </summary>
        /// <param name="selName"></param>
        private void InitFontColor(string selColor)
        {
            if (string.IsNullOrEmpty(selColor))
                selColor = "white";
            List<CommonItem> lst = GetFontColor();

            ToolStripMenuItem menuItem = null;
            foreach (var item in lst)
            {
                menuItem = new System.Windows.Forms.ToolStripMenuItem();
                menuItem.Name = "FontColorMenuItem" + item.key;
                menuItem.Size = new System.Drawing.Size(124, 22);
                menuItem.Text = item.value.ToString();
                menuItem.Tag = item.key;
                if (selColor == item.key)
                {
                    menuItem.Checked = true;
                    menuItem.CheckState = System.Windows.Forms.CheckState.Checked;
                }
                menuItem.Click += MenuItem3_Click;
                this.tmsColor.DropDownItems.Add(menuItem);
            }
        }

        /// <summary>
        /// 颜色选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                string sel = (sender as ToolStripMenuItem).Tag.ToString();
                if (sel != this._KHNode.FontColor)
                {
                    this._KHNode.FontColor = sel;
                    this._KHNode.UpdateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    new KHNodeService().Update(this._KHNode);
                    //改变选中对象
                    ToolStripMenuItem item = null;
                    for (int i = 0; i < this.tmsColor.DropDownItems.Count; i++)
                    {
                        item = this.tmsColor.DropDownItems[i] as ToolStripMenuItem;
                        if (item.Name == "FontColorMenuItem" + sel)
                        {
                            item.Checked = true;
                            item.CheckState = System.Windows.Forms.CheckState.Checked;
                        }
                        else
                        {
                            item.Checked = false;
                            item.CheckState = System.Windows.Forms.CheckState.Unchecked;
                        }
                    }

                    //设置
                    DuiTextBox txtBox = this.lblContent.DUIControls[0] as DuiTextBox;
                    if (txtBox != null)
                        txtBox.ForeColor = Color.FromName(sel);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WriteLogErr(ex.Message);
            }
        }

        #endregion

        #region 设置静态数据

        /// <summary>
        /// 字体列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetFontNameList()
        {
            List<string> lst = new List<string>();
            lst.Add("宋体");
            lst.Add("微软雅黑");
            lst.Add("楷体");
            lst.Add("黑体");
            lst.Add("幼圆体");
            lst.Add("隶书");
            return lst;
        }

        /// <summary>
        /// 字体加粗列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetBoldList()
        {
            List<string> lst = new List<string>();
            lst.Add("加粗");
            lst.Add("不加粗");
            return lst;
        }

        /// <summary>
        /// 字体大小列表
        /// </summary>
        /// <returns></returns>
        public List<int> GetSizeList()
        {
            List<int> lst = new List<int>();
            lst.Add(10);
            lst.Add(11);
            lst.Add(12);
            lst.Add(13);
            lst.Add(14);
            lst.Add(15);
            return lst;
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        /// <returns></returns>
        private List<CommonItem> GetFontColor()
        {
            List<CommonItem> lst = new List<CommonItem>();
            lst.Add(new CommonItem() { value = "红色", key = "red" });
            lst.Add(new CommonItem() { value = "橙色", key = "orange" });
            lst.Add(new CommonItem() { value = "黄色", key = "yellow" });
            lst.Add(new CommonItem() { value = "绿色", key = "green" });
            lst.Add(new CommonItem() { value = "蓝色", key = "blue" });
            lst.Add(new CommonItem() { value = "白色", key = "white" });
            lst.Add(new CommonItem() { value = "紫色", key = "purple" });
            lst.Add(new CommonItem() { value = "黑色", key = "black" });
            return lst;
        }
        #endregion

        #region 拖动
        private bool isDown = false;
        private Point oldPoint, oldForm;
        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmHome_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDown = true;
                oldPoint = e.Location;
                oldForm = this.Location;
            }
        }

        /// <summary>
        /// 鼠标松开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmHome_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown)
            {
                oldForm.Offset(e.X - oldPoint.X, e.Y - oldPoint.Y);
                if (this.Location.X != oldForm.X || this.Location.Y != oldForm.Y)
                {
                    this._KHNode.LocationX = this.Location.X;
                    this._KHNode.LocationY = this.Location.Y;
                    this._KHNode.UpdateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    new KHNodeService().Update(this._KHNode);

                    this.Location = oldForm;
                }
            }
        }
        /// <summary>
        /// 鼠标松开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmHome_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDown)
            {
                isDown = false;//释放鼠标后标注为false;
            }
        }
        #endregion

        #region 显示和隐藏关闭功能

        private void lbtHeader_MouseEnter(object sender, EventArgs e)
        {
            // this.lpnlAutoUp.Visible = true;
        }

        private void lbtHeader_MouseLeave(object sender, EventArgs e)
        {
            //   this.lpnlAutoUp.Visible = false;
        }

        private void layeredButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                cmsConfig.Show(new Point(this.Location.X + lpnlAutoUp.Location.X + 20
                    , this.Location.Y + lpnlAutoUp.Location.Y + 5));
            }
        }


        #endregion

        #region 删除便签
        /// <summary>
        /// 删除便签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmsDel_Click(object sender, EventArgs e)
        {
            try
            {
                new KHNodeService().Delete(this._KHNode.ID);
            }
            catch (Exception ex)
            {
                CommonMethod.WriteLogErr(ex.Message);
            }
            this.Close();
        }
        #endregion
    }
}
