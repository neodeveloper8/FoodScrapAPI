using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodScrap.Application.UseCases.Auth.Commands;
using FoodScrap.Domain.Entities;
using FoodScrap.Domain.Interfaces;
using MediatR;

namespace FoodScrap.Application.UseCases.Auth.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var exists = (await _unitOfWork.Users.FindAsync(u => u.Email == request.User.Email)).Any();
            if (exists) return false;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.User.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.User.Name,
                Email = request.User.Email,
                PasswordHash = hashedPassword
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
