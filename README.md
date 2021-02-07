# OpenAPI
 OpenAPI (formerly Swagger) compliant web service that abstracts away two downstream APIs; the Chuck Norris API and the Star Wars API.

 ## Prerequisite
 
 * Install [.NET Core SDK v3.1.0 or above](https://www.microsoft.com/net/download)

## How to build and run?

Download/clone the OpenAPI repository to local machine

### Visual Studio:

* Open the solution and press F5 to debug. 
* There are other ways of running the project like right clicking the ``` wwwroot ``` folder under ``` Solution Explorer ``` and then ```View in Browser```


### On Powershell/ Command line:

Navigate to the project folder and enter the command:
```
dotnet run
```

or

Once in the project folder enter the following commands:
```
dotnet publish
cd bin\debug\netcoreapp3.1\publish or cd bin\release\netcoreapp3.1\publish
dotnet OpenAPI.dll
```

The output in the console will be something like:
```Now listening on: http://localhost:5000```
So if you then browse to `http://localhost:5000`, you will see the website

## How to query?

### Using Postman
[Postman](https://www.getpostman.com) is a useful tool for testing and experimenting with REST APIs. 

You can test the [OpenAPI](http://openapi.thronosconsolidated.com/swagger/index.html) quickly and easily using Postman.

* In Postman create a new GET API test
* Enter the API URL you are going to test

You are now ready to test the API e.g.
```http://localhost:5000/chuck/categories```

this should return a JSON payload e.g.
```
[
    "animal",
    "career",
    "celebrity",
    "dev",
    "explicit",
    "fashion",
    "food",
    "history",
    "money",
    "movie",
    "music",
    "political",
    "religion",
    "science",
    "sport",
    "travel"
]
```