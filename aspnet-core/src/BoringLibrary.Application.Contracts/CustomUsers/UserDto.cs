using System;
using Volo.Abp.Application.Dtos;

namespace BoringLibrary.CustomUsers;

public class UserDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public double Credit { get; set; }
    public string Token { get; set; }
}