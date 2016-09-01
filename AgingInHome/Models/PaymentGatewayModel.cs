﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;



namespace AgingInHome.Models
{
    public class PaymentGatewayModel
    {
        public class ChargeCreditCard
        {
            public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, decimal amount)
            {
                
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

                // define the merchant information (authentication / transaction id)
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                {
                    name = ApiLoginID,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = ApiTransactionKey,
                };

                var creditCard = new creditCardType
                {
                    cardNumber = "4111111111111111",
                    expirationDate = "0718",
                    cardCode = "123"
                };

                var billingAddress = new customerAddressType
                {
                    firstName = "John",
                    lastName = "Doe",
                    address = "123 My St",
                    city = "OurTown",
                    zip = "98004"
                };

                //standard api call to retrieve response
                var paymentType = new paymentType { Item = creditCard };

                // Add line Items
                var lineItems = new lineItemType[2];
                lineItems[0] = new lineItemType { itemId = "1", name = "t-shirt", quantity = 2, unitPrice = new Decimal(15.00) };
                lineItems[1] = new lineItemType { itemId = "2", name = "snowboard", quantity = 1, unitPrice = new Decimal(450.00) };

                var transactionRequest = new transactionRequestType
                {
                    transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card

                    amount = amount,
                    payment = paymentType,
                    billTo = billingAddress,
                    lineItems = lineItems
                };

                var request = new createTransactionRequest { transactionRequest = transactionRequest };

                // instantiate the contoller that will call the service
                var controller = new createTransactionController(request);
                controller.Execute();

                // get the response from the service (errors contained if any)
                var response = controller.GetApiResponse();

                if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse != null)
                    {
                        Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                    }
                }
                else if (response != null)
                {
                    Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
                    if (response.transactionResponse != null)
                    {
                        Console.WriteLine("Transaction Error : " + response.transactionResponse.errors[0].errorCode + " " + response.transactionResponse.errors[0].errorText);
                    }
                }

                return response;
            }
         }
    }
}