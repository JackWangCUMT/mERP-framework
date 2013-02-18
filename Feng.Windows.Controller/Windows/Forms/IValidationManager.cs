using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Windows.Forms
{
    /// <summary>
    /// ��Validation��Manager
    /// </summary>
    public interface IValidationManager
    {
        /// <summary>
        /// ���Validation
        /// </summary>
        /// <param name="dataControlName"></param>
        /// <param name="expression"></param>
        void SetValidation(string dataControlName, Xceed.Validation.ValidationExpression expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataControlName"></param>
        void RemoveValidation(string dataControlName);
    }
}