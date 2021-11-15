using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NakoShopMVC.Data;
using NakoShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NakoShopMVC.Data.Cart
{
    public class DVDCart
    {
        public AppDbContext _context { get; set; }

        public string DVDCartId { get; set; }
        public List<DVDCartItem> DVDCartItems { get; set; }

        public DVDCart(AppDbContext context)
        {
            _context = context;
        }

        public static DVDCart GetDVDCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("DVDCartId") ?? Guid.NewGuid().ToString();
            session.SetString("DVDCartId", cartId);

            return new DVDCart(context) { DVDCartId = cartId };
        }

        public void AddItemToCart(Movie movie)
        {
            var dVDCartItem = _context.DVDCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.DVDCartId == DVDCartId);

            if(dVDCartItem == null)
            {
                dVDCartItem = new DVDCartItem()
                {
                    DVDCartId = DVDCartId,
                    Movie = movie,
                    Amount = 1
                };

                _context.DVDCartItems.Add(dVDCartItem);
            } else
            {
                dVDCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Movie movie)
        {
            var dVDCartItem = _context.DVDCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.DVDCartId == DVDCartId);

            if (dVDCartItem != null)
            {
                if(dVDCartItem.Amount > 1)
                {
                    dVDCartItem.Amount--;
                } else
                {
                    _context.DVDCartItems.Remove(dVDCartItem);
                }
            }
            _context.SaveChanges();
        }

        public List<DVDCartItem> GetDVDCartItems()
        {
            return DVDCartItems ?? (DVDCartItems = _context.DVDCartItems.Where(n => n.DVDCartId == DVDCartId).Include(n => n.Movie).ToList());
        }

        public double GetDVDCartTotal() =>  _context.DVDCartItems.Where(n => n.DVDCartId == DVDCartId).Select(n => n.Movie.Price * n.Amount).Sum();

        public async Task ClearDVDCartAsync()
        {
            var items = await _context.DVDCartItems.Where(n => n.DVDCartId == DVDCartId).ToListAsync();
            _context.DVDCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
