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
    public class CartController : Controller
    {
        CartDbContext db = new CartDbContext();
        // GET: Cart
        public ActionResult ShoppingCart()
        {
            return View(db.carts);
        }
        public ActionResult AddToCart(string username, CartItemModel item)
        {
            var cart = db.carts.SingleOrDefault(c => c.OwnerName == username);

            if(cart == null)
            {
                cart = db.carts.Add(new CartModel()) ;
                cart.OwnerName = username;               
            }
            cart.items.Add(item);
            cart.TotalMoney += item.TotalMoney;
            db.SaveChanges();
            return View("ViewCart");
        }


        public ActionResult RemoveFromCart(string username, CartItemModel item)
        {
            
            var cart = db.carts.SingleOrDefault(c => c.OwnerName == username);
            if(cart == null)
            {
                return View();
            }
            else
            {
                var cartitem = cart.items.SingleOrDefault(ci => ci == item);
                if(cartitem == null)
                {
                    return View();
                }
                else
                {
                    cart.items.Remove(cartitem);
                    cart.TotalMoney -= item.TotalMoney;
                }
            }
            db.SaveChanges();
            return View();
        }
    }
}