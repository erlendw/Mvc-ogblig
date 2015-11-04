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
    public class ProductLogic : IProductLogic
    {
        private IProductRepo _repo;

        public ProductLogic()
        {
            _repo = new ProductRepo();
        }
        public ProductLogic(IProductRepo stub)
        {
            _repo = stub;
        }
        public IEnumerable<Product> allProducts()
        {
            
            return _repo.allProducts();
        }
        public bool saveProduct (Product inProduct)
        {
            
            return _repo.saveProduct(inProduct);
        }
        public bool SaveImagesToServer(HttpFileCollectionBase innFiler)
        {
            
            return _repo.SaveImagesToServer(innFiler);
        }
        public bool UpdateProduct(FormCollection inList, int productid)
        {
            
            return _repo.UpdateProduct(inList, productid);
        }
    }
}