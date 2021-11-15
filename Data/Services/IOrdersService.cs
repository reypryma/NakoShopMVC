using NakoShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NakoShopMVC.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderMoviesAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);

        Task StoreOrderDVDsAsync(List<DVDCartItem> items, string userId, string userEmailAddress);

        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    }
}
