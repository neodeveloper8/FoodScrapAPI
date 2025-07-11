﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FoodScrap.Application.UseCases.Dishes.Commands
{
    public record DeleteDishCommand(Guid Id) : IRequest<bool>;
}
