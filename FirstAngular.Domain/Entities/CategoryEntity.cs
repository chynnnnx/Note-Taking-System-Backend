using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Domain.Entities
{
    public class CategoryEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<NoteEntity> Notes { get; set; } = new List<NoteEntity>();
    }
}
