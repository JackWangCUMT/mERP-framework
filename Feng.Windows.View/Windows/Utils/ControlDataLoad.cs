using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Feng.Windows.Utils;
using Feng.Windows.Forms;

namespace Feng.Windows.Utils
{
	/// <summary>
	/// Ϊ����֧�����ݰ󶨵Ŀؼ���������
	/// </summary>
	public static class ControlDataLoad
	{
        #region "IWindowDataControl"
        /// <summary>
		/// InitDataControl
		/// </summary>
		/// <param name="dcc"></param>
		/// <param name="nvName"></param>
        public static void InitDataControl(System.Windows.Forms.Control control, string nvName)
		{
            InitDataControl(control, null, nvName, nvName, string.Empty);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dcc"></param>
        /// <param name="nvcName"></param>
        /// <param name="nvNameViewer"></param>
        /// <param name="nvNameEditor"></param>
        /// <param name="editorFilter"></param>
        public static void InitDataControl(System.Windows.Forms.Control control, string nvcName, string nvNameViewer, string nvNameEditor, string editorFilter)
        {
            if (control is IBindingDataValueControl)
            {
                InitDataControl(control as INameValueMappingBindingControl, nvcName, nvNameViewer, nvNameEditor, editorFilter);
            }
            else if (control is IFormatControl)
            {
                if (!string.IsNullOrEmpty(nvNameEditor))
                {
                    ((IFormatControl)control).Format = nvNameEditor;
                }
            }
            
            //else if (dcc.Control is MyTextBox)
            //{
            //    ((MyTextBox)dcc.Control).CharacterCasing = nvNameEditor == "Upper" ? System.Windows.Forms.CharacterCasing.Upper :
            //        (nvNameEditor == "Lower" ? System.Windows.Forms.CharacterCasing.Lower : System.Windows.Forms.CharacterCasing.Normal);
            //}
            //else if (dcc.Control is MyMultilineTextBox)
            //{
            //    ((MyMultilineTextBox)dcc.Control).TextBoxArea.CharacterCasing = (nvNameEditor == "Upper" ? System.Windows.Forms.CharacterCasing.Upper :
            //        (nvNameEditor == "Lower" ? System.Windows.Forms.CharacterCasing.Lower : System.Windows.Forms.CharacterCasing.Normal));
            //}
            // Todo
            else if (control is MyDatePicker)
            {
                if (!string.IsNullOrEmpty(nvNameViewer))
                {
                    ((MyDatePicker)control).DisplayFormatSpecifier = nvNameViewer;
                }
                if (!string.IsNullOrEmpty(nvNameEditor))
                {
                    ((MyDatePicker)control).EditFormatSpecifier = nvNameEditor;
                }
            }
            else if (control is MyDateTimePicker)
            {
                if (!string.IsNullOrEmpty(nvNameViewer))
                {
                    ((MyDateTimePicker)control).CustomFormat = nvNameViewer;
                }
            }
            else
            {
                //throw new NotSupportedException(dcc.Control.GetType().ToString() + "'s param is not supported in DataControlWrapper");
            }
        }

        /// <summary>
        /// InitDataControl
        /// </summary>
        /// <param name="dcc"></param>
        /// <param name="type"></param>
        /// <param name="notUseEnum"></param>
        /// <param name="filter"></param>
        public static void InitDataControl(System.Windows.Forms.Control control, Type type, bool notUseEnum, string filter)
        {
            string newName = NameValueMappingCollection.Instance.Add(type, notUseEnum);
            InitDataControl(control, null, newName, newName, filter);
        }

        /// <summary>
        /// InitDataControl
        /// </summary>
        /// <param name="dcc"></param>
        /// <param name="type"></param>
        /// <param name="notUseEnum"></param>
        public static void InitDataControl(System.Windows.Forms.Control control, Type type, bool notUseEnum)
        {
            InitDataControl(control, type, notUseEnum, string.Empty);
        }

		/// <summary>
		/// InitDataControl
		/// </summary>
		/// <param name="dcc"></param>
		/// <param name="type"></param>
        public static void InitDataControl(System.Windows.Forms.Control control, Type type)
		{
            InitDataControl(control, type, false);
		}

		#endregion

		#region "IBindingDataControl"
        /// <summary>
        /// InitDataControl
        /// </summary>
        /// <param name="control"></param>
        /// <param name="type"></param>
        /// <param name="notUseEnum"></param>
        /// <param name="filter"></param>
        public static void InitDataControl(INameValueMappingBindingControl control, Type type, bool notUseEnum, string filter)
        {
            string newName = NameValueMappingCollection.Instance.Add(type, notUseEnum);
            InitDataControl(control, null, newName, newName, filter);
        }

        /// <summary>
        /// InitDataControl
        /// </summary>
        /// <param name="control"></param>
        /// <param name="type"></param>
        /// <param name="notUseEnum"></param>
        public static void InitDataControl(INameValueMappingBindingControl control, Type type, bool notUseEnum)
        {
            InitDataControl(control, type, notUseEnum, string.Empty);
        }

		/// <summary>
		/// InitDataControl
		/// </summary>
        /// <param name="control"></param>
		/// <param name="type"></param>
        public static void InitDataControl(INameValueMappingBindingControl control, Type type)
		{
            InitDataControl(control, type, false);
		}

        /// <summary>
        /// InitDataControl
        /// </summary>
        /// <param name="control"></param>
        /// <param name="nvName"></param>
        public static void InitDataControl(INameValueMappingBindingControl control, string nvName)
        {
            InitDataControl(control, nvName, string.Empty);
        }

        /// <summary>
        /// InitDataControl
        /// </summary>
        /// <param name="control"></param>
        /// <param name="nvName"></param>
        /// <param name="editorFilter"></param>
        public static void InitDataControl(INameValueMappingBindingControl control, string nvName, string editorFilter)
        {
            InitDataControl(control, null, nvName, nvName, editorFilter);
        }

        /// <summary>
        /// InitDataControl
        /// </summary>
        /// <param name="control"></param>
        /// <param name="nvNameViewer"></param>
        /// <param name="nvNameEditor"></param>
        /// <param name="editorFilter"></param>
        public static void InitDataControl(INameValueMappingBindingControl control, string nvNameViewer, string nvNameEditor, string editorFilter)
        {
            InitDataControl(control, null, nvNameViewer, nvNameEditor, editorFilter);
        }

		/// <summary>
        /// InitDataControl
		/// </summary>
		/// <param name="control"></param>
		/// <param name="nvcName"></param>
		/// <param name="nvNameViewer"></param>
		/// <param name="nvNameEditor"></param>
		/// <param name="editorFilter"></param>
        public static void InitDataControl(INameValueMappingBindingControl control, string nvcName, string nvNameViewer, string nvNameEditor, string editorFilter)
		{
            if (control == null)
			{
                throw new ArgumentNullException("control");
			}

            if (string.IsNullOrEmpty(nvNameViewer))
            {
                // ����� FreeComboBox����nvNameViewerΪ�գ�����ҪDatasource������ʾ��
                nvNameViewer = nvNameEditor;
            }

            NameValueMapping nvViewer = NameValueMappingCollection.Instance[nvNameViewer];
            control.ValueMember = nvViewer.ValueMember;
            control.DisplayMember = nvViewer.DisplayMember;

            NameValueMapping nvEditor = NameValueMappingCollection.Instance[nvNameEditor];
            
            // �Ƕ�̬��Ĭ��DataSet
            if (!nvEditor.IsDynamic)
            {
                nvcName = null;
            }
            control.SetDataBinding(nvcName, nvNameViewer, nvNameEditor, editorFilter);

            IGridDropdownControl gridControl = control as IGridDropdownControl;
            if (gridControl != null)
            {
                gridControl.VisibleColumns(nvViewer.MemberVisible);
                gridControl.AdjustDropDownControlSize();
            }
		}

        ///// <summary>
        ///// ��Sql�������IBindingDataControl������
        ///// </summary>
        ///// <param name="control">IBindingDataControl</param>
        ///// <param name="strParams">�����������һ��ΪValueMember���ڶ���ΪDisplayMember�����һ��ΪSql���</param>
        //public static void InitDataControl(IBindingDataValueControl control, string[] strParams)
        //{
        //    if (control == null)
        //    {
        //        throw new ArgumentNullException("control");
        //    }
        //    if (strParams == null)
        //        throw new ArgumentNullException("strParams");
        //    if (strParams.Length < 2)
        //        throw new ArgumentException("strParams should have at least two strings", "strParams");

        //    DbCommand cmd = DbHelper.Instance.DefaultDatabase.GetSqlStringCommand(strParams[strParams.Length - 1]);
        //    Dictionary<string, bool> members = new Dictionary<string,bool>();
        //    if (strParams.Length == 2)
        //    {
        //        control.ValueMember = strParams[0];
        //        control.DisplayMember = strParams[0];
        //        members[strParams[0]] = true;
        //    }
        //    else if (strParams.Length == 3)
        //    {
        //        control.ValueMember = strParams[0];
        //        control.DisplayMember = strParams[1];
        //        members[strParams[0]] = true;
        //        members[strParams[1]] = true;
        //    }

        //    control.SetDataBinding(DbHelper.Instance.ExecuteDataTable(cmd), string.Empty);

        //    IGridDropdownControl gridControl = control as IGridDropdownControl;
        //    if (gridControl != null)
        //    {
        //        gridControl.VisibleColumns(members);
        //        gridControl.AdjustDropDownControlSize();
        //        gridControl.SetRefreshAction(new DataBindingRefreshAction(delegate
        //        {
        //            control.SetDataBinding(DbHelper.Instance.ExecuteDataTable(cmd), string.Empty);
        //        }));
        //    }
        //}
		#endregion
	}
}
