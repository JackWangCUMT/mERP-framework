using System;
using System.Collections.Generic;
using System.Text;

namespace Feng
{
	/// <summary>
	/// ��ֵ��������Ӧ��ϵ(����Enum���������ݿ�����)
	/// ���磺���� -> �����䣬1��С�䣬2��
	/// </summary>
	public class EnumNameValue
	{
		private object m_value;
        private int m_index;
        private string m_description;
        private Type m_valueType;

        /// <summary>
        /// 
        /// </summary>
        public Type ValueType
        {
            get { return m_valueType; }
        }

		/// <summary>
		/// ���
		/// </summary>
		public int Index
		{
			get { return m_index; }
		}

		/// <summary>
		/// ʵ��ֵ
		/// </summary>
		public object Value
		{
			get { return m_value; }
		}

        /// <summary>
        /// ��������ϸ���ƣ�
        /// </summary>
        public string Description
        {
            get { return m_description; }
        }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="value"></param>
		/// <param name="index"></param>
        /// <param name="description"></param>
        /// <param name="valueType"></param>
        public EnumNameValue(object value, int index, string description, Type valueType)
		{
			m_value = value;
			m_index = index;
            if (string.IsNullOrEmpty(description))
            {
                m_description = value.ToString();
            }
            else
            {
                m_description = description;
            }
            m_valueType = valueType;
		}

		/// <summary>
		/// IndexName = "����"
		/// </summary>
        public const string IndexName = "����";

		/// <summary>
        /// ValueName = "����";
		/// </summary>
        public const string ValueName = "����";

        /// <summary>
        /// ��ϸ����
        /// </summary>
        public const string DescriptionName = "����";

        /// <summary>
        /// Enum����
        /// </summary>
        public const string ValueTypeName = "����";
	}
}
