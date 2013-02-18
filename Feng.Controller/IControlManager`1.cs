using System;
using System.Collections.Generic;
using System.Text;

namespace Feng
{
    /// <summary>
    /// ǿ�������ݲ�����������
    /// ���ڲ���IList(T)���͵�����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IControlManager<T> : IControlManager
        where T : class, IEntity
    {
        /// <summary>
        /// DisplayManager
        /// </summary>
        IDisplayManager<T> DisplayManagerT
        {
            get;
        }
    }
}
