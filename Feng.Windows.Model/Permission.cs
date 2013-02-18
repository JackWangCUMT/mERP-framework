using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Feng
{
    /// <summary>
    /// ����ʵ������ʽ��Ȩ�޶��塣
    /// ����Ȩ�޼�<see cref="Authority"/>��
    /// ���⣬����ʵ������ʽ���壬�������£�
    /// ���ʽ��#..#�������ڲ�������$..$������
    /// ����: ��Ʊ�����У�#$��������$=300001# OR I:200001
    /// </summary>
    public static class Permission
    {
        //private static Regex s_regexExpression = new Regex(@"\$(.*?)\$", RegexOptions.Compiled);
        //private static Regex s_regexEntityParamter = new Regex(@"\#(.*?)\#", RegexOptions.Compiled);

        /// <summary>
        /// �ж��û��Ƿ�����һ���Ĺ���(����ʵ������ʽ).
        /// Tips��������ַ����Ƚϣ���Ҫ��˫����""��
        /// int,double������Ҫ���ַ���
        /// 
        /// </summary>
        /// <param name="ruleExpression">���ʽ���ɰ���ʵ������ʽ��</param>
        /// <param name="entity">��Ӧ��ʵ����</param>
        /// <returns></returns>
        public static bool AuthorizeByRule(string ruleExpression, object entity)
        {
            if (string.IsNullOrEmpty(ruleExpression))
            {
                return false;
            }

            string entityKey = EntityHelper.ReplaceEntity(ruleExpression, entity);
            string key = "Permission.AuthorizeByRule:" + ruleExpression + ";entityKey:" + entityKey;
            return Cache.TryGetCache<bool>(key, new Func<bool>(delegate()
                {
                    try
                    {
                        ruleExpression = EntityHelper.ReplaceExpression(ruleExpression, entity);

                        ruleExpression = ruleExpression.Replace("true", " I:* ").Replace("True", " I:* ").Replace("TRUE", " I:* ");
                        ruleExpression = ruleExpression.Replace("false", " I:Nobody ").Replace("False", " I:Nobody ").Replace("FALSE", " I:Nobody ");
                        return Authority.AuthorizeByRule(ruleExpression);
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException(string.Format("expression of {0} format is invalid!", ruleExpression), ex);
                    }
                }));
        }
    }
}