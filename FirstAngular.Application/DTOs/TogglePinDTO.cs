using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.DTOs
{
    public class TogglePinDTO
    {
        public Guid Id { get; set; }
        public bool IsPinned { get; set; }
    }
}
