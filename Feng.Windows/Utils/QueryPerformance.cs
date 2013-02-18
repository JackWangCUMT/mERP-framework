using System;
using System.Collections.Generic;
using System.Text;


namespace Feng.Windows.Utils
{
    /// <summary>
    /// ���ܲ鿴
    /// </summary>
    public sealed class QueryPerformance
    {
        private QueryPerformance()
        {
        }

        private static long freq;
        private static long count_s, count_f;

        /// <summary>
        /// ��ʼ���ܲ鿴
        /// </summary>
        public static void StartQuery()
        {
            Feng.Windows.NativeMethods.QueryPerformanceFrequency(ref freq);
            Feng.Windows.NativeMethods.QueryPerformanceCounter(ref count_s);
        }

        /// <summary>
        /// �������ܲ鿴�����ؾ��뿪ʼʱ��ʱ�䣬��λ����
        /// </summary>
        /// <returns></returns>
        public static double StopQuery()
        {
            Feng.Windows.NativeMethods.QueryPerformanceCounter(ref count_f);

            return (double) (count_f - count_s) / (double) freq;
        }
    }
}