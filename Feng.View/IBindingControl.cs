using System.ComponentModel;
using System;

namespace Feng
{
	/// <summary>
    /// �󶨿ؼ�����
	/// </summary>
	public interface IBindingControl : IStateControl
	{
		/// <summary>
		/// ��������Դ
		/// </summary>
		/// <param name="dataSource"></param>
		/// <param name="dataMember"></param>
		void SetDataBinding(object dataSource, string dataMember);
	}

    /// <summary>
    /// 
    /// </summary>
    public interface ICanAddItemBindingControl
    {
        /// <summary>
        /// ����һ��������
        /// </summary>
        /// <param name="dataItem"></param>
        void AddDateItem(object dataItem);
    }
}
