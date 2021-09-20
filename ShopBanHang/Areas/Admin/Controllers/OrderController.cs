using PagedList;
using ShopBanHang.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBanHang.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        ShopBanQuanAoEntities dbObj = new ShopBanQuanAoEntities();

        public ActionResult Index(string currentFilter, string Search, int? page)
        {

            var users = new List<Order>();
            if (Search != null)
                page = 1;
            else
            {
                Search = currentFilter;
            }
            if (!string.IsNullOrEmpty(Search))
                // chúng ta gọi lấy thông tin theo tin khóa name
                users = dbObj.Orders.Where(n => n.Name.Contains(Search)).ToList();
            else
            {
                users = dbObj.Orders.ToList();// hiểu thị toàn bộ thông tin sản phẩm
            }
            ViewBag.CurrentFilter = Search;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //  Sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            users = users.OrderByDescending(n => n.Id).ToList();
            return View(users.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult Details(int id)
        {
            var DetailsUsers = dbObj.Orders.Where(n => n.Id == id).FirstOrDefault();
            return View(DetailsUsers);
        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/User/Create
        [HttpPost]
        public ActionResult Create(Order order)
        {
            dbObj.Orders.Add(order);

            dbObj.SaveChanges();

            return RedirectToAction("Index");
        }
        // GET: Admin/User/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objOrder = dbObj.Orders.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        public ActionResult Edit(Order order)
        {
            dbObj.Entry(order).State = EntityState.Modified;
            dbObj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var objUser = dbObj.Orders.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }
        // POST: Admin/User/Delete/5
        [HttpPost]

        public ActionResult Delete(Order order)
        {
            var objUser = dbObj.Orders.Where(n => n.Id == order.Id).FirstOrDefault();
            dbObj.Orders.Remove(objUser);
            dbObj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}