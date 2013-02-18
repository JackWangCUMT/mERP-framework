using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Feng.Collections;

namespace Feng
{
	/// <summary>
	/// ��ʾ������
	/// </summary>
	public abstract class AbstractDisplayManager : IDisplayManager
	{
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_sm != null)
                {
                    m_sm.Dispose();
                }
                this.m_bindingControls.Clear();
                this.m_dataControls.Clear();

                this.SelectedDataValueChanged = null;
                this.PositionChanged = null;
                this.PositionChanging = null;
            }
        }

		/// <summary>
		/// Constructor
		/// </summary>
        /// <param name="sm">���ҹ�����</param>
        protected AbstractDisplayManager(ISearchManager sm)
		{
            if (sm != null)
            {
                m_sm = sm;
                m_sm.DisplayManager = this;
            }

            var ccf = ServiceProvider.GetService<IControlCollectionFactory>();
            if (ccf != null)
            {
                m_bindingControls = ccf.CreateBindingControlCollection(this);
                m_dataControls = ccf.CreateDataControlCollection(this);
            }
            else
            {
                m_bindingControls = new BindingControlCollection();
                m_dataControls = new DataControlCollection();
            }
		}

        private string m_name = string.Empty;
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        private IEntityMetadata m_entityInfo;

        /// <summary>
        /// ʵ������Ϣ
        /// </summary>
        public virtual IEntityMetadata EntityInfo
        {
            get
            {
                if (m_entityInfo == null)
                {
                    var s = ServiceProvider.GetService<IEntityMetadataGenerator>();
                    if (s != null)
                    {
                        m_entityInfo = s.GenerateEntityMetadata(m_sm);
                    }
                    else
                    {
                        m_entityInfo = EmptyEntityMetadata.Instance;
                    }
                }
                return m_entityInfo;
            }
        }

        private IDataControlCollection m_dataControls;
        /// <summary>
        /// ���ݿؼ�����
        /// </summary>
        public IDataControlCollection DataControls
        {
            get { return m_dataControls; }
        }


        private IBindingControlCollection m_bindingControls;
		/// <summary>
		///  �󶨿ռ伯��
		/// </summary>
		public IBindingControlCollection BindingControls
		{
			get { return m_bindingControls; }
		}

        private ISearchManager m_sm;
        /// <summary>
        /// ��ѯ������
        /// </summary>
        public ISearchManager SearchManager
        {
            get { return m_sm; }
        }

        /// <summary>
        /// �������ݰ󶨡�
        /// </summary>
        /// <param name="dataSource">����Դ</param>
        /// <param name="dataMember">���ݳ�Ա</param>
        /// <exception cref="System.NotSupportedException">����Դ�����ϸ�ʽʱ�׳�</exception>
        public abstract void SetDataBinding(object dataSource, string dataMember);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataItem"></param>
        public abstract void AddDataItem(object dataItem);

        /// <summary>
        /// ����Դ��ʵ�����б�
        /// ע�⣺��Grid's DataRows�е��б�˳��һ��(Grid ����Ӱ���б�˳��)
        /// ��ӿ�Ϊ�Ƿ��ͣ�ֻ�ܷ���object���͡�Ŀǰ֧��DataTable��IList(T)
        /// </summary>
        public abstract IList Items
        {
            get;
        }

        //private object m_dataSource;
        ///// <summary>
        ///// ����Դ
        ///// </summary>
        //public object DataSource
        //{
        //    get { return m_dataSource; }
        //    protected set { m_dataSource = value; }
        //}

        //private string m_dataMember;
        ///// <summary>
        ///// ���ݳ�Ա
        ///// </summary>
        //public string DataMember
        //{
        //    get { return m_dataMember; }
        //    protected set { m_dataMember = value; }
        //}

        private int m_position = -1;
        /// <summary>
        /// ��ǰλ�á�
        /// ��Ϊ������Ե�ǰʵ���࣬���Բ���ǰҪ���õ�ǰλ��
        /// λ�øı������PositionChanged�¼�
        /// </summary>
        public virtual int Position
        {
            get
            {
                return m_position;
            }
            set
            {
                if (m_position != value)
                {
                    CancelEventArgs e = new CancelEventArgs();
                    OnPositionChanging(e);
                    if (e.Cancel)
                        return;

                    if (value < 0 || value >= this.Count)
                    {
                        m_position = -1;
                    }
                    else
                    {
                        m_position = value;
                    }

                    OnPositionChanged(System.EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// ����PositionChanged<see cref="PositionChanging"/>�¼�
        /// </summary>
        /// <param name="e"></param>
        protected void OnPositionChanging(CancelEventArgs e)
        {
            if (PositionChanging != null)
            {
                PositionChanging(this, e);
            }
        }

        /// <summary>
        /// ����PositionChanged<see cref="PositionChanged"/>�¼�
        /// </summary>
        /// <param name="e"></param>
        public void OnPositionChanged(System.EventArgs e)
        {
            // When in edit, display also
            //if (this.State == StateType.View || this.State == StateType.None)
            {
                DisplayCurrent();
            }

            if (PositionChanged != null)
            {
                PositionChanged(this, e);
            }
        }

        /// <summary>
        /// <see cref="Position"/>�ı�ǰ����
        /// </summary>
        public event CancelEventHandler PositionChanging;

        /// <summary>
        /// <see cref="Position"/>�ı����
        /// </summary>
        public event EventHandler PositionChanged;

        ///// <summary>
        ///// ����ʾ��ǰλ������ǰ����
        ///// </summary>
        //public event EventHandler CurrentDisplaying;

        /// <summary>
        /// ��ʾ��ǰλ��ʵ��������
        /// </summary>
        public virtual void DisplayCurrent()
        {
            foreach (IDataControl dc in this.DataControls)
            {
                // if in tab control, the data control may be invisible
                //if (!dc.Visible)
                //    continue;

                switch (dc.ControlType)
                {
                    case DataControlType.Unbound:
                        break;
                    case DataControlType.Normal:
                        {
                            if (string.IsNullOrEmpty(dc.PropertyName))
                            {
                                continue;
                            }

                            if (this.CurrentItem == null)
                            {
                                dc.SelectedDataValue = null;
                            }
                            else
                            {
                                try
                                {
                                    dc.SelectedDataValue = EntityScript.GetPropertyValue(this.CurrentItem, dc.Navigator, dc.PropertyName);
                                }
                                catch (Exception ex)
                                {
                                    throw new ArgumentException(string.Format("DataControl of {0} is Invalid!", dc.Name), ex);
                                }
                            }
                        }
                        break;
                    case DataControlType.Expression:
                        if (string.IsNullOrEmpty(dc.PropertyName))
                            continue;
                        if (dc.PropertyName.Contains("%"))
                        {
                            dc.SelectedDataValue = EntityScript.CalculateExpression(dc.PropertyName, this.CurrentItem);
                        }
                        else
                        {
                        }
                        break;
                    default:
                        throw new ArgumentException(string.Format("DataControl of {0} is Invalid!", dc.Name));
                }
                
            }

        }

        ///// <summary>
        ///// DataView
        ///// </summary>
        //private DataView DataView
        //{
        //    get 
        //    {
        //        if (this.Items == null)
        //            return null;
        //        DataView view = this.Items as DataView;
        //        if (view == null)
        //        {
        //            throw new ArgumentException("in DisplayManager Items should be DataView!");
        //        }
        //        return view;
        //    }
        //}

        /// <summary>
        /// ��ǰʵ����
        /// </summary>
        public object CurrentItem
        {
            get
            {
                if (Position >= 0 && Position < this.Count)
                {
                    return this.Items[Position];
                }
                else
                {
                    return null;
                }
            }
        }

        ///// <summary>
        ///// ǿ���͵�ǰʵ����
        ///// </summary>
        //private DataRowView CurrentRow
        //{
        //    get
        //    {
        //        if (this.CurrentItem == null)
        //            return null;
        //        DataRowView rowView = this.CurrentItem as DataRowView;
        //        if (rowView == null)
        //        {
        //            throw new ArgumentException("in DisplayManager Item should be DataRowView!");
        //        }
        //        return rowView; 
        //    }
        //}

        ///// <summary>
        ///// ������Ż��ʵ����
        ///// </summary>
        ///// <param name="idx"></param>
        //public virtual object GetItem(int idx)
        //{
        //    if (idx < 0 || idx >= this.DataTable.Rows.Count)
        //    {
        //        throw new ArgumentException("Invalid index");
        //    }
        //    return this.DataTable.Rows[idx];
        //}

        /// <summary>
        /// ��ǰʵ�����б�����
        /// </summary>
        public int Count
        {
            get { return this.Items == null ? 0 : this.Items.Count; }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public abstract object Clone();

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        protected static void Copy(AbstractDisplayManager src, AbstractDisplayManager dest)
        {
            dest.BindingControls.AddRange(src.BindingControls);
            dest.DataControls.AddRange(src.DataControls);
        }


        /// <summary>
        /// SelectedDataValueChanged event
        /// </summary>
        public event EventHandler<SelectedDataValueChangedEventArgs> SelectedDataValueChanged;

        /// <summary>
        /// ����<see cref="SelectedDataValueChanged"/> �¼�
        /// </summary>
        /// <param name="e"></param>
        public void OnSelectedDataValueChanged(SelectedDataValueChangedEventArgs e)
        {
            if (this.SelectedDataValueChanged != null)
            {
                this.SelectedDataValueChanged(this, e);
            }
        }

        //private int m_preBatchPos = -1;
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        public void BeginBatchOperation()
        {
            //if (m_preBatchPos != -1 || m_cntBatchOperation)
            //{
            //    throw new InvalidOperationException("you should call EndBatchOperation after BeginBatchOperation!");
            //}
            m_cntBatchOperation++;

            //m_preBatchPos = this.Position;
        }

        /// <summary>
        /// ������������
        /// </summary>
        public void EndBatchOperation()
        {
            if (m_cntBatchOperation > 0)
            {
                m_cntBatchOperation--;
            }
            //if (m_preBatchPos != this.Position)   // ��Positionһ�µ�ʱ�����������Ҳ�п��ܸı�
            //{
                //m_preBatchPos = -1;

                // �����Զ�����PositionChanged�����ǳ������ֶ��������ø������
                // OnPositionChanged(System.EventArgs.Empty);  // in PositionChanged, there maybe have BeginBatchOperation
            //}
            //else
            //{
            //    m_preBatchPos = -1;
            //}
        }

        private int m_cntBatchOperation;
        /// <summary>
        /// �Ƿ�������������
        /// ��ʱ��������Row��Row_Saving),����ӦListChanged������ӦPositionChanged��Grid.CurrentRowChanged
        /// </summary>
        public bool InBatchOperation
        {
            get { return m_cntBatchOperation > 0; }
        }
	}
}
