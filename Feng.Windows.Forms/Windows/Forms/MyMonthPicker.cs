using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Windows.Forms
{
    /// <summary>
    /// �·�ѡ��ؼ�
    /// </summary>
    public partial class MyMonthPicker : MyDatePicker
    {
        #region "Default Property"
        /// <summary>
        /// Constructor
        /// </summary>
        public MyMonthPicker()
            : base("yyyy\'��\'MM\'��\'")
        {
            //base.CustomFormat = "yyyy\'��\'MM\'��\'";
        }

        ///// <summary>
        ///// Default CustomFormat
        ///// </summary>
        //[DefaultValue("yyyy\'��\'MM\'��\'")]
        //public new string CustomFormat
        //{
        //    get { return base.CustomFormat; }
        //    set { base.CustomFormat = value; }
        //}

        #endregion

        #region "IDavaValueControl"

        /// <summary>
        /// ����ʱΪDateTimePicker.Value ���� null���ɿ�ʱ��ؼ���
        /// ֵ�е�ʱ��Ϊ00:00:00, Day = 1
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override object SelectedDataValue
        {
            get
            {
                if (base.SelectedDataValue == null)
                {
                    return null;
                }
                else
                {
                    DateTime dt = (DateTime)base.SelectedDataValue;
                    return new System.DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
                }
            }
        }

        #endregion
    }

    ///// <summary>
    ///// �·�ѡ��ؼ�
    ///// </summary>
    //public partial class MyMonthPicker : MyDatePicker, IDataValueControl, IStateControl
    //{
    //    #region "Default Property"
    //    /// <summary>
    //    /// Constructor
    //    /// </summary>
    //    public MyMonthPicker() :
    //        base()
    //    {
    //        base.DisplayFormatProvider = new System.Globalization.DateTimeFormatInfo();
    //        base.DisplayFormatProvider.FullDateTimePattern = "yyyy\'��\'MM\'��\'";
    //        base.DisplayFormatSpecifier = "F";

    //        base.EditFormatProvider = new System.Globalization.DateTimeFormatInfo();
    //        base.EditFormatProvider.ShortDatePattern = "yyyy\'��\'MM\'��\'";
    //        base.EditFormatSpecifier = "d";

    //        base.DropDownControl.Size = new Size(base.DropDownControl.Size.Width, 10);
    //    }

    //    /// <summary>
    //    /// ȡ������(����1�ţ�00:00:00)��Value
    //    /// </summary>
    //    public new object SelectedDataValue
    //    {
    //        get
    //        {
    //            if (base.Value == base.NullDate)
    //                return null;
    //            return new System.DateTime(base.Value.Year, base.Value.Month, 1);
    //        }
    //        set
    //        {
    //            if (value == null)
    //                base.Value = base.NullDate;
    //            DateTime dt = (DateTime)value;
    //            base.Value = new DateTime(dt.Year, dt.Month, 1);
    //        }
    //    }
    //    #endregion
    //}
}