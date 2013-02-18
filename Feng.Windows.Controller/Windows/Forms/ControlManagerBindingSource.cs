using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Feng.Windows.Forms
{
    /// <summary>
    /// ControlManagerBindingSource for DataView
    /// </summary>
    public class ControlManagerBindingSource : AbstractControlManager, IWindowControlManager
    {
        #region "Constructor"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_helper != null)
                {
                    m_helper.Dispose();
                    m_helper = null;
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sm"></param>
        public ControlManagerBindingSource(ISearchManager sm)
            : base(new DisplayManagerBindingSource(sm))
        {
            m_bs = (this.DisplayManager as DisplayManagerBindingSource).BindingSource;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dm"></param>
        protected ControlManagerBindingSource(IDisplayManager dm)
            : base(dm)
        {
            m_bs = (this.DisplayManager as DisplayManagerBindingSource).BindingSource;
        }
        private BindingSource m_bs;
        #endregion

        # region "Operations"
        ///// <summary>
        ///// ˢ�µ�ǰ��,���ڶ�Ӧ�ؼ���ʾ,�����ˢ���������ֵ
        ///// </summary>
        //public void RefreshCurrentRow()
        //{
        //    if (CurrentPosition != -1)
        //    {
        //        RefreshRow(CurrentPosition);
        //        State = State;
        //        ShowCurrentRow();
        //    }
        //}

        ///// <summary>
        ///// ˢ�µ�ǰ��,���ڶ�Ӧ�ؼ���ʾ,�����ˢ���������ֵ
        ///// </summary>
        ///// <param name="position">λ��</param>
        //private void RefreshRow(int position)
        //{
        //    T entity = (m_bs.DataSource as IList<T>)[position];
        //    object id = m_entityType.InvokeMember(m_primaryKeyName, BindingFlags.GetProperty, null, entity, null, null, null, null);
        //    entity = Repository.Get<T>(id);
        //    (m_bs.DataSource as IList<T>)[position] = entity;
        //}
        /// <summary>
        /// �Ƿ��ܱ䶯Position�����м�¼�иĶ��������Ҫѯ�ʣ�
        /// </summary>
        /// <returns></returns>
        public override bool TryCancelEdit()
        {
            if (this.State == StateType.Add)
            {
                if (ServiceProvider.GetService<IMessageBox>().ShowYesNoDefaultNo("����ӵ����ݽ��������棬�Ƿ������", "ȷ��"))
                {
                    this.CancelEdit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (this.State == StateType.Edit)
            {
                // �Ѿ��������û�ɾ��
                if (this.DisplayManager.CurrentItem == null)
                {
                    this.CancelEdit();
                    return true;
                }

                if (this.Changed)
                {
                    if (ServiceProvider.GetService<IMessageBox>().ShowYesNoDefaultNo("���޸ĵ����ݽ��������棬�Ƿ������", "ȷ��"))
                    {
                        this.CancelEdit();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    this.CancelEdit();
                    return true;
                }
            }
            return true;
        }

        /// <summary>
        /// ���б�ĩβ����µ�ʵ���ࡣ
        /// �統ǰ״̬ΪStateType.Add��StateType.Edit����ֱ�ӷ��أ�������¼�¼
        /// ״̬����Ϊ<see cref="StateType.Add"/>��
        /// ��ǰλ������Ϊ����ӵ�ʵ����λ�ã�
        /// ����ת�Ƶ���һ����������ݵĵ����ݿؼ�
        /// </summary>
        protected override object DoAddNew()
        {
            if (State == StateType.Add || State == StateType.Edit)
            {
                return null;
            }

            State = StateType.Add;

            return m_bs.AddNew();

            //foreach (IDataManager dm in this.DisplayManager.DataManagers)
            //{
            //    if (dm != this.DisplayManager.DataManagers)
            //    {
            //        dm.Item = dm.CreateNew();
            //    }
            //}

            //this.DisplayManager.DataControls.FocusFirstInsertableControl();
        }

        /// <summary>
        /// �༭��ǰλ��ʵ���ࡣ
        /// �統ǰ״̬ΪStateType.Add��StateType.Edit����ֱ�ӷ��أ����޸ļ�¼
        /// ״̬����Ϊ<see cref="StateType.Edit"/>��
        /// ����ת�Ƶ���һ���ɱ༭�����ݿؼ�
        /// </summary>
        protected override void DoEditCurrent()
        {
            if (State == StateType.Add || State == StateType.Edit)
            {
                return;
            }

            State = StateType.Edit;

            this.DisplayManager.DataControls.FocusFirstEditableControl();
        }

        /// <summary>
        /// ɾ����ǰλ��ʵ���ࡣ
        /// �統ǰ״̬ΪStateType.Add��StateType.Edit����ֱ�ӷ��أ���ɾ����¼
        /// ״̬Ϊ<see cref=" StateType.View"/>
        /// ��������ʾ��ǰʵ��������
        /// </summary>
        /// <exception cref="InvalidOperationException">��ǰʵ����Ϊ��</exception>
        protected override bool DoDeleteCurrent()
        {
            if (State == StateType.Add || State == StateType.Edit)
            {
                return false;
            }

            if (this.DisplayManager.CurrentItem == null)
            {
                throw new InvalidOperationException("Current is null");
            }

            State = StateType.Delete;
            if (ProcessCurrentEntityChanged())
            {
                m_bs.RemoveCurrent();
            }

            // Remove ��һ�������PositionChanged�¼���Remove Index 0 Item��
            State = StateType.View;
            this.DisplayManager.DisplayCurrent();

            return true;
        }

        /// <summary>
        /// �����༭��
        /// ���ؼ�ֵ�ĺ�����, ����ؼ�ֵ��ʵ���ࡣ
        /// ���δͨ�������Լ�飬����false�����򷵻�true
        /// ����ɹ����棬״̬ת��Ϊ<see cref="StateType.View"/>��
        /// ͬʱ�������
        /// </summary>
        protected override bool DoEndEdit()
        {
            if (ProcessCurrentEntityChanged())
            {
                m_bs.EndEdit();

                State = StateType.View;
            }

            // �Ѿ���DoChangeCurrentEntity()������ListChanged�¼�
            //DisplayCurrent();

            if (base.ControlCheckExceptionProcess != null)
            {
                base.ControlCheckExceptionProcess.ClearAllError();
            }

            return true;
        }

        /// <summary>
        /// ȡ����ǰ�༭�����״̬��
        /// ״̬ת��Ϊ<see cref="StateType.View"/>��
        /// ͬʱ�������
        /// </summary>
        protected override void DoCancelEdit()
        {
            m_bs.CancelEdit();

            // CancelEdit��һ�������PositionChanged�¼�������Edit CancelEdit
            State = StateType.View;
            this.DisplayManager.DisplayCurrent();

            if (base.ControlCheckExceptionProcess != null)
            {
                base.ControlCheckExceptionProcess.ClearAllError();
            }
        }

        #endregion

        #region "Validation"

        private ValidationHelper m_helper;

        /// <summary>
        /// AddValidationExpression
        /// </summary>
        /// <param name="dataControlName"></param>
        /// <param name="expression"></param>
        public void SetValidation(string dataControlName, Xceed.Validation.ValidationExpression expression)
        {
            if (m_helper == null)
            {
                m_helper = new ValidationHelper(this);
            }
            m_helper.SetValidation(dataControlName, expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataControlName"></param>
        public void RemoveValidation(string dataControlName)
        {
            if (m_helper == null)
            {
                m_helper = new ValidationHelper(this);
            }
            m_helper.RemoveValidation(dataControlName);
        }

        /// <summary>
        /// ���ؼ�ֵ�Ƿ���ϱ�׼
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ControlCheckException">�ؼ�ֵ�����ϱ�׼</exception>
        public override bool CheckControlValue(IDataControl dc)
        {
            if (m_helper != null)
            {
                return ControlManager.CheckControlValue(this, m_helper, dc);
            }
            else
            {
                return base.CheckControlValue(dc);
            }
        }

        #endregion

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            ControlManagerBindingSource cm = new ControlManagerBindingSource(this.DisplayManager.Clone() as IDisplayManager);
            Copy(this, cm);
            return cm;
        }

        private static IBaseDao s_defaultDao;
        protected override void OnEntityChanged(EntityChangedEventArgs e)
        {
            if (this.Dao == null)
            {
                if (s_defaultDao == null)
                {
                    s_defaultDao = new Feng.NH.NHibernateDao<IEntity>(null);
                }
                this.Dao = s_defaultDao;
            }
            base.OnEntityChanged(e);
        }
    }
}