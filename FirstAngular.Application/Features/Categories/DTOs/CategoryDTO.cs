using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Categories.DTOs
{
    //public class CategoryDTO
    //{
    //    public Guid Id { get; set; }
    //    public string UserId { get; set; } = string.Empty;
    //    public string Name { get; set; } = string.Empty;
    //    public DateTime CreatedAt { get; set; }
    //}

    public sealed record CategoryDTO(Guid Id, 
        string UserId,
        string Name,
        DateTime CreatedAt);
}
