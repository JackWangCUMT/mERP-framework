using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Feng
{
    /// <summary>
    /// ����ж������ݣ�����<see cref="T:Feng.Grid.DataBoundGrid"/>��<see cref="T:Feng.Grid.DataUnboundGrid"/>
    /// ���ڱ�������Ϣ����<see cref="GridInfo"/>��<see cref="GridColumnInfo"/>��<see cref="GridRowInfo"/>��<see cref="GridCellInfo"/>
    /// ���������ݣ�Ҳ�������ô����ϵ�<see cref="Feng.IDataControl"/>�ؼ�����
    /// </summary>
    [Class(0, Name = "Feng.GridRowInfo", Table = "AD_Grid_Row", OptimisticLock = OptimisticLockMode.Version)]
    [Cache(1, Usage = CacheUsage.NonStrictReadWrite)]
    [Serializable]
    public class GridRowInfo : BaseADEntity
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
        /// ������Ƿ�ֻ���ı��ʽ����ʽ��<see cref="T:Feng.Permission"/>
        /// </summary>
        [Property(Length = 500, NotNull = false)]
        public virtual string ReadOnly
        {
            get;
            set;
        }

        /// <summary>
        /// ������Ƿ��ɾ���ı��ʽ����ʽ��<see cref="T:Feng.Permission"/>
        /// </summary>
        [Property(Length = 500, NotNull = false)]
        public virtual string AllowDelete
        {
            get;
            set;
        }

        /// <summary>
        /// ������Ƿ�ɱ༭�ı��ʽ����ʽ��<see cref="T:Feng.Permission"/>
        /// </summary>
        [Property(Length = 500, NotNull = false)]
        public virtual string AllowEdit
        {
            get;
            set;
        }

        /// <summary>
        /// ������Ƿ���ʾ�ı��ʽ����ʽ��<see cref="T:Feng.Permission"/>
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string Visible
        {
            get;
            set;
        }

        /// <summary>
        /// �����������DetailGrid�Ƿ�ֻ���ı��ʽ��
        /// ��ʽΪ��GridName:<see cref="T:Feng.Permission"/>; ...��
        /// </summary>
        [Property(Length = 500, NotNull = false)]
        public virtual string DetailGridReadOnly
        {
            get;
            set;
        }

        /// <summary>
        /// �����������DetailGrid�Ƿ�����ӵı��ʽ��
        /// ��ʽΪ��GridName:<see cref="T:Feng.Permission"/>; ...��
        /// </summary>
        [Property(Length = 500, NotNull = false)]
        public virtual string DetailGridAllowInsert
        {
            get;
            set;
        }

        /// <summary>
        /// ����ɫ�ı��ʽ����Python�﷨
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string BackColor
        {
            get;
            set;
        }

        /// <summary>
        /// ǰ��ɫ�ı��ʽ����Python�﷨
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string ForeColor
        {
            get;
            set;
        }
    }
}