using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Feng.Collections
{
	/// <summary>
	/// ״̬�ؼ�����
	/// </summary>
    public class StateControlCollection : ControlCollection<IStateControl, IControlManager>, IStateControlCollection
	{
        private StateType m_state = StateType.None;

		/// <summary>
		/// ����״̬
		/// </summary>
		/// <param name="state"></param>
		public void SetState(StateType state)
		{
            m_state = state;

			foreach (IStateControl sc in this)
			{
				sc.SetState(state);
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public override void Add(IStateControl item)
        {
            item.SetState(m_state);

            base.Add(item);
        }
	}
}
