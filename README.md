# PaymentGatewayService

There are five projects created to develop Payment Gateway Serivces

### PaymentGatway.Api Project

    - Provides Transaction Processing Api
    - Provides GetTransactionById Api
    - Provides AddMerchant Api
    - Provides AddCurrency Api
    - Provides AddBank Api

### PaymentGateway.Core

    This class library project contains common code:
    
    - Validations (Attribute validations)
    - Modelw
    - Encryption (in-memory key generation solution whilst testing)

### PaymentGateway.Infrastructure

    This class library project is for data layer:
    
    - Data context
    - Migrations
    - Repositories
 
 ### Banking.Api
 
    - Validates Merchant Request
    - Responds to Merchant Request
    
 ### PaymentGatewayApiTests
 
    - UnitTests 
    
## Note:
  - Docker images and docker-compose files are work-in-process. 
  - Docker compose will create containers which may exit.
  - Currently application captures 50 unit-tests, however, wider set of unit-tests also work-in-process.
    
 
   
