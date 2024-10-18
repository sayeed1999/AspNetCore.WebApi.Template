# ASP.NET Core Web API Template

Build any small, medium to large scale projects in ASP.NET Core Web API using this ASP.NET Core Web API template that comes with out-of-the box support with the followings -

- Dockerized ASP.NET Core Web API with PostgreSQL database
- Clean Architecture with Mediatr
- Sample code for CRUD operation on Products, Categories
- Sample code for functional testing of business usecases
- <i>any many more..</i>

## Table of Contents

- [Setting Up](#setting-up)
- [Running with Docker](#running-with-docker)
- [Adding a Migration](#adding-a-migration)
- [Special Thanks](#special-thanks)

## Setting Up

- Clone the repository & navigate to project root directory.

- Set necessary environment variables in your machine by following the .env.example file e.g

```bash
export POSTGRES_USER=xxx
export POSTGRES_PASSWORD=xxx
export PGADMIN_EMAIL=xxx
export PGADMIN_PASSWORD=xxx
```

## Running with Docker

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

## Adding a Migration

Run the following command to add a migration

```bash
dotnet ef migrations add <Migration_Name> --project ./src/Infrastructure --startup-project ./src/Web --output-dir ./Data/Migrations
```

## Special Thanks

I used the following two templates to start my own template:

- [jasontaylordev/CleanArchitecture](https://github.com/jasontaylordev/CleanArchitecture)
- [jasontaylordev/NorthwindTraders](https://github.com/jasontaylordev/NorthwindTraders)
