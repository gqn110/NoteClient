using KHopeClient.IService;
using KHopeClient.Model;
using KHopeClient.Service;
using LayeredSkin.Forms;
using MiniBlinkPinvoke;
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
    public partial class frmHome : LayeredForm
    {
        public frmHome()
        {
            InitializeComponent();

            Rectangle workAreaRectangle = Screen.GetWorkingArea(this);
            this.Size = new Size(0, 0);
        }

        #region 窗口加载
        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmHome_Load(object sender, EventArgs e)
        {
            this.LoadNodeList();
        }
        #endregion

        #region 桌面便签
        /// <summary>
        /// 加载便签
        /// </summary>
        public void LoadNodeList()
        {
            try
            {
                List<KHNode> lst = new KHNodeService().GetList();
                foreach (KHNode node in lst)
                {
                    if (string.IsNullOrEmpty(node.Content))
                        continue;

                    frmNode fmNode = new frmNode(node);
                    fmNode.Show();
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WriteLogErr(ex.Message);
            }
        }

        /// <summary>
        /// 新增便签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmNode_Click(object sender, EventArgs e)
        {
            KHNode node = null;
            try
            {
                IKHNodeService service = new KHNodeService();
                var lst = service.GetList();
                if (lst.Count >= 10)
                    MessageBox.Show("最多只能建10个");

                node = new KHNode();
                node.FontName = "微软雅黑";
                node.Bold = 0;
                node.FontSize = 12;
                node.FontColor = "white";
                node.Content = "";
                node.Enabled = 1;
                node.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                node.UpdateDate = node.CreateDate;
                node.ID = service.Add(node);
            }
            catch (Exception ex)
            {
                CommonMethod.WriteLogErr(ex.Message);
            }
            frmNode frmNode = new frmNode(node);
            frmNode.Show();
        }
        #endregion
    }
}
