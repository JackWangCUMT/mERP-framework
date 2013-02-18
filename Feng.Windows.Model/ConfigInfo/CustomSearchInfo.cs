using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Feng
{
    /// <summary>
    /// �Զ��������Ϣ����ʾ�ڳ������ҳ���Զ��������
    /// </summary>
    [Class(0, Name = "Feng.CustomSearchInfo", Table = "AD_Search_Custom", OptimisticLock = OptimisticLockMode.Version)]
    [Cache(1, Usage = CacheUsage.NonStrictReadWrite)]
    [Serializable]
    public class CustomSearchInfo : BaseADEntity
    {
        /// <summary>
        /// �������ƣ������<see cref="T:Feng.ISearchManager.Name"/>һ�µ���Ϣ
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string GroupName
        {
            get;
            set;
        }

        /// <summary>
        /// ��ʾ����
        /// </summary>
        [Property(Length = 100, NotNull = true)]
        public virtual string Text
        {
            get;
            set;
        }

        /// <summary>
        /// ������������ʽ��<see cref="Feng.SearchExpression"/>
        /// </summary>
        [Property(Length = 255, NotNull = true)]
        public virtual string SearchExpression
        {
            get;
            set;
        }

        /// <summary>
        /// ��ʾ˳��
        /// </summary>
        [Property(NotNull = true)]
        public virtual int SeqNo
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ���ʾ����ʽ��<see cref="T:Feng.Authority"/>
        /// </summary>
        [Property(Length = 100, NotNull = true)]
        public virtual string Visible
        {
            get;
            set;
        }
    }
}