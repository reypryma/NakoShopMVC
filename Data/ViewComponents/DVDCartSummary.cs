using NakoShopMVC.Data.Cart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NakoShopMVC.Data.ViewComponents
{
    public class DVDCartSummary:ViewComponent
    {
        private readonly DVDCart _dVDCart;
        public DVDCartSummary(DVDCart dVDCart)
        {
            _dVDCart = dVDCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _dVDCart.GetDVDCartItems();

            return View(items.Count);
        }
    }
}
