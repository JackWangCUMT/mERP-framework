using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Feng
{
    /// <summary>
    /// ���ڱ��ɸѡ��ť<see cref="T:Feng.Grid.GridFilterToolStripItem"/>����Ϣ
    /// </summary>
    [Class(0, Name = "Feng.GridFilterInfo", Table = "AD_Grid_Filter", OptimisticLock = OptimisticLockMode.Version)]
    [Cache(1, Usage = CacheUsage.NonStrictReadWrite)]
    [Serializable]
    public class GridFilterInfo : BaseADEntity
    {
        /// <summary>
        /// ��ʾ����
        /// </summary>
        [Property(Length = 50, NotNull = true)]
        public virtual string Text
        {
            get;
            set;
        }

        /// <summary>
        /// ����������ڶ�����Ӧ����ɸѡ����
        /// </summary>
        [Property(Length = 100, NotNull = true)]
        public virtual string GridName
        {
            get;
            set;
        }

        /// <summary>
        /// Grid��ɸѡRow�ķ�ʽ��
        /// ��ʽ���£�
        /// Grid1����:���ʽ1;Grid2����:���ʽ2;...
        /// ���ʽ��$..$���ô��е�Row��Ϣ����ΪColumn���ƣ�������Ϊһ����ʽ��
        /// ���� grdTicket:$���߱�����־$="��";  grdFeeSr:$���������$="�������շ�"; grdFeeDd:$���������$=""; grdFeeCb:$���������$="�������߷�"
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string RowExpression
        {
            get;
            set;
        }

        /// <summary>
        /// Grid��ɸѡColumn�ķ�ʽ��
        /// ��<see cref="RowExpression"/>�����ʽĿǰֻ֧��$PropertyName$����Ϊ�������ơ�
        /// ���磺grdTicket:$PropertyName$="���ص���" OR $PropertyName$="���߱�����־" OR $PropertyName$="��������" OR $PropertyName$="ʵ�ʺ���ʱ��"
        /// </summary>
        [Property(Length = 255, NotNull = false)]
        public virtual string ColumnExpression
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
    }
}