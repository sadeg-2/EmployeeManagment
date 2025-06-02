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
using MudBlazor;
using MudBlazor.Services;

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
builder.Services.AddScoped<UserProfileState>();

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

builder.Services.AddScoped<IGenericServieInterface<OverTime>, GenericServiceImplementaion<OverTime>>();
builder.Services.AddScoped<IGenericServieInterface<OverTimeType>, GenericServiceImplementaion<OverTimeType>>();
builder.Services.AddScoped<IGenericServieInterface<Vacation>, GenericServiceImplementaion<Vacation>>();
builder.Services.AddScoped<IGenericServieInterface<VacationType>, GenericServiceImplementaion<VacationType>>();
builder.Services.AddScoped<IGenericServieInterface<Sanction>, GenericServiceImplementaion<Sanction>>();
builder.Services.AddScoped<IGenericServieInterface<SanctionType>, GenericServiceImplementaion<SanctionType>>();
builder.Services.AddScoped<IGenericServieInterface<Doctor>, GenericServiceImplementaion<Doctor>>();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
}


    );

await builder.Build().RunAsync();
