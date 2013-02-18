using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Feng.Windows.Forms;

namespace Feng.Windows.Collections
{
    /// <summary>
    /// ���ҿؼ�����
    /// </summary>
    public class SearchControlCollection : Feng.Collections.SearchControlCollection
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SearchControlCollection()
        {
        }

        /// <summary>
        /// ��ӡ�
        /// ����Enter KeyPress�¼���
        /// </summary>
        /// <param name="item"></param>
        public override void Add(ISearchControl item)
        {
            base.Add(item);

            IWindowControl outerControl = item as IWindowControl;
            if (outerControl != null)
            {
                outerControl.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(InputBox_KeyPress);
            }
            else
            {
                IWindowControlBetween lfcb = item as IWindowControlBetween;
                if (lfcb != null)
                {
                    System.Windows.Forms.Control control = lfcb.Control1 as System.Windows.Forms.Control;
                    if (control != null)
                    {
                        control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(InputBox_KeyPress);
                    }
                    control = lfcb.Control2 as System.Windows.Forms.Control;
                    if (control != null)
                    {
                        control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(InputBox_KeyPress);
                    }
                }
            }
        }


        private void InputBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) System.Windows.Forms.Keys.Enter)
            {
                base.ParentManager.LoadDataAccordSearchControls();
            }
        }
    }
}