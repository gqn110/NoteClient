using KHopeClient.Model;
using System.Collections.Generic;

namespace KHopeClient.IService
{
    public interface IKHNodeService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        int Add(KHNode param);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool Update(KHNode param);

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        List<KHNode> GetList();

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        KHNode GetByID(int id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
    }
}
