using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace BoringLibrary.CustomUsers;

public interface ICustomUserService : IApplicationService
{
    Task<UserDto> LoginAsync(LoginDto input);
}