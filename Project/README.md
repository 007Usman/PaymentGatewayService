# Instructions

### Setup Database Connection 

  - Update Database connection-string to localhost or your preffered database connection. 
  - Update Databse connection-string in the appsettings.json property "PaymentGatwayConnection" https://github.com/007Usman/PaymentGatewayService/blob/main/Project/PaymentGatewayService/PaymentGateway.API/appsettings.json 

### Setup Project

  - Application will seed data as it runs. You may add data additional data from provided APIs, i.e. AddMerchant, AddBank...
  - Application is configured to use Swagger endpoint. To use POSTMAN define route as https://localhost:5001/{define-route}
  - Start both projects Banking.Api and PaymentGateway.API from solution file present in the folder: https://github.com/007Usman/PaymentGatewayService/tree/main/Project/PaymentGatewayService 
  - PaymentGateway.API endpoint is currently found on port: https://localhost:5001/
  - Banking.API base address is currently set to "BankApiBaseAddress": "https://localhost:8080/api", in order for bank to response from different port. 
  

### PaymentGateway.API Message Structure

  - TransactionProcessing Api Expected Message Input format on route: /PaymentGatewayApi/ProcessTransactions
    
        {
            "merchantName": "Deliveroo",
            "bankName": "Barclays",
            "currencyCode": "GBP",
            "amount": 3000,
            "cardNumber": "1234567890123456",
            "expiryMonth": 6,
            "expiryYear": 24,
            "cardHolderName": "John Hill",
            "cvv": 999
        }
        
      Note: 
        - Guid and timestamps are auto generated and stored in the database.
        - Please add "expiryMonth" and "expiryYear" without leading zero, as its a known bug and currently being fixed. 
        
        
    - Bank Response Message

          {
              "transactionId": "2815026f-2bc8-4a1c-cb1c-08d9334acd66",
              "amount": 3000,
              "transactionStatus": "Successful",
              "bankReferenceId": "e4e77fc9-c4df-4a6e-89ee-2f3452d63032",
              "merchantId": "3b5a651e-b3e7-4c15-bd16-ad9f85202f26",
              "bankId": "2a6b86ee-6340-4be8-a7fc-179ed6aa6604",
              "bankCardId": "4364e2c3-a827-43b4-afcd-ce20ba37cc46",
              "currencyId": "2c65b39a-619b-4230-91c2-e267230fe4dd",
              "createdTimestamp": "2021-06-19T18:51:01.6164546+01:00",
              "modifiedTimestamp": "2021-06-19T18:51:01.6165739+01:00"
          }
        
        
   - GetTransactionById Expected Input Id on route: /PaymentGatewayApi/TransactionId/{id}
        
          2815026f-2bc8-4a1c-cb1c-08d9334acd66

     - Payment Gateway Response

           {
              "amount": 3000,
              "transactionStatus": "Successful",
              "bankResponseId": "e4e77fc9-c4df-4a6e-89ee-2f3452d63032",
              "bankCardId": "4364e2c3-a827-43b4-afcd-ce20ba37cc46",
              "cardNumber": "************3456",
              "expiryMonth": 6,
              "expiryYear": 24,
              "cardHolderName": "John Hill",
              "cvv": 999,
              "createdTimestamp": "2021-06-19T18:51:01.6164546+01:00",
              "modifiedTimestamp": "2021-06-19T18:51:01.6165739+01:00"
           }          
        
    
  - Provides GetMerchantById Api on route: /PaymentGatewayApi/MerchantId/{id}

        3b5a651e-b3e7-4c15-bd16-ad9f85202f26
        
    - Payment Gateway Response

          {
              "merchant": {
                "merchantName": "Deliveroo",
                "description": "Online food delivery service"
              },
              "amount": 3000,
              "transactionStatus": "Successful",
              "bankResponseId": "e4e77fc9-c4df-4a6e-89ee-2f3452d63032",
              "bankCardId": "4364e2c3-a827-43b4-afcd-ce20ba37cc46",
              "cardNumber": "************3456",
              "expiryMonth": 6,
              "expiryYear": 24,
              "cardHolderName": "John Hill",
              "cvv": 999,
              "createdTimestamp": "2021-06-19T18:51:01.6164546+01:00",
              "modifiedTimestamp": "2021-06-19T18:51:01.6165739+01:00"
          }
  
  - Provides AddBank Api on route: /PaymentGatewayApi/AddBank   

        {
            "bankName": "Lloyds"
        }
        
    - Payment Gateway Response

          {
            "bankName": "Lloyds"
          }
        
  - Provides AddCurrency Api on route /PaymentGatewayApi/AddCurrency

        {
            "currencyCode": "JPY",
            "currencyDescription": "Japan currency"
        }
  
     - Payment Gateway Response

            {
                "currencyCode": "JPY",
                "currencyDescription": "Japan currency"
            }
        
  - Provides AddMerchant Api on route: /PaymentGatewayApi/AddMerchant

        {
            "merchantName": "Costco",
            "description": "Wholesale Corporation"
        }
        
    - Payment Gateway Response

          {
              "merchantName": "Costco",
              "description": "Wholesale Corporation"
          }
          
          
          
### Banking.API Message Structure 

  - Transaction Api Expected Message Input format on route: /Transaction/Bank

          {
              "transactionAmount": 3000,
              "cardNumber": "1234567890123456",
              "cvv": 999,
              "cardHolderName": "John Hill",
              "expiryMonth": 6,
              "expiryYear": 24
          }
          
      - Bank Response

            {
              "bankResponseId": "f7ebc5e8-0e9a-4754-a496-f893c9ee738a",
              "transactionStatus": "Successful"
            }
