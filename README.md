# N-Store WebApi

# dotNET 6.0, what's new?

    Add general using for our project.

    Explixit about nullable property

    Simplify program.cs and startup.cs file

# Self-signed Certificate

It already be included when we insatall dotNET SDK.

If it not trusted yet, we can trust seft-signed certificate by:

> dotnet dev-certs https --trust

# Architecture

[Project structure](https://fpt-software.udemy.com/course/learn-to-build-an-e-commerce-app-with-net-core-and-angular/learn/lecture/18136720?start=165#questions)

It is a 3-layer architecture web-api project

<img src="./img/1.png" width="900">

```
> dotnet new classlib -o Core

> dotnet new classlib -o Infrastructure

> dotnet sln add Core

> dotnet sln add Infrastructure

> nstore-webapi/API: dotnet add references ../Infrastructure

> nstore-webapi/Infrastructure: dotnet add references ../Core

> dotnet restore
```

# Reviewing some basic of dotNet Web Api

[Lesson10](https://fpt-software.udemy.com/course/learn-to-build-an-e-commerce-app-with-net-core-and-angular/learn/lecture/18136686#notes)

## Solution

Where we contain/organize our project, there will be three projects in our solution

## Controller

First thing will be hit when client send request

## Controller Attribute

[ApiController] -> Helping model binding, mapping dataSource come from client

## Attribute

[HttpPost] 

    Allow action method to return any-know http status code 

    Decide what action method are hited when client send request

Here are some more examples of attributes that are available:

[Route]	Specifies URL pattern for a controller or action.
[Bind]	Specifies prefix and properties to include for model binding.
[HttpGet]	Identifies an action that supports the HTTP GET action verb.
[Consumes]	Specifies data types that an action accepts.
[Produces]	Specifies data types that an action returns.

## Startup.cs 

<img src="./img/2.png" width="900">

<img src="./img/3.png" width="900">

## Routing matching machenism of ApiController

It combine:

    [Route("api/[controller]")] attribute
    
    [HttpVerb] attribute

    Parameter of action method

# Entity Framework

Entity framework is an convention-based Oject-Relational-Mapper.

## dotNet Entity framework command line tools.

```
> dotnet tool list -g  //list all tool available
> dotnet tool install --global dotnet-ef --version 6.0.4 //install dotnet-ef tool version 6.0.4
```

## dotnet-ef command line 

```
> dotnet ef -h //list all fucntion
> dotnet ef migrations //generate the code which we can use to scaffold our database
> dotnet ef database //create and update our database based on the code in Migrations
```

```
> dotnet ef migrations add <message> -o <path> //create migrations scraffold code in path directory

```

## DbContext(n)

DbContext is a represent for our Database, it is an abstract layer of our database, we not directly query into the database, we use the help of available method in Entity Framework 

We inject our DbContext to where ever we want to query to the database

ex: DbContext.FindAsync Method

> we can say DbContext is combination of generic repository pattern and Unit of work pattern

> away our business code to the database

- Generic Repository: Repository of type T, allow us to central and working on one generic repository. 

- Unit of work: 



## DbSet(n)

Prepresent for our table in database

## Migrations(v)

[Migration](https://fpt-software.udemy.com/course/learn-to-build-an-e-commerce-app-with-net-core-and-angular/learn/lecture/18136702#notes)

Migrate our data models, dbset, dbcontext into code for creating database purpose.

## Database(v) - Update(v)

[Update Databse](https://fpt-software.udemy.com/course/learn-to-build-an-e-commerce-app-with-net-core-and-angular/learn/lecture/18136706#notes)

Apply our lastest migration code - create database if it does not appear, update database if already appeared.

## Override default migrations configuration

<img src="./img/4.png" width="900">

<img src="./img/5.png" width="900">

## Apply latest existing migrations when building project

<img src="./img/6.png" width="900">

---

---

# API Architecture

## Repository pattern
