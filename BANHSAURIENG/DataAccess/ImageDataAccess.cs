using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BANHSAURIENG.Models;
using System.Web.Helpers;
using System.IO;
namespace BANHSAURIENG.DataAccess
{
    public class ImageDataAccess
    {
        private static ImageDataAccess _instance = null;
        private BSR_Entities _db;
        public static ImageDataAccess GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ImageDataAccess();
            }
            return _instance;
        }

        public ImageDataAccess()
        {
            _db = new BSR_Entities();
            _db.Configuration.ProxyCreationEnabled = false;
        }
        //end of ini class
        //Tuan
        public bool UploadImage(WebImage image, string imageName, string folder, int newWidth)
        {
            bool flag = false;
            try
            {
                if (image != null && image.ImageFormat.Equals("jpeg")
                    || image.ImageFormat.Equals("png") || image.ImageFormat.Equals("gif"))
                {
                    if (image.Width > newWidth)
                    {
                        image.Resize(newWidth, ((newWidth * image.Height) / image.Width));
                    }
                    var fileName = Path.GetFileName(imageName);
                    image.Save(Path.Combine(folder, fileName));
                    flag = true;
                }
                else { flag = false; }
            }
            catch (Exception ex) { flag = false; }

            return flag;

        }
        //Tuan
        public bool DeleteImage(string folder, string imageName)
        {
            try
            {
                File.Delete(System.Web.HttpContext.Current.Server.MapPath(folder + imageName));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        // destroy 
        ~ImageDataAccess()
        {
            _db = null;
            _db.Dispose();
        }
    }
}