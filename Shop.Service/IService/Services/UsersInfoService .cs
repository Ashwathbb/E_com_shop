﻿using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shop.DataAccess.DTOs;
using Shop.DataAccess.Models;
using Shop.Repository.IRepositories;
//using Shop.Repository.IRepositories.Repositories;

namespace Shop.Service.IService.Services
{
    public class UsersInfoService : IUsersInfoService
    {

        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public UsersInfoService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<IEnumerable<UsersInfoDto>> GetAllUsers()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<UsersInfoDto> GetUserById(Guid usersInfoGuid)
        {
            return await _userRepository.GetUserById(usersInfoGuid);
        }
        public void UpdateUser(UsersInfoDto userDto)
        {
            _userRepository.UpdateUser(userDto);
        }

        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }
        public UsersInfoDto Authenticate(string username, string password)
        {
            // var user = _userRepository.GetAllUsers().SingleOrDefault(u => u.UserName == username);
            // Retrieve user by username from repository
            var user = _userRepository.GetUserByUsername(username, password);

            if (user == null)
            {
                return null; // User not found
            }

            if (user.Password == password)
            {
                // Reset failed login attempts if password is correct
                _userRepository.UpdateUserLoginAttempts(user.UserId, 0);
                return user; // Authentication successful
            }
            else
            {
                // Increment failed login attempts and update IsActive status if max attempts reached
                user.FailedLoginAttempts++;
                if (user.FailedLoginAttempts >= 3)
                {
                    user.IsActive = false;
                    _userRepository.UpdateUserLoginAttemptsAndBlock(user.UserId, user.FailedLoginAttempts, false);
                    return null; // Exceeded login attempts limit
                }
                else
                {
                    _userRepository.AddFailedLoginAttempt(user.UserId, DateTime.UtcNow);
                    return null; // Wrong password, please enter correct password
                }
            }
        }
        public void AddUserProducts(UserProductSelectionDto userProductSelection)
        {
            _userRepository.AddUserProducts(userProductSelection.UserId, userProductSelection.ProductIds);
        }

        public async Task<RegisterDto> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginDto.UserName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                throw new Exception("Invalid credentials.");
            }

            var token = _jwtTokenGenerator.GenerateToken(user.UserName);

            return new AuthResponseDto
            {
                Token = token,
                UserName = user.UserName
            };
        }

        public async Task<RegisterDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(registerDto.UserName);
            if (existingUser != null)
            {
                throw new Exception("User already exists.");
            }

            return await _userRepository.RegisterUserAsync(registerDto, registerDto.Password);
        }
    }
}

