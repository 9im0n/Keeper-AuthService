using System.ComponentModel.DataAnnotations;

namespace Keeper_AuthService.Models.DB
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
    }
}
