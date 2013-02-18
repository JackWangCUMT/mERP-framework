//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Text;
//using System.Windows.Forms;
//using Xceed.Grid;


//namespace Feng.Grid
//{
//    /// <summary>
//    /// ����ɸѡ��Grid
//    /// </summary>
//    public partial class FilterGrid : MyGrid
//    {
//        /// <summary>
//        /// ѡ��
//        /// </summary>
//        public const string SelectCaption = "ѡ��";

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public FilterGrid()
//            : base()
//        {
//            InitializeComponent();

//            base.SelectionMode = SelectionMode.MultiExtended;
//            base.Columns.Add(new Xceed.Grid.Column(SelectCaption, typeof(bool)));
//        }

//        /// <summary>
//        /// SelectionMode = SelectionMode.MultiExtended
//        /// </summary>
//        [DefaultValue(SelectionMode.MultiExtended)]
//        public new SelectionMode SelectionMode
//        {
//            get { return base.SelectionMode; }
//            set { base.SelectionMode = value; }
//        }

//        /// <summary>
//        /// EndEdit
//        /// </summary>
//        public override void EndInit()
//        {
//            base.EndInit();

//            this.DataRowTemplate.Cells[SelectCaption].ValueChanged += new EventHandler(FilterGrid_SelectionValueChanged);

//            foreach (Xceed.Grid.Row row in this.FixedHeaderRows)
//            {
//                Xceed.Grid.ColumnManagerRow columnManagerRow = row as Xceed.Grid.ColumnManagerRow;
//                if (columnManagerRow != null)
//                {
//                    columnManagerRow.Cells[SelectCaption].MouseDown += new MouseEventHandler(ColumnManageRowSelectionCell_MouseDown);
//                    break;
//                }
//            }
//        }

//        private void ColumnManageRowSelectionCell_MouseDown(object sender, MouseEventArgs e)
//        {
//            Cell cell = (Cell)sender;
//            if (e.Button == MouseButtons.Right && !this.Columns[SelectCaption].ReadOnly)
//            {
//                contextMenuStrip1.Show(cell.PointToScreen(new Point(e.X, e.Y)));
//            }
//        }

//        private void ȫѡToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            if (this.Columns[SelectCaption].ReadOnly)
//                return;

//            foreach (Xceed.Grid.DataRow row in this.DataRows)
//            {
//                if (row.Cells[SelectCaption].ReadOnly)
//                    continue;

//                if (row.Cells[SelectCaption].IsBeingEdited)
//                {
//                    row.Cells[SelectCaption].LeaveEdit(true);
//                }
//                row.Cells[SelectCaption].Value = true;
//            }
//        }

//        private void ȫ��ѡToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            if (this.Columns[SelectCaption].ReadOnly)
//                return;

//            foreach (Xceed.Grid.DataRow row in this.DataRows)
//            {
//                if (row.Cells[SelectCaption].ReadOnly)
//                    continue;

//                if (row.Cells[SelectCaption].IsBeingEdited)
//                {
//                    row.Cells[SelectCaption].LeaveEdit(true);
//                }
//                row.Cells[SelectCaption].Value = false;
//            }
//        }

//        private void ѡ����ѡ��ToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            if (this.Columns[SelectCaption].ReadOnly)
//                return;

//            foreach (Xceed.Grid.DataRow row in this.SelectedRows)
//            {
//                if (row.Cells[SelectCaption].ReadOnly)
//                    continue;

//                if (row.Cells[SelectCaption].IsBeingEdited)
//                {
//                    row.Cells[SelectCaption].LeaveEdit(true);
//                }
//                row.Cells[SelectCaption].Value = true;
//            }
//        }

//        private void ѡ���ѡ��ToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            if (this.Columns[SelectCaption].ReadOnly)
//                return;

//            foreach (Xceed.Grid.DataRow row in this.SelectedRows)
//            {
//                if (row.Cells[SelectCaption].ReadOnly)
//                    continue;

//                if (row.Cells[SelectCaption].IsBeingEdited)
//                {
//                    row.Cells[SelectCaption].LeaveEdit(true);
//                }
//                row.Cells[SelectCaption].Value = false;
//            }
//        }

//        /// <summary>
//        /// FilterGrid_ValueChanged
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        protected virtual void FilterGrid_SelectionValueChanged(object sender, EventArgs e)
//        {
//        }
//    }
//}

