using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nettButikkpls.Models;
using nettButikkpls.DAL;
using System.Web.Mvc;
using System.IO;

namespace nettButikkpls.BLL
{
    public class ProductBLL
    {
        public IEnumerable<Product> allProducts()
        {
            var productDal = new ProductDAL();
            return productDal.allProducts();
        }
        public bool saveProduct (Product inProduct)
        {
            var productDal = new ProductDAL();
            return productDal.saveProduct(inProduct);
        }
        public bool SaveImagesToServer(HttpFileCollectionBase innFiler)
        {
            var productDal = new ProductDAL();
            return productDal.SaveImagesToServer(innFiler);
        }
        public bool UpdateProduct(FormCollection inList, int productid)
        {
            var productDal = new ProductDAL();
            return productDal.UpdateProduct(inList, productid);
        }
    }
}