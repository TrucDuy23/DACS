using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DACS.Models;
using PagedList;

namespace DACS.DAO
{
    public class ProductCategoryDAO
    {
        DBModel db = null;
        public ProductCategoryDAO()
        {
            db = new DBModel();
        }
        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
    }
}