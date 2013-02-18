using System;
using System.Data;
using System.IO;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Feng.Windows
{
	/// <summary>
	/// �쳣����
	/// </summary>
    public class EnterpriseLibraryExceptionProcess : IExceptionProcess
	{
        /// <summary>
        /// ��TextExceptionFormatter��ʽ��Exception������ʾ
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string FormatException(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);

            Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.TextExceptionFormatter formatter
                = new Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.TextExceptionFormatter(writer, ex);

            // Format the exception
            formatter.Format();

            return sb.ToString();
        }

        ///// <summary>
        ///// �õ�Sql��������������Ϣ�������������
        ///// </summary>
        ///// <param name="cmd">Sql����</param>
        ///// <returns>�õ���������Ϣ</returns>
        //private static string GetErrorMsg(DbCommand cmd)
        //{
        //    if (cmd == null)
        //        return "";

        //    System.Text.StringBuilder sb = new System.Text.StringBuilder("Sql��䣺" + cmd.CommandText + Environment.NewLine);
        //    foreach (IDataParameter param in cmd.Parameters)
        //    {
        //        sb.Append(param.ParameterName + " = ");
        //        if (param.Value != null)
        //            sb.Append(param.Value.ToString());
        //        sb.Append(Environment.NewLine);
        //    }
        //    return sb.ToString();
        //}

		/// <summary>
        /// ����δ�����쳣����Ӧ��"Global Policy"
		/// Process any unhandled exceptions that occur in the application.
		/// This code is called by all UI entry points in the application (e.g. button click events)
		/// when an unhandled exception occurs.
		/// You could also achieve this by handling the Application.ThreadException event, however
		/// the VS2005 debugger will break before this event is called.
		/// </summary>
		/// <param name="ex">The unhandled exception</param>
		public virtual bool ProcessUnhandledException(Exception ex)
        {
            bool rethrow = ExceptionPolicy.HandleException(ex, "Global Policy");
            if (rethrow)
            {
                return false;
            }
            return true;
        }

		/// <summary>
        /// �쳣���ֺ󣬳���������У���Ӧ��"Handle and Resume Policy"
		/// Demonstrates handling of exceptions coming out of a layer. The policy
		/// demonstrated here will show how original exceptions can be supressed
		/// and the program is resumed
		/// </summary>
		public bool ProcessWithResume(Exception ex)
		{
			bool rethrow = ExceptionPolicy.HandleException(ex, "Handle and Resume Policy");
            System.Diagnostics.Debug.Assert(rethrow == false, "Resume Policy should not rethrow exception");

            return rethrow;
		}

        /// <summary>
        /// ��ʾ�û�
        /// </summary>
        /// <param name="ex"></param>
        public bool ProcessWithNotify(Exception ex)
        {
            if (ex is InvalidUserOperationException)
            {
                return ProcessWithNotifyInfo(ex);
            }
            else
            {
                return ProcessWithNotifyError(ex);
            }
        }

        /// <summary>
        /// �򵥵���ʾ�û������Ǵ���ֻ�ǳ������Զ����Exception
        /// </summary>
        private bool ProcessWithNotifyInfo(Exception ex)
        {
            bool rethrow = ExceptionPolicy.HandleException(ex, "Notify Policy Info");
            System.Diagnostics.Debug.Assert(rethrow == false, "Notify Policy should not rethrow exception");

            return rethrow;
        }

		/// <summary>
        /// �쳣���ֺ���ʾ�û�����Ӧ��"Notify Policy"
		/// Demonstrates handling of exceptions coming out of a layer. The policy
		/// demonstrated here will show how original exceptions can be notified
		/// </summary>
        private bool ProcessWithNotifyError(Exception ex)
		{
			bool rethrow = ExceptionPolicy.HandleException(ex, "Notify Policy Error");
            System.Diagnostics.Debug.Assert(rethrow == false, "Notify Policy should not rethrow exception");

            return rethrow;
		}

		///// <summary>
		///// Demonstrates handling of exceptions coming out of a layer. The policy
		///// demonstrated here will show how original exceptions can be propagated back out.
		///// </summary>
		//public static void ProcessWithPropagate(Exception ex)
		//{
		//    bool rethrow = ExceptionPolicy.HandleException(ex, "Propagate Policy");

		//    if (rethrow)
		//    {
		//        throw;
		//    }
		//}

        ///// <summary>
        ///// �쳣���ֺ󣬰�װ�쳣���׳�����Ӧ��"Wrap Policy"
        ///// Demonstrates handling of exceptions coming out of a layer. The policy
        ///// demonstrated here will show how original exceptions can be wrapped
        ///// with a different exception before being propagated back out.
        ///// </summary>
        //public bool ProcessWithWrap(Exception ex)
        //{
        //    bool rethrow = ExceptionPolicy.HandleException(ex, "Wrap Policy");
        //    System.Diagnostics.Debug.Assert(rethrow == true, "Notify Policy should rethrow exception");
        //    return rethrow;
        //}

		///// <summary>
		///// Demonstrates handling of exceptions coming out of a layer. The policy
		///// demonstrated here will show how original exceptions can be replaced
		///// with a different exception before being propagated back out.
		///// </summary>
		//public static void ProcessWithReplace(Exception ex)
		//{
		//    bool rethrow = ExceptionPolicy.HandleException(ex, "Replace Policy");

		//    if (rethrow)
		//    {
		//        throw ex;
		//    }
		//}
	}
}
