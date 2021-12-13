using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using CarSharing_Client.Data;
using CarSharing_Client.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace CarSharing_Client.Authentication {
public class CustomAuthenticationStateProvider : AuthenticationStateProvider {
    private readonly IJSRuntime _jsRuntime;
    private readonly IUserService _userService;

    public Customer CachedCustomer;

    public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, IUserService userService) {
        this._jsRuntime = jsRuntime;
        this._userService = userService;
    }

    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();
        if (CachedCustomer == null)
        {
            string accountAsJson = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
            if (!string.IsNullOrEmpty(accountAsJson))
            {
                CachedCustomer = JsonSerializer.Deserialize<Customer>(accountAsJson);
                identity = SetupClaimsForAccount( CachedCustomer);
            }
        }
        else
        {
            identity = SetupClaimsForAccount( CachedCustomer);
        }

        ClaimsPrincipal cachedClaimsPrincipal = new ClaimsPrincipal(identity);
        return await Task.FromResult(new AuthenticationState(cachedClaimsPrincipal));
    }
    
    public async Task ValidateLogin(string username, string password)
    {
        if (string.IsNullOrEmpty(username)) throw new Exception("Enter username");
        if (string.IsNullOrEmpty(password)) throw new Exception("Enter password");

        ClaimsIdentity identity;
        try
        {
            Customer customer = await _userService.ValidateCustomer(username, password);
            identity = SetupClaimsForAccount(customer);
            string serialisedData = JsonSerializer.Serialize(customer);
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
            CachedCustomer = customer;
        }
        catch (Exception)
        {
            throw;
        }

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
    }

    public async Task Logout() {
        CachedCustomer = null;
        var user = new ClaimsPrincipal(new ClaimsIdentity());
         await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    private ClaimsIdentity SetupClaimsForAccount(Customer customer) {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, customer.FirstName));
        claims.Add(new Claim("AccessLevel", customer.AccessLevel.ToString()));

        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth_type");
        return identity;
    }
}
}
