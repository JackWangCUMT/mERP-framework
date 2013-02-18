//namespace Feng.Windows.Forms
//{
//    using System;
//    using System.ComponentModel;
//    using Feng.Utils;

//    /// <summary>
//    /// ���Ĭ��MoneyTextBox��Ϊר����ʾMoney��TextBox
//    /// Ĭ�����ԣ�
//    /// <list type="bullet">
//    /// <item>BorderStyle = BorderStyle.Fixed3D</item>
//    /// <item>TextAlign = System.Windows.Forms.HorizontalAlignment.Right</item>
//    /// <item>Size = (120, 21)</item>
//    /// <item>��ʾ��ǰ���У�����λС����������λһ�����999,999,99����С0��С����ǰ���λ��8λ����������</item>
//    /// </list>
//    /// ȡʵ��Text��RealNumericText����
//    /// ȥ��ֵ��Double����
//    /// </summary>
//    public class MyCurrencyTextBox : AMS.TextBox.CurrencyTextBox, IDataValueControl
//    {
//        #region "Default Property"

//        /// <summary>
//        /// 
//        /// </summary>
//        public MyCurrencyTextBox()
//            : this("+2")
//        {
//        }

//        /// <summary>
//        /// ��ʼ��Ĭ������
//        /// </summary>
//        /// <param name="format">2λ�ַ�����һλΪ��+�����ߡ�-�����ڶ�λΪС����λ���� Ĭ��Ϊ"+2",��ֻ����������2λС�� </param>
//        public MyCurrencyTextBox(string format)
//            : base()
//        {
//            base.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
//            base.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//            base.Size = new System.Drawing.Size(120, 21);

//            base.DecimalPoint = '.';
//            base.DigitsInGroup = 3;
//            base.GroupSeparator = ',';
//            base.MaxWholeDigits = 8;
//            base.RangeMax = 99999999;
//            base.RangeMin = -99999999;
//            base.Flags = (Int32) AMS.TextBox.ValidatingFlag.ShowIcon_IfInvalid;
//            base.CurrencySymbol = "";

//            if (format.Length != 2)
//            {
//                throw new ArgumentException("format is invalid");
//            }
//            this.AllowNegative = (format[0] == '-');
//            this.MaxDecimalPlaces = ConvertHelper.ToInt(format[1]).Value;
//        }

//        /// <summary>
//        /// Default CurrencySymbol 
//        /// </summary>
//        [DefaultValue("")]
//        public new string CurrencySymbol
//        {
//            get { return base.CurrencySymbol; }
//            set { base.CurrencySymbol = value; }
//        }

//        /// <summary>
//        /// Default BorderStyle 
//        /// </summary>
//        [DefaultValue(System.Windows.Forms.BorderStyle.Fixed3D)]
//        public new System.Windows.Forms.BorderStyle BorderStyle
//        {
//            get { return base.BorderStyle; }
//            set { base.BorderStyle = value; }
//        }

//        /// <summary>
//        /// Default TextAlign 
//        /// </summary>
//        [DefaultValue(System.Windows.Forms.HorizontalAlignment.Right)]
//        public new System.Windows.Forms.HorizontalAlignment TextAlign
//        {
//            get { return base.TextAlign; }
//            set { base.TextAlign = value; }
//        }

//        /// <summary>
//        /// Default DecimalPoint 
//        /// </summary>
//        [DefaultValue('.')]
//        public new Char DecimalPoint
//        {
//            get { return base.DecimalPoint; }
//            set { base.DecimalPoint = value; }
//        }

//        /// <summary>
//        /// Default DigitsInGroup 
//        /// </summary>
//        [DefaultValue(3)]
//        public new Int32 DigitsInGroup
//        {
//            get { return base.DigitsInGroup; }
//            set { base.DigitsInGroup = value; }
//        }

//        /// <summary>
//        /// Default GroupSeparator 
//        /// </summary>
//        [DefaultValue(',')]
//        public new Char GroupSeparator
//        {
//            get { return base.GroupSeparator; }
//            set { base.GroupSeparator = value; }
//        }

//        /// <summary>
//        /// Default MaxDecimalPlaces 
//        /// </summary>
//        [DefaultValue(2)]
//        public new Int32 MaxDecimalPlaces
//        {
//            get { return base.MaxDecimalPlaces; }
//            set { base.MaxDecimalPlaces = value; }
//        }

//        /// <summary>
//        /// Default MaxWholeDigits 
//        /// </summary>
//        [DefaultValue(8)]
//        public new Int32 MaxWholeDigits
//        {
//            get { return base.MaxWholeDigits; }
//            set { base.MaxWholeDigits = value; }
//        }

//        /// <summary>
//        /// Default RangeMax 
//        /// </summary>
//        [DefaultValue(99999999.0)]
//        public new Double RangeMax
//        {
//            get { return base.RangeMax; }
//            set { base.RangeMax = value; }
//        }

//        /// <summary>
//        /// Default RangeMin 
//        /// </summary>
//        [DefaultValue(-99999999.0)]
//        public new Double RangeMin
//        {
//            get { return base.RangeMin; }
//            set { base.RangeMin = value; }
//        }

//        /// <summary>
//        /// Default AllowNegative 
//        /// </summary>
//        [DefaultValue(false)]
//        public new bool AllowNegative
//        {
//            get { return base.AllowNegative; }
//            set { base.AllowNegative = value; }
//        }

//        #endregion

//        #region "IDataValueControl"

//        /// <summary>
//        /// �ı����ڵĽ��
//        /// </summary>
//        [Browsable(false)]
//        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//        public object SelectedDataValue
//        {
//            get
//            {
//                if (string.IsNullOrEmpty(Text))
//                {
//                    return null;
//                }
//                else
//                {
//                    return Convert.ToDecimal(Double);
//                }
//            }
//            set
//            {
//                if (value == null)
//                {
//                    Text = string.Empty;
//                }
//                else
//                {
//                    try
//                    {
//                        Double = Feng.Utils.ConvertHelper.ToDouble(value).Value;
//                    }
//                    catch (Exception ex)
//                    {
//                        throw new ArgumentException("MyCurrencyTextBox's SelectedDataValue must be decimal", ex);
//                    }
//                }
//            }
//        }

//        #endregion

//        #region "IStateControl"

//        /// <summary>
//        /// ����ʾ�ؼ�����State
//        /// </summary>
//        public void SetState(StateType state)
//        {
//            StateControlHelper.SetState(this, state);
//        }

//        #endregion
//    }
//}