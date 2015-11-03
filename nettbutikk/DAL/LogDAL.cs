using nettButikkpls.DAL;
using nettButikkpls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    class LogDAL
    {
        HttpContext context = HttpContext.Current;
        NettbutikkContext bmx = new NettbutikkContext();
    }
}
        
    

