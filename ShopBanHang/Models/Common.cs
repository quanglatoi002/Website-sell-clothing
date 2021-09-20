using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ShopBanHang
{
    public class Common
    {
        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueFile, string textfile)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (DataRow row in table.Rows)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Text = row[textfile].ToString(),
                    Value = row[valueFile].ToString()
                });
            }
            return new SelectList(selectListItems, "Value", "Text");
        }
        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                PropertyInfo[] Pops = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Pops)
                {
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new Object[Pops.Length];
                    for (int i = 0; i < Pops.Length; i++)
                    {
                        values[i] = Pops[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                return dataTable;
            }
        }
        public class ProductType
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

    }
}