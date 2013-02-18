using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Feng
{
    /// <summary>
    /// ���������
    /// </summary>
    public enum GridColumnType
    {
        /// <summary>
        /// ����
        /// </summary>
        Normal = 0,
        /// <summary>
        /// ������Column��ֻ���ڲ���
        /// </summary>
        NoColumn = 9,
        /// <summary>
        /// ������ѡ���Column
        /// </summary>
        CheckColumn = 1,
        /// <summary>
        /// ��ʾColumn�������<see cref="GridColumnWarningInfo"/>����ֵ��Ȼ������CellViewManager��Ĭ����ͼ�η�ʽ������
        /// </summary>
        WarningColumn = 2,
        /// <summary>
        /// ����ͳ�Ƶ�Column����������DetailGrid[0].
        /// CellViewManagerParamΪ��ʾ���ݣ���ʽΪ SUM: �տ��� where = "$%������%="101"$"�� where�����Ǳ��ʽ
        /// </summary>
        StatColumn = 3,
        /// <summary>
        /// ���ʽ������ΪPython Expression��Statement������Python�ļ�(.py��β)
        /// </summary>
        ExpressionColumn = 4,
        /// <summary>
        /// ͼ��
        /// </summary>
        ImageColumn = 5,
        /// <summary>
        /// �հ��У����ڷָ�
        /// </summary>
        SplitColumn = 6,
        /// <summary>
        /// �������ݰ󶨵�Column���հ�����
        /// </summary>
        UnboundColumn = 7,
        /// <summary>
        /// ���
        /// </summary>
        IndexColumn = 8
    }

    /// <summary>
    /// ����ж������ݣ�����<see cref="T:Grid.DataBoundGrid"/>��<see cref="T:Grid.DataUnboundGrid"/>
    /// ���ڱ�������Ϣ����<see cref="GridInfo"/>��<see cref="GridColumnInfo"/>��<see cref="GridRowInfo"/>��<see cref="GridCellInfo"/>
    /// ���������ݣ�Ҳ�������ô����ϵ�<see cref="Feng.IDataControl"/>�ؼ����ԡ�
    /// ����Ǻ�Entity�޹ص�Column����������PropertyName = Null������ʱ��������GridColumnName
    /// </summary>
    [Class(0, Name = "Feng.GridColumnInfo", Table = "AD_Grid_Column", OptimisticLock = OptimisticLockMode.Version)]
    [Cache(1, Usage = CacheUsage.NonStrictReadWrite)]
    [Serializable]
    public class GridColumnInfo : BaseADEntity
    {
        #region "Common"
        /// <summary>
        /// ���������<see cref="WindowTabInfo.GridName"/>��Ӧ
        /// </summary>
        [Property(Length = 100, NotNull = true)]
        public virtual string GridName
        {
            get;
            set;
        }
        #endregion

        #region "Entity"
        /// <summary>
        /// �˱����Ҫ��ʾ��ʵ��������·������<see cref="PropertyName"/>��ϡ�
        /// ���磬Ҫȡ��Ա�������������񱸰��У�PropertyName = "����", Navigator = "Ա��"��
        /// ���ұ��ʽ<see cref="Feng.SearchExpression"/>�п�д��"Ա��:����"
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string Navigator
        {
            get;
            set;
        }

        /// <summary>
        /// �˱����Ҫ��ʾ��ʵ�����������ơ�
        /// </summary>
        [Property(Length = 4000, NotNull = false)]
        public virtual string PropertyName
        {
            get;
            set;
        }


        /// <summary>
        /// �ڲ���ֵ����(Column.DateType)
        /// Ŀǰ֧��
        /// <list type="bullet">
        /// <item>System.String</item>
        /// <item>System.DateTime</item>
        /// <item>System.Int32</item>
        /// <item>System.Decimal</item>
        /// <item>System.Double</item>
        /// <item>System.....</item>
        /// <item>�����ڲ��Զ������ͣ�����enum(jkhd2.Model.�Ƿ��־,jkhd2.Model)</item>
        /// </list>
        /// </summary>
        [Property(Column = "Type", Length = 100, NotNull = true)]
        public virtual string TypeName
        {
            get;
            set;
        }

        ///// <summary>
        ///// ����<see cref="TypeName"/>��������
        ///// </summary>
        //public virtual Type CreateType()
        //{
        //    return ReflectionHelper.GetTypeFromName(TypeName);
        //}
        #endregion

        #region "Grid & DataControls"
        /// <summary>
        /// ����(Column.Title) and (Label.Text)
        /// ��Ϊ�գ�Ĭ����PropertyName
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string Caption
        {
            get;
            set;
        }

        /// <summary>
        /// ���������У���Ϊ������
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string GroupCaption
        {
            get;
            set;
        }

        /// <summary>
        /// ��ʾ˳��(Column.VisibleIndex) and (IDataControl.Index)
        /// </summary>
        [Property(NotNull = true)]
        public virtual int SeqNo
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ�ֻ��(Column.ReadOnly) and (IDataControl.ReadOnly)��
        /// ��ʽ��<see cref="T:Feng.Authority"/>
        /// </summary>
        [Property(Length = 255, NotNull = true)]
        public virtual string ReadOnly
        {
            get;
            set;
        }

        /// <summary>
        /// ������Ƿ���ʾ(Column.Visible)����ʽ��<see cref="T:Feng.Authority"/>
        /// </summary>
        [Property(Length = 255, NotNull = true)]
        public virtual string ColumnVisible
        {
            get;
            set;
        }

        /// <summary>
        /// ������������Ƿ�Ҫ��ǿյı��ʽ����ʽ��<see cref="T:Feng.Authority"/>
        /// ������ֻ�������������������Ա༭�������ã�����Ҫ��<see cref="GridCellInfo"/>������
        ///  ���<see cref="ValidRegularExpression"/>�����
        /// </summary>
        [Property(Length = 255, NotNull = true)]
        public virtual string NotNull
        {
            get;
            set;
        }

        /// <summary>
        /// ��������ʱ����������ʽ���Ƿ�Ϊ����<see cref="NotNull"/>�����ã������ֵ�ˣ�ֵ�ĸ�ʽ���������á�
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string ValidRegularExpression
        {
            get;
            set;
        }

        /// <summary>
        /// ûͨ��������ʽ���<see cref="ValidRegularExpression"/>ʱ��ʾ�Ĵ�����Ϣ
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string ValidErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// ��������ʱ����Script
        /// </summary>
        [Property(Length = 4000, NotNull = false)]
        public virtual string ValidScript
        {
            get;
            set;
        }
        #endregion

        #region "only to grid"
        /// <summary>
        /// ����е�����
        /// </summary>
        [Property(NotNull = true)]
        public virtual GridColumnType GridColumnType
        {
            get;
            set;
        }

        /// <summary>
        /// ��������ƣ�Column.Name������Ϊ�գ���<see cref="DefaultGridColumnName"/>��
        /// ��ĳЩʱ�򣬻����2�е�<see cref="DefaultGridColumnName"/>��ͬ����ֵͬ����ͬ��ʾ��ʽ����������Ҫ�ֹ�ָ�����������
        /// </summary>
        [Property(Column = "GridColumnName", Length = 100, NotNull = false)]
        public virtual string SpecialGridColumnName
        {
            get;
            set;
        }

        private string m_gridColumnName = null;
        /// <summary>
        /// ��������ı�������ơ�
        /// ���<see cref="SpecialGridColumnName"/>��Ϊ�գ���Ϊ<see cref="SpecialGridColumnName"/>��
        /// ����Ϊ<see cref="DefaultGridColumnName"/>�����<see cref="FullPropertyName"/>ҲΪ�գ���Ϊ<see cref="Caption"/>
        /// </summary>
        public virtual string GridColumnName
        {
            get
            {
                if (!string.IsNullOrEmpty(m_gridColumnName))
                    return m_gridColumnName;

                if (!string.IsNullOrEmpty(SpecialGridColumnName))
                {
                    m_gridColumnName = SpecialGridColumnName;
                }
                else
                {
                    var s = (string.IsNullOrEmpty(Navigator) ? PropertyName : Navigator + "." + PropertyName);
                    if (!string.IsNullOrEmpty(s))
                    {
                        m_gridColumnName = s;
                    }
                    else
                    {
                        m_gridColumnName = Caption;
                    }
                }

                m_gridColumnName = Feng.Utils.StringHelper.NormalizePropertyName(m_gridColumnName);
                return m_gridColumnName;
            }
        }

        /// <summary>
        /// Cell����ʾ�ؼ��������������<see cref="CellViewerManagerParam"/>��ָ����
        /// Ŀǰ֧��������
        /// <list type="bullet">
        /// <item>Combo: �ڲ�����ֵ����ʾֵ�������������磬�ڲ�ֵ900001����ʾ"�ͻ�1"������Ҫ������
        /// ������Ϊ�ڲ��Զ��建������(��Ӧ���ݱ�in GlobalData.cs)����Enum����ӦEnum���ͣ�</item>
        /// <item>MultiCombo: �ڲ����ֵ����ʾֵ�������������磬�ڲ�ֵa,f,g, ��ʾ"����Ա������Ա������Ա"��
        /// ����Ҫ��������������ͬCombo</item>
        /// <item>MultiLine: ���С����ж������֣������ʾ"..."���޲���</item>
        /// <item>Numeric: ���ң���ֵ������������ʾ��ʽ��������ѡ��Ϊ2λ�ַ�����һλΪ���ţ��ڶ�λΪС����λ����
        /// Ĭ��Ϊ" 2",��ǰ�治��ʾ��ֵ���ţ�2λС��</item>
        /// <item>DateTime: ʱ�䣬��������ʾ��ʽ��������ѡ����ʽΪ"yyyy-MM-dd hh:mm:ss"���Ƹ�ʽ��Ĭ��Ϊ"yyyy-MM-dd"</item>
        /// <item>Image: ͼ�����ڸ�int��ʾ��������Ϊͼ����Դ����</item>
        /// <item>MultiImage: ���ͼ��������ʾ��','�ֿ���int������������','�ֿ���ͼ����Դ���ơ���ʾ��intΪ��������ʾ�ڼ���ͼ��</item>
        /// <item>DetailSummaryRow: DetailGrid�е�SummaryRow�е�ĳ��������ʾ��ParentRow�С�����DetailGridStatistics���</item>
        /// <item>DetailGridStatistics: DetailGrid�е�����ͳ�ơ���ʽΪ "������: ������� level=? format=? where=?"�� ����"SUM: ��� format=n2 where="�ո���־=��"</item>
        /// </list>
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string CellViewerManager
        {
            get;
            set;
        }


        /// <summary>
        /// CellViewerManager�Ĳ���,�������ü�<see cref="CellViewerManager"/>
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string CellViewerManagerParam
        {
            get;
            set;
        }


        /// <summary>
        /// Cell�ı༭�ؼ��������������<see cref="CellEditorManagerParam"/>��ָ����
        /// Ŀǰ֧��������
        /// <list type="bullet">
        /// <item>Combo: �ڲ�����ֵ����ʾֵ�������������磬�ڲ�ֵ900001����ʾ"�ͻ�1"������Ҫ������
        /// ������Ϊ�ڲ��Զ��建������(��Ӧ���ݱ�in GlobalData.cs)����Enum����ӦEnum���ͣ�</item>
        /// <item>FreeCombo: ��ѡ�����ɱ༭���ڲ�ֵ����ʾֵ��ͬ��ֻ���ṩ��ѡ��ѡ��
        /// ����Ҫ��������������ͬCombo</item>
        /// <item>MultiCombo: �ڲ����ֵ����ʾֵ�������������磬�ڲ�ֵa,f,g, ��ʾ"����Ա������Ա������Ա"��
        /// ����Ҫ��������������ͬCombo</item>
        /// <item>Text: �����ı�������ΪUpper, Lower, Normal֮һ������CharacterCasing</item>
        /// <item>MultiLine: �����ı����޲���</item>
        /// <item>Date: ���ڣ��޲�����������ʾ����</item>
        /// <item>Time: ʱ�䣬���ı���ʽ����ʱ�䣬��ʽ�̶���
        /// ����Ҫ��������ʽΪ"yyyy-MM-dd hh:mm:ss"���Ƹ�ʽ��</item>
        /// <item>Numeric: ���ң���ֵ���޲�����������ʾ������</item>
        /// </list>
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string CellEditorManager
        {
            get;
            set;
        }


        /// <summary>
        /// CellEditorManager�Ĳ���,�������ü�<see cref="CellEditorManager"/>
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string CellEditorManagerParam
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ�����ִ���Ҽ�����"Ĭ�ϱ���"
        /// ���Ϊ�գ�������ΪTrue�����������ԭ����ֵ������ʾ��False������ʾֱ�Ӹ���
        /// </summary>
        [Property(NotNull = false)]
        public virtual bool? AllowSetList
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ������Ҽ�����
        /// </summary>
        [Property(NotNull = false)]
        public virtual bool? EnableCopy
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ������Ҽ�ѡ��ȫ��
        /// </summary>
        [Property(NotNull = false)]
        public virtual bool? EnableSelectAll
        {
            get;
            set;
        }

        /// <summary>
        /// ͳ�ƺ����������ڱ��ĩβSummaryRow��ʾͳ��ֵ��ͬʱ�������Ҳ���ʾ������Ϣ����<see cref="StatTitle"/>
        /// Ŀǰ֧��
        /// <list type="bullet">
        /// <item>SUM: �ܼ�</item>
        /// <item>COUNT: ����</item>
        /// </list>
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string StatFunction
        {
            get;
            set;
        }


        /// <summary>
        /// ͳ�ƺ������⣬���Զ��塣
        /// <para>
        /// The following table provides a list of the supported variables and their definitions.
        ///Note: Variables must be preceded and followed by a % symbol. See below for a usage example. 
        ///Supported variables Descriptions 
        ///%GROUPBYCOLUMNFIELDNAME% The Column.FieldName of the column as defined by the parent group's Group.GroupBy property. 
        ///%GROUPBYCOLUMNTITLE% The Title of the column represented by the parent group's Group.GroupBy property. 
        ///%GROUPKEY% The Key of the parent group. 
        ///%GROUPTITLE% The GroupBase.Title of the parent group. 
        ///%DATAROWCOUNT% The number of datarows in the parent group. 
        ///%STATCOLUMNFIELDNAME% The Column.FieldName of the column as defined by the SummaryCell.StatFieldName property. 
        ///%STATCOLUMNTITLE% The Title of the column represented by the SummaryCell.StatFieldName property. 
        ///%COUNT: (see below for usage)% The result of the Count statistical function. 
        ///%MAX: (see below for usage)% The result of the Maximum statistical function. 
        ///%MIN: (see below for usage)% The result of the Minimum statistical function. 
        ///%SUM: (see below for usage)% The result of the Sum statistical function. 
        ///%AVG: (see below for usage)% The result of the Average statistical function. 
        ///%STDEV: (see below for usage)% The result of the Standard Deviation statistical function. 
        ///%STDEVP: (see below for usage)% The result of the Standard Deviation Population statistical function. 
        ///%VAR: (see below for usage)% The result of the Variance statistical function. 
        ///%VARP: (see below for usage)% The result of the Variance Population statistical function. 
        ///%MEDIAN: (see below for usage)% The result of the Median statistical function. 
        ///%MODE: (see below for usage)% The result of the Mode statistical function. 
        ///%GEOMEAN: (see below for usage)% The result of the Geometric Mean statistical function. 
        ///%HARMEAN: (see below for usage)% The result of the Harmonic Mean statistical function. 
        ///%RMS: (see below for usage)% The result of the Root Mean Square statistical function. 
        ///In the case of the variables that represent StatFunctions (COUNT, MAX, MIN, SUM, AVG, STDEV, STDEVP, VAR, VARP, MEDIAN, MODE, GEOMEAN, HARMEAN, and RMS), additional parameters must be provided within the variable after the colon (:). These parameters are the fieldname of the column from which the data is retrieved, the (optional) format specifier with which the result of the statistical function is displayed, and the (optional) group level for which the statistical function is calculated.
        ///%COUNT: ["]stat_fieldname["] [ format=["]format_specifier["] ] [ level=running_stat_group_level ]%
        ///All items placed in square brackets [] are optional. If the stat_fieldname and/or format_specifier parameters contain spaces, quotes must be used. If a running_stat_group_level is not specified, -1 (current group) is assumed. Example 1 below demonstrates how to set the TitleFormat property using statistical function variables.
        ///The format of a mathematical function depends on 2 things: the function itself and the datatype of the column whose values are used for the statistical function. 
        ///The following table provides a list of supported and unsupported format specifiers relative to the datatype of the column whose values are used for the statistical function. 
        ///Statistical function Column datatype Datatype sent to the format specifier Unsupported format specifiers 
        ///COUNT Any Int64 R 
        ///MAX, MIN, MODE Anything that implements the IComparable interface. Always the same as the datatype of the column specified by the SummaryCell.StatFieldName property.  
        ///SUM Integral type* Int64 R 
        ///Floating point types** Double D and X 
        ///Decimal Decimal D, X and R 
        ///AVG, VAR, VARP, HARMEAN Integral* and Floating point** types Double D and X 
        ///Decimal Decimal D, X and R 
        ///STDEV, STDEVP, GEOMEAN, RMS Integral*, Floating point**, and Decimal types Double D and X 
        ///MEDIAN Integral type* (even item count) Double D and X 
        ///Integral type* (odd item count) Int64 R 
        ///Floating point types** Double D and X 
        ///Decimal Decimal D, X and R 
        ///* Integral types : Byte, SByte, Char, Int16, UInt16, Int32, UInt32, Int64, UInt64
        ///** Floating point types: Single and Double
        ///The values are rounded in the same manner as the string.Format round, meaning a banker's round.
        ///Hint: You can use F0 instead of D. It will have the same behavior and will work for either integral types or floating point types. 
        ///Example 2 below demonstrates how quotes can be used to specify literals in the format_specifier. 
        ///Example
        ///Example 1 demonstrates how to set the TitleFormat property using statistical function variables.
        ///Example 2 results in "val: 9.00" without the quotes, for the value "9". The internal quotes are necessary when the format_specifier contains a label (i.e., "val: ").
        ///Example 1VisualBasic Copy Code 
        ///summaryCell.TitleFormat = "A minimum of %MIN:Quantity% units were ordered at an average price of %AVG:UnitPrice format=c%." 
        ///C# Copy Code 
        ///summaryCell.TitleFormat = "A minimum of %MIN:Quantity% units were ordered at an average price of %AVG:UnitPrice format=c%."; 
        ///C# Copy Code 
        ///summaryCell.TitleFormat = "%MIN:Quantity format=\"\"val: \"0.00\"%"; 
        ///</para>
        /// ���빦�ܣ� ֧��Expression�� ���� %SUM expression="$iif[#ʹ������#=40,#������#,0]$"%�� %COUNT expression=""%.
        /// ע�⣬��ʱexpression�в�����%����%����#����
        ///</summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string StatTitle
        {
            get;
            set;
        }

        /// <summary>
        /// ����ɫ
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string BackColor
        {
            get;
            set;
        }

        /// <summary>
        /// ǰ��ɫ
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string ForeColor
        {
            get;
            set;
        }

        /// <summary>
        /// ����
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string FontName
        {
            get;
            set;
        }

        /// <summary>
        /// �����С
        /// </summary>
        [Property(NotNull = false)]
        public virtual float? FontSize
        {
            get;
            set;
        }

        /// <summary>
        /// HorizontalAlignment
        /// </summary>
        [Property(NotNull = false)]
        public virtual string HorizontalAlignment
        {
            get;
            set;
        }

        /// <summary>
        /// Ĭ������
        /// </summary>
        [Property(NotNull = false)]
        public virtual string SortDirection
        {
            get;
            set;
        }

        /// <summary>
        /// �����
        /// </summary>
        [Property(NotNull = false)]
        public virtual int? ColumnMaxWidth
        {
            get;
            set;
        }
        /// <summary>
        /// Ĭ��Fixed
        /// </summary>
        [Property(NotNull = false)]
        public virtual bool? ColumnFixed
        {
            get;
            set;
        }
        #endregion

        #region "only to MasterDetail(DataUnboundWithDetailGridLoadonce) Grid"

        /// <summary>
        /// ����Ŀ�ĸ�����Ŀ������<see cref="PropertyName"/>��ϣ�������ʾDetailGrid��
        /// ֻ����<see cref="T:Feng.Grid.DataUnboundWithDetailGridLoadonce"/>
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string ParentPropertyName
        {
            get;
            set;
        }

        #endregion

        #region "only to Control"

        /// <summary>
        /// ���ݿؼ��Ƿ�ɼ�����ʽ��<see cref="T:Feng.Authority"/>��
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string DataControlVisible { get; set; }

        /// <summary>
        /// ���ҿؼ��Ƿ�ɼ�����ʽ��<see cref="T:Feng.Authority"/>��
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string SearchControlVisible { get; set; }

        /// <summary>
        /// ����<see cref="Feng.ISearchControl.SpecialPropertyName"/>��
        /// ���Ϊ�գ���<see cref="Feng.ISearchControl"/>�Ĳ�����������Ϊ Navigator + "." + PropertyName
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string SearchControlFullPropertyName
        {
            get;
            set;
        }

        /// <summary>
        /// ���ҿؼ�����Ƕ��еģ�ComboBox������Ҫ�����ɸѡ����
        /// ���磬����Դ��Ա�������˴�ֻ��Ҫ��ʾ��Ա������FilterΪ���Ա�=�С�
        /// </summary>
        [Property(Column = "ControlInitParamFilter", Length = 255, NotNull = false)]
        public virtual string SearchControlInitParamFilter { get; set; }


        /// <summary>
        /// ����<see cref="Feng.ISearchControl.AdditionalSearchExpression"/>��
        /// ��ѡ��������ҿؼ�ʱ������������������⣬�����Ĳ���������
        /// ���磬���ҡ����������ʵ������ֻ�����ԡ�������ʱSearchControlAdditionalSearchExpressionҪ����Ϊ"�ո���־=��"
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string SearchControlAdditionalSearchExpression
        {
            get;
            set;
        }

        /// <summary>
        /// �ؼ����͡�
        /// ���ݿؼ�Ŀǰ֧��
        /// <list type="bullet">
        /// <item>MyTextBox�������ı���</item>
        /// <item>MyMultilineTextBox�������ı��򣬶�����������ƽ����ʾΪ����</item>
        /// <item>MyMultilineTextBox2�������ı�����ʾΪ����</item>
        /// <item>MyRichTextBox����ʽ�ḻ�Ķ����ı���</item>
        /// <item>MyIntegerTextBox�����������ı��򣬷�������ΪInt32</item>
        /// <item>MyLongTextBox�����������ı��򣬷�������ΪLong</item>
        /// <item>MyNumericTextBox����ֵ�����ı���</item>
        /// <item>MyCurrencyTextBox�����������ı��򣨴����ҷ��ţ�</item>
        /// <item>MyComboBox��ѡ����޶���������</item>
        /// <item>MyFreeComboBox��ѡ��򣬲���ֻ�����루���޶���������</item>
        /// <item>MyListBox���б��</item>
        /// <item>MyRadioListBox����ѡ��ť�б��</item>
        /// <item>MyCheckedListBox����ѡ��ť�б��</item>
        /// <item>MyOptionPicker��������ѡ��</item>
        /// <item>MyRadioButton����ѡ��ť</item>
        /// <item>MyCheckBox����ѡ��ť</item>
        /// <item>MyThreeStateCheckbox: 3̬��ѡ��ť</item>
        /// <item>MyPictureBox��ͼƬ��</item>
        /// <item>MyDateTimePicker��ʱ������ѡ���</item>
        /// <item>MyDatePicker������ѡ���</item>
        /// <item>MyMonthPicker���·�ѡ���</item>
        /// <item>MyFileBox���ļ�ѡ���</item>
        /// <item>MyGrid�����ݱ��</item>
        /// </list>
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string DataControlType { get; set; }

        /// <summary>
        /// �ؼ����͡�
        /// ���ҿؼ�Ŀǰ֧��
        /// <list type="bullet">
        /// <item>MyTextBox�������ı��򡣿���%ͨ�����Ĭ��Ϊģ��ƥ�䣨��ͷĩβ��%��������辫ȷƥ�䣬ĩβ��'#'</item>
        /// <item>MyMultilineTextBox�������ı���������Ϊ���У�ģʽͬMyTextBox������У�ģʽͬSQL����е�IN����</item>
        /// <item>MyIntegerTextBox��������Χ�ڲ��ң�ƥ��Int32</item>
        /// <item>MyLongTextBox�� ������Χ�ڲ��ң�ƥ��Long</item>
        /// <item>MyNumericTextBox����ֵ��Χ�ڲ���</item>
        /// <item>MyCurrencyTextBox�����ҷ�Χ�ڲ���</item>
        /// <item>MyComboBox��ѡ����޶���������</item>
        /// <item>MyFreeComboBox��ѡ��򣬲���ֻ�����루���޶���������</item>
        /// <item>MyOptionPicker��������ѡ��</item>
        /// <item>MyRadioButton����ѡ��ť</item>
        /// <item>MyDateTimePicker��ʱ�����ڷ�Χ�ڲ���</item>
        /// <item>MyDatePicker�����ڷ�Χ�ڲ���</item>
        /// <item>MyMonthPicker���·ݷ�Χ�ڲ���</item>
        /// <item>CustomSearchControl���Զ�����ң����ݶ�����<see cref="CustomSearchInfo"/>����Ҫָ��PropertyName����ƥ��GroupName</item>
        /// </list>
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string SearchControlType { get; set; }

        /// <summary>
        /// ���ҿؼ���ʼ��������
        /// �������<see cref="SearchControlType"/>��MyComboBox��MyFreeComboBox��MyOptionPicker����Ҫ�в�����
        /// ������Ϊ�ڲ�DataTable��������<see cref="NameValueMappingInfo"/>����Enum����ӦEnum���ͣ��������<see cref="T:Feng.Windows.Forms.ControlFactory"/>
        /// ���ݿؼ��ĳ�ʼ��������<see cref="CellEditorManagerParam"/> 
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string SearchControlInitParam { get; set; }

        /// <summary>
        /// ���ݿؼ���Ĭ��ֵ��ͬʱҲ�Ǳ��InsertRow��Ĭ��ֵ
        /// �����Enum������"�Ա�.��"��ֻ��Ҫд"��"��
        /// ��ʽͬ<see cref="SearchControlDefaultValue"/>
        /// </summary>
        [Property(Length = 400, NotNull = false)]
        public virtual string DataControlDefaultValue { get; set; }

        /// <summary>
        /// ���ҿؼ���Ĭ��ֵ(����LabeledSearchControlBetween�����۷��ص�ֵ��","�ָ�
        /// �����Ҫ��ͬȨ�����ò�ֵͬ����;�ֿ���Ĭ��ֵ���#���Ȩ�ޱ��ʽ��
        /// ����a,b#I:A; c,d#I:B��Ĭ��ֵ����Python���ʽ�����ؽ��Ϊresult��������
        /// ���Ҫ��Python���ʽ��������#Python��ͷ������ǰ��Ҫ���""��
        /// �����Ҫȡ����ǰ���NOT; �����ҪΪ�գ���null���������Ϊ�գ�����NOT null��
        /// </summary>
        [Property(Length = 400, NotNull = false)]
        public virtual string SearchControlDefaultValue { get; set; }

        /// <summary>
        /// ������IsNullʱ����FullPropertyName IsNull ���� Navigator IsNull
        /// ���磬���ʵ�.���˵��� Ϊ�գ����� ���ʵ� Ϊ��
        /// TrueʱΪǰ�ߡ�
        /// </summary>
        [Property(NotNull = true)]
        public virtual bool SearchNullUseFull { get; set; }

        /// <summary>
        /// ���ҿؼ���Ĭ��˳��
        /// <list>
        /// <item>ASC: ˳��</item>
        /// <item>DESC: ����</item>
        /// </list>
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string SearchControlDefaultOrder { get; set; }

        /// <summary>
        /// ����<see cref="CellEditorManager"/>��ɸѡ���������<see cref="SearchControlInitParamFilter"/>
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string CellEditorManagerParamFilter { get; set; }

        #endregion

        #region "Events"
        /// <summary>
        /// ��Column��Cell DoubleClickdʱִ�е�Event Process
        /// </summary>
        [Property(Length = 50, NotNull = false)]
        public virtual string DoubleClick
        {
            get;
            set;
        }
        #endregion
    }
}