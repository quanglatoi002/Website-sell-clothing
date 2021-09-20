using ShopBanHang.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopBanHang.Context
{
       [MetadataType(typeof(ProductData))]
        public partial class Product
        {   
            [NotMapped]
            public System.Web.HttpPostedFileBase ImageUpload { get; set; }
        }

}