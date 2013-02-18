using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Feng.Collections;

namespace Feng
{
    /// <summary>
    /// ���ݲ����������ӿڣ��������ݣ����ӣ�ɾ�����޸ģ���
    /// ���ڲ���DataTable���͵����ݡ���Not Supported Now��
    /// Ŀǰ֧����IList(T)���ݵ�IControlManager(T)��
    /// </summary>
    public interface IControlManager : IDisposable, IStateControl //, ICheckControl
    {
        /// <summary>
        /// ����
        /// </summary>
        string Name
        {
            get;
            set;
        }

        #region "Inner Controls"

        /// <summary>
        /// DisplayManager
        /// </summary>
        IDisplayManager DisplayManager { get; }

        /// <summary>
        /// ״̬�ؼ�����
        /// </summary>
        IStateControlCollection StateControls { get; }

        /// <summary>
        /// ����<see cref="ControlCheckException" />�Ľӿ�
        /// </summary>
        IControlCheckExceptionProcess ControlCheckExceptionProcess { get; }

        /// <summary>
        /// ���ؼ�����
        /// </summary>
        ICheckControlCollection CheckControls { get; }
        /// <summary>
        /// ���ؼ�ֵ
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        /// <exception cref="ControlCheckException">�ؼ�ֵ�����ϱ�׼</exception>
        bool CheckControlValue(IDataControl dc);

        #endregion

        #region "Operations"
        /// <summary>
        /// Data Access Layer
        /// </summary>
        IBaseDao Dao
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ����ڲ���
        /// </summary>
        bool InOperating
        {
            get;
        }

        /// <summary>
        /// ���б�ĩβ����µ�ʵ���ࡣ
        /// </summary>
        object AddNew();

        /// <summary>
        /// �༭��ǰλ��ʵ���ࡣ
        /// </summary>
        void EditCurrent();

        /// <summary>
        /// ɾ����ǰλ��ʵ����
        /// </summary>
        /// <returns>�Ƿ�ɹ�ɾ��</returns>
        bool DeleteCurrent();

        /// <summary>
        /// �����༭�����
        /// </summary>
        /// <returns></returns>
        bool EndEdit();

        /// <summary>
        /// �����༭�����
        /// </summary>
        /// <param name="commit">�Ƿ񱣴浽���ݿ�</param>
        bool EndEdit(bool commit);

        /// <summary>
        /// ȡ����ǰ�༭�����״̬
        /// </summary>
        void CancelEdit();

        /// <summary>
        /// ���浱ǰ����
        /// </summary>
        bool SaveCurrent();

        /// <summary>
        /// ����<see cref="ListChanged"/> �¼�
        /// </summary>
        /// <param name="e"></param>
        void OnListChanged(ListChangedEventArgs e);

       /// <summary>
        /// ����λ��Positionλ�õ�Entity�ı������飨���棬���½��棩
        /// </summary>
        /// <returns></returns>
        bool ProcessEntityChanged(int position);

        ///// <summary>
        ///// ʵ�������Ըı����
        ///// </summary>
        //event EventHandler<EntityChangedEventArgs> EntityChanged;

        /// <summary>
        /// �������б��б仯ʱ����
        /// </summary>
        event ListChangedEventHandler ListChanged;

        /// <summary>
        /// BeginningEdit event
        /// </summary>
        event EventHandler BeginningEdit;

        /// <summary>
        /// EditBegun event
        /// </summary>
        event EventHandler EditBegun;

        /// <summary>
        /// EndingEdit event
        /// </summary>
        event EventHandler EndingEdit;

        /// <summary>
        /// EditEnded event
        /// </summary>
        event EventHandler EditEnded;

        /// <summary>
        /// EditCanceled event
        /// </summary>
        event EventHandler EditCanceled;

        /// <summary>
        /// CancellingEdit
        /// </summary>
        event CancelEventHandler CancellingEdit;
        #endregion

        #region "State"

        /// <summary>
        /// ��ǰ״̬
        /// </summary>
        StateType State { get; set; }

        /// <summary>
        /// StateChanged
        /// </summary>
        event EventHandler StateChanged; 
        #endregion

        #region "Permission"

        /// <summary>
        /// �Ƿ�����ɾ��
        /// </summary>
        bool AllowDelete { get; set; }

        /// <summary>
        /// �Ƿ��������
        /// </summary>
        bool AllowInsert { get; set; }

        /// <summary>
        /// �Ƿ�����༭
        /// </summary>
        bool AllowEdit { get; set; }

        #endregion

        #region "Changed"
        /// <summary>
        /// �����Ƿ�ı�
        /// </summary>
        bool Changed { get; }

        /// <summary>
        /// ����CancelEdit�����Cancel�򷵻�true�����򷵻�false��
        /// </summary>
        /// <returns></returns>
        bool TryCancelEdit();

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        object Clone();

        #endregion
    }
}