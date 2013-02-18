namespace Feng
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// 
    /// </summary>
    public enum DataControlType
    {
        /// <summary>
        /// ����
        /// </summary>
        Normal = 0,
        /// <summary>
        /// ����ͳ�Ƶ�Column����������DetailGrid[0].
        /// CellViewManagerParamΪ��ʾ���ݣ���ʽΪ SUM: �տ��� where = "$%������%="101"$"�� where�����Ǳ��ʽ
        /// </summary>
        Stat = 3,
        /// <summary>
        /// ���ʽ
        /// </summary>
        Expression = 4,
        /// <summary>
        /// ����
        /// </summary>
        Unbound = 7,
    }

	/// <summary>
	/// ����ֵ����ʵ�������԰󶨵����ݿؼ�
	/// </summary>
	public interface IDataControl : IDataValueControl //, ICheckControl
	{
		/// <summary>
        /// ��ʾ���⡣����label����Label����ʾ��Ĭ�ϺͶ�ӦColumnһ�¡�
		/// </summary>
		string Caption
		{
			get;
			set;
		}

        /// <summary>
        /// ˳��
        /// </summary>
        int Index
        {
            get;
            set;
        }
		
        ///// <summary>
        ///// ������Ϣ
        ///// </summary>
        //[Category("Data")]
        //[Description("������Ϣ")]
        //[DefaultValue(0)]
        //int Group
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// DataControl's Name.���ڱ�ʶ��DataControl��������Item[]�β���
        /// </summary>
        string Name
        {
            get;
            set;
        }

		/// <summary>
        /// ��Ӧʵ������������
		/// </summary>
		string PropertyName
		{
			get;
			set;
		}

        /// <summary>
        /// ��Ӧʵ�������Ե���
        /// </summary>
        string Navigator
        {
            get;
            set;
        }

        /// <summary>
        /// ��������
        /// </summary>
        Type ResultType
        {
            get;
            set;
        }

        /// <summary>
        /// ����
        /// </summary>
        DataControlType ControlType
        {
            get;
            set;
        }

        ///// <summary>
        ///// �Ƿ��ڲ������ʱ����
        ///// </summary>
        //bool Insertable
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// �Ƿ��ڱ༭����ʱ����
        ///// </summary>
        //bool Editable
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// ���ɿ�
        /// </summary>
        bool NotNull
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ�ɼ�
        /// </summary>
        bool Visible
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ��û�ѡ��ɼ�����Visible�Ĺ�ϵ�ǣ�Visible��ϵͳ���Ƶģ�Available���û����Ƶģ�
        /// The Available property is different from the Visible property in that Available indicates whether the ToolStripItem is shown, while Visible indicates whether the ToolStripItem and its parent are shown.
        /// Grid.Column.Visible���ã���Parent����ʾ��ʱ��Visible����true
        /// </summary>
        bool Available
        {
            get;
            set;
        }

        /// <summary>
        /// AvailableChanged
        /// </summary>
        event EventHandler AvailableChanged;

        /// <summary>
        /// Tag
        /// </summary>
        object Tag
        {
            get;
            set;
        }

        /// <summary>
        /// SelectedDataValueChanged
        /// </summary>
        event EventHandler SelectedDataValueChanged;
	}
}
