using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();     
        public string TokenHash { get; set; } = default!;  
        public string UserId { get; set; } = default!;    
        public DateTime Expiration { get; set; }         
        public bool IsRevoked { get; set; } = false;    
         public bool IsActive => !IsRevoked && DateTime.UtcNow < Expiration;
    }

}
