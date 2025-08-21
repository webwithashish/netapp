using Infosys.QuickKart.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infosys.QuickKart.Services.Models;

namespace Infosys.QuicKart.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PurchaseController : Controller
    {
        QuickKartRepository repository;
        public PurchaseController(QuickKartRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public JsonResult GetPurchaseDetailsByEmailId(string emailId)
        {
            try
            {
                var purchaseList = this.repository.DisplayPurchaseDetailsByCustomer(emailId);

                var purchases = new List<PurchaseDetail>();
                if (purchaseList.Any())
                {
                    foreach (var purchase in purchaseList)
                    {
                        var purchaseObj = new PurchaseDetail();
                        purchaseObj.PurchaseId = purchase.PurchaseId;
                        purchaseObj.EmailId = purchase.EmailId;
                        purchaseObj.ProductId = purchase.ProductId;
                        purchaseObj.ProductName = purchase.Product.ProductName;
                        purchaseObj.QuantityPurchased = purchase.QuantityPurchased;
                        purchaseObj.PurchaseDate = purchase.DateOfPurchase.ToShortDateString();
                        purchases.Add(purchaseObj);
                    }
                }
                return Json(purchases);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
    }
}