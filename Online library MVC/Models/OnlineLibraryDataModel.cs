using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_library_MVC.Models
{
    public class OnlineLibraryDataModel
    {
        [Display(Name="Item ID")]
        public int itemid { get; set; }
        [Display(Name = "Item Type")]
        public string itemtype { get; set; }
        [Display(Name = "Item Title")]
        public string itemtitle { get; set; }
        [Display(Name = "Cost")]
        public double cost { get; set; }
        [Display(Name = "No of Issue")]
        public int noofissue { get; set; }
        [Display(Name = "Issue Date")]
        public string issuedate { get; set; }
        [Display(Name = "Return Date")]
        public string returndate { get; set; }
        [Display(Name = "UserName")]
        public string uname { get; set; }
        [Display(Name = "Fine")]
        public static double fine { get; set; }

        public OnlineLibraryDataModel()
        {
            fine = 1.25;
        }
      
    }
   
}
