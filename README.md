# Booking API

API de agendamentos com foco 100% no back-end. Este projeto foi pensado para estudo, com domínio bem definido, regras de negócio claras e documentação completa via Swagger (não há UI dedicada).

## Visão geral
O usuário pode ser:
- **Provider**: define sua disponibilidade de horários e gerencia agendamentos da própria agenda.
- **Client**: agenda horários disponíveis.

O fluxo principal é: provider cria disponibilidade -> client agenda -> client pode cancelar respeitando regras do domínio.

## Stack
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core + SQL Server
- JWT (auth + roles)
- BCrypt (hash de senha)
- xUnit / Moq / Bogus (tests)

## Arquitetura (Clean)
- `Booking.Api`: controllers, auth, Swagger e composição de dependências
- `Booking.Application`: casos de uso (services)
- `Booking.Domain`: entidades, value objects, regras de negócio, interfaces
- `Booking.Infrastructure`: EF Core, repositories, JWT token service
- `Booking.Tests`: testes unitários do domínio e serviços

## Decisões de design
- **Clean Architecture + DDD** para manter regras de negócio isoladas de frameworks e facilitar evolução do domínio.
- **JWT + Roles** para separar claramente os acessos de Provider e Client.
- **BCrypt** para hash de senha com custo configurável e padrão de mercado.
- **EF Core + SQL Server** por consistência relacional e facilidade de migrations.
- **Cadastro público cria CLIENT**; Provider é promovido manualmente no banco/suporte.

## Regras de negócio (domínio)
- `Email` valida formato e normaliza para lowercase.
- `TimeRange` exige `Start < End` e não permite data passada.
- Disponibilidade não pode conflitar com outro horário do mesmo provider.
- Agendamento só ocorre se a disponibilidade estiver **OPEN**.
- Cancelamento pelo cliente dono do agendamento ou pelo provider da disponibilidade.
- Cancelamento bloqueado a **2h ou menos** do início.
- Ao cancelar, a disponibilidade volta para **OPEN**.
- Provider só pode editar disponibilidade se estiver **OPEN** (sem booking confirmado).

## Casos de uso (fluxo principal)
1) Provider cria disponibilidade de horário
2) Client lista disponibilidades do provider
3) Client agenda uma disponibilidade **OPEN**
4) Client pode cancelar respeitando a regra de 2h
5) Ao cancelar, a disponibilidade volta para **OPEN**

## API (Swagger)
A UI de documentação é o próprio Swagger em `/swagger`. Todos os exemplos de request/response e contratos estão lá.

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
  **Auth**: qualquer usuário autenticado
- `PUT /availability/{availabilityId}/update`
  **Auth**: `ROLE=PROVIDER`
  Body: `{ "start", "end" }`
- `DELETE /availability/{availabilityId}/cancel`
  **Auth**: `ROLE=PROVIDER`

### Booking (Client)
- `POST /booking/{availabilityId}/create`
  **Auth**: `ROLE=CLIENT`
- `GET /bookings`
  **Auth**: qualquer usuário autenticado
- `DELETE /booking/{bookingId}/cancel`
  **Auth**: `ROLE=CLIENT`

### Booking (Provider)
- `GET /bookings/provider`
  **Auth**: `ROLE=PROVIDER`
- `DELETE /booking/{bookingId}/cancel/provider`
  **Auth**: `ROLE=PROVIDER`

## Autenticação
Use o token do login no header:

```
Authorization: Bearer <token>
```

Os enums são serializados como string (`PROVIDER`, `CLIENT`, `OPEN`, `CLOSED`).

## Rodando local
1) Configure a connection string e o JWT em `Booking.Api/appsettings.json`.

2) Restaurar dependências:
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

Swagger estará disponível em `/swagger`.

## Testes
```
dotnet test
```
