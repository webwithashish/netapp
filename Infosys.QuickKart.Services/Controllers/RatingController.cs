using Infosys.QuickKart.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infosys.QuickKart.Services.Models;

namespace Infosys.QuicKart.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RatingController : Controller
    {
        QuickKartRepository repository;
        public RatingController(QuickKartRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public JsonResult DisplayAllReviewDetailsByEmailId(string emailId)
        {
            try
            {
                var ratingList = this.repository.DisplayAllReviewDetailsByCustomer(emailId);
                var rating = new QuickKart.Services.Models.Rating();
                var ratings = new List<QuickKart.Services.Models.Rating>();
                if (ratingList.Any())
                {
                    foreach (var rate in ratingList)
                    {
                        rating.EmailId = rate.EmailId;
                        rating.ProductId = rate.ProductId;
                        rating.ProductName = rate.ProductName;
                        rating.ReviewComments = rate.ReviewComments;
                        rating.ReviewRating = rate.ReviewRating;
                        ratings.Add(rating);
                    }
                }
                return Json(ratings);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public bool InsertRating(QuickKart.Services.Models.Rating rating)
        {
            bool status = false;
            try
            {

                QuickKart.DAL.Models.Rating ratings = new QuickKart.DAL.Models.Rating();
                ratings.EmailId = rating.EmailId;
                ratings.ProductId = rating.ProductId;
                ratings.ProductName = rating.ProductName;
                ratings.ReviewRating = rating.ReviewRating;
                ratings.ReviewComments = rating.ReviewComments;
                status = this.repository.AddRatings(ratings);
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        [HttpGet]
        public JsonResult GetProductReviewByCustomer(string emailId, string productId)
        {
            var rating = new QuickKart.Services.Models.Rating();
            try
            {
                var rate = this.repository.GetProductReviewByCustomer(emailId, productId);

                rating.EmailId = rate.EmailId;
                rating.ProductId = rate.ProductId;
                rating.ProductName = rate.ProductName;
                rating.ReviewRating = rate.ReviewRating;
                rating.ReviewComments = rate.ReviewComments;

            }
            catch (Exception ex)
            {
                rating = null;
            }
            return Json(rating);
        }

        [HttpPut]
        public bool UpdateReviewComments(QuickKart.Services.Models.Rating rating)
        {
            bool status = false;
            try
            {
                status = this.repository.UpdateReviewComments(rating.EmailId, rating.ProductId, rating.ReviewComments);
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        [HttpDelete]
        public bool DeleteRating(QuickKart.Services.Models.Rating rating)
        {
            var status = false;
            try
            {
                status = this.repository.DeleteRating(rating.EmailId, rating.ProductId);
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
    }
}