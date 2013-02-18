using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Feng
{
    /// <summary>
    /// ��������ݣ�����<see cref="T:Feng.Grid.DataBoundGrid"/>��<see cref="T:Feng.Grid.DataUnboundGrid"/>
    /// ���ڱ�������Ϣ����<see cref="GridInfo"/>��<see cref="GridColumnInfo"/>��<see cref="GridRowInfo"/>��<see cref="GridCellInfo"/>
    /// ���������ݣ�Ҳ�������ô����ϵ�<see cref="Feng.IDataControl"/>�ؼ�����
    /// </summary>
    [Class(0, Name = "Feng.GridInfo", Table = "AD_Grid", OptimisticLock = OptimisticLockMode.Version)]
    [Cache(1, Usage = CacheUsage.NonStrictReadWrite)]
    [Serializable]
    public class GridInfo : BaseADEntity
    {
        /// <summary>
        /// ���������<see cref="WindowTabInfo.GridName"/>��Ӧ
        /// </summary>
        [Property(Length = 100, NotNull = true)]
        public virtual string GridName
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ�������������ʽ��<see cref="T:Feng.Authority"/>
        /// ������Ϊȫ�������ϵ�������ͬ<see cref="P:IControlManager.AllowInsert"/>�������ֹ�����������ϸ���壩�ͳ���Add-EndEdit��
        /// </summary>
        [Property(Length = 255, NotNull = true)]
        public virtual string AllowInsert
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ�����༭����ʽ��<see cref="T:Feng.Authority"/>
        /// ������Ϊȫ�������ϵı༭��ͬ<see cref="P:IControlManager.AllowEdit"/>�������ֹ�����������ϸ���壩�ͳ���Edit-EndEdit��
        /// </summary>
        [Property(Length = 255, NotNull = true)]
        public virtual string AllowEdit
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ�����ɾ������ʽ��<see cref="T:Feng.Authority"/>
        /// ������Ϊȫ�������ϵı༭��ͬ<see cref="P:IControlManager.AllowDelete"/>�������ֹ�����������ϸ���壩�ͳ���Delete��
        /// </summary>
        [Property(Length = 255, NotNull = true)]
        public virtual string AllowDelete
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ���ʾ������ť����ʽ��<see cref="T:Feng.Authority"/>
        /// </summary>
        [Property(Length = 255, NotNull = true)]
        public virtual string AllowOperationInsert
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ���ʾ�༭��ť����ʽ��<see cref="T:Feng.Authority"/>
        /// </summary>
        [Property(Length = 255, NotNull = true)]
        public virtual string AllowOperationEdit
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ���ʾɾ����ť����ʽ��<see cref="T:Feng.Authority"/>
        /// </summary>
        [Property(Length = 255, NotNull = true)]
        public virtual string AllowOperationDelete
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ���ʾ�����༭��ť����ʽ��<see cref="T:Feng.Authority"/>
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string AllowExcelOperation
        {
            get;
            set;
        }
        
        #region "Only to Grid"

        /// <summary>
        /// �Ƿ������ڱ����������ͨ��InsertRow������ʽ��<see cref="T:Feng.Authority"/>
        /// </summary>
        [Property(Length = 255)]
        public virtual string AllowInnerInsert
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ������ڱ����ֱ�ӱ༭����ʽ��<see cref="T:Feng.Authority"/>
        /// </summary>
        [Property(Length = 255)]
        public virtual string AllowInnerEdit
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ������ڱ����ͨ���Ҽ��˵�ɾ������RowSelector���Ҽ�������ʽ��<see cref="T:Feng.Authority"/>
        /// </summary>
        [Property(Length = 255)]
        public virtual string AllowInnerDelete
        {
            get;
            set;
        }

        /// <summary>
        /// ���SummaryRow�ı��⣬��ʽ��<see cref="P:GridColumnInfo.StateTitle"/>
        /// </summary>
        [Property(Length = 255)]
        public virtual string StatTitle
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ�ֻ��
        /// </summary>
        [Property(Length = 255)]
        public virtual string ReadOnly
        {
            get;
            set;
        }

        /// <summary>
        /// �˱���Ƿ�ɼ�����ʽ��<see cref="T:Feng.Authority"/>
        /// </summary>
        [Property(Length = 255, NotNull = true)]
        public virtual string Visible
        {
            get;
            set;
        }

        /// <summary>
        /// �˱����Ϊ��ϸ����Ƿ�ɼ�����ʽ��<see cref="T:Feng.Permission"/>
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string VisibleAsDetail
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ���ʾ������
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string AllowInnerSearch
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ���ʾɸѡ��
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string AllowInnerFilter
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ���ʾGrid���ò˵�
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string AllowInnerMenu
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ���ʾģ���ı�ɸѡ��
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string AllowInnerTextFilter
        {
            get;
            set;
        }
        #endregion
    }
}