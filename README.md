# Job Alert

## Requirements

### Option A

- [.NET 5](https://dotnet.microsoft.com/download)
- [MySQL 8](https://dev.mysql.com/doc/mysql-installation-excerpt/8.0/en/)
- [golang-migrate](https://github.com/golang-migrate/migrate/tree/master/cmd/migrate)

### Option B

Running with Docker does not require the prerequisites in option A to be installed.

- Docker
- Docker Compose

## Installation
1. Clone project
2. cd to cloned folder
3. run `dotnet restore` to download all projects' dependencies

## Running the API
1. cd to `src/API/`
2. run `dotnet run`

## Using the API
1. In your browser, go to `http://localhost:5000/jobsearch`
2. To search specific keyword: `http://localhost:5000/jobsearch?search=keyword`

## Running with Docker

### Services

- api: The API server
- worker: The background job batch program. Main functionality is to scrape websites.
- db: The MySQL database

### Run all

```bash
docker-compose up -d
```

### Running only 1 service

```bash
docker-compose -d db
```

### Migrate DB
```bash
docker-compose -d db migrate
```

### Running adminer
```bash
docker-compose -d db adminer

### Example

#### Running database only and rerun worker during development.

```bash
docker-compose -d db
docker-compose -d worker # run as many times for testing
```

### Stopping containers

To stop the containers without destroying the database.
```bash
docker-compose down
```

### Stop containers and destroy the database

```bash
docker-compose down -v
```

The `-v` argument will remove any persistent volumes declared in the docker-compose file.

