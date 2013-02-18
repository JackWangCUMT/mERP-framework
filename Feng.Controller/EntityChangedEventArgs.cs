using System;
using System.Collections.Generic;
using System.Text;

namespace Feng
{
	/// <summary>
	/// ʵ����ı�����
	/// </summary>
	public enum EntityChangedType
	{
		/// <summary>
		/// �¼�
		/// </summary>
		Add,
		/// <summary>
		/// �༭
		/// </summary>
		Edit,
		/// <summary>
		/// ɾ��
		/// </summary>
		Delete
	}

	/// <summary>
	/// ʵ����ı��¼�����
	/// </summary>
	public class EntityChangedEventArgs : EventArgs
	{
		/// <summary>
		/// Consturctor
		/// </summary>
		/// <param name="type"></param>
		/// <param name="entity"></param>
		public EntityChangedEventArgs(EntityChangedType type, object entity)
		{
			m_type = type;
			m_entity = entity;
		}

		private EntityChangedType m_type;
        private object m_entity;
        private Exception m_failException;

		/// <summary>
		/// �ı�����
		/// </summary>
		public EntityChangedType EntityChangedType 
		{
			get { return m_type; } 
		}

		/// <summary>
		/// �ı��ʵ����
		/// </summary>
		public object Entity
		{
			get { return m_entity; }
		}

		/// <summary>
		/// �Ƿ�ɹ������ⲿ���أ�
        /// �����Null��ɹ�
		/// </summary>
		public Exception Exception
		{
            get { return m_failException; }
            set { m_failException = value; }
		}
	}
}
