# kask-kiosk
Team junior project

#### Build Status
* Build Passing as of 2014-04-18 (merged iteration-2 back into master)

#### Branches Breakdown:
* MASTER:                 Applicant Relations and all.  (Should be for Iteration 1)
* Application-Service:    Application's Service (WCF)   (Service Layer to expose models from DAL layer)
* views:                  ASP.NET Views                 (UI Layer from ASP.NET)

#### Project Breakdown:
* **DAL (Data Access Layer)** - Simple POCO Classes, might have to use repositories for caching
* **Service Layer**           - Exposes data for consuming for WEB API (currently using DAO)
* **ASP.NET WEB API**         - API layer that consumes services
* **ASP.NET MVC (ASPX)**      - Main Application that uses API to access Service that exposes the data.