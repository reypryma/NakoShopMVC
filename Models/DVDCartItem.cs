using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NakoShopMVC.Models
{
    public class DVDCartItem
    {
        [Key]
        public int Id { get; set; }

        public Movie Movie { get; set; }
        public int Amount { get; set; }


        public string DVDCartId { get; set; }
    }
}
