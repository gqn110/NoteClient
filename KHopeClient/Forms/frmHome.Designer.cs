namespace KHopeClient
{
    partial class frmHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHome));
            this.cmsConfig = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmNode = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsConfig
            // 
            this.cmsConfig.AutoSize = false;
            this.cmsConfig.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsConfig.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmNode});
            this.cmsConfig.Name = "cmsTreeView";
            this.cmsConfig.Size = new System.Drawing.Size(142, 32);
            // 
            // tsmNode
            // 
            this.tsmNode.AutoSize = false;
            this.tsmNode.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.tsmNode.Name = "tsmNode";
            this.tsmNode.Size = new System.Drawing.Size(138, 28);
            this.tsmNode.Text = "新增便签";
            this.tsmNode.Click += new System.EventHandler(this.tsmNode_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.cmsConfig;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "小助教";
            this.notifyIcon1.Visible = true;
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.ContextMenuStrip = this.cmsConfig;
            this.Name = "frmHome";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "康希科技";
            this.Load += new System.EventHandler(this.frmHome_Load);
            this.cmsConfig.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cmsConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmNode;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}