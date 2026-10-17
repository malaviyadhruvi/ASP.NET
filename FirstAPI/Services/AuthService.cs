using FirstAPI.Data;
using FirstAPI.Dto;
using FirstAPI.IService;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        public AuthService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Tuple<int, string>> LoginUser(UserDto dto)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);
                if (existingUser == null)
                {
                    return new Tuple<int, string>(0, "Invalid email or password");
                }
                else
                {
                    return new Tuple<int, string>(1, "Login successful");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(3, "Something went wrong");
            }
        }   
    }
}
