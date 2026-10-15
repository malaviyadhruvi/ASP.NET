using FirstAPI.Dto;

namespace FirstAPI.IService
{
    public interface IAuthService
    {
        Task<Tuple<int, string>> LoginUser(UserDto dto);
    }
}
