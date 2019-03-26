using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KHopeClient.Model
{
    /// <summary>
    /// 便签
    /// </summary>
    public class KHNode
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 窗口宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 窗口高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 窗口位置X
        /// </summary>
        public int LocationX { get; set; }

        /// <summary>
        /// 窗口位置Y
        /// </summary>
        public int LocationY { get; set; }

        /// <summary>
        /// 字体
        /// </summary>
        public string FontName { get; set; }

        /// <summary>
        /// 是否加粗
        /// </summary>
        public int Bold { get; set; }

        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        public string FontColor { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否生效0未生效1生效
        /// </summary>
        public int Enabled { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string UpdateDate { get; set; }
    }
}
