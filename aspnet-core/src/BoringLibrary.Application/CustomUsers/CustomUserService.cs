using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace BoringLibrary.CustomUsers;

public class CustomUserService : BoringLibraryAppService
{
    private readonly IAccountAppService _accountAppService;
    private readonly IConfiguration _configuration;
    private readonly IRepository<IdentityUser, Guid> _identityUserRepository;

    public CustomUserService(IConfiguration configuration, IRepository<IdentityUser, Guid> identityUserRepository,
        IAccountAppService accountAppService)
    {
        _configuration = configuration;
        _identityUserRepository = identityUserRepository;
        _accountAppService = accountAppService;
    }

    public async Task<UserDto> LoginAsync(LoginDto input)
    {
        var apiClient = new HttpClient();
        var dataDict = new Dictionary<string, string>
        {
            { "client_id", "BoringLibrary_App" },
            { "grant_type", "password" },
            { "username", input.UserName },
            { "password", input.Password },
            { "scope", "BoringLibrary" }
        };
        var dataEncoded = new FormUrlEncodedContent(dataDict);
        var response = await apiClient.PostAsync($"{_configuration["App:SelfUrl"]}/connect/token", dataEncoded);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new BusinessException("Something wrong", "Something wrong");
        }

        var responseString = response.Content.ReadAsStringAsync().Result;
        var tokenResponseDto = JsonConvert.DeserializeObject<TokenResponseDto>(responseString);

        var user = await _identityUserRepository.GetAsync(e => e.UserName.ToLower() == input.UserName.ToLower());
        var userDto = ObjectMapper.Map<IdentityUser, UserDto>(user);
        userDto.Credit = user.GetCredit();
        userDto.Token = tokenResponseDto.AccessToken;
        return userDto;
    }

    public async Task RegisterAsync(RegisterDto input)
    {
        var customRegister = new Volo.Abp.Account.RegisterDto
        {
            UserName = input.UserName,
            Password = input.Password,
            EmailAddress = $"{input.UserName}@{input.UserName}.for.dev",
            AppName = _configuration["App:Name"]
        };
        await _accountAppService.RegisterAsync(customRegister);
    }

    [Authorize]
    public async Task<double> GetCreditAsync()
    {
        var user = await _identityUserRepository.GetAsync(e => e.Id == CurrentUser.Id);
        Console.WriteLine(user.GetCredit());
        return user.GetCredit();
    }
}