using System;
using System.Collections.Generic;
using System.Text;

namespace Feng
{
    /// <summary>
    /// ��ֻ���ؼ�
    /// </summary>
    public interface IReadOnlyControl
    {
        /// <summary>
        /// �Ƿ�ֻ��
        /// </summary>
        bool ReadOnly { get; set; }

        /// <summary>
        /// ReadOnlyChanged
        /// </summary>
        event EventHandler ReadOnlyChanged;
    }
}