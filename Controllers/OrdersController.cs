using NakoShopMVC.Data.Cart;
using NakoShopMVC.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NakoShopMVC.Data.ViewModels;

namespace NakoShopMVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly DVDCart _dvdCart;
        private readonly IOrdersService _ordersService;

        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrdersService ordersService, DVDCart dVDCart)
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
            _dvdCart = dVDCart;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = "";

            var orders = await _ordersService.GetOrdersByUserIdAsync(userId);
            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }


        public IActionResult DVDCart()
        {
            var items = _dvdCart.GetDVDCartItems();
            _dvdCart.DVDCartItems = items;

            var response = new DVDCartVM()
            {
                DVDCart = _dvdCart,
                DVDCartTotal = _dvdCart.GetDVDCartTotal()
            };

            return View(response);
        }


        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }


        public async Task<IActionResult> AddItemToDVDCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _dvdCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(DVDCart));
        }


        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromDVDCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _dvdCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(DVDCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = "";
            string userEmailAddress = "";

            await _ordersService.StoreOrderMoviesAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }

        public async Task<IActionResult> CompleteDVDOrder()
        {
            var items = _dvdCart.GetDVDCartItems();
            string userId = "";
            string userEmailAddress = "";

            await _ordersService.StoreOrderDVDsAsync(items, userId, userEmailAddress);
            await _dvdCart.ClearDVDCartAsync();

            return View("OrderCompleted");
        }
    }
}
