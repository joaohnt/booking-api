# Booking API

API de agendamentos com foco 100% no back-end. Este projeto foi pensado para portfolio, com dominio bem definido, regras de negocio claras e documentacao completa via Swagger (nao ha UI dedicada).

## Visao geral
O usuario pode ser:
- **Provider**: define sua disponibilidade de horarios e gerencia agendamentos da propria agenda.
- **Client**: agenda horarios disponiveis.

O fluxo principal e: provider cria disponibilidade -> client agenda -> client pode cancelar respeitando regras do dominio.

## Stack
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core + SQL Server
- JWT (auth + roles)
- BCrypt (hash de senha)
- xUnit / Moq / Bogus (tests)

## Arquitetura (Clean)
- `Booking.Api`: controllers, auth, Swagger e composicao de dependencias
- `Booking.Application`: casos de uso (services)
- `Booking.Domain`: entidades, value objects, regras de negocio, interfaces
- `Booking.Infrastructure`: EF Core, repositories, JWT token service
- `Booking.Tests`: testes unitarios do dominio e servicos

## Decisoes de design
- **Clean Architecture + DDD** para manter regras de negocio isoladas de frameworks e facilitar evolucao do dominio.
- **JWT + Roles** para separar claramente os acessos de Provider e Client.
- **BCrypt** para hash de senha com custo configuravel e padrao de mercado.
- **EF Core + SQL Server** por consistencia relacional e facilidade de migrations.
- **Cadastro publico cria CLIENT**; Provider e promovido manualmente no banco/suporte.

## Regras de negocio (dominio)
- `Email` valida formato e normaliza para lowercase.
- `TimeRange` exige `Start < End` e nao permite data passada.
- Disponibilidade nao pode conflitar com outro horario do mesmo provider.
- Agendamento so ocorre se a disponibilidade estiver **OPEN**.
- Cancelamento pelo cliente dono do agendamento ou pelo provider da disponibilidade.
- Cancelamento bloqueado a **2h ou menos** do inicio.
- Ao cancelar, a disponibilidade volta para **OPEN**.

## Casos de uso (fluxo principal)
1) Provider cria disponibilidade de horario
2) Client lista disponibilidades do provider
3) Client agenda uma disponibilidade **OPEN**
4) Client pode cancelar respeitando a regra de 2h
5) Ao cancelar, a disponibilidade volta para **OPEN**

## API (Swagger)
A UI de documentacao e o proprio Swagger em `/swagger`. Todos os exemplos de request/response e contratos estao la.

## Endpoints
### Auth
- `POST /user/signup`
  Body: `{ "name", "email", "password" }`
- `POST /user/signin`
  Body: `{ "email", "password" }`
  Retorna: `{ "email", "token" }`

### Users
- `GET /user/providers`
- `GET /user/clients`
  **Auth**: `ROLE=PROVIDER`

### Availability (Provider)
- `POST /availability/create`
  **Auth**: `ROLE=PROVIDER`
  Body: `{ "start", "end" }`
- `GET /availability/{providerId}`
  **Auth**: qualquer usuario autenticado
- `DELETE /availability/{availabilityId}/cancel`
  **Auth**: `ROLE=PROVIDER`

### Booking (Client)
- `POST /booking/{availabilityId}/create`
  **Auth**: `ROLE=CLIENT`
- `GET /bookings`
  **Auth**: qualquer usuario autenticado
- `DELETE /booking/{bookingId}/cancel`
  **Auth**: `ROLE=CLIENT`

### Booking (Provider)
- `GET /bookings/provider`
  **Auth**: `ROLE=PROVIDER`
- `DELETE /booking/{bookingId}/cancel/provider`
  **Auth**: `ROLE=PROVIDER`

## Autenticacao
Use o token do login no header:

```
Authorization: Bearer <token>
```

Os enums sao serializados como string (`PROVIDER`, `CLIENT`, `OPEN`, `CLOSED`).

## Rodando local
1) Configure a connection string e o JWT em `Booking.Api/appsettings.json`.

2) Restaurar dependencias:
```
dotnet restore
```

3) Aplicar migrations:
```
dotnet ef database update --project Booking.Infrastructure --startup-project Booking.Api
```

4) Subir a API:
```
dotnet run --project Booking.Api
```

Swagger estara disponivel em `/swagger`.

## Testes
```
dotnet test
```
