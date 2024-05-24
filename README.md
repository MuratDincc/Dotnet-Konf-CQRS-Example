# .NET Core Web Api CQRS - Kafka Debezium

In this project, .NET Core 8 is used, focusing on a scenario where read and write principles are completely separated. Kafka Debezium has been utilized for data consistency. This repo is the sample project for the 'Applying CQRS with .NET Core in Financial Technologies' presentation at Dotnet Konf 2024

## Tech Stack

- .NET Core 8
- PostgreSQL
- MongoDB
- YARP
- Entity Framework Core
- Kafka Debezium
- Bogus

## Run with Docker

Start with Docker Compose

```bash
  docker compose up -d
```

Debezium Connector

```bash
{
    "name": "transaction-activity",
    "config": {
        "plugin.name": "pgoutput",
        "connector.class": "io.debezium.connector.postgresql.PostgresConnector",
        "database.hostname": "host.docker.internal",
        "database.port": "5432",
        "database.user": "postgres",
        "database.password": "123456",
        "database.dbname": "transaction_write_db",
        "table.include.list": "public.transaction",
        "topic.prefix": "data",
        "decimal.handling.mode": "double",
        "time.precision.mode": "connect",
        "key.converter": "org.apache.kafka.connect.storage.StringConverter",
        "key.converter.schemas.enable": "false",
        "value.converter": "org.apache.kafka.connect.json.JsonConverter",
        "value.converter.schemas.enable": "false",
        "include.schema.changes": "false",
        "transforms.filter.condition": "value.op == 'c' || value.op == 'u' || value.op == 'd'"
    }
}
```

## Access List

- PostgreSQL: 5432
- MongoDB: 27017
- Kafka: 9092-29092
- Kafka Connect: 8083
- Debezium UI: http://localhost:8083
- Kafka UI: http://localhost:8083
- Mongo UI: http://localhost:8081
- Gateway: http://localhost:5500
- Write Api: http://localhost:5501
- Read Api: http://localhost:5502