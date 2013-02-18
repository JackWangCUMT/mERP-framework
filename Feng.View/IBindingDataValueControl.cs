using System;
using System.Collections.Generic;
using System.Text;
using Feng;

namespace Feng
{
    /// <summary>
    /// 
    /// </summary>
    public interface INameValueMappingBindingControl : IBindingDataValueControl
    {
        /// <summary>
        /// View��Editor״̬�в�ͬDataBinding
        /// </summary>
        /// <param name="nvcName">NameValueMappingCollection����</param>
        /// <param name="viewerName"></param>
        /// <param name="editorName"></param>
        /// <param name="editorFilter"></param>
        void SetDataBinding(string nvcName, string viewerName, string editorName, string editorFilter);

        /// <summary>
        /// ���NameValueMapping��Name
        /// </summary>
        string NameValueMappingName
        {
            get;
        }
    }

	/// <summary>
	/// ��������Դ�󶨣����ҿɶ�ȡ�ؼ�ֵ�Ľӿ�
	/// </summary>
	public interface IBindingDataValueControl : IBindingControl, IDataValueControl
	{
		/// <summary>
        /// ��ȡ������һ�����ԣ������Խ�����<see cref="IBindingDataValueControl"/> �е����ʵ��ֵ��
		/// </summary>
		string ValueMember
		{
			get;
			set;
		}

		/// <summary>
        /// ��ȡ������ҪΪ�� <see cref="IBindingDataValueControl"/> ��ʾ�����ԡ�
		/// </summary>
		string DisplayMember
		{
			get;
			set;
		}

        /// <summary>
        /// ˢ������
        /// </summary>
        void ReloadData();
	}

    ///// <summary>
    ///// ��ˢ��ʱ����еĶ�����Delegate
    ///// </summary>
    //public delegate void DataBindingRefreshAction();
}
