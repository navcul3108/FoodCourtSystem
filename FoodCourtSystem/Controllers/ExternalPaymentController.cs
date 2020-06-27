using System;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using FoodCourtSystem.Models;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace FoodCourtSystem.Controllers
{
    [Authorize]
    public class ExternalPaymentController : Controller
    {
        private ExternalPaymentDbContext db = new ExternalPaymentDbContext();
        private static readonly HttpClient httpClient;
        // GET: ExternalPaymment
        static ExternalPaymentController()
        {
            httpClient = new HttpClient();
        }
        public ActionResult Pay()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Pay(PaymentRequestModel model)
        {
            try
            {
                //if (!ModelState.IsValid)
                //    return View("Error");
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://test-payment.momo.vn/gw_payment/transactionProcessor");

                MomoRequest mr = new MomoRequest();
                mr.partnerCode = "MOMOUPEC20200622";
                mr.accessKey = "WIxjsoFL6wdZ2Sav";
                mr.notifyUrl = "https://momo.vn";
                mr.returnUrl = "https://momo.vn";
                mr.orderId = model.ID;
                mr.amount = model.Amount.ToString();
                mr.orderInfo = model.Info;
                mr.requestId = new string(model.ID.Reverse().ToArray());
                mr.extraData = "";
                mr.GenerateSignature();

                string json = JsonConvert.SerializeObject(mr);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(request);
                var response_content = await response.Content.ReadAsStringAsync();
                MomoResponse mres = JsonConvert.DeserializeObject<MomoResponse>(response_content);
                // Check response is valid
                bool isValid = mres.CompareSignature();
                if (!isValid)
                    return View("Error");
                else
                    return Redirect(mres.payUrl);
            }
            catch(Exception)
            {
                return View("Error");
            }
           
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
                base.Dispose(disposing);
                //httpClient.Dispose();
            }

        }
    }
}