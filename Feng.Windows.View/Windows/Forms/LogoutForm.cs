using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Feng.Windows.Utils;

namespace Feng.Windows.Forms
{
    /// <summary>
    /// ע����
    /// </summary>
    public class LogoutForm
    {
        private AMS.Profile.IProfile m_profile = SystemProfileFile.DefaultUserProfile;

        /// <summary>
        /// ע����ʹ���Զ���¼������ִ�г���
        /// </summary>
        public LogoutForm()
        {
            m_profile.SetValue("Login", "AutoLogin", false);

            ProcessHelper.ExecuteApplication(Application.ExecutablePath, "/username:anonymous /password:nowandfuture");

            Application.Exit();
        }
    }
}