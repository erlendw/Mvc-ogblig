using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using nettButikkpls.Models;

namespace nettButikkpls.BLL
{
    public interface IProductLogic
    {
        IEnumerable<Product> allProducts();
        bool SaveImagesToServer(HttpFileCollectionBase innFiler);
        bool saveProduct(Product inProduct);
        bool UpdateProduct(FormCollection inList, int productid);
    }
}