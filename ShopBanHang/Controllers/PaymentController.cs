using ShopBanHang.Context;
using ShopBanHang.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBanHang.Controllers
{
    public class PaymentController : Controller
    {
        ShopBanQuanAoEntities dbHome = new ShopBanQuanAoEntities();
        // GET: Payment
        void load()
        {
            try
            {
                dbHome.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }

        }
        public ActionResult Index()
        {
            if(Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                // lấy thông tin giỏ hàng từ biến session
                var lstCart = (List<CartModel>)Session["cart"];
                // gán dữ liệu cho Order
                Order objorder = new Order();
                objorder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objorder.UserId = int.Parse(Session["idUser"].ToString());
                objorder.CreatedOnUtc = DateTime.Now;
                objorder.Status = 1;
                dbHome.Orders.Add(objorder);
                dbHome.SaveChanges();
                //Lấy OrderId vừa mới tạo để lưu vào bang OrderDetail.
                int intOrderId = objorder.Id;
                List<OrderDetail> lstorderDetails = new List<OrderDetail>();
                foreach(var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.product.Id;
                    lstorderDetails.Add(obj);
                }
                dbHome.OrderDetails.AddRange(lstorderDetails);
                load();
            }
            return View();
        }
    }
}