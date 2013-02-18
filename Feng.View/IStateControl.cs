namespace Feng
{
    /// <summary>
    /// ״̬
    /// </summary>
    [System.Flags]
    public enum StateType
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //Invalid = -1,
        /// <summary>
        /// ���ݿ�
        /// </summary>
        None = 0x1,
        /// <summary>
        /// ��ʾ
        /// </summary>
        View = 0x2,
        /// <summary>
        /// ���
        /// </summary>
        Add = 0x4,
        /// <summary>
        /// �༭
        /// </summary>
        Edit = 0x8,
        /// <summary>
        /// ɾ��
        /// </summary>
        Delete = 0x10,
    };

    /// <summary>
    /// ״̬�ؼ�
    /// </summary>
    public interface IStateControl
    {
        /// <summary>
        /// ����״̬
        /// </summary>
        void SetState(StateType state);
    }
}