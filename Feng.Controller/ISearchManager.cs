using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Feng
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataLoader
    {
        /// <summary>
        /// ����
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// ����Ƿ�ΪΨһ
        /// </summary>
        bool IsResultDistinct
        {
            get;
            set;
        }

        /// <summary>
        /// ���ݲ�ѯ������ѯ����
        /// </summary>
        /// <param name="searchExpression"></param>
        /// <param name="searchOrders"></param>
        /// <returns></returns>
        System.Collections.IEnumerable GetData(ISearchExpression searchExpression, IList<ISearchOrder> searchOrders);

        /// <summary>
        /// ���ݲ�ѯ������ѯ��������
        /// </summary>
        /// <param name="searchExpression"></param>
        /// <returns></returns>
        int GetCount(ISearchExpression searchExpression);

        /// <summary>
        /// �������
        /// </summary>
        IList<string> EagerFetchs
        {
            get;
        }

        /// <summary>
        /// �Ƿ��÷�ҳ��ȡ
        /// </summary>
        bool EnablePage { get; set; }

        /// <summary>
        /// ��ѯ�������
        /// </summary>
        int FirstResult { get; set; }

        /// <summary>
        /// ��ѯ����������
        /// </summary>
        int MaxResult { get; set; }
    }

    /// <summary>
    /// ��ѯ������
    /// </summary>
    public interface ISearchManager : IDataLoader, IDisposable
    {
        /// <summary>
        /// ��ʾ������
        /// </summary>
        IDisplayManager DisplayManager
        {
            get;
            set;
        }

        /// <summary>
        /// ���ݸ�����ѯ��������������䵽DisplayManager��
        /// </summary>
        /// <param name="searchExpression"></param>
        /// <param name="searchOrders"></param>
        void LoadData(ISearchExpression searchExpression, IList<ISearchOrder> searchOrders);

        /// <summary>
        /// ֹͣ��ȡ���ݡ����δ��ʼ��ȡ������false��
        /// </summary>
        bool StopLoadData();

        /// <summary>
        /// �ȴ�ֱ�������̷߳��ء����δ��ʼ��ȡ������false��
        /// </summary>
        bool WaitLoadData();

        /// <summary>
        /// ���¶�ȡ���ݣ���ѯ�������䣩
        /// ���ԭ���޲�ѯ�����������<see cref="LoadData"/>
        /// </summary>
        void ReloadData();

        /// <summary>
        /// ���¶���ĳһ��
        /// </summary>
        void ReloadItem(int idx);

        /// <summary>
        /// ��ѯ���
        /// ��ΪIList[IEntity] Or DataView, DataTable, DataSet Or 
        /// </summary>
        System.Collections.IEnumerable Result { get; set; }

        /// <summary>
        /// ��ѯ�������
        /// </summary>
        int Count { get; set; }


        /// <summary>
        /// 
        /// </summary>
        bool UseStreamLoad { get; set; }
        

        /// <summary>
        /// ������ŵõ���ѯ��ʷ
        /// </summary>
        /// <param name="idx"></param>
        /// <return></return>
        SearchHistoryInfo GetHistory(int idx);

        /// <summary>
        /// ���ò�ѯ��ʷ
        /// </summary>
        /// <param name="searchExpression"></param>
        /// <param name="searchOrders"></param>
        SearchHistoryInfo SetHistory(ISearchExpression searchExpression, IList<ISearchOrder> searchOrders);

        /// <summary>
        /// ���ݲ�ѯǰ����
        /// </summary>
        event EventHandler<DataLoadingEventArgs> DataLoading;

        /// <summary>
        /// ���ݲ�ѯ����
        /// </summary>
        event EventHandler<DataLoadedEventArgs> DataLoaded;

        /// <summary>
        /// ����<see cref="DataLoading"/>�¼�
        /// </summary>
        void OnDataLoading(DataLoadingEventArgs e);

        /// <summary>
        /// ����<see cref="DataLoaded"/>�¼�
        /// </summary>
        void OnDataLoaded(DataLoadedEventArgs e);

        /// <summary>
        /// ����Entity Schema��ʹGrid����������ʱ������ʾ���
        /// </summary>
        IEnumerable GetSchema();

        /// <summary>
        /// AdditionalSearchExpression
        /// </summary>
        string AdditionalSearchExpression
        {
            get;
            set;
        }

        /// <summary>
        /// AdditionalSearchOrder
        /// </summary>
        string AdditionalSearchOrder
        {
            get;
            set;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        object Clone();

        /// <summary>
        /// ��ѯ�ؼ�����
        /// </summary>
        ISearchControlCollection SearchControls { get; }

        /// <summary>
        /// �Զ����ɲ�ѯ�����������ݲ�ѯ��������������䵽DisplayManager��
        /// �����䵽<see cref="Result"/>��<see cref="Count"/>
        /// </summary>
        void LoadDataAccordSearchControls();
    }

    /// <summary>
    /// 
    /// </summary>
    public class DataLoadedEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="count"></param>
        public DataLoadedEventArgs(object dataSource, int count)
        {
            this.DataSource = dataSource;
            this.Count = count;
        }

        /// <summary>
        /// 
        /// </summary>
        public object DataSource
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get;
            private set;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DataLoadingEventArgs : System.ComponentModel.CancelEventArgs
    {
         /// <summary>
        /// 
        /// </summary>
        /// <param name="se"></param>
        /// <param name="so"></param>
        public DataLoadingEventArgs(ISearchExpression se, IList<ISearchOrder> so)
        {
            this.SearchExpression = se;
            this.SearchOrders = so;
        }

        /// <summary>
        /// 
        /// </summary>
        public ISearchExpression SearchExpression
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public IList<ISearchOrder> SearchOrders
        {
            get;
            set;
        }
    }

}
