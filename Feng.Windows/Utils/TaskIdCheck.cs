using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Windows.Utils
{
    /// <summary>
    /// ��Ÿ�ʽ���
    /// </summary>
    public sealed class TaskIdCheckHelper
    {
        /// <summary>
        /// private Constructor
        /// </summary>
        private TaskIdCheckHelper()
        {
        }

        /// <summary>
        /// ��֤��װ�����
        /// ��װ���Ź�11λ��ǰ��λ����ĸ�����һλΪУ���룬�������������������������
        /// ��ĸȡ��ֵ����Ϊ��A��10��B��K����ȡ12��21��L��U����ȡ23��32��V��Z����ȡ34��38��
        /// ��ŵ�һλ��ֵ����2��0���ݣ��ڶ�λ����2��1���ݣ�...����ʮλ����2��9���ݣ�Ȼ����͡�
        /// ��ͳ���11��������ΪУ�����ֵ
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public static bool Check(string taskId)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                return false;
            }
            if (taskId.Length != 11)
            {
                return false;
            }
            for (int i = 0; i < 4; ++i)
            {
                if (taskId[i] < 'A' || taskId[i] > 'Z')
                {
                    return false;
                }
            }
            for (int i = 4; i < 11; ++i)
            {
                if (taskId[i] < '0' || taskId[i] > '9')
                {
                    return false;
                }
            }
            int sum = 0;
            for (int i = 0; i < 10; ++i)
            {
                int c = 0;
                if (taskId[i] == 'A')
                {
                    c = 10;
                }
                else if (taskId[i] >= 'B' && taskId[i] <= 'K')
                {
                    c = 12 + taskId[i] - 'B';
                }
                else if (taskId[i] >= 'L' && taskId[i] <= 'U')
                {
                    c = 23 + taskId[i] - 'L';
                }
                else if (taskId[i] >= 'V' && taskId[i] <= 'Z')
                {
                    c = 34 + taskId[i] - 'V';
                }
                else if (taskId[i] >= '0' && taskId[i] <= '9')
                {
                    c = taskId[i] - '0';
                }

                sum += c << i;
            }
            if (sum % 11 % 10 != (taskId[10] - '0'))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}