using System.ComponentModel.DataAnnotations;

namespace Keeper_AuthService.Models.DTO
{
    public class RolesDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
