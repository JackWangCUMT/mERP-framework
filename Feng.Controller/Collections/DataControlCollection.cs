using System;
using System.Collections;
using System.Collections.Generic;

namespace Feng.Collections
{
	/// <summary>
	/// ���ݿؼ�����
	/// </summary>
    public class DataControlCollection : ControlCollection<IDataControl, IDisplayManager>, IDataControlCollection
	{
        /// <summary>
        /// Constructor
        /// </summary>
        public DataControlCollection()
        {
        }

        /// <summary>
        /// Clear
        /// </summary>
        public override void Clear()
        {
            m_dict.Clear();
            base.Clear();
        }

		/// <summary>
		/// ��ӡ�
        /// �����ݸ����ݹ�������������Ϣ�������Ƿ�ɿ�
		/// </summary>
        /// <param name="item"></param>
        public override void Add(IDataControl item)
		{
            base.Add(item);

            if (!item.NotNull)
            {
                if (base.ParentManager != null && base.ParentManager.EntityInfo != null)
                {
                    if (base.ParentManager.EntityInfo.IdName == item.Name)
                    {
                        item.NotNull = true;
                    }
                    else
                    {
                        // ��ЩDataControl����Property��
                        if (string.IsNullOrEmpty(item.Navigator))
                        {
                            IPropertyMetadata attr = null;
                            if (!string.IsNullOrEmpty(item.PropertyName))
                            {
                                attr = base.ParentManager.EntityInfo.GetPropertMetadata(item.PropertyName);
                            }
                            if (attr != null && attr.NotNull)
                            {
                                item.NotNull = attr.NotNull;
                            }
                        }
                    }
                }
            }

            item.SelectedDataValueChanged += new EventHandler(item_SelectedDataValueChanged);
		}

        void item_SelectedDataValueChanged(object sender, EventArgs e)
        {
            IDataControl dc = sender as IDataControl;
            if (dc == null)
            {
                throw new ArgumentException("SelectedDataValueChanged's Sender should be IDataControl!");
            }
            if (this.ParentManager != null)
            {
                this.ParentManager.OnSelectedDataValueChanged(new SelectedDataValueChangedEventArgs(dc.Name, dc));
            }
        }

        /// <summary>
        /// ���ý��㵽��һ���ɲ�������ݿؼ�
        /// </summary>
        public virtual void FocusFirstInsertableControl()
        {
        }

        /// <summary>
        /// ���ý��㵽��һ���ɱ༭�����ݿؼ�
        /// </summary>
        public virtual void FocusFirstEditableControl()
        {
        }

        private Dictionary<string, IDataControl> m_dict = new Dictionary<string, IDataControl>();
        private void Init()
        {
            m_dict.Clear();
            foreach (IDataControl dc in this)
            {
                m_dict[dc.Name] = dc;
            }
        }

        /// <summary>
        /// ��ȡ������ָ�����Ƶ�Ԫ��
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IDataControl this[string name]
        {
            get
            {
                if (m_dict.ContainsKey(name))
                    return m_dict[name];
                else
                {
                    Init();
                    if (m_dict.ContainsKey(name))
                        return m_dict[name];
                    else
                        //throw new ArgumentException("Invalid DataControl with Name of " + name);
                        return null;
                }
            }
        }
	}
}
