using System;
using System.Collections.Generic;
using System.Text;

namespace Feng
{
    /// <summary>
    /// ���ؼ�
    /// </summary>
    public interface ICheckControl
    {
        /// <summary>
        /// ���ؼ�ֵ
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ControlCheckException">�ؼ�ֵ�����ϱ�׼</exception>
        bool CheckControlValue();
    }
}