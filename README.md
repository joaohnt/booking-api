# Booking API

API de agendamentos com foco em domínio, usando Clean Architecture + DDD. O usuário pode ser **Provider** (define disponibilidades) ou **Client** (agenda horários disponíveis).

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

## Regras de negócio (domínio)
- `Email` valida formato e normaliza para lowercase.
- `TimeRange` exige `Start < End` e não permite data passada.
- Disponibilidade não pode conflitar com outro horário do mesmo provider.
- Agendamento só ocorre se a disponibilidade estiver **OPEN**.
- Cancelamento só pelo cliente dono do agendamento.
- Cancelamento bloqueado a **2h ou menos** do início.
- Ao cancelar, a disponibilidade volta para **OPEN**.

## Endpoints
### Auth
- `POST /user/signup`  
  Body: `{ "name", "email", "password", "role" }`
- `POST /user/signin`  
  Body: `{ "email", "password" }`  
  Retorna: `{ "email", "token" }`

### Users
- `GET /user/providers`
- `GET /user/clients`

### Availability (Provider)
- `POST /availability/create`  
  **Auth**: `ROLE=PROVIDER`  
  Body: `{ "start", "end" }`
- `GET /availability/{providerId}`  
  **Auth**: qualquer usuário autenticado

### Booking (Client)
- `POST /booking/{availabilityId}/create`  
  **Auth**: `ROLE=CLIENT`
- `GET /bookings`  
  **Auth**: qualquer usuário autenticado
- `DELETE /booking/{bookingId}/cancel`  
  **Auth**: `ROLE=CLIENT`

## Autenticação
Use o token do login no header:

```
Authorization: Bearer <token>
```

Os enums são serializados como string (`PROVIDER`, `CLIENT`, `OPEN`, `CLOSED`).

## Rodando local
1) Configure a connection string e o JWT:
   - `Booking.Api/appsettings.json`  
   - ou variáveis de ambiente equivalentes

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
