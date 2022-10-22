    ____                              ____             _                  _ 
    |  _ \                            |  _ \           | |                | |
    | |_) |_   _ _ __ __ _  ___ _ __  | |_) | __ _  ___| | _____ _ __   __| |
    |  _ <| | | | '__/ _` |/ _ \ '__| |  _ < / _` |/ __| |/ / _ \ '_ \ / _` |
    | |_) | |_| | | | (_| |  __/ |    | |_) | (_| | (__|   <  __/ | | | (_| |
    |____/ \__,_|_|  \__, |\___|_|    |____/ \__,_|\___|_|\_\___|_| |_|\__,_|
                   __/ |                                                  
                  |___/                                                   
# Burger Backend 1.0
The new Burger Backend 1.0 will soon serve websites and apps.

## Installation
Make sure you have SQL Express 2019 installed or change the connectionstring in appsettings.json
1. pull the code
2. Open the solution (via .sln file)
3. In the Package Manager Console select Database project and run "database-update" command
4. Run and debug API

## Endpoints

| Api name     | Endpoint                      | HttpMethod | Description                                         | Requirements               |
|--------------|-------------------------------|------------|-----------------------------------------------------|----------------------------|
| Authenticate | /api/Authenticate/login       | POST       | Is used to authenticate user and get JWT token      | None                       |
| Users        | /api/Users                    | GET        | Get all users in the database                       | None                       |
| File         | /api/Upload                   | POST       | Uploads a file and returns the stored path          | None                       |
| Restaurants  | /api/Restaurants/Nearby       | GET        | Gets all restaurants with a specied range of meters | None                       |
| Restaurants  | /api/Restaurants              | GET        | Gets all restaurants sorted by range                | None                       |
| Restaurants  | /api/Restaurants              | POST       | Create a new restaurant                             | Authenticated + admin role |
| Restaurants  | /api/Restaurants/{id}         | GET        | Get a restaurant details by id                      | None                       |
| Restaurants  | /api/Restaurants/Rate         | POST       | Rate a restaurant                                   | Authenticated              |

## Login flow
1. Go to api via swagger /swagger/index.html
2. call /api/Authenticate/login with admin credentials (admin/1234)
3. copy the token
4. in the upper right corner in swagger click "Authorize"
5. Paste in the token and click "Authorize"
6. You can now create a new restaurant which requires that you are authorized and the role "admin"

## Users

| Username | Password | Roles |
|----------|----------|-------|
| admin    | 1234     | admin |
| test     | 1234     | None  |
| test1    | 1234     | None  |
| test2    | 1234     | None  |

## Infrastructure
- API hosted as an Azure App Service (scale-up, scale-out)
- Database hosted as an serverless Azure SQL Database

## Deployment
- Deployment of infrastructure should be done by pipelines to Azure
- Deployment and configuration of code should be done by pipelines to Azure

## What's missing?
- There's no usefull logging to e.g. application insights, file or serilog/elasticsearch.
- Authentication is hardcoded but in the future we could support google authentication, MSAL or Duende Identity Server.
- A UI - either a web app (angular/react) or a mobile app.

## Notes
If you do not provide a location (latitude/longitude) a default loaction in Copenhagen is chosen.

The data seeding adds 13 restaurants in Copenhagen, 1 in Hillerød and 2 in Billund




