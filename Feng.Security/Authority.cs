using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Security;

namespace Feng
{
    /// <summary>
    /// ����Ȩ��
    /// �������£�
    /// ���ʣ�AND, OR, NOT
    /// ���ⵥ�ʣ� I���û�����R���飩��?����������*�������ˣ�
    /// ���������޶����ȼ���
    /// ���磬I:ABC OR R:XYZ ���� �û�ABC ���� ��XYZ���������Ȩ��
    /// </summary>
    public static class Authority
    {
        static Authority()
        {
            m_isAdministrators = Authority.AuthorizeByRule("R: " + SystemConfiguration.AdministratorsRoleName);
            m_isDevelopers = Authority.AuthorizeByRule("R: " + SystemConfiguration.DeveloperRoleName);
        }
        private static bool m_isAdministrators, m_isDevelopers;
        public static bool IsAdministrators()
        {
            return m_isAdministrators;
        }
        public static bool IsDeveloper()
        {
            return m_isDevelopers;
        }

        /// <summary>
        /// �ж��û��Ƿ�����һ���Ĺ���(����ʵ�ʹ���
        /// </summary>
        /// <param name="ruleExpression">����</param>
        /// <returns></returns>
        public static bool AuthorizeByRule(string ruleExpression, System.Security.Principal.IPrincipal principal = null)
        {
            if (string.IsNullOrEmpty(ruleExpression))
            {
                return false;
            }
            if (principal == null)
            {
                principal = System.Threading.Thread.CurrentPrincipal;
            }
            //if (m_isDevelopers)
            //    return true;
            string key = "Authority.AuthorizeByRule:" + ruleExpression;
            return Cache.TryGetCache<bool>(key, new Func<bool>(delegate()
                {
                    if (ruleExpression.ToUpper() == "TRUE")
                        return true;
                    if (ruleExpression.ToUpper() == "FALSE")
                        return false;
                    ruleExpression = ruleExpression.Replace("or", "OR").Replace("and", "AND").Replace("not", "NOT");
                    Parser parser = new Parser();
                    BooleanExpression booleanExpression = parser.Parse(ruleExpression);
                    if (booleanExpression == null)
                    {
                        throw new InvalidOperationException("Invalid rule format " + ruleExpression);
                    }

                    bool result = booleanExpression.Evaluate(principal);
                    return result;
                }));
        }

        /// <summary>
        /// �ж��û��Ƿ�����һ���Ĺ���(����.config�ļ��ﶨ��Ĺ�������)
        /// </summary>
        /// <param name="rulenName">��������</param>
        /// <returns></returns>
        public static bool AuthorizeByRuleName(string rulenName, System.Security.Principal.IPrincipal principal = null)
        {
            if (principal == null)
            {
                principal = System.Threading.Thread.CurrentPrincipal;
            }
            string key = "Authority.AuthorizeByRuleName:" + rulenName;
            return Cache.TryGetCache<bool>(key, new Func<bool>(delegate()
                {
                    try
                    {
                        return AuthorizationFactory.GetAuthorizationProvider().Authorize(principal, rulenName);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            return AuthorizationFactory.GetAuthorizationProvider().Authorize(principal, "Others");
                        }
                        catch (Exception ex)
                        {
                            ExceptionProcess.ProcessWithResume(ex);
                            return false;
                        }
                    }
                }));
        }
    }
}