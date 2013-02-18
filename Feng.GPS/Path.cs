using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using NHibernate.Mapping.Attributes;

namespace Feng.GPS
{
	/// <summary>
	/// ���·�ߣ���ʼ��������Ӧ��ͼ���·��
	/// </summary>
	[Class(Table = "���_·����", Lazy = false)]
	public class Path : IVersionedEntity
	{
		#region "Constructor"
		/// <summary>
		/// default constructor
		/// </summary>
		public Path()
		{
		}

		/// <summary>
		/// named simplePath with start and end site
		/// </summary>
		/// <param name="pathName"></param>
		/// <param name="regexPattern"></param>
		public Path(string pathName, string regexPattern)
		{
			this.PathName = pathName;
			this.RegexPattern = regexPattern;
		}
		#endregion

		#region  Properties
		///<summary>
		/// ID
		///</summary>
		[Id(0, Name = "ID", Column = "ID")]
		[Generator(1, Class = "guid")]
		public virtual Guid ID
		{
            get;
            set;
		}

		///<summary>
		/// ·������
		///</summary>
		[Property(Length = 20, NotNull = true)]
		public virtual string PathName
		{
            get;
            set;
		}

		///<summary>
		/// ��ʼ�ص�
		///</summary>
		[Property(Length = 20, NotNull = true)]
		public virtual string StartSite
		{
            get;
            set;
		}

		///<summary>
		/// �����ص�
		///</summary>
		[Property(Length = 20, NotNull = true)]
		public virtual string EndSite
		{
            get;
            set;
		}

		///<summary>
		/// ��·����Ӧ��������ʽ
		///</summary>
		[Property(Length = 500, NotNull = false)]
		public virtual string RegexPattern
		{
            get;
            set;
		}

		///<summary>
		/// ��·����Ҫ��ʱ��
		///</summary>
		[Property(NotNull = true)]
		public virtual int PathTime
		{
            get;
            set;
		}

		///<summary>
		/// ��·������
		///</summary>
        [Property(Length = 1, NotNull = false)]
        public virtual string PathType
        {
            get;
            set;
        }

        /// <summary>
        /// �汾��
        /// </summary>
        [Version(Column = "Version", Type = "Int32", UnsavedValue = "0")]
        public virtual int Version
        {
            get;
            set;
        }
		#endregion

		#region "Methods"
		public override string ToString()
		{
			return this.PathName;
		}

		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			Path that = (Path)obj;
			if (that == null)
			{
				return false;
			}
            if (this.PathName == null || that.PathName == null || !this.PathName.Equals(that.PathName))
			{
				return false;
			}
			return true;
		}

		public override int GetHashCode()
		{
            return this.PathName.GetHashCode();
		}

		#endregion
	}
}
