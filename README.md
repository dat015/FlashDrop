# FlashDrop

FlashDrop is a high-concurrency Flash Sale platform built with **ASP.NET Core**, **Microservices Architecture**, and **Event-Driven Architecture**. The project focuses on solving real-world distributed system challenges such as overselling prevention, distributed transactions, fault tolerance, idempotency, and high-volume request processing.

## Problem Statement

Flash sale campaigns typically receive a massive number of purchase requests within a few seconds while only a limited amount of inventory is available.

Example:

| Item              |           Value |
| ----------------- | --------------: |
| Product           | Limited Sneaker |
| Stock             |           1,000 |
| Waiting Users     |          50,000 |
| Incoming Requests |        100,000+ |

The system must guarantee:

- Sell exactly **1,000** products.
- Prevent overselling.
- Allow each customer to purchase only one item.
- Prevent duplicate orders.
- Ensure retry requests do not create duplicate orders.
- Restore inventory when payment fails.
- Prevent event loss during service failures.
- Protect the system from abusive traffic through rate limiting.

---

# Architecture

FlashDrop follows a **Microservices Architecture** combined with **Event-Driven Architecture**.

```
                Client
                   │
             API Gateway
                   │
    ┌──────────────┼──────────────┐
    │              │              │
 Identity      Catalog      Flash Sale
                                  │
                           Waiting Room
                           Redis Inventory
                                  │
                              RabbitMQ
                                  │
          ┌──────────────┬──────────────┐
          │              │              │
        Order        Payment     Notification
```

Each service owns its own database and communicates through REST APIs or asynchronous events.

---

# Core Features

## Customer

- Register and login
- Browse products
- View upcoming flash sale campaigns
- Join the waiting room
- Purchase flash sale products
- Complete payment
- Track order status
- Receive real-time notifications
- View purchase history

## Seller

- Create and manage products
- Create flash sale campaigns
- Configure campaign inventory
- Configure flash sale pricing
- Set campaign schedule
- Monitor sales
- Track campaign revenue

## Administrator

- Manage users and sellers
- Suspend accounts
- Monitor campaigns
- Monitor services
- Monitor message queues
- Monitor system health
- View operational dashboard

---

# Purchase Flow

1. Seller creates a flash sale campaign.
2. Customers join the waiting room.
3. When the campaign starts, eligible customers receive temporary purchase permission.
4. Flash Sale Service validates:
   - Campaign status
   - Purchase permission
   - Purchase limit
   - Inventory availability

5. Inventory is reserved atomically.
6. An `OrderRequested` event is published.
7. Order Service creates a pending order.
8. Payment Service processes payment.
9. Order Service confirms or cancels the order.
10. Reserved inventory is restored automatically if payment fails.

---

# High-Concurrency Design

## Waiting Room

The waiting room controls how many customers are allowed to enter the purchasing flow simultaneously, reducing traffic spikes and improving fairness.

## Atomic Inventory Management

Inventory is temporarily stored in Redis during active campaigns.

Atomic operations guarantee:

- No overselling
- Low latency
- High throughput

## Rate Limiting

Rate limiting protects the platform from:

- Bot attacks
- API abuse
- Excessive retries

## Idempotency

Every purchase request contains a unique idempotency key.

Repeated requests always return the same result instead of creating new orders.

---

# Distributed Transaction

FlashDrop uses the **Saga Pattern** to coordinate business transactions across multiple services.

```
Reserve Inventory
        │
Create Order
        │
Process Payment
        │
 ┌───────────────┐
 │ Success       │
 │ Confirm Order │
 └───────────────┘

or

 ┌────────────────────────┐
 │ Payment Failed         │
 │ Cancel Order           │
 │ Restore Inventory      │
 └────────────────────────┘
```

---

# Event-Driven Workflow

```
ReserveInventorySucceeded
            │
            ▼
     OrderRequested
            │
            ▼
    PaymentRequested
            │
            ▼
PaymentSucceeded / PaymentFailed
            │
            ▼
OrderConfirmed / OrderCancelled
```

Asynchronous communication reduces service coupling and improves scalability.

---

# Reliability

The platform incorporates several distributed system patterns:

- Saga Pattern
- Transactional Outbox Pattern
- Idempotency
- Retry Policy
- Dead Letter Queue
- Correlation ID
- Distributed Tracing

These patterns ensure business consistency even when individual services fail.

---

# Observability

The platform supports end-to-end monitoring through:

- Structured Logging
- Distributed Tracing
- Correlation IDs
- Metrics Collection

Key metrics include:

- Requests per second
- Throughput
- Average latency
- P95 / P99 latency
- Error rate
- Queue length
- Remaining inventory
- Payment success rate

---

# Performance Goals

| Metric            |   Target |
| ----------------- | -------: |
| Stock             |    1,000 |
| Waiting Users     |   50,000 |
| Purchase Requests | 100,000+ |
| Successful Orders |    1,000 |
| Overselling       |        0 |
| Duplicate Orders  |        0 |
| Lost Events       |        0 |

---

# Technology Stack

## Backend

- ASP.NET Core
- C#
- Entity Framework Core
- PostgreSQL

## Architecture

- Microservices
- Clean Architecture
- Vertical Slice Architecture
- CQRS
- Event-Driven Architecture

## Infrastructure

- Docker
- Redis
- RabbitMQ
- YARP API Gateway

## Security

- JWT Authentication
- Role-Based Authorization
- Rate Limiting

## Reliability

- Saga Pattern
- Transactional Outbox Pattern
- Idempotency

## Monitoring

- Structured Logging
- Correlation IDs
- Distributed Tracing
- Metrics

---

# Project Status

The project is currently under active development.

Planned improvements include:

- Kubernetes deployment
- OpenTelemetry integration
- Prometheus & Grafana dashboards
- CI/CD pipeline
- Load testing with K6
- Payment gateway integration

---

# License

This project is intended for educational and research purposes.

# FlashDrop

FlashDrop is a high-concurrency Flash Sale platform built with **ASP.NET Core**, **Microservices Architecture**, and **Event-Driven Architecture**. The project focuses on solving real-world distributed system challenges such as overselling prevention, distributed transactions, fault tolerance, idempotency, and high-volume request processing.

## Problem Statement

Flash sale campaigns typically receive a massive number of purchase requests within a few seconds while only a limited amount of inventory is available.

Example:

| Item              |           Value |
| ----------------- | --------------: |
| Product           | Limited Sneaker |
| Stock             |           1,000 |
| Waiting Users     |          50,000 |
| Incoming Requests |        100,000+ |

The system must guarantee:

- Sell exactly **1,000** products.
- Prevent overselling.
- Allow each customer to purchase only one item.
- Prevent duplicate orders.
- Ensure retry requests do not create duplicate orders.
- Restore inventory when payment fails.
- Prevent event loss during service failures.
- Protect the system from abusive traffic through rate limiting.

---

# Architecture

FlashDrop follows a **Microservices Architecture** combined with **Event-Driven Architecture**.

```
                Client
                   │
             API Gateway
                   │
    ┌──────────────┼──────────────┐
    │              │              │
 Identity      Catalog      Flash Sale
                                  │
                           Waiting Room
                           Redis Inventory
                                  │
                              RabbitMQ
                                  │
          ┌──────────────┬──────────────┐
          │              │              │
        Order        Payment     Notification
```

Each service owns its own database and communicates through REST APIs or asynchronous events.

---

# Core Features

## Customer

- Register and login
- Browse products
- View upcoming flash sale campaigns
- Join the waiting room
- Purchase flash sale products
- Complete payment
- Track order status
- Receive real-time notifications
- View purchase history

## Seller

- Create and manage products
- Create flash sale campaigns
- Configure campaign inventory
- Configure flash sale pricing
- Set campaign schedule
- Monitor sales
- Track campaign revenue

## Administrator

- Manage users and sellers
- Suspend accounts
- Monitor campaigns
- Monitor services
- Monitor message queues
- Monitor system health
- View operational dashboard

---

# Purchase Flow

1. Seller creates a flash sale campaign.
2. Customers join the waiting room.
3. When the campaign starts, eligible customers receive temporary purchase permission.
4. Flash Sale Service validates:
   - Campaign status
   - Purchase permission
   - Purchase limit
   - Inventory availability

5. Inventory is reserved atomically.
6. An `OrderRequested` event is published.
7. Order Service creates a pending order.
8. Payment Service processes payment.
9. Order Service confirms or cancels the order.
10. Reserved inventory is restored automatically if payment fails.

---

# High-Concurrency Design

## Waiting Room

The waiting room controls how many customers are allowed to enter the purchasing flow simultaneously, reducing traffic spikes and improving fairness.

## Atomic Inventory Management

Inventory is temporarily stored in Redis during active campaigns.

Atomic operations guarantee:

- No overselling
- Low latency
- High throughput

## Rate Limiting

Rate limiting protects the platform from:

- Bot attacks
- API abuse
- Excessive retries

## Idempotency

Every purchase request contains a unique idempotency key.

Repeated requests always return the same result instead of creating new orders.

---

# Distributed Transaction

FlashDrop uses the **Saga Pattern** to coordinate business transactions across multiple services.

```
Reserve Inventory
        │
Create Order
        │
Process Payment
        │
 ┌───────────────┐
 │ Success       │
 │ Confirm Order │
 └───────────────┘

or

 ┌────────────────────────┐
 │ Payment Failed         │
 │ Cancel Order           │
 │ Restore Inventory      │
 └────────────────────────┘
```

---

# Event-Driven Workflow

```
ReserveInventorySucceeded
            │
            ▼
     OrderRequested
            │
            ▼
    PaymentRequested
            │
            ▼
PaymentSucceeded / PaymentFailed
            │
            ▼
OrderConfirmed / OrderCancelled
```

Asynchronous communication reduces service coupling and improves scalability.

---

# Reliability

The platform incorporates several distributed system patterns:

- Saga Pattern
- Transactional Outbox Pattern
- Idempotency
- Retry Policy
- Dead Letter Queue
- Correlation ID
- Distributed Tracing

These patterns ensure business consistency even when individual services fail.

---

# Observability

The platform supports end-to-end monitoring through:

- Structured Logging
- Distributed Tracing
- Correlation IDs
- Metrics Collection

Key metrics include:

- Requests per second
- Throughput
- Average latency
- P95 / P99 latency
- Error rate
- Queue length
- Remaining inventory
- Payment success rate

---

# Performance Goals

| Metric            |   Target |
| ----------------- | -------: |
| Stock             |    1,000 |
| Waiting Users     |   50,000 |
| Purchase Requests | 100,000+ |
| Successful Orders |    1,000 |
| Overselling       |        0 |
| Duplicate Orders  |        0 |
| Lost Events       |        0 |

---

# Technology Stack

## Backend

- ASP.NET Core
- C#
- Entity Framework Core
- PostgreSQL

## Architecture

- Microservices
- Clean Architecture
- Vertical Slice Architecture
- CQRS
- Event-Driven Architecture

## Infrastructure

- Docker
- Redis
- RabbitMQ
- YARP API Gateway

## Security

- JWT Authentication
- Role-Based Authorization
- Rate Limiting

## Reliability

- Saga Pattern
- Transactional Outbox Pattern
- Idempotency

## Monitoring

- Structured Logging
- Correlation IDs
- Distributed Tracing
- Metrics

---

# Project Status

The project is currently under active development.

Planned improvements include:

- Kubernetes deployment
- OpenTelemetry integration
- Prometheus & Grafana dashboards
- CI/CD pipeline
- Load testing with K6
- Payment gateway integration

---

# License

This project is intended for educational and research purposes.
