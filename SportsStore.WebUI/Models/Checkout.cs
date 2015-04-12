using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Models
{
    public class Checkout
    {
        public Cart Cart { get; set; }
        public ShippingDetails ShippingDetails { get; set; }
    }
}