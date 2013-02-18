using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.ComponentModel;

namespace Feng.Data
{
    /// <summary>
    /// ��������ִ�н��
    /// </summary>
    public enum ExpectedResultTypes
    {
        /// <summary>
        /// ���ڵ���0��
        /// </summary>
        GreaterThanOrEqualZero = 0,
        /// <summary>
        /// ����0��
        /// </summary>
        GreaterThanZero = 1,
        /// <summary>
        /// ����
        /// </summary>
        Any = 2,
        /// <summary>
        /// ���⣬��<see cref="MyDbCommand.ExpectedResult"/>
        /// </summary>
        [Description("Special Value for ExpectedResult")] Special = 9
    }

    /// <summary>
    /// ����ִ���Զ��������MyDbCommand������ϣ��Ӱ���������������Ϣ 
    /// </summary>
    public class MyDbCommand
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cmd">����</param>
        /// <param name="type">��������ִ�н��</param>
        public MyDbCommand(DbCommand cmd, ExpectedResultTypes type)
            : this(cmd, type, null, s_defaultErrorMsg)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cmd">����</param>
        /// <param name="type">��������ִ�н��</param>
        /// <param name="expected">����������</param>
        public MyDbCommand(DbCommand cmd, ExpectedResultTypes type, string expected)
            : this(cmd, type, expected, s_defaultErrorMsg)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cmd">����</param>
        /// <param name="type">��������ִ�н��</param>
        /// <param name="expected">����������</param>
        /// <param name="errorMsg">������ʾ</param>
        public MyDbCommand(DbCommand cmd, ExpectedResultTypes type, string expected, string errorMsg)
        {
            m_command = cmd;
            m_errorMsg = errorMsg;
            m_expectedResultType = type;
            m_expectedResult = expected;
        }

        private const string s_defaultErrorMsg = "���β���δ�ܳɹ���ɣ�������������ͬʱ��������ˢ�º����ԣ�";


        private DbCommand m_command;
        private ExpectedResultTypes m_expectedResultType;
        private string m_expectedResult;
        private string m_errorMsg;

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public DbCommand Command
        {
            get { return m_command; }
        }

        /// <summary>
        /// �����Update,Insert,Delete, ϣ��Ӱ�������
        /// </summary>
        public ExpectedResultTypes ExpectedResultType
        {
            get { return m_expectedResultType; }
        }

        /// <summary>
        /// �����Select��䣬���뷵��ֵ
        /// </summary>
        public string ExpectedResult
        {
            get { return m_expectedResult; }
        }

        /// <summary>
        /// ������Ϣ 
        /// </summary>
        public string ErrorMsg
        {
            get { return m_errorMsg; }
        }

        /// <summary>
        /// �����ı�
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return m_command.CommandText;
        }
    }
}