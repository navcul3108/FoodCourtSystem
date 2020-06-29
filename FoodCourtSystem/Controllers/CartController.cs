﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using FoodCourtSystem.Models;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Data.Entity.Migrations;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Web.Services.Description;

namespace FoodCourtSystem.Controllers
{ 
    [Authorize(Roles = "Regular, Guest")]
    public class CartController : Controller
    {
        MenuContext menuContext = new MenuContext();
        //OrderContext orderContext = new OrderContext();
        // GET: Cart
        public ActionResult ShoppingCart(string OwnerName)
        {
            CartModel model = menuContext.Carts.SingleOrDefault(item => item.OwnerName==OwnerName);
            if (model != null)
            {
                foreach (var cartItem in model.Items)
                    cartItem.Product = menuContext.Products.Single(item => item.ID == cartItem.ProductID);
                return View(model);
            }       
            else
                return View("Error");
        }

        public ActionResult AddToCart(string productId)
        {
            try
            {
                var product = menuContext.Products.SingleOrDefault(c => c.ID == productId);
                if (product == null)
                    return View("Error");

                HttpContextBase context = this.HttpContext;

                var cart = menuContext.Carts.SingleOrDefault(c => c.OwnerName == context.User.Identity.Name);
                if (cart == null)
                {
                    cart = new CartModel()
                    {
                        ID = DateTime.Now.Ticks.ToString(),
                        OwnerName = context.User.Identity.Name,
                    };
                }
                var cartItem = menuContext.CartItems.SingleOrDefault(item => item.Product.ID == productId && item.CartID == cart.ID);

                if (cartItem == null)
                {
                    cartItem = new CartItemModel
                    {
                        Product = new ProductModel() {
                            ID = product.ID,
                            Name = product.Name,
                            UnitPrice = product.UnitPrice,
                            ImageName = product.ImageName,
                            Description = product.Description,
                            Category = new CategoryModel ()
                            {
                                ID = product.Category.ID,
                                Name = product.Category.Name
                            }
                        },
                        Quantity = 1,
                        ID = DateTime.Now.Ticks.ToString(),
                        CartID = cart.ID,
                        TotalMoney = product.UnitPrice
                    };

                    cart.Items.Add(cartItem);
                    cart.TotalMoney += cartItem.TotalMoney;
                }
                else
                {
                    cartItem.Quantity++;
                    cartItem.TotalMoney += cartItem.Product.UnitPrice;
                    cart.TotalMoney += cartItem.Product.UnitPrice;
                }
                menuContext.CartItems.AddOrUpdate(cartItem);
                menuContext.Carts.AddOrUpdate(cart);
                menuContext.SaveChanges();

                return RedirectToAction("Index", "Menu");
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                   string Message = String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        string sub_mess = String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            
        }


        public ActionResult RemoveFromCart(string cartItemId)
        {
            HttpContextBase context = this.HttpContext;
            var cart = menuContext.Carts.SingleOrDefault(c => c.OwnerName == context.User.Identity.Name);
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
            menuContext.SaveChanges();
            return new EmptyResult();
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                menuContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}