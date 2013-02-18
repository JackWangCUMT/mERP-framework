using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Feng.Windows.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ControlManager : AbstractControlManager, IWindowControlManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void  Dispose(bool disposing)
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
        public ControlManager(ISearchManager sm)
            : base(new DisplayManager(sm))
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dm"></param>
        protected ControlManager(IDisplayManager dm)
            : base(dm)
        {
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            ControlManager cm = new ControlManager(this.DisplayManager.Clone() as IDisplayManager);
            Copy(this, cm);
            return cm;
        }

        #region "Operation"
        /// <summary>
        /// �Ƿ��ܱ䶯Position�����м�¼�иĶ��������Ҫѯ�ʣ�
        /// </summary>
        /// <returns></returns>
        public override bool TryCancelEdit()
        {
            if (base.InOperating)
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
            }
            return true;
        }

        /// <summary>
        /// ������Entity
        /// </summary>
        /// <returns></returns>
        protected virtual object AddNewItem()
        {
            System.Data.DataView dv = this.DisplayManager.Items as System.Data.DataView;
            if (dv != null)
            {
                //System.Data.DataRow row = dv.Table.NewRow();
                //dv.Table.Rows.Add(row);
                //return row;

                System.Data.DataRowView rowView = dv.AddNew();
                rowView.EndEdit();
                return rowView;
            }
            return null;
        }

        /// <summary>
        /// ����������Դ
        /// </summary>
        /// <returns></returns>
        protected virtual IList NewList()
        {
            return this.DisplayManager.SearchManager.GetSchema() as IList;
        }

        private int m_beforeAddPos = -1;

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

            // before load, m_item is null
            if (this.DisplayManager.Items == null)
            {
                IList newList = NewList();

                // not to set BindingControls
                if (this.DisplayManager.BindingControls.Count > 0)
                {
                    IBindingControl[] bcs = new IBindingControl[this.DisplayManager.BindingControls.Count];
                    this.DisplayManager.BindingControls.CopyTo(bcs, 0);
                    this.DisplayManager.BindingControls.Clear();

                    this.DisplayManager.SetDataBinding(newList, string.Empty);

                    this.DisplayManager.BindingControls.AddRange(bcs);
                }
                else
                {
                    this.DisplayManager.SetDataBinding(newList, string.Empty);
                }
            }

            object entity = AddNewItem();

            m_beforeAddPos = this.DisplayManager.Position;
            this.DisplayManager.Position = this.DisplayManager.Items.Count - 1;

            State = StateType.Add;

            return entity;
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

            if (this.DisplayManager.Position == -1)
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

            int oldPos = this.DisplayManager.Position;
            int newPos = oldPos;
            if (newPos < 0)
            {
                newPos = 0;
            }
            if (newPos >= this.DisplayManager.Count - 1)
            {
                newPos = this.DisplayManager.Count - 2; // ���ﻹûɾ����Count����ԭ��Ŀ
            }

            if (ProcessCurrentEntityChanged())
            {
                this.DisplayManager.Items.RemoveAt(oldPos);
                this.DisplayManager.Position = newPos;
            }

            if (this.State == StateType.Delete)
            {
                State = StateType.View;
            }
            // Remove ��һ�������PositionChanged�¼���Remove Index 0 Item��
            this.DisplayManager.OnPositionChanged(System.EventArgs.Empty);

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
                State = StateType.View;
            }

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
            if (base.State == StateType.Add)
            {
                this.DisplayManager.Items.RemoveAt(this.DisplayManager.Items.Count - 1);

                State = StateType.View;

                // maybe count = 0
                //System.Diagnostics.Debug.Assert(m_beforeAddPos != -1, "Before Add Old Pos should not be -1!");

                this.DisplayManager.Position = m_beforeAddPos;
                //this.DisplayManager.Position = (this.DisplayManager.Items.Count == 0 ? -1 : this.DisplayManager.Items.Count - 1);
            }

            State = StateType.View;

            // CancelEdit��һ�������PositionChanged�¼�������Edit CancelEdit
            this.DisplayManager.OnPositionChanged(System.EventArgs.Empty);

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
        /// <param name="fullPropertyName"></param>
        public void RemoveValidation(string dataControlName)
        {
            if (m_helper == null)
            {
                m_helper = new ValidationHelper(this);
            }
            m_helper.RemoveValidation(dataControlName);
        }

        internal static bool CheckControlValue(IControlManager cm, ValidationHelper helper, IDataControl dc)
        {
            bool ret = true;
            if (!dc.ReadOnly)
            {
                string errMsg = helper.ValidateControl(dc.Name);
                cm.ControlCheckExceptionProcess.ShowError(dc, errMsg);

                if (!string.IsNullOrEmpty(errMsg))
                {
                    ret = false;
                }
            }
            return ret;
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
                return CheckControlValue(this, m_helper, dc);
            }
            else
            {
                return base.CheckControlValue(dc);
            }
        }

        #endregion

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