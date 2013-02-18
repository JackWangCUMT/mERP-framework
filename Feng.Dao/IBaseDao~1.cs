using System;
using System.Collections.Generic;
using System.Text;

namespace Feng
{
    /// <summary>
    /// ǿ���ͻ���ҵ���߼���ṹ��
    /// ֻ�ṩSave<see cref="Save"/>��Update<see cref="Update"/>��Delete<see cref="Delete"/>��SaveOrUpdate<see cref="SaveOrUpdate"/>����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseDao<T>
        where T : class, IEntity
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="entity"></param>
        void Save(T entity);

        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// SaveOrUpdate
        /// </summary>
        /// <param name="entity"></param>
        void SaveOrUpdate(T entity);

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// ������ʱ������
        /// </summary>
        void Clear();
    }
}
