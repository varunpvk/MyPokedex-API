## Description

MyPokedex API is a Restfull API with funny pokemon info. 
Powered by Swagger, you can access the document from https://mypokedexapi.azurewebsites.net/swagger/index.html


![Swagger](Swagger.PNG)

****
## Installation

**Step1: Git Clone** 

```` 
HTTPS ⮧
git clone https://github.com/varunpvk/MyPokedex-API.git 
SSH ⮧
git clone git@github.com:varunpvk/MyPokedex-API.git
````

**Step2: Powershell** 

![Powershell](Powershell.PNG)

****

____Docker Configuration____

**Step3: Docker Build**

````
docker build -f "<<PathtoRepoDirectory>>\MyPokedexAPI\Dockerfile" . -t mypokedexapi:dev
````

![Docker1](Docker1.PNG)

**Step4: Docker Run**
 
````
docker run -p 5000:5000 --name MyPokedexAPI <<mypokedexapi:dev imageid>> --entrypoint tail mypokedex:dev -f /dev/null
````

![Docker2](Docker2.PNG)

**Step5: Browse Swagger API from docker container**
 
![Browser](Browser.PNG)
****

____CLI Configuration____

**Step3: dotnet build**

````
dotnet build -c Release "MyPokedexAPI.sln" -o "..\ReleaseBits"
````

![Command Line](CommandLine.PNG)

**Step4: dotnet run**

````
dotnet "..\ReleaseBits\MyPokedex.API.dll"
````

![Command Line2](CommandLine2.PNG)

**Step5: Browse Swagger locally**

![Browser C L I](Browser_CLI.png)

****

## Tests

**Command Line** 

````
dotnet test "..\ReleaseBits\MyPokedex.Tests.dll"
````

![Tests C L I](Tests_CLI.png)

## Technical Stack

* .NET Core 3.0.1
* Restfull API
* C#
* XUnit
* Docker && Docker Engine

## Design Spec

* OnionArchitecture
  * External Service Calls are managed by Infrastructure library
  * Features are implemented in ApplicationServices Library
  * Endpoint implementation in WebAPI Project
  * Domain model implementation in Core
* DI
* SOLID principles

![Onion](Onion.png)

## What could be different in production?

* This project is lacking resilience in its design. For instance, when we fail to translate a text, we return original text.
* This can be improved by implementing fallback or cache policy to apply whenever an exception is raised.
* Another food for thought, is to cache the translation response and maintain the translations in a redis cache cluster. This would reduce the network bandwidth greatly, and helps in improving the concurrency request handling efficiency. 
