using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Feng.Collections
{
    /// <summary>
    /// ���ؼ�����
    /// </summary>
    public class CheckControlCollection : ControlCollection<ICheckControl, IControlManager>, ICheckControlCollection
    {
		/// <summary>
		/// ���ؼ�������пؼ�
		/// </summary>
		public void CheckControlsValue()
		{
            foreach (ICheckControl cc in this)
            {
                if (!cc.CheckControlValue())
                {
                    throw new ControlCheckException("�����������飡", cc);
                }
            }
		}

		//private static int compareTabIndex(ICheckControl a, ICheckControl b)
		//{
		//    LabeledDataControl dca = a as LabeledDataControl;
		//    LabeledDataControl dcb = b as LabeledDataControl;
		//    if (dca == null || dcb == null)
		//        return 0;

		//    return dca.TabIndex.CompareTo(dcb.TabIndex);
		//}

		///// <summary>
		///// ����TabIndex����
		///// </summary>
		//public new void Sort()
		//{
		//    base.Sort(compareTabIndex);
		//}
    }
}
