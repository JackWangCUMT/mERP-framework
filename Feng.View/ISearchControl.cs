using System.Collections;
using System.Collections.Generic;
using System;


namespace Feng
{
	/// <summary>
	/// ��ѯ�ؼ��ӿ�
	/// </summary>
    public interface ISearchControl : IReadOnlyControl
	{
        /// <summary>
        /// ����ѯ����
        /// </summary>
        /// <param name="searchExpression"></param>
        /// <param name="searchOrder"></param>
        void FillSearchConditions(IList<ISearchExpression> searchExpression, IList<ISearchOrder> searchOrder);

         /// <summary>
         /// ����
         /// </summary>
        string Caption
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
        /// ��PropertyNameһ��ĵĵ�����
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
        /// ���֣����ڱ�ʶ��SearchControl
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// ������ʱ�õĲ���Navigator.PropertyName�����������ֶ�ʱ
        /// </summary>
        string SpecialPropertyName
        {
            get;
            set;
        }

        /// <summary>
        /// ���ӵĲ�ѯ����
        /// </summary>
        string AdditionalSearchExpression
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        bool SearchNullUseFull
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ�ȡ��
        /// </summary>
        bool IsNot
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ�Ϊ��
        /// </summary>
        bool IsNull
        {
            get;
            set;
        }

        /// <summary>
        /// ��ѯ˳��
        /// </summary>
        bool? Order
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ����ģ��������
        /// Ϊ������Ĭ��������1����ʱ��ģ����������ʱ��ģ��
        /// </summary>
        bool? UseFuzzySearch
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ����ѡ��ģ������
        /// </summary>
        bool CanSelectFuzzySearch
        {
            get;
            set;
        }

		/// <summary>
		/// ��ѯ�ؼ�ѡ��ֵ
		/// </summary>
		IList SelectedDataValues
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
        ///  �Ƿ��û�ѡ��ɼ�����Visible�Ĺ�ϵ�ǣ�Visible��ϵͳ���Ƶģ�Available���û����Ƶģ�
        /// The Available property is different from the Visible property in that Available indicates whether the ToolStripItem is shown, while Visible indicates whether the ToolStripItem and its parent are shown.
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
        /// ˳��
        /// </summary>
        int Index
        {
            get;
            set;
        }

        /// <summary>
        /// Tag
        /// </summary>
        object Tag
        {
            get;
            set;
        }
	}
}
