using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using FoodCourtSystem.Models;
using System.Web.UI.WebControls;
using System.Net.Http;

namespace FoodCourtSystem.Controllers
{ 
    [Authorize(Roles = "Regular, Guest")]
    public class CartController : Controller
    {
        CartContext cartContext = new CartContext();
        ProductContext productContext = new ProductContext();
        OrderContext orderContext = new OrderContext();
        // GET: Cart
        public ActionResult ShoppingCart(string OwnerName)
        {
            CartModel model = cartContext.Carts.SingleOrDefault(item => item.OwnerName==OwnerName);
            if (model != null)
                return View(model);
            else
                return View("Error");
        }

        public ActionResult AddToCart(string productId)
        {
            var product = productContext.Products.SingleOrDefault(c => c.ID == productId);
            if (product == null) { return View("Error"); }

            HttpContextBase context = this.HttpContext;

            var cart = cartContext.Carts.SingleOrDefault(c => c.OwnerName == context.User.Identity.Name);
            if (cart == null)
            {
                cart = new CartModel()
                {
                    ID = DateTime.Now.Ticks.ToString(),
                    OwnerName = context.User.Identity.Name,
                };
            }
            var cartItem = cart.Items.SingleOrDefault(item => item.Product.ID == productId);

            if (cartItem == null)
            {
                cartItem = new CartItemModel()
                {
                    Product = product,
                    Quantity = 1,
                    ID = DateTime.Now.Ticks.ToString(),
                    Cart = cart,
                    TotalMoney = product.UnitPrice 
                };
                cart.Items.Add(cartItem);
                cart.UpdateTotalMoney();
            }
            else
            {
                cartItem.Quantity++;
                cartItem.TotalMoney += cartItem.Product.UnitPrice;
                cart.UpdateTotalMoney();
            }
            cartContext.Carts.Add(cart);
            cartContext.SaveChanges();

            return new EmptyResult();
        }


        public ActionResult RemoveFromCart(string cartItemId)
        {
            HttpContextBase context = this.HttpContext;
            var cart = cartContext.Carts.SingleOrDefault(c => c.OwnerName == context.User.Identity.Name);
            if (cart == null)
            {
                return View("Error");
            }
            else
            {
                var cartitem = cart.Items.SingleOrDefault(ci => ci.ID == cartItemId);
                if (cartitem == null)
                {
                    return View("Error");
                }
                else
                {
                    cart.TotalMoney -= cartitem.TotalMoney;
                    cart.Items.Remove(cartitem);
                }
            }
            cartContext.SaveChanges();
            return new EmptyResult();
        }
    }
}