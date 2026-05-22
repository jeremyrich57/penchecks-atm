# penchecks-atm

Take home assignment for PenChecks — a simple ATM application with a .NET Web API backend and a Vue 3 frontend.

## Project structure

- `PenChecksAPI/` — ASP.NET Core Web API (.NET 10) using an in-memory EF Core database
- `PenChecksUI/` — Vue 3 + Vite + TypeScript + Vuetify single-page app

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) 18+ and npm

## Running the application

The API and the UI run as two separate processes. Start the API first, then the UI, in separate terminals.

### 1. Start the API

```bash
cd PenChecksAPI/PenChecksAPI
dotnet run
```

Can also open the solution in Visual Studio and run without debugging there. Make sure to run as http NOT https

The API listens on `http://localhost:5144`. The database is in-memory and is seeded on startup, so there is no database setup required.
It's seeded with one customer that has two accounts for demo purposes.

### 2. Start the UI

```bash
cd PenChecksUI
npm install
npm run dev
```

The UI runs on `http://localhost:5173`. The API base URL is already configured in `PenChecksUI/.env` (`VITE_API_BASE_URL=http://localhost:5144`), so no additional setup is needed.

Open `http://localhost:5173` in your browser to use the application.

## Using Different Ports
This application uses the default ports so if you need to run the application on different ports, you'll need to update `Program.cs` in the API and the `VITE_API_BASE_URL` in `PenChecksUI/.env` in the UI. Update the ports accordingly

## Limitations and tradeoffs

A few things to be aware of, given the scope of the assignment:

- **In-memory database.** The API uses EF Core's in-memory provider, so all data resets every time the API restarts. This keeps setup friction to zero, but no data persists between runs and the in-memory provider does not enforce real relational constraints or transactions.
- **No authentication or authorization.** There is no login flow — the API exposes the single seeded customer and their accounts directly. A real ATM would need strong authentication, per-user scoping, and audit logging.
- **Single seeded customer.** The app assumes exactly one customer with two accounts (Checking and Savings). The UI is built around this assumption rather than a generic multi-customer experience.
- **No concurrency control.** Deposits, withdrawals, and transfers read-modify-write the balance without optimistic concurrency or locking. Concurrent requests against the same account could race; acceptable for a single-user demo, but would need to be addressed in production.
- **HTTP only in development.** The dev setup runs the API over plain HTTP to keep CORS and certificate setup simple.
- **CORS pinned to the Vite dev origin.** `http://localhost:5173` is the only allowed origin. If you change the UI port, you'll need to update the CORS policy in `Program.cs` as well.
- **Minimal validation and error handling.** The API validates the basics (positive amounts, sufficient funds, distinct transfer accounts) and the UI surfaces those error messages, but there is no exhaustive input validation, rate limiting, or structured error model.
- **No automated tests.** Given the scope, behavior was verified manually through the UI rather than with a unit/integration test suite.
- **`.env` committed to source control.** Normally the UI's `.env` would not be checked in, but it's included here so the app runs without any extra setup steps. There are no secrets in it.
