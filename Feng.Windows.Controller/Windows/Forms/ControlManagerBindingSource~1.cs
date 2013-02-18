using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;

namespace Feng.Windows.Forms
{
    /// <summary>
    /// ���ƹ��������ɶԶ��ʵ����������������ӡ��޸ġ�ɾ���Ȳ�����
    /// ���˳����<see cref="BindingSource"/>����
    /// </summary>
    public class ControlManagerBindingSource<T> : ControlManagerBindingSource, IWindowControlManager<T>
        where T : class, IEntity, new()
    {
        /// <summary>
        /// Constructor
        ///  �����ؼ�����
        /// </summary>
        /// <param name="sm"></param>
        public ControlManagerBindingSource(ISearchManager sm)
            : base(new DisplayManagerBindingSource<T>(sm))
        {
            
        }

        /// <summary>
        /// Typed DisplayManager
        /// </summary>
        public IDisplayManager<T> DisplayManagerT
        {
            get { return this.DisplayManager as DisplayManager<T>; }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            ControlManagerBindingSource<T> cm = new ControlManagerBindingSource<T>(this.DisplayManager.SearchManager.Clone() as ISearchManager);
            Copy(this, cm);
            return cm;
        }
    }
}