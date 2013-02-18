using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Feng.Collections
{
	/// <summary>
	/// �ؼ�����
	/// </summary>
	/// <typeparam name="T">�ؼ�����</typeparam>
    /// <typeparam name="S">�ⲿ����������</typeparam>
	public class ControlCollection<T, S> : IList<T>, IEnumerable<T>
	{
		#region "Constructor"
		private List<T> m_items = new List<T>();
		
		private S m_cm;

        /// <summary>
        /// �ⲿ����������Ϊ<see cref="IControlManager"/>����<see cref="IDisplayManager"/>
        /// </summary>
        public S ParentManager
        {
            get { return m_cm; }
            set { m_cm = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ControlCollection()
        {
		}

		/// <summary>
		/// Constructor
		/// </summary>
        /// <param name="cm">�ⲿ������</param>
        public ControlCollection(S cm)
		{
			m_cm = cm;
		}

        /// <summary>
        /// ��Ӽ���
        /// </summary>
        /// <param name="controls"></param>
        public void AddRange(IEnumerable<T> controls)
        {
            if (controls == null)
            {
                throw new ArgumentNullException("controls");
            }

            foreach (T control in controls)
            {
                Add(control);
            }
        }
		#endregion

		#region "Interface"
		/// <summary>
		/// ����
		/// </summary>
		public int Count
		{
			get
			{
				return this.m_items.Count;
			}
		}

		/// <summary>
		/// �Ƿ�ֻ��
		/// </summary>
		public bool IsReadOnly
		{
			get
			{
				return ((IList)this.m_items).IsReadOnly;
			}
		}

        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(T item)
        {
            return m_items.IndexOf(item);
        }

        /// <summary>
        /// ��ȡ������ָ����������Ԫ��
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                return m_items[index];
            }
            set
            {
                m_items[index] = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            m_items.Insert(index, item);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            m_items.RemoveAt(index);
        }

		/// <summary>
		/// ���
		/// </summary>
        /// <param name="item"></param>
		public virtual void Add(T item)
		{
			if (item == null)
			{
                throw new ArgumentNullException("item");
			}

			m_items.Add(item);
		}

		/// <summary>
		/// ���
		/// </summary>
		public virtual void Clear()
		{
			m_items.Clear();
		}

		/// <summary>
		/// �Ƿ����
		/// </summary>
        /// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(T item)
		{
			return this.m_items.Contains(item);
		}

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="array"></param>
        /// <param name="arrayIndex"></param>
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.m_items.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// �Ƴ�
		/// </summary>
        /// <param name="item"></param>
		public bool Remove(T item)
		{
			int idx = this.m_items.IndexOf(item);
			if (idx == -1)
			{
				return false;
			}
			else
			{
				m_items.RemoveAt(idx);
				return true;
			}
		}

		/// <summary>
        /// ȡ��ö��
		/// </summary>
		/// <returns></returns>
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return m_items.GetEnumerator();
		}

		/// <summary>
        /// ȡ��ö��
		/// </summary>
		/// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
		{
			return m_items.GetEnumerator();
		}

		/// <summary>
		/// �Ƴ�����
		/// </summary>
		/// <param name="controls"></param>
		public void RemoveRange(IEnumerable<T> controls)
		{
			if (controls == null)
			{
				throw new ArgumentNullException("controls");
			}

			foreach (T control in controls)
			{
				Remove(control);
			}
		}
		#endregion
	}
}
