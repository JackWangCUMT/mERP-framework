using System;
using System.Collections.Generic;
using System.Text;

namespace Feng
{
    /// <summary>
    /// Application Dictionary Info Bll
    /// </summary>
    public class ADInfoBll : Singleton<ADInfoBll>
    {
        public ADInfoBll()
        {
            if (System.Configuration.ConfigurationManager.ConnectionStrings[Feng.Windows.Utils.SecurityHelper.DataConnectionStringName] == null)
            {
                m_dal = new ADInfoDal();
                m_useWSWindowTab = true;
            }
            else
            {
                m_dal = new Feng.NH.ADInfoDal();
                m_useWSWindowTab = false;
            }
        }
        private ADInfoDal m_dal;

        private string GetCacheKey<T>(string Name, string otherKey)
             where T : class
        {
            if (string.IsNullOrEmpty(otherKey))
            {
                return string.Format("ApplicationInfo, {0}:{1}", typeof(T).FullName, Name);
            }
            else
            {
                return string.Format("ApplicationInfo, {0}:{1}, {2}", typeof(T).FullName, Name, otherKey);
            }
        }

        private string GetCacheKey<T>(string Name)
            where T : class
        {
            return GetCacheKey<T>(Name, null);
        }

        #region "Get Common Info"
        internal T GetInfo<T>(string idName)
            where T : class, new()
        {
            try
            {
                return m_dal.GetInfo<T>(idName);
            }
            catch (Exception ex)
            {
                ExceptionProcess.ProcessWithResume(ex);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<T> GetInfos<T>()
            where T : class, new()
        {
            try
            {
                return Cache.TryGetCache<IList<T>>(GetCacheKey<T>("All"), new Func<IList<T>>(delegate()
                {
                    return m_dal.GetInfos<T>();
                }));
            }
            catch (Exception ex)
            {
                ExceptionProcess.ProcessWithResume(ex);
            }
            return EmptyInstance.GetEmpty<List<T>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public IList<T> GetInfos<T>(string queryString)
            where T : class, new()
        {
            try
            {
                return Cache.TryGetCache<IList<T>>(GetCacheKey<T>(queryString), new Func<IList<T>>(delegate()
                {
                    return m_dal.GetInfos<T>(queryString);
                }));
            }
            catch (Exception ex)
            {
                ExceptionProcess.ProcessWithResume(ex);
            }
            return EmptyInstance.GetEmpty<List<T>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IList<T> GetInfos<T>(string queryString, Dictionary<string, object> parameters)
             where T : class, new()
        {
            try
            {
                return m_dal.GetInfos<T>(queryString, parameters);
            }
            catch (Exception ex)
            {
                ExceptionProcess.ProcessWithResume(ex);
            }
            return EmptyInstance.GetEmpty<List<T>>();
        }
        #endregion

        #region "BufferData"
        /// <summary>
        /// GetWindowTabEventInfos
        /// </summary>
        /// <param name="windowTabId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList<WindowTabEventInfo> GetWindowTabEventInfos(string windowTabId, WindowTabEventManagerType type)
        {
            return Cache.TryGetCache<IList<WindowTabEventInfo>>(GetCacheKey<WindowTabEventInfo>(windowTabId, type.ToString()), new Func<IList<WindowTabEventInfo>>(delegate()
            {
                IList<WindowTabEventInfo> list = GetInfos<WindowTabEventInfo>("ParentWindowTab.ID = :windowTabId and ManagerType = :type",
                new Dictionary<string, object> { { "windowTabId", windowTabId }, { "type", type } });

                return list;
            }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public ResourceInfo GetResourceInfo(string resourceName, ResourceType resourceType)
        {
            return Cache.TryGetCache<ResourceInfo>(GetCacheKey<ResourceInfo>(resourceName), new Func<ResourceInfo>(delegate()
            {
                IList<ResourceInfo> list = GetInfos<ResourceInfo>("ResourceName = :resourceName and ResourceType = :resourceType",
                new Dictionary<string, object> { { "resourceName", resourceName }, { "resourceType", resourceType } });

                System.Diagnostics.Debug.Assert(list.Count <= 1, "Too much resource with same name!");
                if (list.Count == 0)
                    return null;
                else
                    return list[0];
            }));
        }


        /// <summary>
        /// �õ�����<see cref="MenuInfo"/>��(Parent is null)
        /// </summary>
        /// <returns>�����˵���Ϣ</returns>
        public IList<MenuInfo> GetTopMenuInfos()
        {
            try
            {
                IList<MenuInfo> listAll = null;
                listAll = Cache.TryGetCache<IList<MenuInfo>>(GetCacheKey<MenuInfo>("All"), new Func<IList<MenuInfo>>(delegate()
                {
                    try
                    {
                        return GetInfos<MenuInfo>();
                    }
                    catch (Exception ex)
                    {
                        ExceptionProcess.ProcessWithResume(ex);
                        return null;
                    }
                }));

                if (listAll == null)
                    return null;

                ICache c = ServiceProvider.GetService<ICache>();
                if (c != null)
                {
                    foreach (MenuInfo info in listAll)
                    {
                        c.Put(GetCacheKey<MenuInfo>(info.ID), info);
                    }
                }

                IList<MenuInfo> listTop = new List<MenuInfo>();
                foreach (MenuInfo menu in listAll)
                {
                    if (menu.ParentMenu == null)
                    {
                        listTop.Add(menu);
                        SearchMenuChilds(menu, listAll);
                    }
                }

                return listTop;
            }
            catch (Exception ex)
            {
                ExceptionProcess.ProcessWithResume(ex);
                return null;
            }
        }

        private void SearchMenuChilds(MenuInfo parent, IList<MenuInfo> listAll)
        {
            parent.ChildMenus = new List<MenuInfo>();
            foreach (MenuInfo menu in listAll)
            {
                if (menu.ParentMenu != null
                    && menu.ParentMenu.ID == parent.ID)
                {
                    menu.ParentMenu = parent;
                    parent.ChildMenus.Add(menu);

                    SearchMenuChilds(menu, listAll);
                }
            }
        }

        /// <summary>
        /// ����<see cref="P:Feng.MenuInfo.Name"/>�õ�<see cref="MenuInfo"/>����
        /// </summary>
        /// <param name="menuName"></param>
        /// <returns>�˵�������Ϣ</returns>
        public MenuInfo GetMenuInfo(string menuName)
        {
            return Cache.TryGetCache<MenuInfo>(GetCacheKey<MenuInfo>(menuName), new Func<MenuInfo>(delegate()
            {
                return GetInfo<MenuInfo>(menuName);
            }));
        }

        private const string m_defaultName = "Default";
        private GridInfo m_defaultGridInfo = new GridInfo
        {
            ID = "defaultGridInfo",
            AllowInsert = "TRUE",
            AllowEdit = "TRUE",
            AllowDelete = "TRUE",
            AllowOperationInsert = "TRUE",
            AllowOperationEdit = "TRUE",
            AllowOperationDelete = "TRUE",
            AllowInnerInsert = "TRUE",
            AllowInnerEdit = "TRUE",
            AllowInnerDelete = "TRUE",
            Visible = "TRUE",
            AllowExcelOperation = "FALSE",
            AllowInnerFilter = null,
            AllowInnerMenu = null,
            AllowInnerSearch = null,
            AllowInnerTextFilter = null
        };

        /// <summary>
        /// ����<see cref="GridInfo.GridName"/>�õ�<see cref="GridInfo"/>����
        /// </summary>
        /// <param name="gridName"></param>
        /// <returns>���������Ϣ</returns>
        public GridInfo GetGridInfo(string gridName)
        {
            return Cache.TryGetCache<GridInfo>(GetCacheKey<GridInfo>(gridName), new Func<GridInfo>(delegate()
                {
                    try
                    {
                        IList<GridInfo> gridInfos = GetInfos<GridInfo>(string.Format("GridName = '{0}'", gridName));
                        if (gridInfos.Count > 0)
                        {
                            return gridInfos[0];
                        }
                        else
                        {
                            gridInfos = GetInfos<GridInfo>(string.Format("GridName = '{0}'", m_defaultName));
                            if (gridInfos.Count > 0)
                            {
                                return gridInfos[0];
                            }
                            else
                            {
                                return m_defaultGridInfo;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionProcess.ProcessWithResume(ex);
                        return null;
                    }
                }));
        }

        /// <summary>
        /// ����<see cref="GridColumnInfo.GridName"/>�õ�<see cref="GridColumnInfo"/>����
        /// </summary>
        /// <param name="gridName"></param>
        /// <returns>�����������Ϣ</returns>
        public IList<GridColumnInfo> GetGridColumnInfos(string gridName)
        {
            return Cache.TryGetCache<IList<GridColumnInfo>>(GetCacheKey<GridColumnInfo>(gridName), new Func<IList<GridColumnInfo>>(delegate()
                {
                    try
                    {
                        IList<GridColumnInfo> ret = GetInfos<GridColumnInfo>(string.Format("GridName = '{0}'", gridName));
                        return ret;
                    }
                    catch (Exception ex)
                    {
                        ExceptionProcess.ProcessWithResume(ex);
                        return null;
                    }
                }));
        }

        private GridRowInfo m_defaultGridRowInfo = new GridRowInfo
        {
            ID = "defaultGridRowInfo",
            ReadOnly = "FALSE",
            AllowDelete = "TRUE",
            Visible = "TRUE",
            DetailGridReadOnly = null
        };

        /// <summary>
        /// ����<see cref="GridRowInfo.GridName"/>�õ�<see cref="GridRowInfo"/>����
        /// </summary>
        /// <param name="gridName"></param>
        /// <returns>�����������Ϣ</returns>
        public GridRowInfo GetGridRowInfo(string gridName)
        {
            return Cache.TryGetCache<GridRowInfo>(GetCacheKey<GridRowInfo>(gridName), new Func<GridRowInfo>(delegate()
                {
                    try
                    {
                        IList<GridRowInfo> ret = GetInfos<GridRowInfo>(string.Format("GridName = '{0}'", gridName));
                        if (ret.Count > 0)
                        {
                            return ret[0];
                        }
                        else
                        {
                            ret = GetInfos<GridRowInfo>(string.Format("GridName = '{0}'", m_defaultName));
                            if (ret.Count > 0)
                            {
                                return ret[0];
                            }
                            else
                            {
                                return m_defaultGridRowInfo;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionProcess.ProcessWithResume(ex);
                        return null;
                    }
                }));
        }

        /// <summary>
        /// ����<see cref="GridCellInfo.GridName"/>�õ�<see cref="GridCellInfo"/>����
        /// </summary>
        /// <param name="gridName"></param>
        /// <returns>���Ԫ��������Ϣ</returns>
        public IList<GridCellInfo> GetGridCellInfos(string gridName)
        {
            return Cache.TryGetCache<IList<GridCellInfo>>(GetCacheKey<GridCellInfo>(gridName), new Func<IList<GridCellInfo>>(delegate()
                {
                    try
                    {
                        IList<GridCellInfo> ret = GetInfos<GridCellInfo>(string.Format("GridName = '{0}'", gridName));
                        return ret;
                    }
                    catch (Exception ex)
                    {
                        ExceptionProcess.ProcessWithResume(ex);
                        return null;
                    }
                }));
        }


        /// <summary>
        /// ����<see cref="GridRelatedInfo.GridName"/>�õ�<see cref="GridRelatedInfo"/>����
        /// </summary>
        /// <param name="gridName"></param>
        /// <returns>��������Ϣ������Ϣ</returns>
        public IList<GridRelatedInfo> GetGridRelatedInfo(string gridName)
        {
            if (string.IsNullOrEmpty(gridName))
            {
                return null;
            }

            return Cache.TryGetCache<IList<GridRelatedInfo>>(GetCacheKey<GridRelatedInfo>(gridName), new Func<IList<GridRelatedInfo>>(delegate()
            {
                try
                {
                    IList<GridRelatedInfo> ret = GetInfos<GridRelatedInfo>(string.Format("GridName = '{0}'", gridName));
                    return ret;
                }
                catch (Exception ex)
                {
                    ExceptionProcess.ProcessWithResume(ex);
                    return null;
                }
            }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridName"></param>
        /// <returns></returns>
        public IList<GridRelatedAddressInfo> GetGridRelatedAddressInfo(string gridName)
        {
            if (string.IsNullOrEmpty(gridName))
            {
                return null;
            }

            return Cache.TryGetCache<IList<GridRelatedAddressInfo>>(GetCacheKey<GridRelatedAddressInfo>(gridName), new Func<IList<GridRelatedAddressInfo>>(delegate()
            {
                try
                {
                    return GetInfos<GridRelatedAddressInfo>(string.Format("GridName = '{0}'", gridName));
                }
                catch (Exception ex)
                {
                    ExceptionProcess.ProcessWithResume(ex);
                    return null;
                }
            }));
        }

        /// <summary>
        /// ����<see cref="GridFilterInfo.GridName"/>�õ�<see cref="GridFilterInfo"/>����
        /// </summary>
        /// <param name="gridName"></param>
        /// <returns>���ɸѡ������Ϣ</returns>
        public IList<GridFilterInfo> GetGridFilterInfos(string gridName)
        {
            return Cache.TryGetCache<IList<GridFilterInfo>>(GetCacheKey<GridFilterInfo>(gridName), new Func<IList<GridFilterInfo>>(delegate()
            {
                try
                {
                    return GetInfos<GridFilterInfo>(string.Format("GridName = '{0}'", gridName));
                }
                catch (Exception ex)
                {
                    ExceptionProcess.ProcessWithResume(ex);
                    return null;
                }
            }));
        }

        /// <summary>
        /// ����<see cref="GridGroupInfo.GridName"/>�õ�<see cref="GridGroupInfo"/>����
        /// </summary>
        /// <param name="gridName"></param>
        /// <returns>������������Ϣ</returns>
        public IList<GridGroupInfo> GetGridGroupInfos(string gridName)
        {
            if (string.IsNullOrEmpty(gridName))
            {
                return null;
            }

            return Cache.TryGetCache<IList<GridGroupInfo>>(GetCacheKey<GridGroupInfo>(gridName), new Func<IList<GridGroupInfo>>(delegate()
            {
                try
                {
                    return GetInfos<GridGroupInfo>(string.Format("GridName = '{0}'", gridName));
                }
                catch (Exception ex)
                {
                    ExceptionProcess.ProcessWithResume(ex);
                    return null;
                }
            }));
        }

        /// <summary>
        /// ����<see cref="TaskInfo.GroupName"/>�õ�<see cref="TaskInfo"/>����
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns>����������Ϣ</returns>
        public IList<TaskInfo> GetTaskInfo(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return null;
            }

            return Cache.TryGetCache<IList<TaskInfo>>(GetCacheKey<TaskInfo>(groupName), new Func<IList<TaskInfo>>(delegate()
            {
                try
                {
                    return GetInfos<TaskInfo>(string.Format("GroupName = '{0}'", groupName));
                }
                catch (Exception ex)
                {
                    ExceptionProcess.ProcessWithResume(ex);
                    return null;
                }
            }));
        }

        /// <summary>
        /// ����<see cref="GridColumnWarningInfo.GroupName"/>�õ�<see cref="GridColumnWarningInfo"/>����
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns>���ʾ��Ԫ��������Ϣ</returns>
        public IList<GridColumnWarningInfo> GetWarningInfo(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return null;
            }

            return Cache.TryGetCache<IList<GridColumnWarningInfo>>(GetCacheKey<GridColumnWarningInfo>(groupName), new Func<IList<GridColumnWarningInfo>>(delegate()
            {
                try
                {
                    return GetInfos<GridColumnWarningInfo>(string.Format("GroupName = '{0}'", groupName));
                }
                catch (Exception ex)
                {
                    ExceptionProcess.ProcessWithResume(ex);
                    return null;
                }
            }));
        }


        ///// <summary>
        ///// ����<see cref="P:Feng.MenuPropertyInfo.Name"/>�õ�<see cref="MenuPropertyInfo"/>����
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns>�˵���������ť������Ϣ</returns>
        //public MenuPropertyInfo GetMenuPropertyInfo(string name)
        //{
        //    try
        //    {
        //        IList<MenuPropertyInfo> list = GetMenuPropertyInfo(name);
        //        if (list.Count > 0)
        //        {
        //            return list[0];
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionProcess.ProcessWithResume(ex);
        //        return null;
        //    }
        //}

        

        /// <summary>
        /// ����<see cref="WindowSelectInfo.GroupName"/>�õ�<see cref="CustomSearchInfo"/>����
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns>�Զ������������Ϣ</returns>
        public IList<CustomSearchInfo> GetCustomSearchInfo(string groupName)
        {
            return Cache.TryGetCache<IList<CustomSearchInfo>>(GetCacheKey<CustomSearchInfo>(groupName), new Func<IList<CustomSearchInfo>>(delegate()
            {
                try
                {
                    return GetInfos<CustomSearchInfo>(string.Format("GroupName = '{0}'", groupName));
                }
                catch (Exception ex)
                {
                    ExceptionProcess.ProcessWithResume(ex);
                    return null;
                }
            }));
        }

        /// <summary>
        /// ����<see cref="WindowSelectInfo.GroupName"/>�õ�<see cref="WindowSelectInfo"/>����
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns>ѡ����������Ϣ</returns>
        public IList<WindowSelectInfo> GetWindowSelectInfo(string groupName)
        {
            return Cache.TryGetCache<IList<WindowSelectInfo>>(GetCacheKey<WindowSelectInfo>(groupName), new Func<IList<WindowSelectInfo>>(delegate()
            {
                try
                {
                    return GetInfos<WindowSelectInfo>(string.Format("GroupName = '{0}'", groupName));
                }
                catch (Exception ex)
                {
                    ExceptionProcess.ProcessWithResume(ex);
                    return null;
                }
            }));
        }

        /// <summary>
        /// ����<see cref="P:Feng.WindowInfo.Name"/>�õ�<see cref="WindowInfo"/>����
        /// </summary>
        /// <param name="windowName"></param>
        /// <returns>��ͨ����������Ϣ</returns>
        public WindowInfo GetWindowInfo(string windowName)
        {
            return Cache.TryGetCache<WindowInfo>(GetCacheKey<WindowInfo>(windowName), new Func<WindowInfo>(delegate()
            {
                WindowInfo r = null;
                ICache c = ServiceProvider.GetService<ICache>();
                IEnumerable<WindowInfo> list = GetInfos<WindowInfo>();
                foreach (WindowInfo info in list)
                {
                    if (c != null)
                    {
                        c.Put(GetCacheKey<WindowInfo>(info.ID), info);
                    }

                    if (info.ID == windowName)
                    {
                        r = info;
                    }
                }
                return r;
            }));

        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public IList<WindowTabInfo> GetWindowTabInfos()
        //{
        //    try
        //    {
        //        return GetWindowTabInfos();
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionProcess.ProcessWithResume(ex);
        //        return null;
        //    }
        //}

        private bool m_useWSWindowTab = false;
        public WindowTabInfo GetWindowTabInfo(string windowTabId)
        {
            if (m_useWSWindowTab && !windowTabId.StartsWith("WS_"))
            {
                var r = GetWindowTabInfoReal("WS_" + windowTabId);
                if (r == null)
                {
                    r = GetWindowTabInfoReal("WSV_" + windowTabId);
                }
                if (r != null)
                    return r;
            }
            return GetWindowTabInfoReal(windowTabId);
        }

        /// <summary>
        /// ����<see cref="P:Feng.WindowTabInfo.Name"/>�õ�<see cref="WindowTabInfo"/>����
        /// </summary>
        /// <param name="windowTabId"></param>
        /// <returns></returns>
        private WindowTabInfo GetWindowTabInfoReal(string windowTabId)
        {
            return Cache.TryGetCache<WindowTabInfo>(GetCacheKey<WindowTabInfo>(windowTabId), new Func<WindowTabInfo>(delegate()
            {
                WindowTabInfo r = null;
                ICache c = ServiceProvider.GetService<ICache>();

                IEnumerable<WindowTabInfo> listAll = GetInfos<WindowTabInfo>();
                foreach (WindowTabInfo info in listAll)
                {
                    if (info.Parent == null)
                    {
                        SearchTabChilds(info, listAll);
                    }

                    if (c != null)
                    {
                        c.Put(GetCacheKey<WindowTabInfo>(info.ID), info);
                    }

                    if (info.ID == windowTabId)
                    {
                        r = info;
                    }
                }
                return r;
            }));
        }

        /// <summary>
        /// ����<see cref="P:Feng.TabInfo.WindowId"/>�õ�<see cref="WindowTabInfo"/>����
        /// </summary>
        /// <param name="windowId"></param>
        /// <returns>��ͨ���ڷּ�������Ϣ</returns>
        public IList<WindowTabInfo> GetWindowTabInfosByWindowId(string windowId)
        {
            return Cache.TryGetCache<IList<WindowTabInfo>>(GetCacheKey<WindowTabInfo>(windowId, "ListForWindowId"), new Func<IList<WindowTabInfo>>(delegate()
            {
                IList<WindowTabInfo> r = null;
                ICache c = ServiceProvider.GetService<ICache>();

                IEnumerable<WindowTabInfo> listAll = GetInfos<WindowTabInfo>();
                foreach (WindowTabInfo info in listAll)
                {
                    if (info.Window == null)
                        continue;
                    if (info.Parent == null)
                    {
                        SearchTabChilds(info, listAll);

                        IList<WindowTabInfo> list = new List<WindowTabInfo> { info };

                        if (c != null)
                        {
                            c.Put(GetCacheKey<WindowTabInfo>(info.Window.ID, "ListForWindowId"), list);
                        }

                        if (info.Window.ID == windowId
                            && (windowId.StartsWith("WS_")
                                || (!m_useWSWindowTab && !info.ID.StartsWith("WS_"))
                                || (m_useWSWindowTab && info.ID.StartsWith("WS_"))))
                        {
                            r = list;
                        }
                    }
                }
                return r;
            }));
        }

        private void SearchTabChilds(WindowTabInfo parent, IEnumerable<WindowTabInfo> listAll)
        {
            parent.ChildTabs = new List<WindowTabInfo>();
            foreach (WindowTabInfo menu in listAll)
            {
                if (menu.Parent != null
                    && menu.Parent.ID == parent.ID)
                {
                    menu.Parent = parent;
                    parent.ChildTabs.Add(menu);

                    SearchTabChilds(menu, listAll);
                }
            }
        }

        /// <summary>
        /// ����<see cref="P:Feng.FormInfo.Name"/>�õ�<see cref="FormInfo"/>����
        /// </summary>
        /// <param name="formName"></param>
        /// <returns>���ⴰ��������Ϣ</returns>
        public FormInfo GetFormInfo(string formName)
        {
            return Cache.TryGetCache<FormInfo>(GetCacheKey<FormInfo>(formName), new Func<FormInfo>(delegate()
            {
                return GetInfo<FormInfo>(formName);
            }));
        }

        /// <summary>
        /// ����<see cref="P:Feng.ProcessInfo.Name"/>�õ�<see cref="ProcessInfo"/>����
        /// </summary>
        /// <param name="processName"></param>
        /// <returns>����������Ϣ</returns>
        public ProcessInfo GetProcessInfo(string processName)
        {
            try
            {
                return Cache.TryGetCache<ProcessInfo>(GetCacheKey<ProcessInfo>(processName), new Func<ProcessInfo>(delegate()
                {
                    return GetInfo<ProcessInfo>(processName);
                }));
            }
            catch (Exception ex)
            {
                ExceptionProcess.ProcessWithResume(ex);
                return null;
            }
        }

        /// <summary>
        /// ����<see cref="P:Feng.EventProcessInfo.EventName"/>�õ�<see cref="EventProcessInfo"/>����
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns>����������Ϣ</returns>
        public IList<EventProcessInfo> GetEventProcessInfos(string eventName)
        {
            try
            {
                return Cache.TryGetCache<IList<EventProcessInfo>>(GetCacheKey<EventProcessInfo>(eventName), new Func<IList<EventProcessInfo>>(delegate()
                {
                    return GetInfos<EventProcessInfo>(string.Format("EventName = '{0}'", eventName));
                }));
            }
            catch (Exception ex)
            {
                ExceptionProcess.ProcessWithResume(ex);
                return null;
            }
        }


        /// <summary>
        /// ����<see cref="P:WindowMenuInfo.Name"/>�õ�<see cref="WindowMenuInfo"/>����
        /// </summary>
        /// <param name="windowName"></param>
        /// <returns>��ͨ���ڹ�������ť������Ϣ</returns>
        public IList<WindowMenuInfo> GetWindowMenuInfo(string windowName)
        {
            try
            {
                return Cache.TryGetCache<IList<WindowMenuInfo>>(GetCacheKey<WindowMenuInfo>(windowName), new Func<IList<WindowMenuInfo>>(delegate()
                {
                    return GetInfos<WindowMenuInfo>(string.Format("Window.ID = '{0}'", windowName));
                }));
            }
            catch (Exception ex)
            {
                ExceptionProcess.ProcessWithResume(ex);
                return null;
            }
        }

        /// <summary>
        /// ����<see cref="P:Feng.NameValueMappingInfo.Name"/>�õ�<see cref="NameValueMappingInfo"/>����
        /// </summary>
        /// <param name="nvName"></param>
        /// <returns>ϵͳ���ݱ�����Ϣ</returns>
        public NameValueMappingInfo GetNameValueMappingInfo(string nvName)
        {
            return Cache.TryGetCache<NameValueMappingInfo>(GetCacheKey<NameValueMappingInfo>(nvName), new Func<NameValueMappingInfo>(delegate()
            {
                return GetInfo<NameValueMappingInfo>(nvName);
            }));
        }


        /// <summary>
        /// ����<see cref="P:Feng.ActionInfo.Name"/>�õ�<see cref="ActionInfo"/>����
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns>�õ�<see cref="T:Feng.ActionInfo"/>��Ϣ</returns>
        public ActionInfo GetActionInfo(string actionName)
        {
            return Cache.TryGetCache<ActionInfo>(GetCacheKey<ActionInfo>(actionName), new Func<ActionInfo>(delegate()
            {
                ActionInfo r = null;
                ICache c = ServiceProvider.GetService<ICache>();

                IEnumerable<ActionInfo> list = GetInfos<ActionInfo>();
                foreach (ActionInfo info in list)
                {
                    if (c != null)
                    {
                        c.Put(GetCacheKey<ActionInfo>(info.ID), info);
                    }

                    if (info.ID == actionName)
                    {
                        r = info;
                    }
                }
                return r;
            }));
        }

        ///// <summary>
        ///// �õ�����<see cref="AlertRuleInfo"/>����
        ///// </summary>
        ///// <returns>����������Ϣ</returns>
        //public IList<AlertRuleInfo> GetAlertRuleInfo()
        //{
        //    try
        //    {
        //        return GetAlertRuleInfo();
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionProcess.ProcessWithResume(ex);
        //        return null;
        //    }
        //}

        /// <summary>
        /// ����<see cref="P:ReportInfo.Name"/>�õ�<see cref="ReportInfo"/>��Ϣ
        /// </summary>
        /// <param name="reportName"></param>
        /// <returns></returns>
        public ReportInfo GetReportInfo(string reportName)
        {
            try
            {
                return Cache.TryGetCache<ReportInfo>(GetCacheKey<ReportInfo>(reportName), new Func<ReportInfo>(delegate()
                {
                    return GetInfo<ReportInfo>(reportName);
                }));
            }
            catch (Exception ex)
            {
                ExceptionProcess.ProcessWithResume(ex);
                return null;
            }
        }

        /// <summary>
        /// ����<see cref="P:ReportDataInfo.ReportName"/>�õ�<see cref="ReportDataInfo"/>��Ϣ
        /// </summary>
        /// <param name="reportName"></param>
        /// <returns></returns>
        public IList<ReportDataInfo> GetReportDataInfo(string reportName)
        {
            try
            {
                return Cache.TryGetCache<IList<ReportDataInfo>>(GetCacheKey<ReportDataInfo>(reportName), new Func<IList<ReportDataInfo>>(delegate()
                {
                    return GetInfos<ReportDataInfo>(string.Format("Report.ID = '{0}'", reportName));
                }));
            }
            catch (Exception ex)
            {
                ExceptionProcess.ProcessWithResume(ex);
                return null;
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public IList<ServerTaskScheduleInfo> GetTaskScheduleInfo()
        //{
        //    try
        //    {
        //        return m_dal.GetTaskScheduleInfo();
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionProcess.ProcessWithResume(ex);
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public IList<WebServiceInfo> GetWebServiceInfos()
        //{
        //    try
        //    {
        //        return m_dal.GetWebServiceInfos();
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionProcess.ProcessWithResume(ex);
        //        return null;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public IList<ParamCreatorInfo> GetParamCreatorInfos(string paramName)
        {
            try
            {
                return Cache.TryGetCache<IList<ParamCreatorInfo>>(GetCacheKey<ParamCreatorInfo>(paramName), new Func<IList<ParamCreatorInfo>>(delegate()
                {
                    return GetInfos<ParamCreatorInfo>(string.Format("ParamName = '{0}'", paramName));
                }));
            }
            catch (Exception ex)
            {
                ExceptionProcess.ProcessWithResume(ex);
                return null;
            }
        }
#endregion

        #region "NoBufferUserData"
        /// <summary>
        /// �����û����õ��û�����
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>�û�������Ϣ</returns>
        public UserConfigurationInfo GetUserConfigurationInfo(string userName)
        {
            try
            {
                var list = m_dal.GetInfos<UserConfigurationInfo>(string.Format("UserName = '{0}'", userName));
                if (list != null && list.Count > 0)
                    return list[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                ExceptionProcess.ProcessWithResume(ex);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        public void GetUserConfigurationData(UserConfigurationInfo userInfo)
        {
            try
            {
                m_dal.GetUserConfigurationData(userInfo);
            }
            catch (Exception ex)
            {
                ExceptionProcess.ProcessWithResume(ex);
                return;
            }
        }
        /// <summary>
        /// ���������û�����
        /// </summary>
        /// <param name="userInfo"></param>
        public void SaveOrUpdateUserConfigurationInfo(UserConfigurationInfo userInfo)
        {
            m_dal.SaveOrUpdateUserConfigurationInfo(userInfo);
        }

        /// <summary>
        /// �õ�����<see cref="AlertInfo"/>����
        /// </summary>
        /// <returns>������Ϣ</returns>
        public IList<AlertInfo> GetAlertInfo()
        {
            try
            {
                return GetInfos<AlertInfo>(string.Format("IsFixed = false and (RecipientUser = '{0}' or RecipientRole in '{1}",
                    SystemConfiguration.UserName, Feng.Utils.ConvertHelper.StringArrayToString(SystemConfiguration.Roles)));
            }
            catch (Exception ex)
            {
                ExceptionProcess.ProcessWithResume(ex);
                return null;
            }
        }
        #endregion
    }
}