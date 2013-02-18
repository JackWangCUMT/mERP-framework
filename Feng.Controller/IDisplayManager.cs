using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Feng.Collections;

namespace Feng
{
	/// <summary>
    /// ��ʾ�������ӿ�
	/// </summary>
	public interface IDisplayManager : IDisposable, IEntityList
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
        /// ���ݿؼ�����
        /// </summary>
        IDataControlCollection DataControls
        {
            get;
        }

		/// <summary>
        /// �󶨿ռ伯��
		/// </summary>
        IBindingControlCollection BindingControls
		{
			get;
		}

        /// <summary>
        /// ��ѯ������
        /// </summary>
        ISearchManager SearchManager
        {
            get;
        }

        ///// <summary>
        ///// ����Դ
        ///// </summary>
        //object DataSource
        //{
        //    get;
        //}

        ///// <summary>
        ///// ���ݳ�Ա
        ///// </summary>
        //string DataMember
        //{
        //    get;
        //}

		/// <summary>
		/// ������Դ��
		/// </summary>
		/// <param name="dataSource"></param>
		/// <param name="dataMember"></param>
		void SetDataBinding(object dataSource, string dataMember);

        /// <summary>
        /// ����һ��������
        /// </summary>
        /// <param name="dataItem"></param>
        void AddDataItem(object dataItem);

        /// <summary>
        /// ��ǰλ��
        /// </summary>
        int Position
        {
            get;
            set;
        }

        /// <summary>
        /// ����PositionChanged<see cref="PositionChanged"/>�¼�
        /// </summary>
        void OnPositionChanged(System.EventArgs e);

        /// <summary>
        /// <see cref="Position"/>�ı�ǰ����
        /// </summary>
        event CancelEventHandler PositionChanging;

        /// <summary>
        /// <see cref="Position"/>�ı����
        /// </summary>
        event EventHandler PositionChanged;

        /// <summary>
        /// ��ʾ��ǰλ��ʵ��������
        /// </summary>
        void DisplayCurrent();

        

        ///// <summary>
        ///// ������Ż��ʵ����
        ///// </summary>
        ///// <param name="idx"></param>
        ///// <returns></returns>
        //object GetItem(int idx);

        /// <summary>
        /// ʵ������Ϣ
        /// </summary>
        IEntityMetadata EntityInfo
        {
            get;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        object Clone();

        /// <summary>
        /// SelectedDataValueChanged event
        /// </summary>
        event EventHandler<SelectedDataValueChangedEventArgs> SelectedDataValueChanged;

        /// <summary>
        /// ����<see cref="SelectedDataValueChanged"/> �¼�
        /// </summary>
        /// <param name="e"></param>
        void OnSelectedDataValueChanged(SelectedDataValueChangedEventArgs e);

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        void BeginBatchOperation();

        /// <summary>
        /// ������������
        /// </summary>
        void EndBatchOperation();

        /// <summary>
        /// �Ƿ�������������
        /// </summary>
        /// <returns></returns>
        bool InBatchOperation
        {
            get;
        }
	}
}
