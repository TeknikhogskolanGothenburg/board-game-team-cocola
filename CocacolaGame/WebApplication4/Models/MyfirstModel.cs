using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class MyfirstModel
    {
        public HtmlString F;
       
       
        public string s  = "<div style=\"width: 200px; background:#F9EECF;border:1px dotted black;text-align:center\" >"+ " <p> Generic content...</p></ div > ";
        public void Tet()
        {


         
           
            F = new HtmlString(s);

          
        }
    }
}