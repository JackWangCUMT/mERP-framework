using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace Feng.Windows.Forms
{
    /// <summary>
    /// ����Button��ʾ״̬������Enable״̬��Visible״̬����ÿ��Button�ɵ������û�����"|"��"��~"�����������á�
    /// <p>
    /// <example>
    /// ֻ����"Add��Edit"
    /// <code>state = EnabledStateType.Add | EnabledStateType.Edit</code>
    /// ֻ������ "Cancel"
    /// <code>state = EnabledStateType.All �� ~EnabledStateType.Calcel</code>
    /// </example>
    /// ���⣬������4������״̬
    /// <list type="bullet">
    /// <item>None: ȫ������</item>
    /// <item>All: ȫ����</item>
    /// <item>Normal: "Add, Edit, Delete" ����, "OK, Cancel" ������</item>
    /// <item>Acting: "Add, Edit, Delete" ������, "OK, Cancel" ����</item>
    /// </list>
    /// </p>
    /// </summary>
    [Flags]
    public enum ShowStateType
    {
        /// <summary>
        /// ȫ������
        /// </summary>
        None = 0,
        /// <summary>
        /// ֻ����Add
        /// </summary>
        Add = 1,
        /// <summary>
        /// ֻ����Edit
        /// </summary>
        Edit = 2,
        /// <summary>
        /// ֻ����Delete
        /// </summary>
        Delete = 4,
        /// <summary>
        /// ֻ����OK
        /// </summary>
        Ok = 8,
        /// <summary>
        /// ֻ����Cancel
        /// </summary>
        Cancel = 16,
        /// <summary>
        /// ȫ����
        /// </summary>
        All = Add | Edit | Delete | Ok | Cancel,
        /// <summary>
        /// "Add, Edit, Delete" ����, "OK, Cancel" ������
        /// </summary>
        Normal = Add | Edit | Delete,
        /// <summary>
        /// "Add, Edit, Delete" ������, "OK, Cancel" ����
        /// </summary>
        Acting = Ok | Cancel,
    };

	/// <summary>
	/// һ��Button����������(Add)���޸�(Edit)��ɾ��(Delete)��ȷ��(OK)��ȡ��(Cancel)��
	/// </summary>
    [ToolboxItem(false)]
    [Obsolete()]
    [DefaultProperty("State")]
	public class ActionButtons : System.Windows.Forms.UserControl, IStateControl, IReadOnlyControl
	{
		#region "ActionButtons States"

		// ��button�������飬�������
		private ShowStateType[] m_stateTypes = 
			{ShowStateType.Add, ShowStateType.Edit, ShowStateType.Delete, ShowStateType.Cancel, ShowStateType.Ok};
        private FlowLayoutPanel flowLayoutPanel1;
        private MyButton btnOK;
        private MyButton btnCancel;
        private MyButton btnDelete;
        private MyButton btnEdit;
        private MyButton btnAdd;
		private MyButton[] m_buttons;

		/// <summary>
		/// ����Button Enable״̬
		/// </summary>
		public void SetEnabledState(ShowStateType state)
		{
			for (int i=0; i<m_buttons.Length; ++i)
			{
				if ((state & m_stateTypes[i]) > 0)
					m_buttons[i].Enabled = true;
				else
					m_buttons[i].Enabled = false;
			}
		}

        /// <summary>
        /// ����Button Enable״̬
        /// </summary>
        /// <param name="index"></param>
        /// <param name="enable"></param>
        public void SetEnabledState(int index, bool enable)
        {
            if (index < 0 || index >= 5)
            {
				throw new ArgumentException("index is beyond the Range", "index");
            }
            m_buttons[index].Enabled = enable;
        }

		/// <summary>
		/// ����Button Visible״̬
		/// </summary>
		public void SetVisibleState(ShowStateType state)
		{
			for (int i=0; i<m_buttons.Length; ++i)
			{
				if ((state & m_stateTypes[i]) > 0)
					m_buttons[i].Visible = true;
				else
					m_buttons[i].Visible = false;
			}

            OnResize(System.EventArgs.Empty);
		}

		/// <summary>
		/// See<see cref="IReadOnlyControl.ReadOnly"/>
		/// </summary>
		public bool ReadOnly
		{
			get { return false; }
            set
            {
                if (!value)
                {
                    if (ReadOnlyChanged != null)
                    {
                        ReadOnlyChanged(this, System.EventArgs.Empty);
                    }
                }
            }
		}

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler ReadOnlyChanged;

		/// <summary>
		/// ActionButtons����״̬
		/// </summary>
		public void SetState(StateType state)
		{
			switch (state)
            {
                case StateType.None:
                    SetEnabledState(ShowStateType.Add);
                    break;
                case StateType.View:
					SetEnabledState(ShowStateType.Normal);
                    break;
                case StateType.Add:
					SetEnabledState(ShowStateType.Acting);
                    break;
                case StateType.Edit:
					SetEnabledState(ShowStateType.Acting);
                    break;
                default:
                    throw new NotSupportedException("Invalid State");
            }
		}

		#endregion

		#region Component Designer generated code

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Constructor
		/// </summary>
		public ActionButtons()
		{
			InitializeComponent();

			m_buttons = new MyButton[] { btnAdd, btnEdit, btnDelete, btnCancel, btnOK};

        }


		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
        }
		private void InitializeComponent()
		{
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new Feng.Windows.Forms.MyButton();
            this.btnEdit = new Feng.Windows.Forms.MyButton();
            this.btnDelete = new Feng.Windows.Forms.MyButton();
            this.btnCancel = new Feng.Windows.Forms.MyButton();
            this.btnOK = new Feng.Windows.Forms.MyButton();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnAdd);
            this.flowLayoutPanel1.Controls.Add(this.btnEdit);
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnOK);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(373, 25);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(72, 21);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "����";
            this.btnAdd.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(75, 0);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(72, 21);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.Text = "�޸�";
            this.btnEdit.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(150, 0);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(72, 21);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "ɾ��";
            this.btnDelete.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(225, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 21);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "ȡ��";
            this.btnCancel.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(300, 0);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 21);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "ȷ��";
            this.btnOK.Click += new System.EventHandler(this.Button_Click);
            // 
            // ActionButtons
            // 
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ActionButtons";
            this.Size = new System.Drawing.Size(377, 24);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region "Events"

		private bool m_showWarning = true;

		/// <summary>
		/// �Ƿ���ɾ����ȡ��ʱ��ʾ��ʾ��Ϣ
		/// </summary>
		[Description("�Ƿ���ɾ����ȡ��ʱ��ʾ��ʾ��Ϣ")]
		[DefaultValue(true)]
		public bool ShowWarning
		{
			get { return m_showWarning; }
			set { m_showWarning = value; }
		}
	
		/// <summary>
		/// Event for Add Button's Click
		/// </summary>
		public event EventHandler AddClick;
		/// <summary>
		/// Event for Edit Button's Click
		/// </summary>
		public event EventHandler EditClick;
		/// <summary>
		/// Event for Delete Button's Click
		/// </summary>
		public event EventHandler DeleteClick;
		/// <summary>
		/// Event for OK Button's Click
		/// </summary>
		public event EventHandler OkClick;
		/// <summary>
		/// Event for Cancel Button's Click
		/// </summary>
		public event EventHandler CancelClick;

		// ��Button Clickת��Ϊ��ӦEvent
		private void Button_Click (object sender, System.EventArgs e)
		{
			if (sender == btnAdd && AddClick != null)
				AddClick(sender, e);
			else if (sender == btnEdit && EditClick != null)
				EditClick(sender, e);
			else if (sender == btnDelete && DeleteClick != null)
			{
				// ɾ����¼��������ȷ��
                if (!m_showWarning || MessageForm.ShowYesNo("��������" + btnDelete.Text + "��¼", btnDelete.Text + "��¼"))
				{
					DeleteClick(sender, e);
				}
			}
			else if (sender == btnOK && OkClick != null)
			{
				OkClick(sender, e);
			}
			else if (sender == btnCancel && CancelClick != null)
			{
                if (!m_showWarning || MessageForm.ShowYesNo("������ֹ����д�����ݲ������棬��ȷ��", "ȡ��"))
				{
					CancelClick(sender, e);
				}
			}
		}
		#endregion

		#region "Button's Text and Visiblity"
		///// <summary>
        ///// ��ActionButtons��С�ı�ʱ���ı����Buttons��λ�ã��Ⱦ�����
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnResize(System.EventArgs e)
        //{
        //    int x = this.Size.Width / 5;
        //    int n = 0;
        //    btnAdd.Location = new Point(x * n, btnAdd.Location.Y);
        //    if (btnAdd.Visible) n++;

        //    btnEdit.Location = new Point(x * n, btnEdit.Location.Y);
        //    if (btnEdit.Visible) n++;

        //    btnDelete.Location = new Point(x * n, btnDelete.Location.Y);
        //    if (btnDelete.Visible) n++;

        //    btnCancel.Location = new Point(x * n, btnCancel.Location.Y);
        //    if (btnCancel.Visible) n++;

        //    btnOK.Location = new Point(x * n, btnOK.Location.Y);
        //    if (btnOK.Visible) n++;

        //    base.OnResize(e);
        //}

        /// <summary>
        /// ����Button's Text
        /// </summary>
        /// <param name="buttonsText"></param>
        public void SetButtonsText(string[] buttonsText)
        {
            for (int i = 0; i < buttonsText.Length; ++i)
            {
                if (!string.IsNullOrEmpty(buttonsText[i]))
                {
                    m_buttons[i].Text = buttonsText[i];
                }
            }
        }

        /// <summary>
        /// ����Button's Text
        /// </summary>
        /// <param name="index"></param>
        /// <param name="buttonText"></param>
        public void SetButtonText(int index, string buttonText)
        {
            if (index < 0 || index >= 5)
            {
                throw new ArgumentException("index is beyond the Range", "index");
            }
            m_buttons[index].Text = buttonText;
        }

        /// <summary>
        /// ����Button's Text
        /// </summary>
        public void ResetButtonTexts()
        {
            string[] s = new string[] {"���", "�޸�", "ɾ��", "ȡ��", "ȷ��"};
            for (int i=0; i<5; ++i)
            {
                m_buttons[i].Text = s[i];
            }
		}
		#endregion
	}
}


