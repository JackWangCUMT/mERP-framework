namespace Feng.Windows.Forms
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// ���Ĭ��MyPanel�������������ԣ�
    /// <list type="bullet">
    /// <item>BorderStyle = BorderStyle.None</item>
    /// <item>AutoScroll = true</item>
    /// </list>
    /// </summary>
    public class MyPanel : System.Windows.Forms.Panel, IStateControl, IReadOnlyControl
    {
        #region "Default Property"

        /// <summary>
        /// ��ʼ��Ĭ������
        /// </summary>
        public MyPanel()
            : base()
        {
            base.BorderStyle = System.Windows.Forms.BorderStyle.None;
            base.AutoScroll = true;
        }

        /// <summary>
        /// Default BorderStyle
        /// </summary>
        [DefaultValue(System.Windows.Forms.BorderStyle.None)]
        public new System.Windows.Forms.BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; }
        }

        /// <summary>
        /// Default AutoScroll 
        /// </summary>
        [DefaultValue(true)]
        public new bool AutoScroll
        {
            get { return base.AutoScroll; }
            set { base.AutoScroll = value; }
        }

        #endregion

        #region "IStateControl"

        /// <summary>
        /// See<see cref="IReadOnlyControl.ReadOnly"/>
        /// </summary>
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

        /// <summary>
        /// See<see cref="IStateControl.SetState"/>
        /// </summary>
        public void SetState(StateType state)
        {
            StateControlHelper.SetState(this, state);
        }

        #endregion
    }
}