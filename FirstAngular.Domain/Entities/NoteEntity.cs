using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Domain.Entities
{
    public class NoteEntity
    {
        public Guid Id { get; private set; }
        public string UserId { get; private set; } = string.Empty;
        public string Title { get; private set; } = string.Empty;
        public string Content { get; private set; } = string.Empty;
        public bool IsPinned { get; private set; }
        public bool IsArchived { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Guid? CategoryId { get; private set; }
        public virtual CategoryEntity? Category { get; private set; }

        protected NoteEntity() { } 

        public static NoteEntity Create(string userId, string title, string content,Guid? categoryId)
        {
            return new NoteEntity
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Title = title,
                Content = content,
                CategoryId = categoryId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
        public void Update(string title, string content, Guid? categoryId)
        {
            if (!string.IsNullOrWhiteSpace(title))
                Title = title;

            if (!string.IsNullOrWhiteSpace(content))
                Content = content;

            CategoryId = categoryId; 

            UpdatedAt = DateTime.UtcNow;
        }

        public bool SetPinned(bool isPinned)
        {
            if (IsArchived) return false;      
            if (IsPinned == isPinned) return false; 

            IsPinned = isPinned;
            UpdatedAt = DateTime.UtcNow;
            return true;
        }


        public bool Archive()
        {
            if (IsArchived) return false;
            IsPinned = false;
            IsArchived = true;
            UpdatedAt = DateTime.UtcNow;
            return true;
        }

        public bool Unarchive()
        {
            if (!IsArchived) return false;
            IsArchived = false;
            UpdatedAt = DateTime.UtcNow;
            return true;
        }
    }
}
