//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Reflection;
//using System.Data;
//using System.ComponentModel;
//using System.Diagnostics;
//using Feng.Collections;
//using Feng.Utils;

//namespace Feng
//{
//    /// <summary>
//    /// ���ƹ��������ɶԶ��ʵ����������������ӡ��޸ġ�ɾ���Ȳ�����
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public abstract class AbstractControlManager<T> : AbstractControlManager, IControlManager<T>
//        where T : class, IEntity
//    {
//        #region "Constructor"

//        /// <summary>
//        /// Consturctor
//        /// </summary>
//        /// <param name="displayManager"></param>
//        protected AbstractControlManager(IDisplayManager<T> displayManager)
//            : base(displayManager)
//        {
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        public IDisplayManager<T> DisplayManagerT
//        {
//            get { return base.DisplayManager as IDisplayManager<T>; }
//        }

//        #endregion

//        # region "Operations"


//        //protected override void DoOnFailedOperation()
//        //{
//            //// Todo: ���ܲ������ֹ�ˢ�¸��á� ��Ҫ����load Detail�� MemoryBll�ȵĹ�ϵ
//            //IList<ISearchExpression> searchExpression = new List<ISearchExpression>();
//            //object id = this.DisplayManager.EntityInfo.EntityType.InvokeMember(this.DisplayManager.EntityInfo.IdName,
//            //                                                                   BindingFlags.GetProperty, null,
//            //                                                                   this.DisplayManagerT.CurrentEntity, null,
//            //                                                                   null, null, null);

//            //searchExpression.Add(SearchExpression.Eq(this.DisplayManager.EntityInfo.IdName, id));
//            //IList<T> list = this.DisplayManagerT.SearchManager.FindData(searchExpression, null) as IList<T>;
//            //Debug.Assert(list.Count <= 1);

//            //if (list.Count == 0)
//            //{
//            //    if (this.State == StateType.Edit
//            //        || this.State == StateType.Delete)
//            //        //|| this.State == StateType.Add)
//            //    {
//            //        this.DisplayManager.Items.RemoveAt(this.DisplayManagerT.Position);
//            //        OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, this.DisplayManagerT.Position));
//            //    }
//            //    else if (this.State == StateType.Add)
//            //    {
//            //        this.DisplayManager.OnPositionChanged(System.EventArgs.Empty);

//            //        // there is no grid row now
//            //        //OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, this.DisplayManagerT.Position));
//            //    }
//            //}
//            //else if (list.Count == 1)
//            //{
//            //    if (this.State == StateType.Edit)
//            //    {
//            //        // �������ô��ڱ༭״̬�������ΪȨ�޸ı�������޸ģ�Ҳ�����������˳��༭״̬��
//            //        // this.CancelEdit();
//            //        this.DisplayManager.Items[this.DisplayManagerT.Position] = list[0];
//            //        OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, this.DisplayManagerT.Position));

//            //        // �����Ѿ������޸ģ����ٽ����޸�״̬
//            //        // this.EditCurrent();
//            //    }
//            //    else if (this.State == StateType.Delete)
//            //    // ||  this.State == StateType.Add
//            //    {
//            //        this.DisplayManager.Items[this.DisplayManagerT.Position] = list[0];
//            //        OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, this.DisplayManagerT.Position));
//            //    }
//            //    else if (this.State == StateType.View)
//            //    {
//            //        this.DisplayManager.Items[this.DisplayManagerT.Position] = list[0];
//            //        OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, this.DisplayManagerT.Position));
//            //    }
//            //}

//            //this.DisplayManager.DisplayCurrent();
//        //}


//        #endregion
//    }
//}