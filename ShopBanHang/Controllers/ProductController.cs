using ShopBanHang.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBanHang.Controllers
{
    public class ProductController : Controller
    {
        ShopBanQuanAoEntities shopBanHangEntities = new ShopBanQuanAoEntities();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var product = shopBanHangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(product);
        }
    }
}