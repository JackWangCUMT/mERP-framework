using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Feng.Collections;
using Feng.Async;

namespace Feng
{
    /// <summary>
    /// 
    /// </summary>
    public static class SearchManagerDefaultValue
    {
        /// <summary>
        /// 
        /// </summary>
        public static int MaxResult = 25;
    }

    /// <summary>
    /// ��ѯ������
    /// </summary>
    public abstract class AbstractSearchManager : ISearchManager
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
                this.DataLoaded = null;
                this.DataLoading = null;

                if (m_searchControls != null)
                {
                    foreach (ISearchControl sc in m_searchControls)
                    {
                        IDisposable i = sc as IDisposable;
                        if (i != null)
                        {
                            i.Dispose();
                        }
                    }
                    m_searchControls.Clear();
                }

                this.m_dm = null;

                //if (m_loadingThread != null)
                //{
                //    m_loadingThread = null;
                //}
                DisposeAsyncManager();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="smName"></param>
        protected AbstractSearchManager(string smName)
        {
            this.Name = smName;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public AbstractSearchManager()
        {
            this.Name = this.GetType().Name;

            m_searchControls = CreateSearchControlCollection();
        }

        /// <summary>
        /// CreateSearchControlCollection
        /// </summary>
        /// <returns></returns>
        protected virtual ISearchControlCollection CreateSearchControlCollection()
        {
            var ccf = ServiceProvider.GetService<IControlCollectionFactory>();
            if (ccf != null)
            {
                return ccf.CreateSearchControlCollection(this);
            }
            else
            {
                return SearchControlCollection.Empty;
            }
        }

        private ISearchControlCollection m_searchControls;
        /// <summary>
        /// ��ѯ�ؼ�
        /// </summary>
        public ISearchControlCollection SearchControls
        {
            get { return m_searchControls; }
        }

        /// <summary>
        /// ���ݲ�ѯ�ؼ���������ѯ����
        /// </summary>
        private void FillSearchConditions(out IList<ISearchExpression> searchExpressions, out IList<ISearchOrder> searchOrders)
        {
            searchExpressions = new List<ISearchExpression>();
            searchOrders = new List<ISearchOrder>();

            foreach (ISearchControl fc in SearchControls)
            {
                fc.FillSearchConditions(searchExpressions, searchOrders);
            }
        }

        
        /// <summary>
        ///
        /// </summary>
        public string AdditionalSearchExpression
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AdditionalSearchOrder
        {
            get;
            set;
        }

        private IDisplayManager m_dm;
        /// <summary>
        /// ��ʾ������
        /// </summary>
        public IDisplayManager DisplayManager
        {
            get { return m_dm; }
            set { m_dm = value; }
        }

        private bool m_enablePage = true;
        /// <summary>
        /// �Ƿ�ʹ�÷�ҳ
        /// </summary>
        public bool EnablePage
        {
            get { return m_enablePage; }
            set { m_enablePage = value; }
        }

        private int m_maxResult = SearchManagerDefaultValue.MaxResult;
        /// <summary>
        /// ��ѯ����������
        /// </summary>
        public int MaxResult
        {
            get { return m_maxResult; }
            set { m_maxResult = value; }
        }

        private int m_firstResult;
        /// <summary>
        /// ��ѯ�������
        /// </summary>
        public int FirstResult
        {
            get { return m_firstResult; }
            set { m_firstResult = value; }
        }


        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// ����Ƿ�ΪΨһ
        /// </summary>
        public bool IsResultDistinct
        {
            get;
            set;
        }

        private IList<string> m_eagerFetchs = new List<string>();
        /// <summary>
        /// EagerFetchs
        /// </summary>
        public IList<string> EagerFetchs
        {
            get { return m_eagerFetchs; }
        }

        /// <summary>
        /// ��ѯ�������
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// ��ѯ���
        /// </summary>
        public System.Collections.IEnumerable Result { get; set; }

        /// <summary>
        /// ���ݲ�ѯǰ����
        /// </summary>
        public event EventHandler<DataLoadingEventArgs> DataLoading;

        /// <summary>
        /// ���ݲ�ѯ����
        /// </summary>
        public event EventHandler<DataLoadedEventArgs> DataLoaded;

        /// <summary>
        /// ����<see cref="DataLoading"/>�¼�
        /// </summary>
        public void OnDataLoading(DataLoadingEventArgs e)
        {
            if (DataLoading != null)
            {
                DataLoading(this, e);
            }
        }

        /// <summary>
        /// ����<see cref="DataLoaded"/>�¼�
        /// </summary>
        public void OnDataLoaded(DataLoadedEventArgs e)
        {
            if (DataLoaded != null)
            {
                DataLoaded(this, e);
            }
        }

        /// <summary>
        /// ���ݲ�ѯ������ѯ����(��ѯ��������������������<see cref="Result"/>������������<see cref="Count"/>
        /// </summary>
        /// <param name="searchExpression"></param>
        /// <param name="searchOrders"></param>
        protected abstract void GetDataCount(ISearchExpression searchExpression, IList<ISearchOrder> searchOrders);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchExpression"></param>
        /// <param name="searchOrders"></param>
        /// <param name="func"></param>
        protected virtual void GetDataCount(ISearchExpression searchExpression, IList<ISearchOrder> searchOrders, Action<object> func) 
        {
            throw new NotSupportedException("GetDataCount is not supported in base class.");
        }

        private bool m_useStreamLoad = false;
        /// <summary>
        /// �Ƿ��������ȡ
        /// </summary>
        public bool UseStreamLoad
        {
            get { return m_useStreamLoad; }
            set { m_useStreamLoad = value; }
        }

        /// <summary>
        /// ���ݲ�ѯ������ѯ���ݣ�ֻ���ز�ѯ�����������������Ҫ��ѯ����������<see cref="GetCount"/>
        /// </summary>
        /// <param name="searchExpression"></param>
        /// <param name="searchOrders"></param>
        /// <returns></returns>
        public abstract System.Collections.IEnumerable GetData(ISearchExpression searchExpression, IList<ISearchOrder> searchOrders);

        /// <summary>
        /// ���ݲ�ѯ�������ط�������������
        /// </summary>
        /// <param name="searchExpression"></param>
        /// <returns></returns>
        public abstract int GetCount(ISearchExpression searchExpression);

        private AsyncManager<object, DataLoadedEventArgs> m_asyncManager;
        private void DisposeAsyncManager()
        {
            if (m_asyncManager != null)
            {
                m_asyncManager.WorkerDone -= new EventHandler<WorkerDoneEventArgs<DataLoadedEventArgs>>(LoadWork_WorkerDone);
                m_asyncManager = null;
            }
        }
        private void InitAsyncManager(DataLoadingEventArgs e)
        {
            m_asyncManager = new AsyncManager<object, DataLoadedEventArgs>(this.Name, new DataLoadWorker(this, e));
            m_asyncManager.WorkerDone += new EventHandler<WorkerDoneEventArgs<DataLoadedEventArgs>>(LoadWork_WorkerDone);
            m_asyncManager.WorkerProgress += new EventHandler<WorkerProgressEventArgs<object>>(m_asyncManager_WorkerProgress);
        }

        void m_asyncManager_WorkerProgress(object sender, WorkerProgressEventArgs<object> e)
        {
            object entity = e.ProgressData;
            if (this.DisplayManager != null && entity != null)
            {
                this.DisplayManager.AddDataItem(entity);
            }
        }

        /// <summary>
        /// Occur when Load Thread is done
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void LoadWork_WorkerDone(object sender, WorkerDoneEventArgs<DataLoadedEventArgs> e)
        {
            // ����;ֹͣ��e.Interrupted = true, result = null;
            DataLoadedEventArgs e2 = e.Result;

            try
            {
                if (this.DisplayManager != null && e2 != null && e2.DataSource != null)
                {
                    this.DisplayManager.SetDataBinding(e2.DataSource, string.Empty);
                }
            }
            finally
            {
                OnDataLoaded(e2);
            }

            if (e.Exception != null)
            {
                ExceptionProcess.ProcessWithNotify(e.Exception);
            }
        }

        /// <summary>
        /// �Զ�����������Ȼ�����
        /// </summary>
        public void LoadDataAccordSearchControls()
        {
            IList<ISearchExpression> searchExpressions;
            IList<ISearchOrder> searchOrders;
            FillSearchConditions(out searchExpressions, out searchOrders);
            LoadData(SearchExpression.ToSingle(searchExpressions), searchOrders);
        }

        /// <summary>
        /// ���ݲ�ѯ������ѯ���ݣ������ò�ѯ��ʷ������<see cref="DataLoading"/>��<see cref="DataLoaded"/>�¼�
        /// </summary>
        /// <param name="searchExpression"></param>
        /// <param name="searchOrders"></param>
        public void LoadData(ISearchExpression searchExpression, IList<ISearchOrder> searchOrders)
        {
            lock (m_syncObject)
            {
                //if (m_loadingThread != null && m_loadingThread.IsAlive)
                //{
                //    m_loadingThread.Abort();
                //    m_loadingThread.Join();
                //}

                //if (m_isDataLoding)
                //{
                //    throw new InvalidUserOperationException("����ֹͣ�����������¿�ʼ������");
                //}

                // ReloadҲ���ô˺�������������������
                //this.FirstResult = 0;


                if (m_asyncManager != null && m_asyncManager.IsWorkerBusy)
                {
                    return;
                }

                SetHistory(searchExpression, searchOrders);

                this.FillSearchAdditionals(ref searchExpression, ref searchOrders);

                DisposeAsyncManager();

                DataLoadingEventArgs e = new DataLoadingEventArgs(searchExpression, searchOrders);
                OnDataLoading(e);

                if (e.Cancel)
                {
                    return;
                }

                //if (m_loadingThread != null && m_loadingThread.IsAlive)
                //{
                //    m_loadingThread.Abort();
                //}

                //// �߳������˾Ͳ�������������Ҫ���½�һ��
                //m_loadingThread = new Thread(new ParameterizedThreadStart(ThreadLoadData));
                //m_loadingThread.IsBackground = true;

                //if (asyncOperation == null)
                //{
                //    this.asyncOperation = System.ComponentModel.AsyncOperationManager.CreateOperation(null);
                //    this.operationCompleted = new SendOrPostCallback(this.AsyncOperationCompleted);
                //}

                //m_isDataLoding = true;

                //m_loadingThread.Start(e);

                InitAsyncManager(e);

                m_asyncManager.StartWorker(SystemConfiguration.UseMultiThread);
            }
        }

        //private bool m_isDataLoding;
        private object m_syncObject = new object();

        /// <summary>
        /// ֹͣ��ȡ����
        /// </summary>
        public bool StopLoadData()
        {
            //if (m_loadingThread != null && m_loadingThread.IsAlive)
            //{
            //    m_loadingThread.Abort();
            //    m_loadingThread.Join();
            //}
            if (m_asyncManager != null)
            {
                WorkerDoneEventArgs<DataLoadedEventArgs> e = m_asyncManager.AbortWorker();
                LoadWork_WorkerDone(m_asyncManager, e);

                return true;
            }
            return false;
        }

        /// <summary>
        /// �ȴ�ֱ�������̷߳���
        /// </summary>
        public bool WaitLoadData()
        {
            //if (m_loadingThread != null && m_loadingThread.IsAlive)
            //{
            //    m_loadingThread.Join();
            //}
            if (m_asyncManager != null)
            {
                WorkerDoneEventArgs<DataLoadedEventArgs> e = m_asyncManager.WaitForWorker();
                LoadWork_WorkerDone(m_asyncManager, e);

                return true;
            }
            return false;
        }

        private static SearchHistoryInfo m_emptySearchHistory = new SearchHistoryInfo();
        /// <summary>
        /// ������ŵõ���ѯ��ʷ(0Ϊ���µ�, ��������)
        /// </summary>
        /// <param name="idx"></param>
        public SearchHistoryInfo GetHistory(int idx)
        {
            if (idx < 0)
            {
                idx += historyCnt;
            }
            if (idx < 0 || idx >= historyCnt)
            {
                return m_emptySearchHistory;
            }

            if (m_searchHistoryInfos[idx] == null)
            {
                return m_emptySearchHistory;
            }
            else
            {
                return m_searchHistoryInfos[idx];
            }
        }

        private SearchHistoryInfo[] m_searchHistoryInfos = new SearchHistoryInfo[10];
        private const int historyCnt = 10;
        //private string[] historySearchExpressions = new string[historyCnt];
        //private string[] historySearchOrders = new string[historyCnt];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchExpression"></param>
        /// <param name="searchOrders"></param>
        public SearchHistoryInfo SetHistory(ISearchExpression searchExpression, IList<ISearchOrder> searchOrders)
        {
            return SetHistory(searchExpression, searchOrders, true);
        }

        /// <summary>
        /// SetHistory
        /// </summary>
        /// <param name="searchExpression"></param>
        /// <param name="searchOrders"></param>
        /// <param name="isCurrent"></param>
        private SearchHistoryInfo SetHistory(ISearchExpression searchExpression, IList<ISearchOrder> searchOrders, bool isCurrent)
        {
            string nowExpression = SearchExpression.ToString(searchExpression);

            int existIdx = -1;

            // �������ظ���Ҳһ����ӣ���Ϊ��ҳ�õ���ʷ������һ��(ֻ�����һ���Ƚϣ�
            for (int i = 0; i < 1; ++i)
            {
                if (m_searchHistoryInfos[i] != null 
                    && m_searchHistoryInfos[i].Expression == nowExpression)
                {
                    existIdx = i;
                    break;
                }
            }

            if (existIdx == -1)
            {
                //historySearchExpressions[historyNow] = SearchExpression.ToString(searchExpressions);
                //historySearchOrders[historyNow] = SearchOrder.ToString(searchOrders);
                //historyNow = (historyNow + 1) % historyCnt;
                for (int i = historyCnt - 1; i > 0; --i)
                {
                    m_searchHistoryInfos[i] = m_searchHistoryInfos[i - 1];
                }
                m_searchHistoryInfos[0] = new SearchHistoryInfo(SearchExpression.ToString(searchExpression),
                    SearchOrder.ToString(searchOrders), isCurrent);

                return m_searchHistoryInfos[0];
            }
            else
            {
                m_searchHistoryInfos[existIdx].IsCurrentSession = isCurrent;
                return m_searchHistoryInfos[existIdx];
            }
        }

        //private bool m_needSearchExpression = true;
        ///// <summary>
        ///// �Ƿ���Ҫ��������������Ҫ��ˢ�µ�ʱ�����Ҫ����������
        ///// </summary>
        //protected bool NeedSearchConddition
        //{
        //    get { return m_needSearchExpression; }
        //    set { m_needSearchExpression = value; }
        //}

        /// <summary>
        /// ���¶��뵱ǰ��
        /// </summary>
        public void ReloadItem(int idx)
        {
            //if (this.DisplayManager.CurrentItem == null)
            //    return;
            if (this.DisplayManager.EntityInfo == null)
                return;
            if (idx < 0 || idx >= this.DisplayManager.Count)
            {
                return;
            }
            object entity = this.DisplayManager.Items[idx];
            object id = EntityScript.GetPropertyValue(entity, this.DisplayManager.EntityInfo.IdName);

            IList list = this.GetData(SearchExpression.Eq(this.DisplayManager.EntityInfo.IdName, id), null) as IList;
            System.Diagnostics.Debug.Assert(list.Count <= 1);

            if (list.Count == 0)
            {
                return;
            }
            else if (list.Count == 1)
            {
                this.DisplayManager.Items[idx] = list[0];
            }
        }

        /// <summary>
        /// ������һ�β�ѯ�������¶�������
        /// ��һ����¼���ò���
        /// </summary>
        public virtual void ReloadData()
        {
            //// ��û���ҹ�������ˢ��
            //if (m_needFindConddition && !m_findConditionLoaded)
            //    return;

            //if (m_needSearchExpression)
            //{
                SearchHistoryInfo his = GetHistory(0);

                // his.Expression = string.Empty �����ѯ����Ϊ�գ���ʱ�Ѿ���ѯ��
                if (his != null && his.IsCurrentSession && his.Expression != null)
                {
                    LoadData(SearchExpression.Parse(his.Expression), SearchOrder.Parse(his.Order));
                }
                // �����޲�ѯ��������ˢ��
                //else
                //{
                //    LoadData();
                //}
            //}
            //else
            //{
            //    LoadData(new List<ISearchExpression>(), null, this.FirstResult);
            //    return true;
            //}
        }

        /// <summary>
        /// ����Entity Schema��ʹGrid����������ʱ������ʾ���
        /// </summary>
        public virtual IEnumerable GetSchema()
        {
            int saveMaxResult = this.MaxResult;
            this.MaxResult = 0;
            this.FirstResult = 0;
            IEnumerable ret = GetData(SearchExpression.False(), null);
            this.MaxResult = saveMaxResult;
            return ret;
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
        protected void Copy(AbstractSearchManager src, AbstractSearchManager dest)
        {
            dest.EnablePage = src.EnablePage;
            dest.FirstResult = src.FirstResult;
            dest.IsResultDistinct = src.IsResultDistinct;
            dest.MaxResult = src.MaxResult;
            dest.Name = src.Name;
            dest.AdditionalSearchExpression = src.AdditionalSearchExpression;
            dest.AdditionalSearchOrder = src.AdditionalSearchOrder;
            dest.m_searchHistoryInfos = src.m_searchHistoryInfos;

            dest.EagerFetchs.Clear();
            foreach (string s in src.EagerFetchs)
            {
                dest.EagerFetchs.Add(s);
            }

            dest.SearchControls.Clear();
            dest.SearchControls.AddRange(src.SearchControls);
        }

        private class DataLoadWorker : WorkerBase<DataLoadingEventArgs, object, DataLoadedEventArgs>
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="sm"></param>
            /// <param name="inputParams"></param>
            public DataLoadWorker(AbstractSearchManager sm, DataLoadingEventArgs inputParams)
                : base(inputParams)
            {
                m_sm = sm;
            }

            private AbstractSearchManager m_sm;
            protected override DataLoadedEventArgs DoWork(DataLoadingEventArgs inputParams)
            {
                //bool threadAborted = false;
                //try
                //{
                if (m_sm.UseStreamLoad)
                {
                    m_sm.GetDataCount(inputParams.SearchExpression, inputParams.SearchOrders, new Action<object>(delegate(object entity)
                        {
                            this.WorkerProgressSignal(entity);
                        }));
                    return new DataLoadedEventArgs(null, -1);
                }
                else
                {
                    m_sm.GetDataCount(inputParams.SearchExpression, inputParams.SearchOrders);
                    return new DataLoadedEventArgs(m_sm.Result, m_sm.Count);
                }

                //}
                //catch (ThreadAbortException)
                //{
                //    //threadAborted = true;
                //}
                //catch (Exception ex)
                //{
                    //if (ex.InnerException is ThreadAbortException)
                    //{
                    //    threadAborted = true;
                    //}
                    //else
                    //{
                    //    ExceptionProcess.ProcessWithNotify(ex);
                    //}
                //}
                //finally
                //{
                    //if (!threadAborted)
                    //{
                    //    this.asyncOperation.PostOperationCompleted(this.operationCompleted, new DataLoadedEventArgs(dataSource));
                    //}
                    //this.asyncOperation = null;

                    //m_loadingThread = null;

                    //m_isDataLoding = false;
                //}
                
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class SearchManagerExtention
    {
        /// <summary>
        /// ��������ѯ
        /// </summary>
        /// <param name="sm"></param>
        public static void LoadData(this ISearchManager sm)
        {
            sm.LoadData(null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sm"></param>
        /// <param name="searchExpression"></param>
        /// <param name="searchOrders"></param>
        public static void FillSearchAdditionals(this ISearchManager sm, ref ISearchExpression searchExpression, ref IList<ISearchOrder> searchOrders)
        {
            if (!string.IsNullOrEmpty(sm.AdditionalSearchExpression))
            {
                searchExpression = SearchExpression.And(searchExpression,
                    SearchExpression.Parse(sm.AdditionalSearchExpression));
            }
            if (!string.IsNullOrEmpty(sm.AdditionalSearchOrder))
            {
                if (searchOrders == null)
                {
                    searchOrders = new List<ISearchOrder>();
                }
                foreach (var i in SearchOrder.Parse(sm.AdditionalSearchOrder))
                {
                    searchOrders.Add(i);
                }
            }
        }
    }
}