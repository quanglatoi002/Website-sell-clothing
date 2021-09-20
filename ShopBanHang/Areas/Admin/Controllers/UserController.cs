using PagedList;
using ShopBanHang.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ShopBanHang.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        ShopBanQuanAoEntities dbObj = new ShopBanQuanAoEntities();
        
        public ActionResult Index(string currentFilter, string Search, int? page)
        {
            
            var users = new List<User>();
            if (Search != null)
                page = 1;
            else
            {
                Search = currentFilter;
            }
            if (!string.IsNullOrEmpty(Search))
                // chúng ta gọi lấy thông tin theo tin khóa name
                users = dbObj.Users.Where(n => n.Email.Contains(Search)).ToList();
            else
            {
                users = dbObj.Users.ToList();// hiểu thị toàn bộ thông tin sản phẩm
            }
            ViewBag.CurrentFilter = Search;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
           //  Sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            users = users.OrderByDescending(n => n.Id).ToList();
            return View(users.ToPagedList(pageNumber, pageSize));
            
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(int id)
        {
            var DetailsUsers = dbObj.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(DetailsUsers);
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
                    dbObj.Users.Add(user);

                    dbObj.SaveChanges();

                    return RedirectToAction("Index");
                }
            // GET: Admin/User/Edit/5
            [HttpGet]
        public ActionResult Edit(int id)
        {
            var objUser = dbObj.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        public ActionResult Edit(User user)
        {
            dbObj.Entry(user).State = EntityState.Modified;
            dbObj.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/User/Delete/5
        public ActionResult Delete(int id)
        {
            var objUser = dbObj.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }
        // POST: Admin/User/Delete/5
        [HttpPost]
     
        public ActionResult Delete(User user)
        {
            var objUser = dbObj.Users.Where(n => n.Id == user.Id).FirstOrDefault();
            dbObj.Users.Remove(objUser);
            dbObj.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
