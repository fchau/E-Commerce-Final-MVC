using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class States
    {
        
        public int id { get; set; }
        public string name { get; set; }
        public string abbreviation { get; set; }
        public string country { get; set; }
        public string type { get; set; }
        public int sort { get; set; }
        public string status { get; set; }
        public string occupied { get; set; }
        public string notes { get; set; }
        public string fips_state { get; set; }
        public string assoc_press { get; set; }
        public string standard_federal_region { get; set; }
        public string census_region { get; set; }
        public string census_region_name { get; set; }
        public string census_division { get; set; }
        public string census_division_name { get; set; }
        public string circuit_court { get; set; }


    }
}
