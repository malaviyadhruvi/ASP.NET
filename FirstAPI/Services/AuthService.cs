using FirstAPI.Data;
using FirstAPI.Dto;
using FirstAPI.Entities;
using FirstAPI.IService;
using Microsoft.AspNetCore.Identity;
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
                if(dto == null)
                {
                    return new Tuple<int, string>(1, "Please Fill All Fields");
                }
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
                if (existingUser == null)
                {
                    return new Tuple<int, string>(0, "Invalid email or password");
                }
               
                var passwordHasher = new PasswordHasher<string>();
                var verifypass = passwordHasher.VerifyHashedPassword(dto.Email, existingUser.Password, dto.Password);
                if(verifypass == PasswordVerificationResult.Success)
                {
                    return new Tuple<int, string>(2,"Login successful");
                }
                else if(verifypass == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    existingUser.Password = passwordHashing(dto);
                    _context.Users.Update(existingUser);
                    _context.SaveChanges();
                    return new Tuple<int, string>(2, "Login successful");
                }
                else if(verifypass == PasswordVerificationResult.Failed)
                {
                    return new Tuple<int, string>(1, "Invalid email or password");
                }
                
                return new Tuple<int, string>(1, "Login successful");
             
            }
            catch (Exception ex)
            {
                throw;
            }
        }   
        public async Task<Tuple<int, string>> RegisterUser(UserDto dto)
        {
            try
            {
                var existingUser = await _context.Users.AnyAsync(u => u.Email == dto.Email);
                if (existingUser)
                {
                    return new Tuple<int, string>(0, "This User is Already Registred,Please Register with a Different Email");
                }
                _context.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    Email = dto.Email,
                    Password = dto.Password

                });
                await _context.SaveChangesAsync();
                return new Tuple<int, string>(1, "User Registered Successfully");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private string passwordHashing(UserDto dto)
        {
            var passwordHaser = new PasswordHasher<string>();
            var hash = passwordHaser.HashPassword(dto.Email, dto.Password);
            return hash;
        }
    }
}
