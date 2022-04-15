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

It is a 3-layer architecture web-api project

<img src="./img/1.PNG" width="900">

# Reviewing some basic of dotNet Web Api

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

<img src="./img/2.PNG" width="900">

╔══════════════════════════════════════════╦═══════════════════════════════════════╗
║             app.UseRouting()             ║          app.UseEndPoints()           ║
╠══════════════════════════════════════════╬═══════════════════════════════════════╣
║               Find Endpoint              ║           Execute Endpoint            ║
║                                          ║                                       ║
║  Adds route matching to the middleware   ║  Adds endpoint execution to the       ║
║  pipeline. This middleware looks at the  ║  middleware pipeline.                 ║
║  set of endpoints defined in the app,    ║  It runs the delegate associated      ║
║  and selects the best match based        ║  with the selected endpoint.          ║
║  on the request.                         ║                                       ║
║                                          ║                                       ║
╚══════════════════════════════════════════╩═══════════════════════════════════════╝

## Routing matching machenism of ApiController

It combine:

    [Route("api/[controller]")] attribute
    
    [HttpVerb] attribute

    Parameter of action method

# Entity Framework

Entity framework is an convention-based Oject-Relational-Mapper.

## DbContext

DbContext is a represent for our Database, it is an abstract layer of our database, we not directly query into the database, we use the help of Entity Framework 