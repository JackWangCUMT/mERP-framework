using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using Feng.Utils;

namespace Feng
{
    /// <summary>
    /// 
    /// </summary>
    public static class EntityHelper
    {
        #region "Replace: replace expression in string(mostly return permission string)"
#if !SILVERLIGHT
        private static Regex s_regexExpression = new Regex(@"\$(.*?)\$", RegexOptions.Compiled);
        private static Regex s_regexEntityParamter = new Regex(@"\%(.*?)\%", RegexOptions.Compiled);
#else
        private static Regex s_regexExpression = new Regex(@"\$(.*?)\$");
        private static Regex s_regexEntityParamter = new Regex(@"\%(.*?)\%", RegexOptions.None);
#endif

        /// <summary>
        /// �滻�ַ������$����$(ֻ���Զ��庯�������������""
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string ReplaceExpression(string expression)
        {
            try
            {
                Match m = s_regexExpression.Match(expression);
                while (m.Success)
                {
                    object result = Script.CalculateExpression(m.Groups[1].Value);
                    string replaceString = string.Empty;
                    if (result != null)
                    {
                        replaceString = result.ToString();
                    }
                    expression = expression.Replace(m.Groups[0].Value, replaceString);

                    m = s_regexExpression.Match(expression);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("expression of " + expression + " format is invalid!", ex);
            }
            return expression;
        }

        /// <summary>
        ///  �滻�ַ������$����$��%%�������������""
        ///  ���ʽ������Ϊnull������string.Empty����
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string ReplaceExpression(string expression, object entity)
        {
            try
            {
                Match m = s_regexExpression.Match(expression);
                while (m.Success)
                {
                    string ownExpression = m.Groups[1].Value;

                    object result = EntityScript.CalculateExpression(ownExpression, entity);

                    string replaceString = string.Empty;
                    if (result != null)
                    {
                        replaceString = result.ToString();
                    }
                    expression = expression.Replace(m.Groups[0].Value, replaceString);

                    m = s_regexExpression.Match(expression);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("expression of " + expression + " format is invalid!", ex);
            }
            return expression;
        }
        #endregion

        #region "Replace: replace entity parameter in string"
        /// <summary>
        /// �滻�ַ������%����%(��entity�ڱ���)����','����
        /// �ڲ�������quoto����
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="entities"></param>
        /// <param name="quoto"></param>
        /// <returns></returns>
        public static string ReplaceEntities(string expression, object[] entities, char quoto)
        {
            try
            {
                Match m = s_regexEntityParamter.Match(expression);
                while (m.Success)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append(quoto);
                    foreach (object entity in entities)
                    {
                        object v = EntityScript.GetPropertyValue(entity, m.Groups[1].Value);

                        string s = ConvertEntityParamToString(v, null);

                        sb.Append(s);
                        sb.Append(",");
                    }
                    if (sb[sb.Length - 1] == ',')
                    {
                        sb.Remove(sb.Length - 1, 1);
                    }
                    sb.Append(quoto);
                    expression = expression.Replace(m.Groups[0].Value, sb.ToString());

                    m = s_regexEntityParamter.Match(expression);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("expression of " + expression + " format is invalid!", ex);
            }
            return expression;
        }


        private static string ConvertEntityParamToString(object entityParam, char? quoto)
        {
            string s;
            object v = entityParam;
            if (v != null && v != System.DBNull.Value)
            {
                if (v is DateTime)
                {
                    s = ((DateTime)v).ToString("MM.dd.yyyy");
                }
                else if (v is string || v.GetType().IsEnum || v is Boolean || v is Boolean?
                        || v is Guid || v is Guid?
                        || v.GetType().IsClass)
                {
                    if (quoto.HasValue)
                    {
                        s = quoto.Value + v.ToString() + quoto.Value;
                    }
                    else
                    {
                        s = v.ToString();
                    }
                }
                else // int...
                {
                    string s1 = v.ToString();
                    if (!string.IsNullOrEmpty(s1))
                    {
                        s = s1;
                    }
                    else
                    {
                        s = string.Empty;
                    }
                }
            }
            else
            {
                if (quoto.HasValue)
                {
                    s = quoto.ToString() + quoto.ToString();
                }
                else
                {
                    s = string.Empty;
                }
            }
            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="entity"></param>
        /// <param name="asTypedEntity"></param>
        /// <param name="isNull"></param>
        /// <returns></returns>
        public static string ReplaceEntityParameters(string expression, object entity, bool asTypedEntity, bool isNull = false)
        {
            try
            {
                Match m = s_regexEntityParamter.Match(expression);
                while (m.Success)
                {
                    if (isNull)
                    {
                        expression = expression.Replace(m.Groups[0].Value, "\"\"");
                    }
                    else
                    {
                        if (asTypedEntity)
                        {
                            expression = expression.Replace(m.Groups[0].Value, "entity." + m.Groups[1].Value);
                        }
                        else
                        {
                            expression = expression.Replace(m.Groups[0].Value, "row[\"" + m.Groups[1].Value + "\"]");
                        }
                    }
                    m = s_regexEntityParamter.Match(expression);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("expression of " + expression + " format is invalid!", ex);
            }
            return expression;
        }

        /// <summary>
        /// ReplaceParameterizedEntity
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="funcGetReplaceValue"></param>
        /// <param name="quoto"></param>
        /// <returns></returns>
        public static string ReplaceEntity(string expression, GetReplaceValue funcGetReplaceValue, char? quoto = '\"')
        {
            try
            {
                Match m = s_regexEntityParamter.Match(expression);
                while (m.Success)
                {
                    object v = funcGetReplaceValue(m.Groups[1].Value);
                    string s = ConvertEntityParamToString(v, quoto);

                    expression = expression.Replace(m.Groups[0].Value, s);

                    m = s_regexEntityParamter.Match(expression);
                }
            }
            catch (InvalidUserOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("expression of " + expression + " format is invalid!", ex);
            }
            return expression;
        }

        /// <summary>
        /// �滻�ַ������%����%(��entity�ڱ�������ֻ��entity������
        /// ע�⣬�����ַ������entity�������""""(Ϊ�˱��ʽ������Ҫ�����������Ҫ��������quoto����
        /// �����Enum�������ո���־.�գ�ֻ��Ҫд%�ո���־%="��"
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="entity"></param>
        /// <param name="quoto"></param>
        /// <returns></returns>
        public static string ReplaceEntity(string expression, object entity, char? quoto = '\"')
        {
            return ReplaceEntity(expression, new GetReplaceValue(delegate(string paramName)
            {
                return EntityScript.GetPropertyValue(entity, paramName);
            }),
            quoto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public delegate object GetReplaceValue(string paramName);
        #endregion
    }
}
