﻿using XBInsaat.Core.Entites;
using XBInsaat.Service.CustomExceptions;
using XBInsaat.Service.Helper;
using XBInsaat.Service.HelperService.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace XBInsaat.Service.HelperService.Implementations
{
    public class ManageImageHelper : IManageImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly IImageValue _key;

        public ManageImageHelper(IWebHostEnvironment env, IImageValue key)
        {
            _env = env;
            _key = key;
        }
        public void PosterCheck(IFormFile PosterImageFile)
        {
            if (PosterImageFile.ContentType != _key.ValueStr("ImageType1") && PosterImageFile.ContentType != _key.ValueStr("ImageType2"))
                throw new ImageFormatException("Şəkil yalnız (png ve ya jpg) type-ında ola bilər");

            if (PosterImageFile.Length > _key.ValueInt("ImageSize") * 1048576)
                throw new ImageFormatException("Şəklin max yaddaşı" + _key.ValueInt("ImageSize") + "MB ola bilər!");
        }
        public void ImagesCheck(List<IFormFile> Images)
        {
            if (Images.Count > 8)
                throw new ImageCountException("Maksimum 8 şəkil əlavə edə bilərsiniz");

            foreach (var image in Images)
            {

                if (image.ContentType != _key.ValueStr("ImageType1") && image.ContentType != _key.ValueStr("ImageType2"))
                    throw new ImageFormatException("Şəkil yalnız (png ve ya jpeg) type-ında ola bilər");
                if (image.Length > _key.ValueInt("ImageSize") * 1048576)
                    throw new ImageFormatException("Şəklin max yaddaşı " + _key.ValueInt("ImageSize") + "MB ola bilər!");
            }
        }
        public string FileSave(IFormFile Image, string folderName)
        {
            string image = FileManager.Save(_env.WebRootPath, "uploads/" + folderName, Image);
            return image;
        }
        public void DeleteFile(string image, string folderName)
        {
            FileManager.Delete(_env.WebRootPath, "uploads/" + folderName, image);
        }
    }
}
