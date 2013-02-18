using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Feng.NH
{
    /// <summary>
    /// ����NHibernate��ѯ���������޲�ѯ����
    /// </summary>
    public class SearchManagerProxy<T> : SearchManagerCriteria<T>
        where T : IEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repCfgName"></param>
        public SearchManagerProxy(string repCfgName)
            : base(repCfgName)
        {
            this.EnablePage = false;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchManagerProxy()
            : this(null)
        {
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            SearchManagerProxy<T> sm = new SearchManagerProxy<T>();
            Copy(this, sm);
            return sm;
        }
    }
}