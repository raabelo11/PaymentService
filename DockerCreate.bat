
rem Criar RabbitMQ - cria 5672 porta .NET 15672 porta web
docker run -d --hostname my-rabbit --name RabbitMQ -p 5672:5672 -p 15672:15672 rabbitmq:3-management

rem Cria Redis
docker run -d --name Redis -p 6379:6379 redis

rem Cria Kafka
docker run -d --name kafka -p 9092:9092 -e KAFKA_CFG_NODE_ID=1 -e KAFKA_CFG_PROCESS_ROLES=broker,controller -e KAFKA_CFG_LISTENERS=PLAINTEXT://:9092,CONTROLLER://:9093 -e KAFKA_CFG_ADVERTISED_LISTENERS=PLAINTEXT://localhost:9092 -e KAFKA_CFG_LISTENER_SECURITY_PROTOCOL_MAP=CONTROLLER:PLAINTEXT,PLAINTEXT:PLAINTEXT -e KAFKA_CFG_CONTROLLER_LISTENER_NAMES=CONTROLLER -e KAFKA_CFG_CONTROLLER_QUORUM_VOTERS=1@localhost:9093 -e ALLOW_PLAINTEXT_LISTENER=yes bitnami/kafka:latest