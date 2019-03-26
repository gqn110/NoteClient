namespace KHopeClient.Model
{
    /// <summary>
    /// 系统参数
    /// </summary>
    public class KHParam
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否生效
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
