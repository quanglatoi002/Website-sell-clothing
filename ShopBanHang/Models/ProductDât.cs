using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopBanHang.Models
{
    public partial class ProductData
    {
        public int Id { get; set; }
        
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Nhập tên sản phẩm")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Hình ảnh không được bỏ trống")]
        [Display(Name = "Hình ảnh")]
        public string Avartar { get; set; }
        [Display(Name = "Danh mục")]
        public Nullable<int> CategoryId { get; set; }

        [Display(Name = "Mô tả")]
        public string ShortDes { get; set; }

        [Display(Name = "Mô tả đầy đủ")]
        public string FullDescription { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập giá")]
        [Display(Name = "Giá gốc (VNĐ)")]
        public Nullable<double> Price { get; set; }

        [Display(Name = "Giá giảm giá")]
        public Nullable<double> PriceDiscount { get; set; }

        [Display(Name = "Loại")]
        public Nullable<int> TypeId { get; set; }
        
        public string Slug { get; set; }
        [Display(Name = "Thương hiệu")]
        public Nullable<int> BrandId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<bool> ShowOnHomePage { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }
    }
}