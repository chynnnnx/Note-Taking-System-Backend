using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Domain.Entities
{
    public class CategoryEntity
    {
        public Guid Id { get; private set; }
        public string UserId { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public DateTime CreatedAt {  get; private set; }

        public virtual ICollection<NoteEntity> Notes { get; private set; } = new List<NoteEntity>();


        protected CategoryEntity() { }

        public static CategoryEntity Create(string userId, string name)
        {
            return new CategoryEntity
            {
                Id = new Guid(),
                UserId = userId,
                Name = name,
                CreatedAt = DateTime.UtcNow
            };
        }
        public void Update(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                Name = name;
        }
    }
}