using Infosys.QuickKart.DAL;
using Infosys.QuickKart.DAL.Models;
using Infosys.QuickKart.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Infosys.QuicKart.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        QuickKartRepository repository;
        public UserController(QuickKartRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public JsonResult GetCartProducts(string emailId)
        {
            var cartProducts = new List<CartProductsDetail>();
            try
            {
                var cartProductList = this.repository.FetchCartProductsByEmailId(emailId);
                CartProductsDetail product;              
                if (cartProductList.Any())
                {
                    foreach (var prod in cartProductList)
                    {
                        product = new CartProductsDetail();
                        product.ProductId = prod.ProductId;
                        product.ProductName = prod.ProductName;
                        product.Quantity = prod.Quantity;
                        product.QuantityAvailable = prod.QuantityAvailable;
                        product.Price = prod.Price;

                        cartProducts.Add(product);
                    }
                } 
            }
            catch (Exception)
            {
                return null;
            }
            return Json(cartProducts);
        }
        [HttpPut]
        public JsonResult UpdateCartProducts(QuickKart.Services.Models.Cart cartObj)
        {
            bool status = false;
            try
            {
                status = this.repository.UpdateCartProductsLinq(cartObj.ProductId, cartObj.EmailId, cartObj.Quantity);
            }
            catch (Exception)
            {
                status = false;
            }

            return Json(status);
        }

        [HttpDelete]
        public JsonResult DeleteCartProduct(QuickKart.Services.Models.Cart cartObj)
        {
            var status = false;
            try
            {
                status = this.repository.DeleteCartProduct(cartObj.ProductId, cartObj.EmailId);
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(status);
        }

        [HttpPost]
        public JsonResult ValidateUserCredentials(QuickKart.Services.Models.User userObj)
        {
            string roleName = "";
            try
            {
                roleName = this.repository.ValidateLoginUsingLinq(userObj.EmailId, userObj.UserPassword);
            }
            catch (Exception)
            {
                roleName = "Invalid credentials";
            }

            return Json(roleName);
        }

        [HttpPost]
        public JsonResult InsertUserDetails(QuickKart.Services.Models.User user)
        {
            bool status;
            try
            {
                QuickKart.DAL.Models.User userObj = new QuickKart.DAL.Models.User();
                userObj.EmailId = user.EmailId;
                userObj.UserPassword = user.UserPassword;
                userObj.Gender = user.Gender;
                userObj.DateOfBirth = user.DateOfBirth;
                userObj.Address = user.Address;


                status = this.repository.RegisterUserUsingLinq(userObj);
            }
            catch (Exception)
            {
                status = false;
            }
            return Json(status);
        }

        [HttpPost]
        public JsonResult AddProductToCart(QuickKart.Services.Models.Cart cartObj)
        {
            int returnvalue = -1;
            try
            {
                returnvalue = this.repository.AddProductToCartUsingUSP(cartObj.ProductId, cartObj.EmailId);
            }
            catch (Exception)
            {
                returnvalue = -1;
            }
            return Json(returnvalue);
        }

    }
}
