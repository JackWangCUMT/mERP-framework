/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) dotPDN LLC, Rick Brewster, Tom Jackson, and contributors.     //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Feng.Windows
{
    /// <summary>
    /// ͼ����Դ
    /// </summary>
    public sealed class ImageResource
        : Resource<Image>
    {
        /// <summary>
        /// �Ӹ���;�����Image�����ȼ�Ϊ ��ǰĿ¼���ļ�->������Դ��Ŀ->Feng��Դ��Ŀ
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Image TryGet(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                return new System.Drawing.Bitmap(fileName);
            }
            else
            {
                System.Drawing.Image image = ImageResource.Get(fileName).Reference;
                if (image != null)
                {
                    return image;
                }
                else
                {
                    return ImageResource.Get("Feng", fileName).Reference;
                }
            }
        }

        private static Dictionary<string, ImageResource> images = new Dictionary<string, ImageResource>();

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        protected override Image Load()
        {
            PdnResources res = PdnResourcesCollection.Instance[this.ResourceName];
            if (res != null)
            {
                return res.GetImage(this.Name);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ����ͼ����Դ
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public static ImageResource Get(string imageName)
        {
            return Get(null, imageName);
        }

        /// <summary>
        /// ����ͼ����Դ
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public static ImageResource Get(string resourceName, string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
            {
                throw new ArgumentNullException("imageName");
            }
            if (string.IsNullOrEmpty(resourceName))
            {
                resourceName = PdnResourcesCollection.Instance.DefaultResourceName;
            }
            ImageResource ir;
            string name = string.Format("{0}.{1}", resourceName, imageName);

            if (!images.TryGetValue(name, out ir))
            {
                ir = new ImageResource(resourceName, imageName);
                images.Add(name, ir);
            }

            if (ir.Reference == null && resourceName != FengResourceName)
            {
                return Get(FengResourceName, imageName);
            }
            return ir;
        }

        private const string FengResourceName = "Feng";
        /// <summary>
        /// ��Image���ͼ����Դ
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static ImageResource FromImage(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            ImageResource resource = new ImageResource(image);
            return resource;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="name"></param>
        private ImageResource(string resourceName, string name)
            : base(resourceName, name)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="image"></param>
        private ImageResource(Image image)
            : base(null, null, image)
        {
        }
    }
}