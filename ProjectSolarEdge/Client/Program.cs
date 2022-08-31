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


string baseUrl = "https://localhost:7181/";
//string baseUrl = "https://projects.telem-hit.net/2022/KnowlEdge_LironLimor/Server/";

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IQuestionsDataService, QuestionDataService>(client => client.BaseAddress = new Uri(baseUrl));
builder.Services.AddHttpClient<IGamesDataService, GameDataService>(client => client.BaseAddress = new Uri(baseUrl));
builder.Services.AddHttpClient<IGameAppService, GameAppService>(client => client.BaseAddress = new Uri(baseUrl));
builder.Services.AddHttpClient<IUsersDataService, UserDataService>(client => client.BaseAddress = new Uri(baseUrl));

builder.Services.AddBlazoredLocalStorage();



builder.Services.AddMudServices();
await builder.Build().RunAsync();
