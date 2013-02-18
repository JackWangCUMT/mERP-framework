using System;
using System.Collections.Generic;
using System.Text;

namespace Feng
{
	/// <summary>
    /// �����ͻ���ҵ���߼���ṹ��
    /// ֻ�ṩSave<see cref="Save"/>��Update<see cref="Update"/>��Delete<see cref="Delete"/>��SaveOrUpdate<see cref="SaveOrUpdate"/>����
	/// </summary>
	public interface IBaseDao
	{
		/// <summary>
        /// ����
		/// </summary>
		/// <param name="entity"></param>
		void Save(object entity);

		/// <summary>
        /// �޸�
		/// </summary>
		/// <param name="entity"></param>
		void Update(object entity);

        /// <summary>
        /// SaveOrUpdate
        /// </summary>
        /// <param name="entity"></param>
        void SaveOrUpdate(object entity);

		/// <summary>
        /// ɾ��
		/// </summary>
		/// <param name="entity"></param>
		void Delete(object entity);

        /// <summary>
        /// ������ʱ������
        /// </summary>
        void Clear();
	}
}
