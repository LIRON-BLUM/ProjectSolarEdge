using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProjectSolarEdge.Client;
using ProjectSolarEdge.Client.Services;
using ProjectSolarEdge.Client.Services.Questions;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IQuestionsDataService, QuestionDataService>(client => client.BaseAddress = new Uri("https://localhost:7181/"));
builder.Services.AddMudServices();
await builder.Build().RunAsync();
