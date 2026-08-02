# SRE Agent Demo App

A tiny ASP.NET Core Web API used for the Azure SRE Agent proof-of-concept demo.

## Endpoints
- `GET /` - landing/status message
- `GET /health` - health check
- `GET /api/marketdata/{symbol}` - returns a fake stock price (e.g. `/api/marketdata/MSFT`).
  This is the endpoint you will intentionally break during the demo.

## Run it locally
1. Open `SreAgentDemoApp.sln` in Visual Studio.
2. Press **F5** (or **Ctrl+F5** to run without debugging).
3. Your browser should open to the `/health` endpoint automatically.

## Full walkthrough
See the companion document **azure-sre-agent-poc-beginner-guide.md** for the complete,
click-by-click walkthrough: running this locally, pushing it to GitHub, publishing it to
Azure Container Apps, wiring up monitoring and alerts, setting up Azure SRE Agent, and
simulating the "bad deployment" demo end to end.
