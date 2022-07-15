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

# API Architecture - Section 04

## Generic Repository pattern

### Problem

Currently we have one specific Repository (Product Repository) are used for 3 Entity (Product, ProductBrand, ProductType). And 3 methods to get list of each Entity.

There will be a problem when our applicaiton scale up, there will be more and more entity added into Repository, it definitely lead to duplicated code.

=> Generic Repository come into play

<img src="./img/7.png" width="900">

### Solution - Generic Repository

Basic implementation, utilize the generic type.

<img src="./img/8.png" width="900">

> Note that remember to add IGenericRepository into Startup.cs class

> services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

### Downside 

<img src="./img/9.png" width="900">

=> Specification pattern come into rescue

## Specification Pattern

[udemy.com/course/learn-to-build-an-e-commerce-app-with-net-core-and-angular/learn/lecture/18137170#questions](Specification pattern)

### Problem

With generic repository - it too much generic so we need to derived from generic repos to achieve Include or other requirement in ProductRepository

<img src="./img/10.png" width="900">

```
public class IProductRepository : IRepository
{
    IReadOnly<Product> FindProductAsync(Expression<Func<T, bool>> query)
    // query: x => x.Include(p => p.ProductionBrand)
}
```

=> This make generic repository pointless, so we need to cooperate with other pattern to include our navigation properties in Product Entity.

=> Specification pattern come into rescue

### Solution 

We describe a query as an object with meaningfull name, and we pass it into generic repository.

<img src="./img/11.png" width="900">

<img src="./img/12.png" width="900">

### Implementation

1. Step 1: create the specification class which contain the `criteria` (product.Where(p => p.Id == id)) and `includes` list (product.Where(p => p.Includes(x => x.ProductBrand)))

<img src="./img/13.png" width="900">

2. Step 2: create specification evaluator which translate `criteria` and `includes` into IQueryable<T> which is executable agains the database

<img src="./img/14.png" width="900">

3. Step 3: in GenericRepository, execute IQueryable<T> agains the database

<img src="./img/15.png" width="900">

4. Step 4: create specification class with specific purpose and meaningful name 

5. Step 5: pass the specification class from Controller to GenericRepository and it will be executed in database

<img src="./img/16.png" width="900">

## AutoMapper and Dtos object

### Problem

We do not want to return to user ugly, unflatten object which is hard to comsume

### Solution

Thanks to

AutoMapper - a convension based libery which is allow us to map the actual class into a Dtos object 

### Implementation

1. Step 1: Create MappingProfiles contain out custom convention profile

<img src="./img/17.png" width="900">

2. Step 2: Add our custom MappingProfile into AutoMapper services

<img src="./img/18.png" width="900">

3. Step 3: Inject Mapper dependency into Controller

<img src="./img/19.png" width="900">

# API Error Handling

To have a consitency - easy-to-comsume error response 

=> need a common-use class which is retruned to client whenever error occured

## ApiResponse Class

Responsible for return any kind of error in when application running

<img src="./img/20.png" width="900">

## BadRequest and NotFound Error

<img src="./img/21.png" width="900">

## Invalid EndPoint Error

This kind of request - we need to add one midleware at the very beginning of the request pipeline

=> This middleware will always fallback to Error controller whenever the end point is not matched

<img src="./img/22.png" width="900">

## Validation Error

This kind of error happened when client passing invalid parameters for any controller endpoints ( usually happend in a form - post request)

Model state ( ASP.NET Controller attribute) are responsible for generating this kind of error 

=> need to overide the config of [ControllerAttribute]

<img src="./img/23.png" width="900">

























