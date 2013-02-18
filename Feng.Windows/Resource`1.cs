/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) dotPDN LLC, Rick Brewster, Tom Jackson, and contributors.     //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;

namespace Feng.Windows
{
    /// <summary>
    /// ǿ������Դ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Resource<T>
    {
        private string name;
        private string resourceName;

        private T resource;

        /// <summary>
        /// ��Դ��������
        /// </summary>
        public string ResourceName
        {
            get { return this.resourceName; }
        }

        /// <summary>
        /// ��Դ����
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// 
        /// </summary>
        public T Reference
        {
            get
            {
                if (this.resource == null)
                {
                    this.resource = Load();
                }

                return this.resource;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T GetCopy()
        {
            return Load();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract T Load();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="name"></param>
        protected Resource(string resourceName, string name)
        {
            this.resourceName = resourceName;
            this.name = name;
            this.resource = default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="name"></param>
        /// <param name="resource"></param>
        protected Resource(string resourceName, string name, T resource)
        {
            this.resourceName = resourceName;
            this.name = name;
            this.resource = resource;
        }
    }
}