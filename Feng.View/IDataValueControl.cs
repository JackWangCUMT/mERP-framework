namespace Feng
{
    using System.ComponentModel;

    /// <summary>
    /// ��ֵ���ݿؼ�
    /// </summary>
    public interface IDataValueControl : IStateControl, IReadOnlyControl
    {
        /// <summary>
        /// ѡ��ֵ
        /// </summary>
        object SelectedDataValue
        {
            get;
            set;
        }
    }
}
