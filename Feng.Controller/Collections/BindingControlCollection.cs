using System;
using System.Collections;
using System.Collections.Generic;

namespace Feng.Collections
{
    /// <summary>
    /// �󶨿ؼ�����
    /// </summary>
    public class BindingControlCollection : ControlCollection<IBindingControl, IDisplayManager>, IBindingControlCollection
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BindingControlCollection()
        {
        }

		/// <summary>
		/// ��ӡ�
        /// �縸���ƹ�������Ϊ�գ�����ӵ����ƹ�������<see cref="IControlManager.StateControls"/>��
		/// </summary>
        /// <param name="item"></param>
		public override void Add(IBindingControl item)
		{
            base.Add(item);
		}


        ///// <summary>
        ///// ������Դ
        ///// </summary>
        ///// <param name="dataSource"></param>
        ///// <param name="dataMember"></param>
        //public void SetDataBinding(object dataSource, string dataMember)
        //{
        //    foreach (IBindingControl item in this)
        //    {
        //        item.SetDataBinding(dataSource, dataMember);
        //    }
        //}

        /// <summary>
        /// ����State���ÿؼ�״̬
        /// </summary>
        public void SetState(StateType state)
        {
            foreach (IBindingControl item in this)
            {
                item.SetState(state);
            }
        }
    }
}
