using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProjectSolarEdge.Client;
using ProjectSolarEdge.Client.Services.Questions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IQuestionsDataService, QuestionDataService>(client => client.BaseAddress = new Uri("https://localhost:7050/"));
await builder.Build().RunAsync();
