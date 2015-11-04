using nettButikkpls.DAL;
using nettButikkpls.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        public bool SaveLog(string log)
        {
            string path = "Log.txt";
            var _Path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/"), path);
            if (!File.Exists(_Path))
            {
                string createText = log + Environment.NewLine;
                File.WriteAllText(_Path, createText);
            }
            else
            {
                string appendText = log + Environment.NewLine;
                File.AppendAllText(_Path, appendText);
            }
            return true;
        }
    }
}
        
    

