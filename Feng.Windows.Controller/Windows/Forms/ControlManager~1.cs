using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Feng.Windows.Forms
{
    /// <summary>
    /// ���ƹ��������ɶԶ��ʵ����������������ӡ��޸ġ�ɾ���Ȳ�����
    /// �б�˳�򲻿��ƣ����ֹ�����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ControlManager<T> : ControlManager, IWindowControlManager<T>
        where T : class, IEntity, new()
    {
        /// <summary>
        /// Constructor
        /// �����ؼ�����
        /// </summary>
        /// <param name="sm"></param>
        public ControlManager(ISearchManager sm)
            : base(new DisplayManager<T>(sm))
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
        /// ������Entity
        /// </summary>
        /// <returns></returns>
        protected override object AddNewItem()
        {
            T entity = new T();
            this.DisplayManager.Items.Add(entity);
            return entity;
        }

        /// <summary>
        /// ����������Դ
        /// </summary>
        /// <returns></returns>
        protected override IList NewList()
        {
            return new List<T>();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            ControlManager<T> cm = new ControlManager<T>(this.DisplayManager.SearchManager.Clone() as ISearchManager);
            Copy(this, cm);
            return cm;
        }

    }
}