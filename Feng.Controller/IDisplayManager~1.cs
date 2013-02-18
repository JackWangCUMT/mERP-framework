using System;
using System.Collections.Generic;
using System.Text;

namespace Feng
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDisplayManager<T> : IDisplayManager
        where T : class, IEntity
    {
        /// <summary>
        /// ǿ����ʵ�����б�
        /// </summary>
        IList<T> Entities
        {
            get;
        }

        /// <summary>
        /// ǿ���͵�ǰʵ����
        /// </summary>
        T CurrentEntity
        {
            get;
        }

        
    }
}
