using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Feng
{
    /// <summary>
    /// �˵���Ϣ���������ɳ��򡰹��ܡ�ģ���µĸ����Ӳ˵���֧���Ӳ˵���
    /// </summary>
    [Class(0, Name = "Feng.MenuInfo", Table = "AD_Menu", OptimisticLock = OptimisticLockMode.Version)]
    [Cache(1, Usage = CacheUsage.NonStrictReadWrite)]
    [Serializable]
    public class MenuInfo : BaseADEntity
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
        /// ͼ���ļ���������ͼ����Resource��Ŀ�¡�
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string ImageName
        {
            get;
            set;
        }

        /// <summary>
        /// �˲˵��������Ķ�����������˵��������Ķ�������<see cref="ActionInfo"/>
        /// </summary>
        [ManyToOne(ForeignKey = "FK_Menu_Action")]
        public virtual ActionInfo Action
        {
            get;
            set;
        }

        ///// <summary>
        ///// �˲˵��������Ķ�����������˵��������Ķ�������<see cref="ActionInfo"/>
        ///// </summary>
        //[Property(Column = "ActionId", NotNull = false)]
        //public virtual long? ActionId
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// �����������Ϊ���壬�����Ƿ���ģʽ�Ի�����ʾ��
        /// Ĭ��Ϊ��ģʽ�Ի���Ϊ�ര��ṹ�µ��Ӵ���
        /// </summary>
        [Property(NotNull = true)]
        public virtual bool AsDialog
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
        /// �����˵�������ȷ���˵��ĸ��ӹ�ϵ
        /// </summary>
        [ManyToOne(ForeignKey = "FK_Menu_Parent")]
        public virtual MenuInfo ParentMenu
        {
            get;
            set;
        }

        ///// <summary>
        ///// �����˵�Id������ȷ���˵��ĸ��ӹ�ϵ
        ///// </summary>
        //[Property(Column = "ParentMenuId", NotNull = false)]
        //public virtual long? ParentMenuId
        //{
        //    get;
        //    set;
        //}

        //private IList<MenuInfo> m_childs = new List<MenuInfo>();
        ///// <summary>
        ///// �Ӽ��˵������ڻ�������Զ����ɡ�
        ///// Ϊ�����Ч�ʣ��˵�Ϊһ���Զ��룬Ȼ��������<see cref="ParentMenu"/>���Զ����ɸ��ӹ�ϵ��
        ///// ������ͨ�����ݿ�������NHibernate�Զ���ȡ�Ӳ˵���
        ///// </summary>
        //public virtual IList<MenuInfo> Childs
        //{
        //    get { return m_childs; }
        //}

        /// <summary>
        /// �Ӽ��˵�(ͨ�����ݿ��ȡ��ΪLazyLoad��Proxy)
        /// </summary>
        [Bag(0, Cascade = "none", Inverse = true, OrderBy = "SeqNo")]
        [Key(1, Column = "ParentMenu")]
        [Index(2, Column = "SeqNo", Type = "int")]
        [OneToMany(3, ClassType = typeof(MenuInfo), NotFound = NotFoundMode.Ignore)]
        public virtual IList<MenuInfo> ChildMenus
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ�ɼ�����ʽ��<see cref="T:Feng.Authority"/>
        /// </summary>
        [Property(Length = 100, NotNull = false)]
        public virtual string Visible 
        { 
            get; 
            set;
        }

        // Todo: �����MultiComboBox���޸�
        ///// <summary>
        ///// ��ݼ�
        ///// </summary>
        //[Property(NotNull = false)]
        //public virtual System.Windows.Forms.Keys? Shortcut
        //{
        //    get;
        //    set; 
        //}
    }
}