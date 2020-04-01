using System;
using System.Collections.Generic;
using System.Text;

namespace FlowersAndCandyCustomer.Models
{
    public class DeleteCardResponse
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
    }

    public class SavedCardModel
    {
        public string card_registrationsid { get; set; }
        public string card_number { get; set; }
        public string card_type { get; set; }
        public string card_type_image { get; set; }
        public string card_holder_name { get; set; }
        public string card_expiry_value { get; set; }
        public string card_expiry_month { get; set; }
        public string card_expiry_year { get; set; }

    }



    public class ParentOrder
    {
        public string id { get; set; }
        public string gateway_response { get; set; }
        public string registrationsid { get; set; }
    }

    public class Datum
    {
        public ParentOrder ParentOrder { get; set; }
    }

    public class CardResponse
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public List<Datum> data { get; set; }
    }









    // gatewayresponse

    public class Result
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    //public class ResultDetails
    //{
    //    public string ExtendedDescription { get; set; }
    //    public string ConnectorTxID1 { get; set; }
    //    public string connectorId { get; set; }
    //    public string __invalid_name__authorizationResponse.stan { get; set; }
    //    public string __invalid_name__transaction.receipt { get; set; }
    //    public string __invalid_name__transaction.acquirer.settlementDate { get; set; }
    //    public string ConnectorTxID2 { get; set; }
    //    public string AcquirerResponse { get; set; }
    //    public string reconciliationId { get; set; }
    //    public string __invalid_name__transaction.authorizationCode { get; set; }
    //    public string __invalid_name__sourceOfFunds.provided.card.issuer { get; set; }
    //}

    public class Card
    {
        public string bin { get; set; }
        public string binCountry { get; set; }
        public string last4Digits { get; set; }
        public string holder { get; set; }
        public string expiryMonth { get; set; }
        public string expiryYear { get; set; }
    }

    //public class Customer
    //{
    //    public string givenName { get; set; }
    //    public string email { get; set; }
    //    public string ip { get; set; }
    //    public string ipCountry { get; set; }
    //}

    public class ThreeDSecure
    {
        public string eci { get; set; }
        public string verificationId { get; set; }
        public string xid { get; set; }
        public string version { get; set; }
        public string dsTransactionId { get; set; }
        public string acsTransactionId { get; set; }
        public string paRes { get; set; }
    }

    public class CustomParameters
    {
        public string SHOPPER_EndToEndIdentity { get; set; }
        public string bill_number { get; set; }
        public string device_id { get; set; }
        public string teller_id { get; set; }
        public string branch_id { get; set; }
        public string CTPE_DESCRIPTOR_TEMPLATE { get; set; }
    }

    public class Risk
    {
        public string score { get; set; }
    }

    public class CardgatewayResponse
    {
        public string id { get; set; }
        public string registrationId { get; set; }
        public string paymentType { get; set; }
        public string paymentBrand { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string descriptor { get; set; }
        public string merchantTransactionId { get; set; }
        public Result result { get; set; }
        //public ResultDetails resultDetails { get; set; }
        public Card card { get; set; }
        //public Customer customer { get; set; }
        public ThreeDSecure threeDSecure { get; set; }
        public CustomParameters customParameters { get; set; }
        public Risk risk { get; set; }
        public string buildNumber { get; set; }
        public string timestamp { get; set; }
        public string ndc { get; set; }
    }
}
