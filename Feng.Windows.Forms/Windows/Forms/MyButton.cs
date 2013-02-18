namespace Feng.Windows.Forms
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// MyButton
    /// </summary>
    public class MyButton : System.Windows.Forms.Button, IStateControl, IReadOnlyControl, IButton
    {
        #region "Default Property"

        /// <summary>
        /// ��ʼ��Ĭ������
        /// </summary>
        public MyButton() : base()
        {
            base.FlatStyle = System.Windows.Forms.FlatStyle.System;
            base.Size = new System.Drawing.Size(72, 21);
        }

        /// <summary>
        /// Default FlatStyle = FlatStyle.System
        /// </summary>
        [DefaultValue(System.Windows.Forms.FlatStyle.System)]
        public new System.Windows.Forms.FlatStyle FlatStyle
        {
            get { return base.FlatStyle; }
            set { base.FlatStyle = value; }
        }

        #endregion

        #region "IStateControl"

        /// <summary>
        /// See<see cref="IReadOnlyControl.ReadOnly"/>
        /// </summary>
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get { return !base.Enabled; }
            set
            {
                if (base.Enabled != !value)
                {
                    base.Enabled = !value;
                    if (ReadOnlyChanged != null)
                    {
                        ReadOnlyChanged(this, System.EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler ReadOnlyChanged;

        private bool m_readOnlyWhenView = true;

        /// <summary>
        /// State==StatusType.Viewʱ��Button Enable or Unable
        /// Ĭ��ΪViewʱUnable(ActionButtons)
        /// ����Ҫ��Edit(Add)ʱ��Enable��������Ϊ��Ҫѡ��ĳЩ������
        /// </summary>
        [Description("�����Ƿ���View״̬ReadOnly")]
        [DefaultValue(true)]
        public bool ReadOnlyWhenView
        {
            get { return m_readOnlyWhenView; }
            set { m_readOnlyWhenView = value; }
        }

        /// <summary>
        /// See<see cref="IStateControl.SetState"/>
        /// </summary>
        public void SetState(StateType state)
        {
            StateControlHelper.SetState(this, state, m_readOnlyWhenView);
        }

        #endregion
    }
}