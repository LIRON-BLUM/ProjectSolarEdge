using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProjectSolarEdge.Client;
using ProjectSolarEdge.Client.Services;
using ProjectSolarEdge.Client.Services.Questions;
using MudBlazor.Services;
using Blazored.LocalStorage;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Client.Services.GameApp;
using ProjectSolarEdge.Client.Services.Users;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IQuestionsDataService, QuestionDataService>(client => client.BaseAddress = new Uri("https://localhost:7181/"));
builder.Services.AddHttpClient<IGamesDataService, GameDataService>(client => client.BaseAddress = new Uri("https://localhost:7181/"));
builder.Services.AddHttpClient<IGameAppService, GameAppService>(client => client.BaseAddress = new Uri("https://localhost:7181/"));
builder.Services.AddHttpClient<IUsersDataService, UserDataService>(client => client.BaseAddress = new Uri("https://localhost:7181/"));

//builder.Services.AddHttpClient<IQuestionsDataService, QuestionDataService>(client => client.BaseAddress = new Uri("https://projects.telem-hit.net/2022/KnowlEdge_LironLimor/Server/"));
//builder.Services.AddHttpClient<IGamesDataService, GameDataService>(client => client.BaseAddress = new Uri("https://projects.telem-hit.net/2022/KnowlEdge_LironLimor/Server/"));
//builder.Services.AddHttpClient<IGameAppService, GameAppService>(client => client.BaseAddress = new Uri("https://projects.telem-hit.net/2022/KnowlEdge_LironLimor/Server/"));
//builder.Services.AddHttpClient<IUsersDataService, UserDataService>(client => client.BaseAddress = new Uri("https://projects.telem-hit.net/2022/KnowlEdge_LironLimor/Server/"));

builder.Services.AddBlazoredLocalStorage();



builder.Services.AddMudServices();
await builder.Build().RunAsync();
