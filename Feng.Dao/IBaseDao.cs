using System;
using System.Collections.Generic;
using System.Text;

namespace Feng
{
	/// <summary>
    /// 弱类型基本业务逻辑层结构。
    /// 只提供Save<see cref="Save"/>、Update<see cref="Update"/>、Delete<see cref="Delete"/>、SaveOrUpdate<see cref="SaveOrUpdate"/>功能
	/// </summary>
	public interface IBaseDao
	{
		/// <summary>
        /// 增加
		/// </summary>
		/// <param name="entity"></param>
		void Save(object entity);

		/// <summary>
        /// 修改
		/// </summary>
		/// <param name="entity"></param>
		void Update(object entity);

        /// <summary>
        /// SaveOrUpdate
        /// </summary>
        /// <param name="entity"></param>
        void SaveOrUpdate(object entity);

		/// <summary>
        /// 删除
		/// </summary>
		/// <param name="entity"></param>
		void Delete(object entity);

        /// <summary>
        /// 当出错时，清理
        /// </summary>
        void Clear();
	}
}
