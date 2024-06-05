# ACME Learning Center Platform

## Summary
ACME Learning Center Platform API Application, made with Microsoft C#, ASP.NET Core, Entity Framework Core and MySQL persistence. It also illustrates open-api documentation configuration and integration with Swagger UI.

## Features
- RESTful API
- OpenAPI Documentation
- Swagger UI
- ASP.NET Framework
- Entity Framework Core
- Audit Creation and Update Date
- Custom Route Naming Conventions
- Custom Object-Relational Mapping Naming Conventions.
- MySQL Database
- Domain-Driven Design

## Bounded Contexts
This version of ACME Learning Center Platform is divided into three bounded contexts: Profiles, Publishing, and IAM.

### Profiles Context

The Profiles Context is responsible for managing the profiles of the users. It includes the following features:

- Create a new profile.
- Get a profile by id.
- Get all profiles.

This context includes also an anti-corruption layer to communicate with the Publishing Context. The anti-corruption layer is responsible for managing the communication between the Profiles Context and the Publishing Context. It offers the following capabilities to other bounded contexts:
- Create a new Profile, returning ID of the created Profile on success.
- Get a Profile by Email, returning the associated Profile ID on success.

### Publishing Context

The Publishing Context is responsible for managing the publishing lifecycle of learning resources, like tutorials and their owned assets (reading content, images, videos). Its features include:

- Create a Category.
- Get a Category by ID.
- Get All Categories.
- Create a Tutorial.
- Get a Tutorial by ID.
- Get Tutorials by Category ID.
- Get All Tutorials.
- Add a Video Asset to an existing Tutorial.

### Identity and Access Management (IAM) Context

The IAM Context is responsible for managing platform users, including the sign in and sign up processes. It applies JSON Web Token based authorization and Password hashing. It also adds a request authorization middleware to ASP.NET Core Pipeline, in order to validate included token in request header on endpoints that require authorization. Its capabilities include:

- Create a new User (Sign Up).
- Authenticate a User (Sign In).
- Get a User by ID.
- Get All Users.
- Add Authorization support to HTTP request middleware pipeline.
- Provide Annotation Attributes for decorating inbound services and actions in order to enable request authorization.
- Generate and validate JSON Web Tokens.
- Apply Password Hashing.

This context includes also an anti-corruption layer (ACL). The ACL is responsible for managing the communication between the IAM Context and other Bounded Contexts. Its capabilities include:

- Create a new User, returning ID of the created User on success.
- Get a Username by ID, returning the associated Username on success.
- Get a User ID by Username, returning the associated User ID on success.

In this version, Open API documentation includes support for JSON Web Token based authorization.