# WooliesXChallenge

Coding challenge from WooliesX, using .Net Core 2.2

## Authors

* **Roy Ren** - [renxiangyu16@hotmail.com](mailto:renxiangyu16@hotmail.com)

## Endpoints

* Exercise 1: [https://wooliesxchallenge20200313045751.azurewebsites.net/api]()
* Exercise 2: [https://wooliesxchallenge20200313045751.azurewebsites.net/api/product]()
* Exercise 3: [https://wooliesxchallenge20200313045751.azurewebsites.net/api/trolley]()
* Extra for Experts: [https://wooliesxchallenge20200313045751.azurewebsites.net/api/v2/trolley]()


## Main libraries from NuGet
* RestSharp: used for calling APIs
* Newtonsoft.Json: for converting between JSON and Object


## Project structure

### WolliesXchallenge

* Controllers: Entry point
* Models: Data model
* Services: Business logic layer
* Cache: Cache layer (Cache the popularity data)

### WolliesXchallengeTest

Provide unit test

* ProductPopularityServiceTest: Test the logic to generate popularity table
* ProductSortManagerTest: Test sorting product logic
* TrolleyCalculatorTest: The the trolley calculation algorithm
