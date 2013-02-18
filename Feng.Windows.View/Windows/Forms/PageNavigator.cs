using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using Feng.Windows.Utils;

namespace Feng.Windows.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public class PageNavigator : BindingNavigator, IProfileLayoutControl
    {
        /// <summary> 
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }

                //this.WireUpTextBox(ref this.pageCountItem, null, new KeyEventHandler(this.OnPositionKey), new EventHandler(this.OnPositionLostFocus));
                this.PageCountItem = null;
                this.PositionItem = null;

                this.BindingSource = null;
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 
        /// </summary>
        public PageNavigator() :
            base()
        {
            this.components = new System.ComponentModel.Container();
            this.����SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            // 
            // ����SToolStripMenuItem
            // 
            this.����SToolStripMenuItem.Name = "����SToolStripMenuItem";
            this.����SToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.����SToolStripMenuItem.Text = "����(&S)";
            this.����SToolStripMenuItem.Click += new System.EventHandler(this.����SToolStripMenuItem_Click);

            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                                                  {
                                                      this.����SToolStripMenuItem
                                                  });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 114);

            this.ContextMenuStrip = contextMenuStrip1;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void AddStandardItems()
        {
            base.AddStandardItems();

            this.AllCountItem = new ToolStripLabel();
            this.AllCountItem.Name = "bindingNavigatorAllCountItem";
            this.allCountItem.Text = "��0��/ÿҳ";
            this.AllCountItem.ToolTipText = "������";
            this.AllCountItem.AutoToolTip = false;
            this.Items.Add(AllCountItem);

            this.pageCountItem = new ToolStripTextBox();
            

            this.pageCountItem.Name = "bindingNavigatorPageCountItem";
            this.pageCountItem.Text = "50";
            this.pageCountItem.ToolTipText = "ÿҳ����";
            this.pageCountItem.AutoToolTip = false;
            this.Items.Add(pageCountItem);

            this.pageNameItem = new ToolStripLabel();
            this.pageNameItem.Name = "bindingNavigatorPageNameItem";
            this.pageNameItem.Text = "��";
            this.pageNameItem.ToolTipText = "������";
            this.pageNameItem.AutoToolTip = false;
            this.Items.Add(pageNameItem);


            this.MoveFirstItem.Text = "�Ƶ���һҳ";
            this.MovePreviousItem.Text = "�Ƶ���һҳ";
            this.MoveNextItem.Text = "�Ƶ���һҳ";
            this.MoveLastItem.Text = "�Ƶ����һҳ";
            this.CountItem.Text = "��ҳ��";

            this.MoveFirstItem.ToolTipText = "�Ƶ���һҳ";
            this.MovePreviousItem.ToolTipText = "�Ƶ���һҳ";
            this.MoveNextItem.ToolTipText = "�Ƶ���һҳ";
            this.MoveLastItem.ToolTipText = "�Ƶ����һҳ";
            this.CountItem.ToolTipText = "��ҳ��";
            this.CountItemFormat = "/ {0}ҳ";

            (this.PositionItem as ToolStripTextBox).Size = new System.Drawing.Size(30, 25);
            (this.pageCountItem as ToolStripTextBox).Size = new System.Drawing.Size(40, 25);
        }

        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ����SToolStripMenuItem;

        private ToolStripItem allCountItem;
        private ToolStripItem pageCountItem;
        private ToolStripItem pageNameItem;

        private void ����SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FindToolWindowSetupForm form = new FindToolWindowSetupForm(BindingSource.FingerManager);
            //if (form.ShowDialog(this) == DialogResult.OK)
            //{
            //    BindingSource.FingerManager.ReloadData();

            //    m_profile.SetValue("SearchManager." + this.BindingSource.FingerManager.ManagerName, "MaxResult",
            //                       BindingSource.FingerManager.MaxResult);
            //}
        }

        private void WireUpTextBox(ref ToolStripItem oldTextBox, ToolStripItem newTextBox, KeyEventHandler keyUpHandler, EventHandler lostFocusHandler)
        {
            if (oldTextBox != newTextBox)
            {
                ToolStripControlHost host = oldTextBox as ToolStripControlHost;
                ToolStripControlHost host2 = newTextBox as ToolStripControlHost;
                if (host != null)
                {
                    host.KeyUp -= keyUpHandler;
                    host.LostFocus -= lostFocusHandler;
                }
                if (host2 != null)
                {
                    host2.KeyUp += keyUpHandler;
                    host2.LostFocus += lostFocusHandler;
                }
                oldTextBox = newTextBox;
                this.RefreshItemsInternal();
            }
        }
        private void WireUpLabel(ref ToolStripItem oldLabel, ToolStripItem newLabel)
        {
            if (oldLabel != newLabel)
            {
                oldLabel = newLabel;
                this.RefreshItemsInternal();
            }
        }

        private void RefreshItemsInternal()
        {
            //if (!this.initializing)
            {
                this.OnRefreshItems();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        [TypeConverter(typeof (ReferenceConverter))]
        public ToolStripItem AllCountItem
        {
            get
            {
                if ((this.allCountItem != null) && this.allCountItem.IsDisposed)
                {
                    this.allCountItem = null;
                }
                return this.allCountItem;
            }
            set
            {
                this.WireUpLabel(ref this.allCountItem, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TypeConverter(typeof(ReferenceConverter))]
        public ToolStripItem PageNameItem
        {
            get
            {
                if ((this.pageNameItem != null) && this.pageNameItem.IsDisposed)
                {
                    this.pageNameItem = null;
                }
                return this.pageNameItem;
            }
            set
            {
                this.WireUpLabel(ref this.pageNameItem, value);
            }
        }

        [ TypeConverter(typeof(ReferenceConverter))]
        public ToolStripItem PageCountItem
        {
            get
            {
                if ((this.pageCountItem != null) && this.pageCountItem.IsDisposed)
                {
                    this.pageCountItem = null;
                }
                return this.pageCountItem;
            }
            set
            {
                this.WireUpTextBox(ref this.pageCountItem, value, new KeyEventHandler(this.OnPositionKey), new EventHandler(this.OnPositionLostFocus));
            }
        }

        private void OnPositionKey(object sender, KeyEventArgs e)
        {
            Keys keyCode = e.KeyCode;
            if (keyCode != Keys.Return)
            {
                if (keyCode != Keys.Escape)
                {
                    return;
                }
            }
            else
            {
                this.AcceptNewPageCount();
                return;
            }
            this.CancelNewPosition();
        }

        private void OnPositionLostFocus(object sender, EventArgs e)
        {
            this.AcceptNewPageCount();
        }

        private void AcceptNewPageCount()
        {
            if ((this.pageCountItem != null))
            {
                int pageCount = BindingSource.SearchManager.MaxResult;
                try
                {
                    pageCount = Convert.ToInt32(this.pageCountItem.Text, System.Globalization.CultureInfo.CurrentCulture);
                }
                catch (FormatException)
                {
                }
                catch (OverflowException)
                {
                }
                if (pageCount != BindingSource.SearchManager.MaxResult)
                {
                    this.BindingSource.SearchManager.MaxResult = pageCount;

                    SaveLayout();

                    // �����Զ�Reload����������Ƴ�ȥ��Search��ť���ᵼ��Search��ť��Ч���¼��Ⱥ��ϵ
                    //this.BindingSource.SearchManager.ReloadData();
                }

                this.RefreshItemsInternal();
            }
        }

        private static string m_layoutDefaultFileName = "system.xmls.default";
        public string LayoutFilePath
        {
            get
            {
                if (this.SearchManager != null)
                {
                    return this.SearchManager.Name + "\\" + m_layoutDefaultFileName;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool LoadLayout()
        {
            return LayoutControlExtention.LoadLayout(this);
        }

        public bool SaveLayout()
        {
            return LayoutControlExtention.SaveLayout(this);
        }

        public bool LoadLayout(AMS.Profile.IProfile profile)
        {
            if (this.SearchManager != null)
            {
                int r = profile.GetValue("SearchManager." + this.SearchManager.Name, "MaxResult", -1);
                if (r != -1)
                {
                    this.SearchManager.MaxResult = r;
                    return true;
                }
            }
            return false;
        }

        public bool SaveLayout(AMS.Profile.IProfile profile)
        {
            if (this.SearchManager != null)
            {
                if (this.SearchManager.MaxResult != SearchManagerDefaultValue.MaxResult)
                {
                    profile.SetValue("SearchManager." + this.BindingSource.SearchManager.Name, "MaxResult", this.BindingSource.SearchManager.MaxResult);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private ISearchManager SearchManager
        {
            get
            {
                if (this.BindingSource != null)
                {
                    return this.BindingSource.SearchManager;
                }
                else
                {
                    return null;
                }
            }
        }
        private void CancelNewPosition()
        {
            this.RefreshItemsInternal();
        }

        /// <summary>
        /// 
        /// </summary>
        public new PageBindingSource BindingSource
        {
            get { return base.BindingSource as PageBindingSource; }
            set
            {
                if (base.BindingSource == value)
                {
                    return;
                }
                if (base.BindingSource != null)
                {
                    base.BindingSource.ListChanged -= new System.ComponentModel.ListChangedEventHandler(BindingSource_ListChanged);
                }
                base.BindingSource = value;

                if (base.BindingSource != null)
                {
                    base.BindingSource.ListChanged += new System.ComponentModel.ListChangedEventHandler(BindingSource_ListChanged);

                    this.Enabled = this.BindingSource.SearchManager.EnablePage;
                    this.pageCountItem.Enabled = this.Enabled;
                    this.PositionItem.Enabled = this.Enabled;

                    if (this.Enabled)
                    {
                        LoadLayout();

                        this.PageCountItem.Text = this.BindingSource.SearchManager.MaxResult.ToString();
                    }
                    else
                    {
                        this.pageCountItem.Text = string.Empty;
                    }

                    UpdateStatus();
                }
            }
        }

        private void BindingSource_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            if (e.ListChangedType == System.ComponentModel.ListChangedType.Reset)
            {
                UpdateStatus();
            }
        }

        private void UpdateStatus()
        {
            this.AllCountItem.Text = "��" + BindingSource.SearchManager.Count + "��/ÿҳ";
                                      //+ BindingSource.FingerManager.MaxResult + "��";
        }
    }
}