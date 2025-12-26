using FirstAngular.Application.Common.Results;
using FirstAngular.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace FirstAngular.Application.Features.Categories.Queries
{
    public class GetCategoriesByUserQuery : IRequest<Result<List<CategoryDTO>>>
    {
        public string? SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
