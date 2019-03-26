using KHopeClient.Model;

namespace KHopeClient.IService
{
    public interface IKHParamService
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool Save(string name, string value);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        KHParam GetByName(string name);
    }
}
