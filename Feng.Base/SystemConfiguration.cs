using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Feng.Utils;

namespace Feng
{
    ///// <summary>
    ///// ��������
    ///// </summary>
    //public enum ApplicationType
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    None = 0,
    //    /// <summary>
    //    /// WinForm
    //    /// </summary>
    //    WinForm = 1,
    //    /// <summary>
    //    /// WPF
    //    /// </summary>
    //    WPF = 2,
    //    /// <summary>
    //    /// Web
    //    /// </summary>
    //    Web = 3
    //}

    /// <summary>
    /// ϵͳȫ�ֱ���
    /// ����Ŀ¼���ã�
    /// Layout: 
    /// Ĭ�����ã�
    /// SearchControlContainer: 
    /// "SearchControlContainer." + "." + m_sm.ManagerName + ".Layout";
    /// in SystemConfiguration.DefaultUserProfile
    /// Grid: 
    /// "MyGrid." + (form != null ? form.Name : "") + "." + grid.GridName + ".Layout" 
    /// "MyGrid." + (form != null ? form.Name : "") + "." + grid.GridName + "." + level.ToString() + "." + detailGrid.GridName + ".Layout"
    /// in SystemConfiguration.DefaultUserProfile
    /// 
    /// MaxResult: "SearchManager." + m_sm.Name
    /// 
    /// �Զ������ã�
    /// SearchControlContainer:  (system.xmls.default)
    /// SystemConfiguration.UserDataDirectory + m_sm.DisplayManager.Name + "\\" + m_sm.Name + "\\" + "*.xmls"
    /// SystemConfiguration.GlobalDataDirectory + ...
    /// Grid:  (system.xmlg.default)
    /// SystemConfiguration.UserDataDirectory + (this.FindForm().Name + "\\") + (this.GridName + "\\") + "*.xmlg"
    /// SystemConfiguration.GlobalDataDirectory + ...
    /// GridReport��
    /// SystemConfiguration.UserDataDirectory + (this.FindForm().Name + "\\") + (this.GridName + "\\") + "*.xml"
    /// SystemConfiguration.GlobalDataDirectory + (this.FindForm().Name + "\\") + (this.GridName + "\\") + "*.xml"
    /// SystemConfiguration.GlobalDataDirectory
    /// ArchiveDetailForm: (system.xmlc.default)
    /// 
    /// PositionPersistForm:
    /// DefaultUserProfile(Todo: SystemConfiguration.UserDataDirectory + (this.Name + "\\") + "PositionPersist.xml")
    /// 
    /// ILayoutControl: ��ȡ���ȼ�����User��Global
    /// </summary>
    public static class SystemConfiguration
    {
        /// <summary>
        /// ����ģʽ
        /// </summary>
        public static bool IsInDebug = false;

        /// <summary>
        /// ���ö��߳�
        /// </summary>
        public static bool UseMultiThread = true;

        /// <summary>
        /// GlobalUserName
        /// </summary>
        public const string GlobalUserName = "Global";

        ///// <summary>
        ///// ��������
        ///// </summary>
        //public static ApplicationType ApplicationType
        //{
        //    get;
        //    set;
        //}

        private static string s_applicationName;

        /// <summary>
        /// ��������������֤��
        /// </summary>
        public static string ApplicationName
        {
            get { return GetAppName(); }
            set { s_applicationName = value; }
        }

        /// <summary>
        /// GetAppName
        /// </summary>
        /// <returns></returns>
        private static string GetAppName()
        {
            if (!string.IsNullOrEmpty(s_applicationName))
            {
                return s_applicationName;
            }
            Assembly clientAssembly = Assembly.GetExecutingAssembly();
            if (clientAssembly == null)
            {
                return null;
            }
            AssemblyName assemblyName = clientAssembly.GetName();
            s_applicationName = assemblyName.Name;
            return s_applicationName;
        }

        private static string _name;
        /// <summary>
        /// CurrentUserName
        /// </summary>
        public static string UserName
        {
            get
            {
                // �����̵߳�ʱ�򣬲�����ô��
                //string name = System.Threading.Thread.CurrentPrincipal == null
                //                  ? "anonymous"
                //                  : System.Threading.Thread.CurrentPrincipal.Identity.Name;
                //return name;
                return string.IsNullOrEmpty(_name) ? "anonymous" : _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Role
        /// </summary>
        public static string[] Roles { get; set; }

        //private static string[] s_emptyRoles = new string[0];
        ///// <summary>
        ///// Role
        ///// </summary>
        //public static string[] Roles
        //{
        //    get
        //    {
        //        System.Security.Principal.GenericPrincipal p = System.Threading.Thread.CurrentPrincipal as System.Security.Principal.GenericPrincipal;
        //        string[] roles = p == null ? s_emptyRoles : 
        //        return name;
        //    }
        //}

        /// <summary>
        /// ����
        /// </summary>
        public static string Password;

        /// <summary>
        /// Current User's Client Id
        /// </summary>
        public static int ClientId;

        /// <summary>
        /// Current User's Organization Id
        /// </summary>
        public static int OrgId;

        /// <summary>
        /// ��������ַ
        /// </summary>
        public static string Server;

        ///// <summary>
        ///// �������˿�
        ///// </summary>
        //public static int ServerPort { get; set; }

        ///// <summary>
        ///// ��Ʒ��
        ///// </summary>
        //public static string ProductName
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// ��˾��
        ///// </summary>
        //public static string CompanyName
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        /////  �汾
        ///// </summary>
        //public static string Version
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 
        /// </summary>
        public const string AdministratorsRoleName = "ϵͳ����Ա";
        
        /// <summary>
        /// 
        /// </summary>
        public const string DeveloperRoleName = "ϵͳ������Ա";

        /// <summary>
        /// ����ģʽ�����������������ڴ棬�ٶȿ죩
        /// </summary>
        public static bool LiteMode = true;

        ///// <summary>
        ///// ϵͳģʽ(Ĭ��ΪHd��Cd��������ΪZkzx)
        ///// </summary>
        //public static string ApplicationMode
        //{
        //    get;
        //    set;
        //}
    }
}