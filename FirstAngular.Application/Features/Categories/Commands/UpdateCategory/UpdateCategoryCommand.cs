using FirstAngular.Application.Common.Results;
using FirstAngular.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand: IRequest<Result<CategoryDTO>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

    }
}
