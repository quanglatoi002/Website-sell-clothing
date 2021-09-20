using PagedList;
using ShopBanHang.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ShopBanHang.Common;

namespace ShopBanHang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ShopBanQuanAoEntities dbObj = new ShopBanQuanAoEntities();
        // GET: Admin/Product
        void load()
        {
            try
            {
                dbObj.SaveChanges();
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
        public ActionResult Index(string currentFilter, string Search, int? page)
        {
            var products = new List<Product>();
            if(Search != null)
                page = 1;
            else{
                Search = currentFilter;
            }
            if (!string.IsNullOrEmpty(Search))
                // chúng ta gọi lấy thông tin theo tin khóa name
                products = dbObj.Products.Where(n => n.Name.Contains(Search)).ToList();
            else{
                products = dbObj.Products.ToList();// hiểu thị toàn bộ thông tin sản phẩm
            }
            ViewBag.CurrentFilter = Search;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            // Sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            products = products.OrderByDescending(n => n.Id).ToList();
            return View(products.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int Id)
        {
            var DetailsProduct = dbObj.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(DetailsProduct);
        }

        [HttpGet]
        public ActionResult Create()
        {
            LoadData();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product objproduct)
        {
            this.LoadData();
            if (!ModelState.IsValid)
            {

                try
                {
                    if (objproduct.ImageUpload != null)
                    {
                        //vd iphone12.png
                        string filename = Path.GetFileNameWithoutExtension(objproduct.ImageUpload.FileName);// lấy ra tên hình iphone12

                        string extension = Path.GetExtension(objproduct.ImageUpload.FileName);//lấy cái sau dấu chấm vd .png

                        filename = filename + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;// cộng với extension thì sẽ ra iphone12.png
                        objproduct.Avartar = filename;
                        objproduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                    }
                    dbObj.Products.Add(objproduct);
                    load();
                }

                catch (Exception)
                {
                    return View();
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = dbObj.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product product)
        {
            var objProduct = dbObj.Products.Where(n => n.Id == product.Id).FirstOrDefault();
            dbObj.Products.Remove(objProduct);
            dbObj.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = dbObj.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
          
                if (product.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
                    string extension = Path.GetExtension(product.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    product.Name = fileName;
                    product.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                }
                dbObj.Entry(product).State = EntityState.Modified;
                load();
                return RedirectToAction("Index");
        }

        void LoadData()
        {
            Common common = new Common();
            var lstCat = dbObj.Categories.ToList();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = common.ToSelectList(dtCategory, "Id", "Name");

            var lstBrand = dbObj.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            ViewBag.ListBrand = common.ToSelectList(dtBrand, "Id", "Name");

            // Loại sản phẩm
            List<ProductType> lstproductType = new List<ProductType>();
            ProductType objproductType = new ProductType();
            objproductType.Id = 01;
            objproductType.Name = "Giảm giá cực mạnh";
            lstproductType.Add(objproductType);


            objproductType = new ProductType();
            objproductType.Id = 02;
            objproductType.Name = "Đề xuất";
            lstproductType.Add(objproductType);

            DataTable dataTableType = converter.ToDataTable(lstproductType);
            ViewBag.ProductType = common.ToSelectList(dataTableType, "Id", "Name");
        }
    }

}
