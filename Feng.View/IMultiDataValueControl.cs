using System;
using System.Collections;
using System.Text;

namespace Feng
{
    /// <summary>
    /// ��ֵ���ݿؼ�
    /// </summary>
    public interface IMultiDataValueControl : IStateControl, IReadOnlyControl
    {
        /// <summary>
        /// ѡ��ֵ
        /// </summary>
        IList SelectedDataValues { get; set; }
    }
}