using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.Net;

namespace Feng.Net
{
    /// <summary>
    /// ����SoapWebServices�Ĳ��ҹ������Ķ�ӦWebServiceClient
    /// </summary>
    public class WebSoapClient : SoapHttpClientProtocol, IWebServiceClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceAddress"></param>
        public WebSoapClient(string serviceAddress)
        {
            base.CookieContainer = new CookieContainer();
            base.Credentials = CredentialCache.DefaultCredentials;
            base.PreAuthenticate = true; //Optional

            base.Url = serviceAddress;
        }

        private const string GetDataMethod = "GetData";
        private const string GetDataCountMethod = "GetDataCount";
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns>�������������ݣ����ݾ�����ҷ�ʽ������ΪIList����DataView</returns>
        public System.Collections.IEnumerable GetData(string searchExpression = null, string searchOrder = null, int? firstResult = null, int? maxResult = null)
        {
            object[] results = this.Invoke(GetDataMethod, new object[] { searchExpression, searchOrder, firstResult, maxResult });
            if (results != null && results.Length > 0)
            {
                return (results[0] as System.Collections.IList);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="searchExpression">��������</param>
        /// <returns>������������������</returns>
        public int GetDataCount(string searchExpression = null)
        {
            object[] results = this.Invoke(GetDataCountMethod, new object[] { searchExpression });
            if (results != null && results.Length > 0)
            {
                return ((int)(results[0]));
            }
            else
            {
                return -1;
            }
        }
    }
}