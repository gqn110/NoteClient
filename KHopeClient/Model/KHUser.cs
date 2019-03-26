namespace KHopeClient.Model
{
    /// <summary>
    /// 用户
    /// </summary>
    public class KHUser
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        
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
