using System;
using System.Collections.Generic;
using System.Text;

namespace Feng
{
    /// <summary>
    /// �ж��Ƿ��ܱ�ɾ����ʵ����ӿ�
    /// </summary>
    public interface IDeletableEntity
    {
        /// <summary>
        /// �Ƿ��ɾ��
        /// </summary>
        /// <returns></returns>
        bool CanBeDelete(OperateArgs e);
    }
}
