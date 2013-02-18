using System;
using System.Collections.Generic;
using System.Text;

namespace Feng
{
    /// <summary>
    /// ��ֵ���ƶ�Ӧ
    /// </summary>
    public class NameValue
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public NameValue(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        private object m_value;

        /// <summary>
        /// ��ֵ
        /// </summary>
        public object Value
        {
            get { return m_value; }
            set { m_value = value; }
        }

        private string m_name;

        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
    }
}