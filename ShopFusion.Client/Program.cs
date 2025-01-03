using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShopFusion.Client;
using ShopFusion.Client.Services;
using ShopFusion.Client.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("API_BASEURL")) });
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetSection("API_BASEURL").Value) });
//builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
await builder.Build().RunAsync();
