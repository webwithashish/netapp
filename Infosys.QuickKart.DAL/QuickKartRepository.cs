using Infosys.QuickKart.DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infosys.QuickKart.DAL
{
    public class QuickKartRepository
    {
        private QuickKartDbContext Context { get; set; }

        public QuickKartRepository(QuickKartDbContext context)
        {
            Context = context;
        }

        public string ValidateLoginUsingLinq(string emailId, string password)
        {
            string roleName = "";
            try
            {
                var objUser = (from usr in Context.Users
                               where usr.EmailId == emailId && usr.UserPassword == password
                               select usr.Role).FirstOrDefault<Role>();

                if (objUser != null)
                {
                    roleName = objUser.RoleName;
                }
                else
                {
                    roleName = "Invalid credentials";
                }
            }
            catch (Exception)
            {
                roleName = "Invalid credentials";
            }
            return roleName;
        }

        //Register customer using LINQ
        public bool RegisterUserUsingLinq(User user)
        {
            bool status;
            try
            {
                var role = (from rol in Context.Roles where rol.RoleName == "Customer" select rol).FirstOrDefault<Role>();

                if (role != null)
                {
                    user.Role = role;
                }
                else
                {
                    status = false;
                }
                Context.Users.Add(user);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        //Get all categories using LINQ
        public List<Category> GetCategoriesUsingLinq()
        {
            List<Category> lstCategories = new List<Category>();
            try
            {
                lstCategories = (from c in Context.Categories
                                 orderby c.CategoryId
                                     ascending
                                 select c).ToList<Category>();
            }
            catch (Exception)
            {
                lstCategories = null;
            }
            return lstCategories;
        }

        //Display all the products
        public List<Product> DisplayProductDetails()
        {
            List<Product> lstProducts = new List<Product>();
            try
            {
                lstProducts = (from c in Context.Products
                               orderby c.CategoryId
                                   ascending
                               select c).ToList<Product>();
            }
            catch (Exception ex)
            {
                lstProducts = null;
            }
            return lstProducts;
        }

        //Display purchases of customer
        public List<PurchaseDetail> DisplayPurchaseDetailsByCustomer(string emailId)
        {
            List<PurchaseDetail> lstPurchaseDetails = new List<PurchaseDetail>();
            try
            {
                lstPurchaseDetails = Context.PurchaseDetails.Include(x => x.Product).Where(x => x.EmailId == emailId).OrderByDescending(x => x.DateOfPurchase).Select(x => x).ToList<PurchaseDetail>();
            }
            catch (Exception ex)
            {
                lstPurchaseDetails = null;
            }
            return lstPurchaseDetails;
        }

        //Display product based upon product name
        public List<Product> DisplayProductDetailsByProductName(string subStr)
        {
            List<Product> lstProducts = null;
            try
            {
                lstProducts = (from c in Context.Products
                               where c.ProductName.ToLower().Contains(subStr.ToLower())
                               orderby c.CategoryId ascending
                               select c).ToList<Product>();
            }
            catch (Exception ex)
            {
                lstProducts = null;
            }
            return lstProducts;
        }

        //Add Products to cart
        public int AddProductToCartUsingUSP(string productId, string emailId)
        {
            System.Nullable<int> returnvalue = -1;
            try
            {
                SqlParameter prmCategoryName = new SqlParameter("@ProductId", productId);
                SqlParameter prmCategoryId = new SqlParameter("@EmailId", emailId);
                var returnval = Context.Database.ExecuteSqlRaw("EXEC dbo.usp_AddProductToCart @ProductId, @EmailId", new[] { prmCategoryName, prmCategoryId });
                returnvalue = Convert.ToInt32(returnval);
            }
            catch (Exception ex)
            {
                returnvalue = -1;
            }
            return Convert.ToInt32(returnvalue);
        }

        public List<CartProduct> FetchCartProductsByEmailId(string emailId)
        {
            List<CartProduct> lstProduct = new List<CartProduct>();
            try
            {
                SqlParameter prmEmailId = new SqlParameter("@EmailId", emailId);
                lstProduct = Context.CartProducts
                                    .FromSqlRaw("SELECT * FROM dbo.ufn_FetchCartProductByEmailId(@EmailId)", prmEmailId).AsNoTracking()
                                    .ToList();
            }
            catch (Exception ex)
            {
                lstProduct = null;
            }
            return lstProduct;
        }


        public bool UpdateCartProductsLinq(string productId, string emailId, short quantity)
        {
            bool status = false;

            Cart cartproduct = new Cart();
            try
            {
                using (var con = new QuickKartDbContext())
                {
                    cartproduct = (from cartProd in con.Carts where cartProd.ProductId == productId && cartProd.EmailId == emailId select cartProd).FirstOrDefault<Cart>();
                    cartproduct.Quantity = quantity;
                    con.Carts.Update(cartproduct);
                    con.SaveChanges();
                    status = true;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public bool DeleteCartProduct(string productId, string emailId)
        {
            bool status = false;
            try
            {
                var product = (from cart in Context.Carts
                               where cart.ProductId == productId && cart.EmailId == emailId
                               select cart).FirstOrDefault<Cart>();
                Context.Carts.Remove(product);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public List<Rating> DisplayAllReviewDetailsByCustomer(string emailId)
        {
            List<Rating> lstReviewDetails = new List<Rating>();
            try
            {
                lstReviewDetails = Context.Ratings.Where(x => x.EmailId == emailId).ToList<Rating>();
            }
            catch (Exception ex)
            {
                lstReviewDetails = null;
            }
            return lstReviewDetails;
        }

        public bool AddRatings(Rating rating)
        {
            bool status = false;
            try
            {
                Rating ratingObj = new Rating();
                ratingObj.EmailId = rating.EmailId;
                ratingObj.ProductId = rating.ProductId;
                ratingObj.ProductName = rating.ProductName;
                ratingObj.ReviewRating = rating.ReviewRating;
                ratingObj.ReviewComments = rating.ReviewComments;

                Context.Ratings.Add(ratingObj);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public Rating GetProductReviewByCustomer(string emailId, string productId)
        {
            Rating rating = new Rating();
            try
            {
                rating = Context.Ratings.Where(r => r.EmailId == emailId && r.ProductId == productId).Select(r => r).SingleOrDefault();
            }
            catch (Exception ex)
            {
                rating = null;
            }
            return rating;
        }

        public bool UpdateReviewComments(string emailId, string productId, string newComment)
        {
            bool status = false;
            try
            {
                Rating rating = Context.Ratings.Where(r => (r.EmailId == emailId && r.ProductId == productId)).Select(r => r).FirstOrDefault<Rating>();
                rating.ReviewComments = newComment;
                Context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public bool DeleteRating(string emailId, string productId)
        {
            bool status = false;
            try
            {
                Rating rating = Context.Ratings.Where(r => (r.EmailId == emailId && r.ProductId == productId)).Select(r => r).FirstOrDefault<Rating>();
                Context.Ratings.Remove(rating);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
    }
}