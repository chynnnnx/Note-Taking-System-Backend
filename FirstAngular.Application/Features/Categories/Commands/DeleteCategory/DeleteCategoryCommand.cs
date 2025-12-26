using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstAngular.Application.Common.Results;

namespace FirstAngular.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand: IRequest<Result<object>>
    {
        public Guid Id { get; set; }
    }
}
