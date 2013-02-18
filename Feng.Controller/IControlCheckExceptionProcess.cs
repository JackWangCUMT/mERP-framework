using System;
using System.Collections.Generic;
using System.Text;

namespace Feng
{
    /// <summary>
    /// ����<see cref="ControlCheckException"/>�Ĵ������
    /// </summary>
    public interface IControlCheckExceptionProcess : IDisposable
    {
        /// <summary>
        /// �������
        /// </summary>
        void ClearAllError();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invalidControl"></param>
        /// <param name="msg"></param>
        void ShowError(object invalidControl, string msg);
    }
}
