# X-Labs | Beer Quest

## API

API to search for pubs in Leeds area.

## Assumptions/Considerations

- Solution was built using .Net 6
- Used Clean Architecture for the solution folder structure, where infrastructure dependencies can be swapped easily. For example, solution doesn't need to change if we are moving from SQL Server to MongoDB.
- Used an InMemory database as its just for technical test,
- CSV data is imported during the startup into a InMemory database to return data when API is called. This can be done as a migration process before deploying the API on production on a relational or non-relational database,
- Added only POST http method for searching/filtering the data based on search criteria. Not added any other API operation as ther is no requirement to get single pub information or CRUD operations,
- Assumed the staff are interested in an area they are currently at using radius (in meters) with latitude and longitude. Also mainly added fields like address, stars and tags for filtering data. If needed other fields can be added as part of [PubsSearchParameters](/src/Core/Models/PubsSearchParameters.cs) class.
- Data returned by API is not ordered,
- For test, only In-Memory data store has been used. For production purpose that can be replace by using SQL database. This can be achieved by updating the DI for the `AppDbContext` class. An example is already provided in the [ServiceCollectionExtensions](/src/Infrastructure/ServiceCollectionExtensions.cs) class
- The Pubs model was kept to minimum to avoid complexity of the test but for production system the data model needs to be designed considering various aspect like separating pub details and reviews and ratings.
- I have also assumed that the API will always return 400 (BadReqeust) status when search parameter is incorrectly passed. 
- If the search parameter is empty, then api response will contain all records from the store.
- I have ignored the `category` field from the [leedsbeerquest.csv](/src/Infrastructure/CsvData/leedsbeerquest.csv) file as part of search criteria as I wasn't sure about it and if the records with `Closed venues` and `Uncategorized` category can be ignored in the search result.

## Improvements

- Healthcheck for the API dependencies can be added to check it's healthy and working.
- For data persistence, SQL Server database can replace the In-Memory store.
- Writing more logs for observablity purpose.
- Authentication for API can be added to secure the API.
- Code for API Client can be auto-generated using [NSwagStudio](https://github.com/RicoSuter/NSwag/wiki/NSwagStudio) required by front-end or third-party that are using the API.
- Paging the search API response can be helpful if it's returning more than 100 records,
- Adding a front-end to use this API and displaying result in a maps would be really cool.
- It would be better to search `Tags` by list instead of string containing text.

## Dev - Dependencies

### Tools
- Visual Studio Code or 2022
- Docker
- Powershell

### Libraries
- [EF Core](https://docs.microsoft.com/en-us/ef/) : database mapper for .NET
- [Swashbuckle.Aspnetcore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) : for [Swagger](https://swagger.io/) tooling for API's built with ASP.NET Core
- [CsvHelper](https://joshclose.github.io/CsvHelper/) : A .NET library for reading and writing CSV files. Extremely fast, flexible, and easy to use.
- [AutoMapper](https://automapper.org/) : A simple little library built to solve a deceptively complex problem - getting rid of code that mapped one object to another.
- [Geolocation](https://github.com/scottschluer/Geolocation) : Library that will calculate distance and cardinal direction between two sets of coordinates and provides the lat/long boundaries around an origin coordinate allowing for simple SQL or LINQ selection of locations within the given radius.

## Build using Powershell

Run the below command to build the solution locally;

> Note: Powershell script added to the solution folder are the build script that I have prepared which is used in most of the personal projects I work on.

Execute [build.ps1](/src/build.ps1) to compile application and package release artifacts. Outputs to `/dist/` folder.

The build script has configurable options which helps split build execution during the CI pipeline process. Below are some examples;

- Full Build: `PS> .\build.ps1`
- Debug Build: `PS> .\build.ps1 -Config "Debug"`
- Fast Build: `PS> .\build.ps1 -SkipTests`

### Code Coverage Report

The build script also outputs the code coverage result. The coverage report files can be found under `dist\Coverage` folder. After the build process is executed you can open the coverage report using [dist\Coverage\index.html](/dist/Coverage/index.html)

### `How to` build and run recording;

- Running full build using build.ps1 [build-and-test-run.mp4](images/build-and-test-run.mp4)
- Start the contaier using docker-compose [docker-compose-test-run.mp4](images/docker-compose-test-run.mp4)

## Build using Docker

The API is an ASP.Net core application intended to run as a docker container.

To Build, (re)create container image for service(s), run the below command:

``` shell
docker-compose build
```

To start, and attach to container for service(s), run: 

``` shell
docker-compose up
```

## Build using `dotnet` CLI

``` shell
dotnet build Test.sln
```

``` shell
dotnet test Test.sln
```

### To run the API

``` shell
cd "src\Api"
dotnet run Api.csproj
```
## Sample payloads for testing API

> `POST /api/pubs/` 

<details>
<summary>Sample 1 - Search by location and radius (in meters)</summary>
  
  Payload:
  ```json
  {
    "latitude": 53.799468,
    "longitude": -1.545427,
    "radiusInMeters": 50
  }
  ```
  Response: 200 Status Code
  ```json
  [
    {
      "name": "115 The Headrow",
      "category": "Pub reviews",
      "url": "http://leedsbeer.info/?p=2753",
      "date": "2014-10-18T15:48:51+00:00",
      "excerpt": "A bar that lives up to its name.",
      "thumbnail": "http://leedsbeer.info/wp-content/uploads/2014/10/115.jpg",
      "latitude": 53.7994003,
      "longitude": -1.545981,
      "address": "115 The Headrow, Leeds, LS1 5JW",
      "phone": "",
      "twitter": "BLoungeGrp",
      "starsBeer": 1.5,
      "starsAtmosphere": 3,
      "starsAmenities": 2.5,
      "starsValue": 2,
      "tags": "coffee,food",
      "id": "466cdcd6-f73c-4de2-8980-61923a3c24a1"
    },
    {
      "name": "The Northern Monkey",
      "category": "Closed venues",
      "url": "http://leedsbeer.info/?p=1770",
      "date": "2013-07-30T07:19:16+00:00",
      "excerpt": "A friendly, unpretentious Northern stereotype, with amazing burgers. Shame about the terrible beer selection!",
      "thumbnail": "http://leedsbeer.info/wp-content/uploads/2013/07/IMG_20130724_130306.jpg",
      "latitude": 53.7994461,
      "longitude": -1.5460253,
      "address": "115 The Headrow, Leeds LS1 5JW",
      "phone": "0113 242 6630",
      "twitter": "NorthernMonkey0",
      "starsBeer": 0.5,
      "starsAtmosphere": 3,
      "starsAmenities": 4,
      "starsValue": 3,
      "tags": "breakfast,dance floor,food,jukebox,live music,sports,sunday roasts",
      "id": "ab1aacf3-fe2c-4607-b610-250a4412d3ec"
    }
  ]
  ```
</details>

<details>
<summary>Sample 2 - search by Atmosphere Stars and Tags</summary>
  Payload:

  ```json
  {
    "atmosphereStars": 5,
    "tags": "coffee"
  }
  ```

  Expected Response: 200 Status Code
  ```json
  [
    {
      "name": "Cha Lounge",
      "category": "Bar reviews",
      "url": "http://leedsbeer.info/?p=3367",
      "date": "2016-01-10T22:17:37+00:00",
      "excerpt": "A delightful tea room and bar, offering a handful of good local beers and with room to grow. ",
      "thumbnail": "http://leedsbeer.info/wp-content/uploads/2016/01/IMG_20160109_192652572.jpg",
      "latitude": 53.793766,
      "longitude": -1.5395629,
      "address": "24 Dock Street, Leeds LS10 1JF",
      "phone": "07749 811218",
      "twitter": "ChaLoungeLeeds",
      "starsBeer": 2.5,
      "starsAtmosphere": 5,
      "starsAmenities": 4,
      "starsValue": 3,
      "tags": "breakfast,coffee,food,free wifi,sofas",
      "id": "f196280c-b4c7-4a98-bcbc-4583e7e2f70d"
    },
    {
      "name": "Veritas",
      "category": "Pub reviews",
      "url": "http://leedsbeer.info/?p=393",
      "date": "2012-10-11T19:38:27+00:00",
      "excerpt": "Great selection of beer and good pub food, but go for the great atmosphere.",
      "thumbnail": "http://leedsbeer.info/wp-content/uploads/2012/10/20121010_125049.jpg",
      "latitude": 53.8009071,
      "longitude": -1.5511496,
      "address": "43 Great George Street, Leeds LS1 3BB",
      "phone": "0113 242 8094",
      "twitter": "Veritas_Leeds",
      "starsBeer": 4.5,
      "starsAtmosphere": 5,
      "starsAmenities": 3,
      "starsValue": 2.5,
      "tags": "coffee,food,sunday roasts",
      "id": "e49ef170-93b1-4113-8722-f12b6a33309e"
    }
  ]
  ```
</details>

<details>
<summary>Sample 3 - Failed Response</summary>
  Payload:

  ```json
  // empty payload
  ```

  Response: 400 Status code
  ```json
  {
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-a5ea86789f4531dd9d63061b0538716a-ca20e81b669548fd-00",
    "errors": {
      "": [
        "A non-empty request body is required."
      ],
      "searchCriteria": [
        "The searchCriteria field is required."
      ]
    }
  }
  ```
</details>


