using System;
using System.Collections.Generic;

namespace Feng
{
#if !NMA
    using NHibernate.Mapping.Attributes;

    /// <summary>
    /// �����־��Ϣ
    /// </summary>
    [Class(Name = "Feng.AuditLogRecord", Table = "SD_AuditLogRecord")]
    public class AuditLogRecord : BaseDataEntity
    {
        /// <summary>
        /// ��־����
        /// </summary>
        [Property(Length = 50, NotNull = true)]
        public virtual string LogType
        {
            get;
            set;
        }

        [Property(Length = 255, NotNull = false)]
        public virtual string EntityName
        {
            get;
            set;
        }

        [Property(Length = 255, NotNull = false)]
        public virtual string EntityId
        {
            get;
            set;
        }

        /// <summary>
        /// ��־��Ϣ
        /// </summary>
        [Property(Length = 4000, NotNull = false)]
        public virtual string Message
        {
            get;
            set;
        }
#else
    /// <summary>
    /// �����־��Ϣ
    /// </summary>
    public class AuditLogRecord : IEntity
    {
        /// <summary>
        /// ��־����
        /// </summary>
        public virtual string LogType
        {
            get;
            set;
        }

        public virtual string EntityName
        {
            get;
            set;
        }

        public virtual string EntityId
        {
            get;
            set;
        }

        /// <summary>
        /// ��־��Ϣ
        /// </summary>
        public virtual string Message
        {
            get;
            set;
        }
#endif

    } 
}
