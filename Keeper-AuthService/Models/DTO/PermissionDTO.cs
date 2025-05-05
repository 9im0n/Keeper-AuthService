using System.ComponentModel.DataAnnotations;

namespace Keeper_AuthService.Models.DTO
{
    public class PermissionDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
