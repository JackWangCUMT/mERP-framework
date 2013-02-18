using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Grid
{
    /// <summary>
    /// �ɱ༭��Grid
    /// </summary>
    public interface IArchiveGrid : IBoundGrid, ICheckControl
    {
        /// <summary>
        /// �Ƿ������ڲ�����
        /// </summary>
        bool AllowInnerInsert { get; set; }

        /// <summary>
        /// �Ƿ������ڲ��༭
        /// </summary>
        bool AllowInnerEdit { get; set; }

        /// <summary>
        /// �Ƿ������ڲ�ɾ��
        /// </summary>
        bool AllowInnerDelete { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        IControlManager ControlManager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Xceed.Grid.InsertionRow InsertionRow { get; }

        /// <summary>
        /// ArchiveGridHelper
        /// </summary>
        ArchiveGridHelper ArchiveGridHelper { get; }

        /// <summary>
        /// �Ƿ�ͨ��InsertRow���
        /// </summary>
        bool AddThrowInsertRow { get; set; }

        /// <summary>
        /// �û��Զ���Validate
        /// </summary>
        event System.ComponentModel.CancelEventHandler ValidateRow;

        /// <summary>
        /// �Ƿ�������ģʽ��Ĵ�Grid������ǣ�Ҫ����״̬��
        /// ��ͬ��DetailGrid��������ģʽ�����Grid
        /// </summary>
        bool IsInDetailMode
        {
            get;
            set;
        }
    }
}