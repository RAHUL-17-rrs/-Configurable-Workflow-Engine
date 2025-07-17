#  Configurable Workflow Engine (State Machine API)

This is a minimal .NET 8 Web API for defining and executing configurable workflows using state machines.

## ✅ Features

- Define workflow with `States` and `Actions`
- Start new workflow instances
- Execute actions with validation
- Inspect current state and history of any instance
- In-memory storage, no database needed

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)

### Run the project

```bash
dotnet run
```

### Sample Endpoints
- `POST /workflow` → define workflow
- `GET /workflow/{id}` → get workflow
- `POST /workflow/{id}/start` → create instance
- `POST /instance/{id}/action/{actionId}` → transition state
- `GET /instance/{id}` → inspect instance

##  Assumptions & Notes

- Only one initial state allowed per workflow.
- Final states cannot be transitioned from.
- Transitions are validated based on current state, action enablement, and allowed source states.
- No persistence between restarts — data is in-memory only.

## TODO (with more time)
- Add unit tests.
- Add Swagger UI or API documentation.
- Persist definitions and instances to JSON/YAML files.

##  Folder Structure

- `/Models` → Domain models: State, Action, WorkflowDefinition, WorkflowInstance
- `/Services` → Core logic
- `Program.cs` → Minimal API setup

