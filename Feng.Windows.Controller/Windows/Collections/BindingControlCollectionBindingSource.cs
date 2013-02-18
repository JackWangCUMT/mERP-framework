using System;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

namespace Feng.Windows.Collections
{
    /// <summary>
    /// �󶨿ؼ�����
    /// </summary>
    public class BindingControlCollectionBindingSource : Feng.Collections.BindingControlCollection
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BindingControlCollectionBindingSource()
            : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bc">����Դ</param>
        public BindingControlCollectionBindingSource(BindingSource bc)
            : base()
        {
            m_bindingSource = bc;
        }

        private BindingSource m_bindingSource;

        internal BindingSource BindingSource
        {
            get { return m_bindingSource; }
            set { m_bindingSource = value; }
        }


        /// <summary>
        /// ��ӡ�
        /// ��BindingSource��Ϊ�գ�����������Դ
        /// </summary>
        /// <param name="item"></param>
        public override void Add(IBindingControl item)
        {
            if (m_bindingSource != null)
            {
                item.SetDataBinding(m_bindingSource, "");
            }

            base.Add(item);
        }
    }
}