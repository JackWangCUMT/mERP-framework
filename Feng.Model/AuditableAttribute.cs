using System;
using System.Collections.Generic;
using System.Text;

namespace Feng
{
    /// <summary>
    /// �����־����
    /// ����ʵ����IEntity�ӿڣ��ɶ�IEntity��Save��Update��Delete���������¼
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class AuditableAttribute : Attribute
    {
    }
}
