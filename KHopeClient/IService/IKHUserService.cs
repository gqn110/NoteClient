using KHopeClient.Model;

namespace KHopeClient.IService
{
    public interface IKHUserService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        int Add(KHUser param);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool Update(KHUser param);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        KHUser GetByUserID(string userID);

        /// <summary>
        /// 归档所有用户
        /// </summary>
        void ArchiveAllUser();

        /// <summary>
        /// 获取有效的用户
        /// </summary>
        /// <returns></returns>
        KHUser GetByEnabled();
    }
}
