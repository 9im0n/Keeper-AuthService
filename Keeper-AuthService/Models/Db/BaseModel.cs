using System.ComponentModel.DataAnnotations;

namespace Keeper_AuthService.Models.Db
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
    }
}
