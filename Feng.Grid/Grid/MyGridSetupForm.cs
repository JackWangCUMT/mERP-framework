using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Feng.Windows.Forms;

namespace Feng.Grid
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MyGridSetupForm : PositionPersistForm
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        public MyGridSetupForm(IBoundGrid grid)
        {
            InitializeComponent();

            m_masterGrid = grid;
        }

        private IBoundGrid m_masterGrid;

        private void MyGridSetupForm_Load(object sender, EventArgs e)
        {
            grdGridColumns.Columns.Add(new Xceed.Grid.Column("����", typeof(string)));
            grdGridColumns.Columns.Add(new Xceed.Grid.Column("�Ƿ���ʾ", typeof(bool)));
            grdGridColumns.ReadOnly = false;
            grdGridColumns.Columns["�Ƿ���ʾ"].ReadOnly = false;
            grdGridColumns.Columns["����"].ReadOnly = true;

            LoadGridInfos();
        }

        private void LoadGridInfos()
        {
            grdGridColumns.DataRows.Clear();

            IBoundGrid masterGrid = m_masterGrid as IBoundGrid;
            if (masterGrid != null)
            {
                Dictionary<string, GridColumnInfo> visibleColumns = new Dictionary<string, GridColumnInfo>();
                bool hasInfo = GridSettingInfoCollection.Instance[masterGrid.GridName].GridColumnInfos.Count > 0;

                foreach (GridColumnInfo info in GridSettingInfoCollection.Instance[masterGrid.GridName].GridColumnInfos)
                {
                    if (!Authority.AuthorizeByRule(info.ColumnVisible))
                    {
                        continue;
                    }
                    visibleColumns[info.GridColumnName] = info;
                }

                foreach (Xceed.Grid.Column column in masterGrid.Columns)
                {
                    if (hasInfo && !visibleColumns.ContainsKey(column.FieldName))
                    {
                        continue;
                    }

                    Xceed.Grid.DataRow row = grdGridColumns.DataRows.AddNew();
                    row.Cells["�Ƿ���ʾ"].Value = column.Visible;
                    row.Cells["����"].Value = column.Title;
                    row.EndEdit();

                    if (visibleColumns.ContainsKey(column.FieldName))
                    {
                        row.ReadOnly = Authority.AuthorizeByRule(visibleColumns[column.FieldName].NotNull);
                    }
                }
            }
        }

        private void SaveGridInfos()
        {
            foreach (Xceed.Grid.DataRow row in grdGridColumns.DataRows)
            {
                IBoundGrid masterGrid = m_masterGrid as IBoundGrid;
                foreach (Xceed.Grid.Column column in masterGrid.Columns)
                {
                    if (row.Cells["����"].Value.ToString() == column.Title)
                    {
                        column.Visible = Convert.ToBoolean(row.Cells["�Ƿ���ʾ"].Value);
                        break;
                    }
                }
            }
        }

        private void btnResetGrid_Click(object sender, EventArgs e)
        {
            IBoundGrid grid = m_masterGrid as IBoundGrid;
            if (grid != null)
            {
                grid.LoadDefaultLayoutBoundGrid();
                grid.AutoAdjustColumnWidth();

                LoadGridInfos();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveGridInfos();
        }
    }
}