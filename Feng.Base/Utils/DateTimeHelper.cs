using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Utils
{
	/// <summary>
	/// ʱ�������
	/// </summary>
	public sealed class DateTimeHelper
	{
		private DateTimeHelper()
		{
		}

		/// <summary>
		/// ����ʱ��Ϊһ���еĿ�ʼ
		/// </summary>
		/// <param name="time"></param>
		public static DateTime GetDateTimeStartofDay(DateTime time)
		{
			return new DateTime(time.Year, time.Month, time.Day);
		}

		/// <summary>
		/// ����ʱ��Ϊһ���еĽ���
		/// </summary>
		/// <param name="time"></param>
		public static DateTime GetDateTimeEndofDay(DateTime time)
		{
			return new DateTime(time.Year, time.Month, time.Day, 23, 59, 59);
		}

		/// <summary>
		/// �����ʱ��
		/// </summary>
		/// <param name="date1"></param>
		/// <param name="date2"></param>
		/// <returns></returns>
		public static DateTime MinDateTime(DateTime date1, DateTime date2)
		{
			if (date1 < date2)
				return date1;
			else
				return date2;
		}

		/// <summary>
		/// �����ʱ��
		/// </summary>
		/// <param name="date1"></param>
		/// <param name="date2"></param>
		/// <returns></returns>
		public static DateTime MaxDateTime(DateTime date1, DateTime date2)
		{
			if (date1 > date2)
				return date1;
			else
				return date2;
		}
	}
}
