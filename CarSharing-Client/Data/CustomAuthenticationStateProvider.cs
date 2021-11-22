using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using CarSharing_Client.Data.Impl;
using Entity.ModelData;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace CarSharing_Client.Data {
public class CustomAuthenticationStateProvider : AuthenticationStateProvider {
    private readonly IJSRuntime jsRuntime;
    private readonly IUserService IUserService;

    public Customer cachedCustomer;

    public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, IUserService IUserService) {
        this.jsRuntime = jsRuntime;
        this.IUserService = IUserService;
    }

    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();
        if (cachedCustomer == null)
        {
            string accountAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
            if (!string.IsNullOrEmpty(accountAsJson))
            {
                cachedCustomer = JsonSerializer.Deserialize<Customer>(accountAsJson);
                identity = SetupClaimsForAccount( cachedCustomer);
            }
        }
        else
        {
            identity = SetupClaimsForAccount( cachedCustomer);
        }

        ClaimsPrincipal cachedClaimsPrincipal = new ClaimsPrincipal(identity);
        return await Task.FromResult(new AuthenticationState(cachedClaimsPrincipal));
    }
    
    public async Task ValidateLogin(string username, string password)
    {
        Console.WriteLine("Validating log in");
        if (string.IsNullOrEmpty(username)) throw new Exception("Enter username");
        if (string.IsNullOrEmpty(password)) throw new Exception("Enter password");

        ClaimsIdentity identity = new ClaimsIdentity();
        try
        {
            Customer customer = await IUserService.ValidateCustomer(username, password);
            identity = SetupClaimsForAccount(customer);
            string serialisedData = JsonSerializer.Serialize(customer);
            await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
            cachedCustomer = customer;
        }
        catch (Exception e)
        {
            throw e;
        }

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
    }

    public async Task Logout() {
        cachedCustomer = null;
        var user = new ClaimsPrincipal(new ClaimsIdentity());
         await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    private ClaimsIdentity SetupClaimsForAccount(Customer customer) {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, customer.FirstName));
        //claims.Add(new Claim("Level", customer.CredentialLevel.ToString()));

        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth_type");
        return identity;
    }
}
}
