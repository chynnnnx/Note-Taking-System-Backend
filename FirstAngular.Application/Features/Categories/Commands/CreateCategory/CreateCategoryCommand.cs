using FirstAngular.Application.Common.Results;
using FirstAngular.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand: IRequest<Result<CategoryDTO>>
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
