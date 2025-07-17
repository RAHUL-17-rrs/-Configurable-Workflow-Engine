using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using WorkflowEngine.Models;
using WorkflowEngine.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JsonOptions>(opts =>
{
    opts.SerializerOptions.WriteIndented = true;
});
builder.Services.AddSingleton<WorkflowService>();
var app = builder.Build();

var service = app.Services.GetRequiredService<WorkflowService>();

app.MapPost("/workflow", (WorkflowDefinition def) =>
{
    var result = service.CreateWorkflow(def);
    return result.IsSuccess ? Results.Ok(result.Message) : Results.BadRequest(result.Message);
});

app.MapGet("/workflow/{id}", (string id) =>
{
    var def = service.GetWorkflow(id);
    return def is null ? Results.NotFound("Workflow not found.") : Results.Ok(def);
});

app.MapPost("/workflow/{id}/start", (string id) =>
{
    var result = service.StartInstance(id);
    return result.IsSuccess ? Results.Ok(result.Instance) : Results.BadRequest(result.Message);
});

app.MapPost("/instance/{id}/action/{actionId}", (string id, string actionId) =>
{
    var result = service.ExecuteAction(id, actionId);
    return result.IsSuccess ? Results.Ok(result.Instance) : Results.BadRequest(result.Message);
});

app.MapGet("/instance/{id}", (string id) =>
{
    var instance = service.GetInstance(id);
    return instance is null ? Results.NotFound("Instance not found.") : Results.Ok(instance);
});

app.Run();
