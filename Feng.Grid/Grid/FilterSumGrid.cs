//using System;
//using System.Collections.Generic;
//using System.Text;
//using Xceed.Grid;
//using Feng.Utils;

//namespace Feng.Grid
//{
//    /// <summary>
//    /// ���ɹ�ѡ���ܼ�����ֵ��Grid��Ĭ�ϼ�����Ϊ������
//    /// </summary>
//    public class FilterSumGrid : FilterGrid
//    {
//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public FilterSumGrid()
//        {
//            m_valueRow = this.AddValueRowToFixedFooter();
//        }

//        private Xceed.Grid.ValueRow m_valueRow;
//        private string m_sumColumnName = "���";

//        /// <summary>
//        /// Sum's cell columnName
//        /// </summary>
//        public string SumColumnName
//        {
//            get { return m_sumColumnName; }
//            set { m_sumColumnName = value; }
//        }

//        /// <summary>
//        /// ����ѡ��ֵ�ı�ʱ�����¼���Sum��
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        protected override void FilterGrid_SelectionValueChanged(object sender, EventArgs e)
//        {
//            bool allSelect = true;
//            bool noneSelect = true;
//            decimal sum = 0m;
//            foreach (Xceed.Grid.DataRow row in base.DataRows)
//            {
//                if (Convert.ToBoolean(row.Cells[FilterGrid.SelectCaption].Value))
//                {
//                    sum += ConvertHelper.ToDecimal(row.Cells[m_sumColumnName].Value).Value;
//                    noneSelect = false;
//                }
//                else
//                {
//                    allSelect = false;
//                }
//            }
//            m_valueRow.Cells[m_sumColumnName].Value = sum;

//            if (allSelect)
//            {
//                m_valueRow.Cells[FilterGrid.SelectCaption].Value = true;
//            }
//            else if (noneSelect)
//            {
//                m_valueRow.Cells[FilterGrid.SelectCaption].Value = false;
//            }
//            else
//            {
//                m_valueRow.Cells[FilterGrid.SelectCaption].Value = null;
//            }

//            base.FilterGrid_SelectionValueChanged(sender, e);
//        }
//    }
//}