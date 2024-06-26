# Euronext Weather API

Euronext Weather API provides weekly weather forecasts to users and allows the addition of new forecasts.

![Euronext Weather API Swagger UI](image.png)

## Getting Started

This API is built on .NET 8.0 and can be easily run on Docker. Ensure that Docker and Docker Compose are installed on your system.

### Installation

Start the service using the Docker Compose command below in the project directory:

```bash
docker-compose up -d
```
The API listens on port 8080 by default.

###  API Usage
The API can be accessed through Swagger UI at https://localhost:32790/swagger. It includes the following endpoints:

GET /weather/weekly: Retrieves weather forecasts for the current week.
POST /weather/add: Adds a new weather forecast.
### Postman Collection

Requests in the Collection
```bash
weekly docker: Retrieves weekly forecasts from the API running on Docker.
weekly docker-compose: Retrieves weekly forecasts from the API running with Docker Compose.
add docker: Adds a new forecast to the API running on Docker.
add docker-compose: Adds a new forecast to the API running with Docker Compose.
```