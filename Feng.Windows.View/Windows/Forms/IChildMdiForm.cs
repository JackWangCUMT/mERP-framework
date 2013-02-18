using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Xceed.DockingWindows;

namespace Feng.Windows.Forms
{
    /// <summary>
    /// ����<see cref="TabbedMdiForm"/>���Ӵ���
    /// </summary>
    public interface IChildMdiForm : IForm
    {
        ///// <summary>
        ///// ������������ʾ�Ŀؼ�
        ///// </summary>
        //Control SearchPanel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        void SetCustomProperty(string propertyName, Func<object> propertyValue);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        void SetCustomProperty(string propertyName, object propertyValue);

        object GetCustomProperty(string propertyName, bool useCreator = true);

        bool Visible
        {
            get;
            set;
        }

        event EventHandler VisibleChanged;
    }
}