using System;
using System.Collections.Generic;
using System.Text;
using Feng.Utils;

namespace Feng.Data
{
    /// <summary>
    /// ����������
    /// </summary>
    public static class PrimaryMaxIdGenerator
    {
        /// <summary>
        /// �õ�DateTime��Ӧ��YYMM�ַ���
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetIdYearMonth(DateTime dt)
        {
            return dt.ToString("yyMM");
        }

        /// <summary>
        /// �õ������Ӧ��YYMM�ַ���
        /// </summary>
        /// <returns></returns>
        public static string GetIdYearMonth()
        {
            return GetIdYearMonth(System.DateTime.Today);
        }


        /// <summary>
        /// �õ���ǰ�ı��п��õ�ID��Int��ʽ��
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryKeyName"></param>
        /// <returns></returns>
        public static long GetMaxId(string tableName, string primaryKeyName)
        {
            return ConvertHelper.ToLong(DbHelper.Instance.ExecuteScalar("SELECT " + GetIsNullString("MAX(" + primaryKeyName + ")", "0") + " + 1 FROM " + tableName)).Value;
        }

        /// <summary>
        /// �õ���ǰ�ı��п��õ�ID���ַ�����ʽ��
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryKeyName"></param>
        /// <param name="primaryKeyLen"></param>
        /// <returns></returns>
        public static string GetMaxId(string tableName, string primaryKeyName, int primaryKeyLen)
        {
            return GetMaxId(tableName, primaryKeyName, primaryKeyLen, null);
        }

        /// <summary>
        /// �õ���ǰ�ı��п��õ�ID���ַ�����ʽ��
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryKeyName"></param>
        /// <param name="preName"></param>
        /// <param name="primaryKeyLen"></param>
        /// <returns></returns>
        public static string GetMaxId(string tableName, string primaryKeyName, int primaryKeyLen, string preName)
        {
            return GetMaxId(tableName, primaryKeyName, primaryKeyLen, preName, 0);
        }

        private static string GetIsNullString(string exp, string value)
        {
            return "CASE WHEN " + exp + " IS NULL THEN " + value + " ELSE " + exp + " END";
        }

        /// <summary>
        /// GetMaxIdFromPrevId
        /// </summary>
        /// <param name="prePrimaryKeyId"></param>
        /// <param name="preName"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        public static string GetMaxIdFromPrevId(string prePrimaryKeyId, string preName, int delta)
        {
            string s1 = prePrimaryKeyId.Substring(preName.Length, prePrimaryKeyId.Length - preName.Length);
            int pre = Feng.Utils.ConvertHelper.ToInt(s1).Value;
            return preName + (pre + delta).ToString("D" + s1.Length);
        }

        /// <summary>
        /// ��õ�ǰ���е����������ֵ��������ǰ׺��
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryKeyName"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static long GetMaxInt(string tableName, string primaryKeyName, string pattern)
        {
            return GetMaxInt(tableName, primaryKeyName, pattern, null);
        }

        /// <summary>
        /// ��õ�ǰ���е����������ֵ��������ǰ׺��
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryKeyName"></param>
        /// <param name="pattern">����ֵģʽ��������������?��ʾ,%�����������֣�����ABCD?-%</param>
        /// <param name="whereLike"></param>
        /// <returns></returns>
        public static long GetMaxInt(string tableName, string primaryKeyName, string pattern, string whereLike)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }
            if (string.IsNullOrEmpty(primaryKeyName))
            {
                throw new ArgumentNullException("primaryKeyName");
            }

            object strID;

            if (!string.IsNullOrEmpty(pattern))
            {
                int left = -1;
                int right = -1;
                for (int i = 0; i < pattern.Length; ++i)
                {
                    if (left == -1)
                    {
                        if (pattern[i] == '?')
                        {
                            left = i;
                        }
                    }
                    else
                    {
                        if (pattern[i] != '?')
                        {
                            right = i;
                            break;
                        }
                    }
                }
                if (left == -1)
                {
                    throw new ArgumentException("Pattern format is invalid! pattern should contain at least one ?", "pattern");
                }

                string newPattern;
                if (string.IsNullOrEmpty(whereLike))
                {
                    newPattern = pattern.Substring(0, left) + "%";
                }
                else
                {
                    newPattern = whereLike;
                }

                string sql;
                if (right == -1)
                {
                    string nullString = string.Format("MAX(CONVERT(INT, SUBSTRING({0}, {1}, LEN({0}) - {1} + 1)))", primaryKeyName, left + 1);
                    nullString = GetIsNullString(nullString, "0");
                    sql = string.Format("SELECT {0} FROM {1} WHERE {2} LIKE '{3}'", nullString, tableName, primaryKeyName, newPattern);
                }
                else
                {
                    string endSign = pattern.Substring(right).Replace("%", "").Replace("?", "");

                    string nullString = string.Format("MAX(CONVERT(INT, SUBSTRING({0}, {1}, CASE CHARINDEX('{2}', {0}) WHEN 0 THEN LEN({0}) + 1 ELSE CHARINDEX('{2}', {0}) END - {1}) ))", primaryKeyName, left + 1, endSign);
                    nullString = GetIsNullString(nullString, "0");
                    sql = string.Format("SELECT {0} FROM {1} WHERE {2} LIKE '{3}'", nullString, tableName, primaryKeyName, newPattern);
                }

                strID = DbHelper.Instance.ExecuteScalar(sql);
            }
            else
            {
                strID = DbHelper.Instance.ExecuteScalar("SELECT " +
                    GetIsNullString("MAX(CONVERT(INT, " + primaryKeyName + "))", "0") +
                                    " FROM " + tableName);
            }

            return ConvertHelper.ToLong(strID).Value;
        }

        /// <summary>
        /// ��õ�ǰ���е����������ֵ��������ǰ׺��
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryKeyName"></param>
        /// <param name="primaryKeyLen"></param>
        /// <param name="preName"></param>
        /// <returns></returns>
        public static long GetMaxInt(string tableName, string primaryKeyName, int primaryKeyLen, string preName)
        {
            return GetMaxInt(tableName, primaryKeyName, primaryKeyLen, preName, null);
        }

        /// <summary>
        /// ��õ�ǰ���е����������ֵ��������ǰ׺��
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryKeyName"></param>
        /// <param name="primaryKeyLen"></param>
        /// <param name="preName"></param>
        /// <param name="specialWhere">������ŵ�ʱ����Զ���Where</param>
        /// <returns></returns>
        public static long GetMaxInt(string tableName, string primaryKeyName, int primaryKeyLen, string preName, string specialWhere)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }
            if (string.IsNullOrEmpty(primaryKeyName))
            {
                throw new ArgumentNullException("primaryKeyName");
            }

            object strID;

            if (!string.IsNullOrEmpty(preName))
            {
                if (string.IsNullOrEmpty(specialWhere))
                {
                    strID = DbHelper.Instance.ExecuteScalar("SELECT " +
                        GetIsNullString("MAX(CONVERT(INT, SUBSTRING(" + primaryKeyName + ", " + (preName.Length + 1).ToString() + ", " + (primaryKeyLen - preName.Length).ToString() + ")))", "0") +
                                         " FROM " + tableName + " WHERE " + primaryKeyName + " LIKE '" + preName + "%'");
                }
                else
                {
                     strID = DbHelper.Instance.ExecuteScalar("SELECT " +
                        GetIsNullString("MAX(CONVERT(INT, SUBSTRING(" + primaryKeyName + ", " + (preName.Length + 1).ToString() + ", " + (primaryKeyLen - preName.Length).ToString() + ")))", "0") +
                                         " FROM " + tableName + " WHERE " + specialWhere);
                }
            }
            else
            {
                strID = DbHelper.Instance.ExecuteScalar("SELECT " + 
                    GetIsNullString("MAX(CONVERT(INT, " + primaryKeyName + "))", "0") + 
                                    " FROM " + tableName);
            }

            return ConvertHelper.ToLong(strID).Value;
        }

        /// <summary>
        /// �õ���ǰ�ı��п��õ�ID����Ҫ���ڲ�����ǰ׺��˳��ţ��ַ�����ʽ��
        /// preClass==Others: �з���ţ��������ͬȡ���
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="primaryKeyName">������</param>
        /// <param name="preName">���ݿ������ֶΡ�null: ��ǰ����ţ�ֱ��ȡ���</param>
        /// <param name="primaryKeyLen">��������</param>
        /// <param name="delta">�����ֵ�ϼӵ�������0Ϊ����1��</param>
        /// <returns>���õ�ID</returns>
        public static string GetMaxId(string tableName, string primaryKeyName, int primaryKeyLen, string preName, int delta)
        {
            long nowMaxId = GetMaxInt(tableName, primaryKeyName, primaryKeyLen, preName);
            return GetMaxId(nowMaxId, primaryKeyLen, preName, delta);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nowMaxInt"></param>
        /// <param name="primaryKeyLen"></param>
        /// <param name="preName"></param>
        /// <returns></returns>
        public static string GetMaxId(long nowMaxInt, int primaryKeyLen, string preName, int delta)
        {
            long nextId = nowMaxInt + delta + 1;
            string strId = nextId.ToString();
            if (strId.Length != primaryKeyLen)
            {
                strId = strId.PadLeft(primaryKeyLen - (preName == null ? 0 : preName.Length), '0');
            }

            return preName + strId;
        }
    }
}