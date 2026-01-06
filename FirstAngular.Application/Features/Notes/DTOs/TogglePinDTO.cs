using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Notes.DTOs
{
    //public class TogglePinDTO
    //{
    //    public Guid Id { get; set; }
    //    public bool IsPinned { get; set; }
    //}
    public sealed record TogglePinDTO(Guid Id, bool IsPinned);
}
