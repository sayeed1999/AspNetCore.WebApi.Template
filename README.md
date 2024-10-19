# ASP.NET Core Web API Template

**AspNetCore.WebApi.Template** is an open-source project to speed up the API development process of any small-large scale application in ASP.NET Core. This comes with out-of-the box prebuilt setups including -

- **ASP.NET Core** Web API with **PostgreSQL** database
- **Docker** container support
- **Clean Architecture** with **CQRS** and **Mediatr**
- **FluentValidation** middleware pipeline to validate requests
- Sample code for **CRUD operation** on Products, Categories
- Sample code for **functional testing** of business usecases
- <i>any many more..</i>

## Table of Contents

- [Architecture](#architecture)
- [Set Up Environment Variables](#set-up-environment-variables)
- [Run with Docker](#run-with-docker)
- [Add a Migration](#add-a-migration)
- [Contribute to this Project](#contribute-to-this-project)
- [Special Thanks](#special-thanks)

## Architecture

This project is built on top of **Clean Architecture** with **CQRS** and **Mediator** pattern in **ASP.NET Core**.

To get a high-level overview of the responsibilites each of the layers in this project, please refer to -

- [Presentation Layer](src/Web/README.md)
- [Application Layer](src/Application/README.md)
- [Infrastructure Layer](src/Infrastructure/README.md)
- [Domain Layer](src/Domain/README.md)

## Set Up Environment Variables

- Clone the repository & navigate to project root directory.

- Set necessary environment variables in your machine by following the .env.example file e.g

```bash
export POSTGRES_USER=xxx
export POSTGRES_PASSWORD=xxx
export PGADMIN_EMAIL=xxx
export PGADMIN_PASSWORD=xxx
```

## Run with Docker

- Ensure Docker is installed on your system.

- Run the docker containers using the following command.

```bash
docker-compose up -d
```

- Hit the healthcheck endpoint to see API is working on not.

```
URI: /GET localhost:5000/health
```

- Stop the docker container using the following command.

```bash
docker-compose down
```

## Add a Migration

Run the following command to add a migration

```bash
dotnet ef migrations add <Migration_Name> --project ./src/Infrastructure --startup-project ./src/Web --output-dir ./Data/Migrations
```

## Contribute to this Project

Please refer to contribution guide [page](./CONTRIBUTING.md).

## Special Thanks

I used the following two templates to start my own template:

- [jasontaylordev/CleanArchitecture](https://github.com/jasontaylordev/CleanArchitecture)
- [jasontaylordev/NorthwindTraders](https://github.com/jasontaylordev/NorthwindTraders)
