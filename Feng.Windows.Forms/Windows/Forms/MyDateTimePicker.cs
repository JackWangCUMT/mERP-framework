namespace Feng.Windows.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;
    using System.ComponentModel;
    using System.Windows.Forms.VisualStyles;


    /// <summary>
    /// ���Ĭ��MyDateTimePicker�������������ԣ�
    /// <list type="bullet">
    /// <item>Size = (120,21)</item>
    /// <item>�����ó�Null���ڿؼ��ϰ�Delete��</item>
    /// <item>��ʽΪ"yy/MM/dd"</item>
    /// <item>��ֵΪNullʱ����ʾ��</item>
    /// </list>
    /// </summary>
    public class MyDateTimePicker : NullableDateTimePicker, IDataValueControl
    {
        #region "Default Proeprty"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format">"yyyy-MM-dd"; "HH:mm"; "yy-MM-dd HH:mm"; "MM-dd HH:mm"</param>
        public MyDateTimePicker(string format)
            : base()
        {
            base.CustomFormat = format;
            base.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            base.NullValue = "";
            base.Size = new System.Drawing.Size(120, 21);

            base.ShowUpDown = true;
            base.Value = null;
            this.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;

            base.ShowUpDown = true;
        }
        


        /// <summary>
        /// ��ʼ��Ĭ������
        /// </summary>
        public MyDateTimePicker()
            : this("yy-MM-dd HH:mm")
        {
        }

        /// <summary>
        /// Default Format
        /// </summary>
        [DefaultValue(System.Windows.Forms.DateTimePickerFormat.Custom)]
        public new System.Windows.Forms.DateTimePickerFormat Format
        {
            get { return base.Format; }
            set { base.Format = value; }
        }

        /// <summary>
        /// Default CustomFormat
        /// </summary>
        [DefaultValue("yyyy-MM-dd HH:mm")]
        public new string CustomFormat
        {
            get { return base.CustomFormat; }
            set { base.CustomFormat = value; }
        }

        /// <summary>
        /// Default NullValue
        /// </summary>
        [DefaultValue("")]
        public new string NullValue
        {
            get { return base.NullValue; }
            set { base.NullValue = value; }
        }

        /// <summary>
        /// Default DropDownAlign
        /// </summary>
        [DefaultValue(System.Windows.Forms.LeftRightAlignment.Right)]
        public new System.Windows.Forms.LeftRightAlignment DropDownAlign
        {
            get { return base.DropDownAlign; }
            set { base.DropDownAlign = value; }
        }
        //protected bool m_bRoundTime;
        ///// <summary>
        ///// �Ƿ�ȡ������(��������23:59:59)
        ///// </summary>
        //public bool RoundTime
        //{
        //    get { return m_bRoundTime; }
        //    set { m_bRoundTime = value; }
        //}

        #endregion

        #region "IDataValueControl"

        /// <summary>
        /// ����ʱΪDateTimePicker.Value ���� null���ɿ�ʱ��ؼ���
        /// �����9998-12-31 or С��1753-1-1������Ϊnull
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual object SelectedDataValue
        {
            get { return base.Value; }
            set
            {
                if (value == null)
                {
                    base.Value = null;
                }
                else
                {
                    try
                    {
                        DateTime d = Feng.Utils.ConvertHelper.ToDateTime(value).Value;
                        if (d > this.MaxDate || d < this.MinDate)
                        {
                            base.Value = null;
                        }
                        else
                        {
                            base.Value = d;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException("MyDateTimePicker's SelectedDataValue must be DateTime", ex);
                    }
                }
            }
        }

        #endregion

        #region "IStateControl"

        /// <summary>
        /// ReadOnly = !Enable
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
        /// ����ʾ�ؼ�����ReadOnly
        /// </summary>
        public void SetState(StateType state)
        {
            StateControlHelper.SetState(this, state);
        }

        #endregion
    }
}