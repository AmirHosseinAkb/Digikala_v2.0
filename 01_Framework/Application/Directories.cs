﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Framework.Application
{
    public  class Directories
    {
        public static string ProductGroupDirectory(string? imageName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "Products",
                "ProductGroupImages",
                imageName!);
        }
        public static string ProductImageDirectory(string imageName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "Products",
                "ProductImages",
                imageName);
        }
        public static string ProductBrandDirectory(string imageName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "Products",
                "BrandImages",
                imageName);
        }
        public static string ProductDirectory(string imageName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "Products",
                "Images",
                imageName);
        }
    }
}
