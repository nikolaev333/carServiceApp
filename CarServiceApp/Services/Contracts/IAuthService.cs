using BaseLibrary.Responses;
using CarServiceApp.DTO;
using Microsoft.Win32;

namespace CarServiceApp.Services.Contracts
{
    public interface IAuthService
    {
        Task<GeneralResponse> CreateAsync(RegisterDTO registerDto);
        Task<LoginResponse> SignInAsync(LoginDTO loginDto);
    }
}
