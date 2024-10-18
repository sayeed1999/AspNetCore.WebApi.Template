# ASP.NET Core Web API Template

This template helps you to start building any small, medium to large scale projects in ASP.NET Core Web API.

## Getting Started

- Clone the repository & navigate to project root directory.

- Set necessary environment variables in your machine by following the .env.example file e.g

```bash
export POSTGRES_USER=xxx
export POSTGRES_PASSWORD=xxx
export PGADMIN_EMAIL=xxx
export PGADMIN_PASSWORD=xxx
```

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
