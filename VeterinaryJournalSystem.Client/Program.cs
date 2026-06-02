using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VeterinaryJournalSystem.Client;
using VeterinaryJournalSystem.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7288/")
});

builder.Services.AddScoped<AuthApiService>();
builder.Services.AddScoped<OwnerApiService>();
builder.Services.AddScoped<PetApiService>();

await builder.Build().RunAsync();
