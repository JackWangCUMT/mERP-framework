using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Feng
{
    /// <summary>
    /// �����Ϣ����
    /// </summary>
    public enum GridRelatedType
    {
        /// <summary>
        /// ������=0��
        /// </summary>
        Invalid = 0,
        /// <summary>
        /// ����ѡ����ת���������壨=1��
        /// </summary>
        ByRows = 1,
        /// <summary>
        /// ������������ת���������壨=2��
        /// </summary>
        BySearchExpression = 2,
        /// <summary>
        /// �������ݿؼ�״̬��=3��
        /// </summary>
        ByDataControl = 3,
        /// <summary>
        /// ��������ֱ��ת����=9��
        /// </summary>
        ByNone = 9
    }

    /// <summary>
    /// ���ת����������������Ϣ�����ڳ���-���ҳ��Ҳ������<see cref="T:Feng.Windows.GridGotoFormToolStripItem"/>
    /// </summary>
    [Class(0, Name = "Feng.GridRelatedInfo", Table = "AD_Grid_Related", OptimisticLock = OptimisticLockMode.Version)]
    [Cache(1, Usage = CacheUsage.NonStrictReadWrite)]
    [Serializable]
    public class GridRelatedInfo : BaseADEntity
    {
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
        /// ����������Զ�����Ӧ��������Ϣ
        /// </summary>
        [Property(Length = 100, NotNull = true)]
        public virtual string GridName
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
        /// �Ƿ���ʾ����ʽ��<see cref="T:Feng.Permission"/>
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string Visible
        {
            get;
            set;
        }

        /// <summary>
        /// �����Ϣ����
        /// </summary>
        [Property(NotNull = true)]
        public virtual GridRelatedType RelatedType
        {
            get;
            set;
        }

        /// <summary>
        /// ����ǰ���ѡ����������ָ�����������ʽ����ִ�ж����򿪴���󣬰��մ��������ʽ������������ʽ��<see cref="Feng.SearchExpression"/>
        /// </summary>
        [Property(Length = 100, NotNull = true)]
        public virtual string SearchExpression
        {
            get;
            set;
        }

        ///// <summary>
        ///// ����ǰ���ѡ������������Ӧ�ı���е�����ʵ�������͡����ѡ��ı����ʵ�������ͺʹ˴��趨�Ĳ�ͬ������ʾ��
        ///// </summary>
        //[Property(Length = 100, NotNull = false)]
        //public virtual string EntityType
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// ��񼶱���������ϸ���ʱ����ʱ����Ϊ0���ڶ����һ��GridΪ0.0���ڶ���Ϊ0.1
        /// </summary>
        [Property(NotNull = false)]
        public virtual string GridLevel
        {
            get;
            set;
        }

        ///// <summary>
        ///// Ҫת������ϸ����
        ///// </summary>
        //[Property(NotNull = false, Length = 100)]
        //public virtual string ToDetailForm
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// �����Ҫִ�еĶ����ģ�Ŀǰ֧��Window��Form
        /// </summary>
        [ManyToOne(ForeignKey = "FK_GridRelated_Action")]
        public virtual ActionInfo Action
        {
            get;
            set;
        }

        ///// <summary>
        ///// �����Ҫִ�еĶ�����<see cref="P:Feng.ActionInfo.Name"/>��Ŀǰ֧��Window��Form
        ///// </summary>
        //[Property(Column = "ActionId", NotNull = true)]
        //public virtual long ActionId
        //{
        //    get;
        //    set;
        //}
    }
}