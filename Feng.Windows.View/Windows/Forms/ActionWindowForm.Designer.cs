namespace Feng.Windows.Forms
{
    partial class ActionWindowForm
    {
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���

        /// <summary>
        /// �����֧������ķ��� - ��Ҫ
        /// ʹ�ô���༭���޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.actionButtons1 = new Feng.Windows.Forms.ActionButtons();
            this.SuspendLayout();
            // 
            // actionButtons1
            // 
            this.actionButtons1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.actionButtons1.Location = new System.Drawing.Point(228, 577);
            this.actionButtons1.Name = "actionButtons1";
            this.actionButtons1.ReadOnly = false;
            this.actionButtons1.Size = new System.Drawing.Size(389, 24);
            this.actionButtons1.TabIndex = 0;
            // 
            // ActionWindowForm
            // 
            this.ClientSize = new System.Drawing.Size(850, 613);
            this.Controls.Add(this.actionButtons1);
            this.Name = "ActionWindowForm";
            this.Text = "ActionWindowForm";
            this.ResumeLayout(false);

        }

        #endregion

		private Feng.Windows.Forms.ActionButtons actionButtons1;
	}
}
