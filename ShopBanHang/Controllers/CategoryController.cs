using ShopBanHang.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBanHang.Controllers
{
    public class CategoryController : Controller
    {
        ShopBanQuanAoEntities shopBanHangEntities = new ShopBanQuanAoEntities();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = shopBanHangEntities.Categories.ToList();
                return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var lstProduct = shopBanHangEntities.Products.Where(n => n.CategoryId == Id).ToList();
            return View(lstProduct);
        }
    }
}