using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace FoodCourtSystem.Models
{
    public class PaymentRequestModel {
        public string ID { get; set; }
        public string UserName { get; set; }
        [Range(0, 20000)]
        public int Amount { get; set; }
        public string Info { get; set; }

    }

    public class MomoRequest
    {
        public string partnerCode { get; set; }
        public string accessKey { get; set; }
        public string requestId { get; set; }
        public string amount { get; set; }
        public string orderId { get; set; }
        public string orderInfo { get; set; }
        public string returnUrl { get; set; }
        public string notifyUrl { get; set; }
        public string requestType { get; set; }
        public string signature { get; set; }
        public string extraData { get; set; }
        public MomoRequest()
        {
            requestType = "captureMoMoWallet";
            signature = "";
            extraData = "";
        }
        private string SerializeFields()
        {
            return "partnerCode=" + partnerCode +
                    "&accessKey=" + accessKey +
                    "&requestId=" + requestId +
                    "&amount=" + amount +
                    "&orderId="+ orderId+
                    "&orderInfo="+ orderInfo+
                    "&returnUrl=" + returnUrl +
                    "&notifyUrl=" + notifyUrl +
                    "&extraData=" + extraData;
        }
        public void GenerateSignature()
        {
            string serialFields = SerializeFields();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(serialFields);
            HMACSHA256 hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes("IV8mR4KEBlBlBlkn4VAAnrzjNnmSJpoV"));
            byte[] encryted_byte = hmacsha256.ComputeHash(bytes);
            string hex = BitConverter.ToString(encryted_byte);

            signature = hex.Replace("-", "").ToLower(); ;
            hmacsha256.Dispose();
        }
    }

    public class MomoResponse
    {
        public string requestId { get; set; }
        public int errorCode { get; set; } 
        public string orderId { get; set; }
        public string message { get; set; }
        public string localMessage { get; set; }
        public string requestType { get; set; }
        public string payUrl { get; set; }
        public string signature { get; set; }
        
        public MomoResponse()
        {
            requestType = "captureMoMoWallet";
        }
        private string SerializeFields()
        {
            return "requestId=" + requestId +
                   "&orderId=" + orderId +
                   "&message=" + message +
                   "&localMessage=" + localMessage +
                   "&payUrl=" + payUrl +
                   "&errorCode=" + errorCode +
                   "&requestType=" + requestType;
                   
        }
        public bool CompareSignature()
        {
            string serialFields = SerializeFields();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(serialFields);
            HMACSHA256 hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes("IV8mR4KEBlBlBlkn4VAAnrzjNnmSJpoV"));
            byte[] encryted_byte = hmacsha256.ComputeHash(bytes);
            string hex = BitConverter.ToString(encryted_byte);

            string created_signature = hex.Replace("-", "").ToLower(); ;
            hmacsha256.Dispose();
            return created_signature == signature;
        }
    }
    public class ExternalPaymentDbContext: DbContext
    {
        public ExternalPaymentDbContext():
            base("ExternalPaymentContext")
        { }
        DbSet<PaymentRequestModel> paymentRequests;
    }
}