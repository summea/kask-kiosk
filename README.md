# kask-kiosk
==========
Team junior project

#### Branches Breakdown:
* MASTER:                 Applicant Relations and all.  (Should be for Iteration 1)
* HR_Manager_Branch:      Staff Relations and all.      (Will merge later, should be for later iteration)
* Application-Service:    Application's Service (WCF)   (Service Layer to expose models from DAL layer)

#### Project Breakdown:
* **DAL (Data Access Layer)** - Simple POCO Classes, might have to use repositories for caching
* **Service Layer**           - Exposes data for consuming for WEB API
* **ASP.NET WEB API**         - API layer that consumes services
* **ASP.NET MVC (ASPX)**      - Main Application that uses API to access Service that exposes the data.
