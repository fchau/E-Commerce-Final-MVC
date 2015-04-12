using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the first address line")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered mobile format is not valid.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }

        public string State { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }

        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter a country name")]

        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
