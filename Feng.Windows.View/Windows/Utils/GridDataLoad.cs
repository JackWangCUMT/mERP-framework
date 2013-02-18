using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Feng.Windows.Utils;
using Feng.Data;
using Feng.Windows.Forms;
using Feng.Grid;
using Feng.Grid.Viewers;
using Feng.Grid.Editors;

namespace Feng.Windows.Utils
{
	/// <summary>
	/// Ϊ����֧�����ݰ󶨵Ŀؼ���������
	/// </summary>
	public sealed class GridDataLoad
	{
		#region "Constructor"
		/// <summary>
		/// Consturtor
		/// </summary>
        private GridDataLoad()
		{
		}
		#endregion

		#region "GridViewer"
		/// <summary>
		/// ���DataGrid��ComboBox����ʾ��ʽ
		/// </summary>
        /// <param name="nvName"></param>
        /// <param name="filter"></param>
		/// <returns></returns>
        public static MyComboBoxViewer GetGridComboViewer(string nvName, string filter)
		{
            return GetGridComboViewer(null, nvName, filter);
		}

        /// <summary>
        /// ���DataGrid��ComboBox����ʾ��ʽ
        /// </summary>
        /// <param name="dsName"></param>
        /// <param name="nvName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static MyComboBoxViewer GetGridComboViewer(string dsName, string nvName, string filter)
        {
            string newNvName = NameValueMappingCollection.Instance.GetDataSourceName(dsName, nvName, filter);
            return new MyComboBoxViewer(nvName);
        }

        /// <summary>
        /// ���DataGrid��ComboBox����ʾ��ʽ
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MyComboBoxViewer GetGridComboViewer(Type type)
        {
            // ����Entity��ʽʱ������ҪViewer������DataTable��ʽʱ��Ĭ���ǲ���enum
            return GetGridComboViewer(type, true);
        }

        /// <summary>
        /// ���DataGrid��ComboBox����ʾ��ʽ
        /// </summary>
        /// <param name="type"></param>
        /// <param name="notUseEnum"></param>
        /// <returns></returns>
        public static MyComboBoxViewer GetGridComboViewer(Type type, bool notUseEnum)
        {
            string typeNvName = NameValueMappingCollection.Instance.Add(type, notUseEnum);
            string newNvName = NameValueMappingCollection.Instance.GetDataSourceName(null, typeNvName, string.Empty);

            return new MyComboBoxViewer(newNvName);

            //// ���������DataSet+Name�ķ�ʽ������DataView��ListChanged�¼�������dispose
            //return new MyComboBoxViewer
            //    (NameValueMappingCollection.Instance.DataTable(newName, filter), string.Empty, nv.ValueMember, "%" + nv.DisplayMember + "%");
        }

		/// <summary>
		/// ���DataGrid��MultiCombo����ʾ��ʽ
		/// </summary>
		/// <param name="nvName"></param>
        /// <param name="filter"></param>
		/// <returns></returns>
        public static MyOptionPickerViewer GetGridMultiComboViewer(string nvName, string filter)
		{
            return GetGridMultiComboViewer(null, nvName, filter);
		}

        /// <summary>
        /// ���DataGrid��MultiCombo����ʾ��ʽ
        /// </summary>
        /// <param name="dsName"></param>
        /// <param name="nvName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static MyOptionPickerViewer GetGridMultiComboViewer(string dsName, string nvName, string filter)
        {
            string newNvName = NameValueMappingCollection.Instance.GetDataSourceName(dsName, nvName, filter);
            return new MyOptionPickerViewer(newNvName);
        }

        /// <summary>
        /// ���DataGrid��ComboBox����ʾ��ʽ
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MyOptionPickerViewer GetGridMultiComboViewer(Type type)
        {
            return GetGridMultiComboViewer(type, true);
        }

        /// <summary>
        /// ���DataGrid��ComboBox����ʾ��ʽ
        /// </summary>
        /// <param name="type"></param>
        /// <param name="notUseEnum"></param>
        /// <returns></returns>
        public static MyOptionPickerViewer GetGridMultiComboViewer(Type type, bool notUseEnum)
        {
            string typeNvName = NameValueMappingCollection.Instance.Add(type, notUseEnum);
            string newNvName = NameValueMappingCollection.Instance.GetDataSourceName(null, typeNvName, string.Empty);

            return new MyOptionPickerViewer(newNvName);
        }
#endregion

        #region "GridEditor"
        /// <summary>
		///  ���DataGrid��ComboBox�ı༭��ʽ
		/// </summary>
		/// <param name="nvName"></param>
        /// <param name="filter"></param>
		/// <returns></returns>
        public static MyComboBoxEditor GetGridComboEditor(string nvName, string filter)
		{
            return GetGridComboEditor(null, nvName, filter); 
		}

		/// <summary>
		/// ���DataGrid��ComboBox�ı༭��ʽ
		/// </summary>
        /// <param name="dsName"></param>
		/// <param name="nvName"></param>
		/// <param name="filter"></param>
		/// <returns></returns>
		public static MyComboBoxEditor GetGridComboEditor(string dsName, string nvName, string filter)
		{
            string newNvName = NameValueMappingCollection.Instance.GetDataSourceName(dsName, nvName, filter);

            MyComboBoxEditor editor = new MyComboBoxEditor(newNvName);
            ControlDataLoad.InitDataControl((editor.TemplateControl as INameValueMappingBindingControl), dsName, nvName, nvName, filter);
			return editor;
		}

        /// <summary>
        /// ���DataGrid��ComboBox�ı༭��ʽ
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MyComboBoxEditor GetGridComboEditor(Type type)
        {
            // Ĭ��ģʽֵ��Enum
            return GetGridComboEditor(type, false);
        }

        /// <summary>
        /// ���DataGrid��ComboBox�ı༭��ʽ
        /// </summary>
        /// <param name="type"></param>
        /// <param name="notUseEnum"></param>
        /// <returns></returns>
        public static MyComboBoxEditor GetGridComboEditor(Type type, bool notUseEnum)
        {
            return GetGridComboEditor(type, notUseEnum, string.Empty);
        }

		/// <summary>
		/// ���DataGrid��ComboBox�ı༭��ʽ
		/// </summary>
		/// <param name="type"></param>
        /// <param name="notUseEnum"></param>
        /// <param name="filter"></param>
		/// <returns></returns>
        public static MyComboBoxEditor GetGridComboEditor(Type type, bool notUseEnum, string filter)
		{
            string typeNvName = NameValueMappingCollection.Instance.Add(type, notUseEnum);
            string newNvName = NameValueMappingCollection.Instance.GetDataSourceName(null, typeNvName, string.Empty);

            MyComboBoxEditor editor = new MyComboBoxEditor(newNvName);
            ControlDataLoad.InitDataControl(editor.TemplateControl as INameValueMappingBindingControl, type, notUseEnum, filter);

			return editor;
		}


		/// <summary>
		///  ���DataGrid��ComboBox�ı༭��ʽ
		/// </summary>
		/// <param name="nvName"></param>
        /// <param name="filter"></param>
		/// <returns></returns>
        public static MyFreeComboBoxEditor GetGridFreeComboEditor(string nvName, string filter)
		{
            return GetGridFreeComboEditor(null, nvName, filter);
		}

		/// <summary>
		/// ���DataGrid��ComboBox�ı༭��ʽ
		/// </summary>
        /// <param name="dsName"></param>
		/// <param name="nvName"></param>
		/// <param name="filter"></param>
		/// <returns></returns>
		public static MyFreeComboBoxEditor GetGridFreeComboEditor(string dsName, string nvName, string filter)
		{
            MyFreeComboBoxEditor editor = new MyFreeComboBoxEditor();
            ControlDataLoad.InitDataControl(editor.TemplateControl as INameValueMappingBindingControl, dsName, nvName, nvName, filter);

			return editor;
		}

        /// <summary>
        ///  ���DataGrid��ComboBox�ı༭��ʽ
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MyFreeComboBoxEditor GetGridFreeComboEditor(Type type)
        {
            return GetGridFreeComboEditor(type, false);
        }

        /// <summary>
        /// ���DataGrid��ComboBox�ı༭��ʽ
        /// </summary>
        /// <param name="type"></param>
        /// <param name="notUseEnum"></param>
        /// <returns></returns>
        public static MyFreeComboBoxEditor GetGridFreeComboEditor(Type type, bool notUseEnum)
        {
            return GetGridFreeComboEditor(type, notUseEnum, string.Empty);
        }

		/// <summary>
		/// ���DataGrid��ComboBox�ı༭��ʽ
		/// </summary>
		/// <param name="type"></param>
        /// <param name="notUseEnum"></param>
        /// <param name="filter"></param>
		/// <returns></returns>
        public static MyFreeComboBoxEditor GetGridFreeComboEditor(Type type, bool notUseEnum, string filter)
		{
            string typeNvName = NameValueMappingCollection.Instance.Add(type, notUseEnum);
			MyFreeComboBoxEditor editor = new MyFreeComboBoxEditor();
            ControlDataLoad.InitDataControl(editor.TemplateControl as INameValueMappingBindingControl, type, notUseEnum, filter);

			return editor;
		}

        /// <summary>
        ///  ���DataGrid��MultiCombo�ı༭��ʽ
		/// </summary>
		/// <param name="nvName"></param>
        /// <param name="filter"></param>
		/// <returns></returns>
        public static MyOptionPickerEditor GetGridMultiComboEditor(string nvName, string filter)
		{
            return GetGridMultiComboEditor(null, nvName, filter);
		}

		/// <summary>
        /// ���DataGrid��MultiCombo�ı༭��ʽ
		/// </summary>
        /// <param name="dsName"></param>
		/// <param name="nvName"></param>
		/// <param name="filter"></param>
		/// <returns></returns>
        public static MyOptionPickerEditor GetGridMultiComboEditor(string dsName, string nvName, string filter)
		{
            string newNvName = NameValueMappingCollection.Instance.GetDataSourceName(dsName, nvName, filter);
            MyOptionPickerEditor editor = new MyOptionPickerEditor(newNvName);
            ControlDataLoad.InitDataControl(editor.TemplateControl as INameValueMappingBindingControl, dsName, nvName, nvName, filter);

			return editor;
		}

        /// <summary>
        /// ���DataGrid��ComboBox�ı༭��ʽ
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MyOptionPickerEditor GetGridMultiComboEditor(Type type)
        {
            return GetGridMultiComboEditor(type, false);
        }

        /// <summary>
        /// ���DataGrid��ComboBox�ı༭��ʽ
        /// </summary>
        /// <param name="type"></param>
        /// <param name="notUseEnum"></param>
        /// <returns></returns>
        public static MyOptionPickerEditor GetGridMultiComboEditor(Type type, bool notUseEnum)
        {
            return GetGridMultiComboEditor(type, notUseEnum, string.Empty);
        }

        /// <summary>
        /// ���DataGrid��ComboBox�ı༭��ʽ
        /// </summary>
        /// <param name="type"></param>
        /// <param name="notUseEnum"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static MyOptionPickerEditor GetGridMultiComboEditor(Type type, bool notUseEnum, string filter)
        {
            string typeNvName = NameValueMappingCollection.Instance.Add(type, notUseEnum);
            MyOptionPickerEditor editor = new MyOptionPickerEditor(typeNvName);
            ControlDataLoad.InitDataControl(editor.TemplateControl as INameValueMappingBindingControl, type, notUseEnum, filter);

            return editor;
        }
		#endregion


	}
}
