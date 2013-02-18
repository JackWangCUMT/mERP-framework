using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Feng
{
    /// <summary>
    /// ��Ԫ��������,����<see cref="T:Grid.DataBoundGrid"/>��<see cref="T:Grid.DataUnboundGrid"/>�ġ�
    /// ���ڱ�������Ϣ����<see cref="GridInfo"/>��<see cref="GridColumnInfo"/>��<see cref="GridRowInfo"/>��<see cref="GridCellInfo"/>
    /// ���������ݣ�Ҳ�������ô����ϵ�<see cref="Feng.IDataControl"/>�ؼ�����
    /// </summary>
    [Class(0, Name = "Feng.GridCellInfo", Table = "AD_Grid_Cell", OptimisticLock = OptimisticLockMode.Version)]
    [Cache(1, Usage = CacheUsage.NonStrictReadWrite)]
    [Serializable]
    public class GridCellInfo : BaseADEntity
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
        /// Ҫ���õĵ�Ԫ�񡣺�<see cref="GridColumnInfo.GridColumnName"/>��Ӧ
        /// </summary>
        [Property(Column = "FullPropertyName", Length = 255, NotNull = true)]
        public virtual string GridColumName
        {
            get;
            set;
        }

        /// <summary>
        /// ��Ԫ���Ƿ�ֻ���ı��ʽ����ʽ��<see cref="T:Feng.Permission"/>
        /// ֻ��Trueʱ�Ż����ã�FalseʱĬ�ϣ�ͬ��һ����һ������
        /// GridColumn, GridCell, GridRow ��һΪ True��ΪTrue����GridColumnΪTrue��GridCellΪFalseʱ����ҲΪTrue��
        /// </summary>
        [Property(Length = 255, NotNull = true)]
        public virtual string ReadOnly
        {
            get;
            set;
        }

        /// <summary>
        /// ��Ԫ��༭ʱ�Ƿ�Ҫ��ǿյı��ʽ������ʽ���ΪTrue����༭ʱ���鵥Ԫ��ֵ�Ƿ�Ϊ�գ����Ϊ�����д�����ʾ��
        /// ��ʽ��<see cref="T:Feng.Permission"/>��
        /// �������ȼ�����<see cref="ReadOnly"/>
        /// </summary>
        [Property(Length = 255, NotNull = true)]
        public virtual string NotNull
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