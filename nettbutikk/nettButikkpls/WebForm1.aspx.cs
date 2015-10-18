using nettButikkpls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nettButikkpls
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var db = new NettbutikkContext())
            {
                var product_1 = new Products();
                {
                    product_1.ProductId = 0;
                    product_1.Productname = "Aurablad te";
                    product_1.Price = 69;
                    product_1.Category = "Healingte";
                };
                var product_2 = new Products();
                {
                    product_2.ProductId = 1;
                    product_2.Productname = "Alpelyng te";
                    product_2.Price = 169;
                    product_2.Category = "Humbugte";
                };
                var product_3 = new Products();
                {
                    product_3.ProductId = 2;
                    product_3.Productname = "Bulgarsk pottete";
                    product_3.Price = 269;
                    product_3.Category = "Detoxte";
                };
                var product_4 = new Products();
                {
                    product_4.ProductId = 3;
                    product_4.Productname = "Hemp te";
                    product_4.Price = 169;
                    product_4.Category = "Healingte";
                };

                try
                {
                    db.Products.Add(product_1);
                    db.Products.Add(product_2);
                    db.Products.Add(product_3);
                    db.Products.Add(product_4);
                    db.SaveChanges();
                }
                catch (Exception feil)
                {
                }
            }
        }
    }
}