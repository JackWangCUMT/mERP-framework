using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Feng.Windows.Forms
{
    /// <summary>
    /// MyCalendar
    /// </summary>
    public class MyCalendar : Xceed.Editors.WinCalendar, IDataValueControl
    {
        #region "Default Property"

        /// <summary>
        /// Constructor
        /// </summary>
        public MyCalendar()
            : base()
        {
            this.DayMargins = new Xceed.Editors.Margins(10, 10, 0, 0);
            this.EnableMultipleMonths = false;

            this.WeekDaysHeader.DayNames = new string[]
                                           {
                                               "һ",
                                               "��",
                                               "��",
                                               "��",
                                               "��",
                                               "��",
                                               "��"
                                           };

            if (this.TodayButton != null)
            {
                this.TodayButton.Text = "����";
                this.TodayButton.Height = 20;
                this.TodayButton.Location = new System.Drawing.Point(65, 123);
            }
            if (this.NoneButton != null)
            {
                this.NoneButton.Text = "��";
                this.NoneButton.Height = 20;
                this.NoneButton.Location = new System.Drawing.Point(160, 123);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MyCalendar(Xceed.Editors.WinCalendar template)
            : base(template)
        {
            this.DayMargins = new Xceed.Editors.Margins(10, 10, 0, 0);
            this.EnableMultipleMonths = false;

            this.WeekDaysHeader.DayNames = new string[]
                                           {
                                               "һ",
                                               "��",
                                               "��",
                                               "��",
                                               "��",
                                               "��",
                                               "��"
                                           };

            if (this.TodayButton != null)
            {
                this.TodayButton.Text = "����";
                this.TodayButton.Height = 20;
                this.TodayButton.Location = new System.Drawing.Point(65, 123);
            }
            if (this.NoneButton != null)
            {
                this.NoneButton.Text = "��";
                this.NoneButton.Height = 20;
                this.NoneButton.Location = new System.Drawing.Point(160, 123);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="createTodayButton"></param>
        /// <param name="createNoneButton"></param>
        public MyCalendar(bool createTodayButton, bool createNoneButton)
            : base(createTodayButton, createNoneButton)
        {
            this.DayMargins = new Xceed.Editors.Margins(10, 10, 0, 0);
            this.EnableMultipleMonths = false;

            this.WeekDaysHeader.DayNames = new string[]
                                           {
                                               "һ",
                                               "��",
                                               "��",
                                               "��",
                                               "��",
                                               "��",
                                               "��"
                                           };

            if (createTodayButton)
            {
                this.TodayButton.Text = "����";
                this.TodayButton.Height = 20;
                this.TodayButton.Location = new System.Drawing.Point(65, 123);
            }
            if (createNoneButton)
            {
                this.NoneButton.Text = "��";
                this.NoneButton.Height = 20;
                this.NoneButton.Location = new System.Drawing.Point(160, 123);
            }
        }

        /// <summary>
        /// DefaultDayMargins = Xceed.Editors.Margins(10, 10, 0, 0)
        /// </summary>
        protected override Xceed.Editors.Margins DefaultDayMargins
        {
            get { return new Xceed.Editors.Margins(10, 10, 0, 0); }
        }

        /// <summary>
        /// DefaultEnableMultipleMonths = false
        /// </summary>
        protected override bool DefaultEnableMultipleMonths
        {
            get { return false; }
        }


        /// <summary>
        /// EnableMultipleMonths = false
        /// </summary>
        [DefaultValue(false)]
        public new bool EnableMultipleMonths
        {
            get { return base.EnableMultipleMonths; }
            set { base.EnableMultipleMonths = value; }
        }

        #endregion

        #region "IDataValueControl"

        /// <summary>
        /// ����ʱΪDateTimePicker.Value ���� null���ɿ�ʱ��ؼ���
        /// ֵ�е�ʱ��Ϊ00:00:00
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedDataValue
        {
            get
            {
                if (base.SelectedDate == base.NullDate)
                {
                    return null;
                }
                else
                {
                    return base.SelectedDate;
                }
            }
            set
            {
                if (value == null)
                {
                    base.SelectedDate = base.NullDate;
                }
                else
                {
                    try
                    {
                        base.SelectedDate = Feng.Utils.ConvertHelper.ToDateTime(value).Value;
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException("MyCalendar's SelectedDataValue must be DateTime", ex);
                    }
                }
            }
        }

        #endregion

        #region "IStateControl"

        /// <summary>
        /// ReadOnly = !Enable
        /// </summary>
        [Category("Data")]
        [Description("�Ƿ�ɶ�")]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get { return !this.Enabled; }
            set
            {
                if (this.Enabled != !value)
                {
                    this.Enabled = !value;
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