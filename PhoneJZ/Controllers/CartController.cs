using Microsoft.AspNet.Identity;
using PhoneJZ.App_Data;
using PhoneJZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace PhoneJZ.Controllers
{
    public class CartController : Controller
    {
        PhoneDB db = new PhoneDB();
        public Cart getCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddCart(int id)
        {
            var pro = db.Products.SingleOrDefault(s => s.Id == id);
            if (pro != null)
            {
                getCart().Add(pro);
            }
            return RedirectToAction("ShowCart", "Cart");
        }
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("ShowCart", "Cart");
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult UpdateCart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id = int.Parse(form["Id"]);
            int quantity = int.Parse(form["quantity"]);
            cart.Update(id, quantity);
            if (quantity < 1)
            {
                cart.ClearCart();
            }
            if (quantity > 10)
            {
                cart.ClearCart();
            }
            return RedirectToAction("ShowCart", "Cart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove(id);
            return RedirectToAction("ShowCart", "Cart");
        }
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                OrderPro bill = new OrderPro();
                bill.dateOrder = DateTime.Now;
                bill.totalPrice = cart.Total();
                bill.userID = User.Identity.GetUserId();
                bill.fullname = form["shipName"];
                bill.address = form["shipAddress"];
                db.OrderProes.Add(bill);
                foreach (var item in cart.Items)
                {
                    OrderDetail detailOrder = new OrderDetail();
                    detailOrder.detailID = bill.Id;
                    detailOrder.productID = item.product.Id;
                    detailOrder.quantity = item.quantity;
                    detailOrder.unitPrice = item.product.totalPrice;
                    db.OrderDetails.Add(detailOrder);
                }
                db.SaveChanges();
                cart.ClearCart();
                return Redirect("/Bills/Details/" + bill.Id);
            }
            catch
            {
                return Content("Lỗi rồi bạn êi");
            }
        }
    }
}