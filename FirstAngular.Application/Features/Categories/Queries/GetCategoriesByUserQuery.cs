using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Features.Categories.DTOs;
using MediatR;
using System.Collections.Generic;

namespace FirstAngular.Application.Features.Categories.Queries
{
    //public class GetCategoriesByUserQuery : IRequest<Result<List<CategoryDTO>>>
    //{
    //    public string? SearchTerm { get; set; }
    //    public int PageNumber { get; set; } = 1;
    //    public int PageSize { get; set; } = 10;
    //}

    public sealed record GetCategoriesByUserQuery(
        string? SearchTerm,
        int PageNumber = 1,
        int PageSize = 10
    ) : IRequest<Result<List<CategoryDTO>>>;
}
