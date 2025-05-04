using BaseLibrary.Entities;
using Blazored.LocalStorage;
using Client;
using Client.ApplicationStates;
using Client.Library.Helpers;
using Client.Library.Services.Contracts;
using Client.Library.Services.Implementaions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CustomHttpHandler>();
builder.Services.AddHttpClient("SystemApiClient",client => {
    client.BaseAddress = new Uri("https://localhost:7293");

}).AddHttpMessageHandler<CustomHttpHandler>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7293") });



builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<GetHttpClient>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider,CustomeAuthenticationStateProvider>();
builder.Services.AddScoped<IUserAccountService,UserAccountService>();
builder.Services.AddScoped<AllState>();

// GeneralDepartment , Department , Branch 
builder.Services.AddScoped<IGenericServieInterface<GeneralDepartment>, GenericServiceImplementaion<GeneralDepartment>>();
builder.Services.AddScoped<IGenericServieInterface<Department>, GenericServiceImplementaion<Department>>();
builder.Services.AddScoped<IGenericServieInterface<Branch>, GenericServiceImplementaion<Branch>>();

// Country , City , Town 
builder.Services.AddScoped<IGenericServieInterface<Country>, GenericServiceImplementaion<Country>>();
builder.Services.AddScoped<IGenericServieInterface<City>, GenericServiceImplementaion<City>>();
builder.Services.AddScoped<IGenericServieInterface<Town>, GenericServiceImplementaion<Town>>();

// Employee
builder.Services.AddScoped<IGenericServieInterface<Employee>, GenericServiceImplementaion<Employee>>();


builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<SfDialogService>();


await builder.Build().RunAsync();
