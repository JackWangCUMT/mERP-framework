using System;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Feng.Utils;

namespace Feng.Windows.Forms
{
    /// <summary>
    /// �쿴ͼƬ�Ŀؼ�
    /// </summary>
    public class MyPictureBox : System.Windows.Forms.PictureBox, IDataValueControl
    {
        #region Windows ������������ɵĴ���

        /// <summary>
        /// Constructor
        /// </summary>
        public MyPictureBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemModify;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemClear;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpen;

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

        /// <summary>
        /// �����֧������ķ��� - ��Ҫ
        /// ʹ�ô���༭���޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemModify = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                                                  {
                                                      this.toolStripMenuItemModify,
                                                      this.toolStripMenuItemClear,
                                                      this.toolStripMenuItemSave,
                                                      this.toolStripMenuItemOpen
                                                  });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(95, 70);
            this.contextMenuStrip1.Opening +=
                new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItemModify
            // 
            this.toolStripMenuItemModify.Name = "toolStripMenuItemModify";
            this.toolStripMenuItemModify.Size = new System.Drawing.Size(94, 22);
            this.toolStripMenuItemModify.Text = "�޸�";
            this.toolStripMenuItemModify.Click += new System.EventHandler(this.toolStripMenuItemModify_Click);
            // 
            // toolStripMenuItemClear
            // 
            this.toolStripMenuItemClear.Name = "toolStripMenuItemClear";
            this.toolStripMenuItemClear.Size = new System.Drawing.Size(94, 22);
            this.toolStripMenuItemClear.Text = "���";
            this.toolStripMenuItemClear.Click += new System.EventHandler(this.toolStripMenuItemClear_Click);
            // 
            // toolStripMenuItemSave
            // 
            this.toolStripMenuItemSave.Name = "toolStripMenuItemSave";
            this.toolStripMenuItemSave.Size = new System.Drawing.Size(94, 22);
            this.toolStripMenuItemSave.Text = "����";
            this.toolStripMenuItemSave.Click += new System.EventHandler(this.toolStripMenuItemSave_Click);
            // 
            // toolStripMenuItemOpen
            // 
            this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            this.toolStripMenuItemOpen.Size = new System.Drawing.Size(94, 22);
            this.toolStripMenuItemOpen.Text = "��";
            this.toolStripMenuItemOpen.Click += new System.EventHandler(this.toolStripMenuItemOpen_Click);
            // 
            // DataPictureBox
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Size = new System.Drawing.Size(120, 42);
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.DoubleClick += new System.EventHandler(this.DataPictureBox_DoubleClick);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        #region "IDataValueControl"

        /// <summary>
        /// PictureBox.Picture, byte[] ����
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedDataValue
        {
            get { return m_data; }
            set
            {
                if (value == null)
                {
                    m_data = null;
                    this.Image = null;
                }
                else
                {
                    try
                    {
                        byte[] data = value as byte[];
                        MemoryStream ms = new MemoryStream(data, 0, data.Length);

                        try
                        {
                            Image im = Image.FromStream(ms);
                            this.Image = im;
                            m_data = data;
                        }
                        catch (Exception)
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException(
                            "MyPicture's SelectedDataValue must be valid picture stream(byte[])", ex);
                    }
                }
            }
        }

        #endregion

        #region "IStateControl"

        private bool m_bReadOnly;
        /// <summary>
        /// ReadOnly
        /// </summary>
        public bool ReadOnly
        {
            get { return m_bReadOnly; }
            set
            {
                if (m_bReadOnly != !value)
                {
                    m_bReadOnly = !value;
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
        /// ����ʾ�ؼ�����State
        /// </summary>
        public void SetState(StateType state)
        {
            StateControlHelper.SetState(this, state);
        }

        #endregion

        #region "Operations"

        private byte[] m_data;

        private void toolStripMenuItemClear_Click(object sender, EventArgs e)
        {
            if (!toolStripMenuItemClear.Enabled)
            {
                return;
            }

            SelectedDataValue = null;
        }

        private void toolStripMenuItemModify_Click(object sender, EventArgs e)
        {
            if (!toolStripMenuItemModify.Enabled)
            {
                return;
            }

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.RestoreDirectory = true;
            //ofd.InitialDirectory = "c:\\";
            openFileDialog1.Filter =
                "����ͼƬ�ļ�|*.BMP;*.JPG;*.JPEG;*.JPE;*JEIF|JPEG (*.JPG;*.JPEG;*.JPE;*JEIF)|*.JPG;*.JPEG;*.JPE;*JEIF";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream myStream;
                if ((myStream = openFileDialog1.OpenFile()) != null)
                {
                    FileInfo fi = new FileInfo(openFileDialog1.FileName);
                    int len = Convert.ToInt32(fi.Length);
                    byte[] data = new byte[len];
                    myStream.Read(data, 0, len);
                    myStream.Close();

                    SelectedDataValue = data;
                }
            }
            openFileDialog1.Dispose();
        }

        private void toolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            if (this.Image != null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.Filter = "JPEG |*.JPG|BMP |*.BMP";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.Title = "����ͼ��";
                saveFileDialog1.ShowDialog();

                if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    System.IO.FileStream fs = (System.IO.FileStream) saveFileDialog1.OpenFile();
                    switch (saveFileDialog1.FilterIndex)
                    {
                        case 1:
                            this.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;

                        case 2:
                            this.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                    }

                    fs.Close();
                }
                saveFileDialog1.Dispose();
            }
        }

        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            if (this.Image != null)
            {
                string fileName = Path.GetTempPath() + "temp1.jpg";
                System.IO.FileStream fs = new FileStream(fileName, FileMode.Create);
                this.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                fs.Close();
                Feng.Windows.Utils.ProcessHelper.ExecuteApplication(fileName);
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            toolStripMenuItemModify.Enabled = !m_bReadOnly;
            toolStripMenuItemClear.Enabled = !m_bReadOnly;

            toolStripMenuItemSave.Enabled = (this.Image != null);
            toolStripMenuItemOpen.Enabled = (this.Image != null);
        }

        private void DataPictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (!m_bReadOnly)
            {
                toolStripMenuItemModify_Click(toolStripMenuItemModify, System.EventArgs.Empty);
            }
            else
            {
                toolStripMenuItemOpen_Click(toolStripMenuItemOpen, System.EventArgs.Empty);
            }
        }

        #endregion
    }
}