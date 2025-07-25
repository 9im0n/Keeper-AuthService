﻿using System.ComponentModel.DataAnnotations;

namespace Keeper_AuthService.Models.DTO
{
    public class RefreshTokenDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool Revoked { get; set; } = false;
    }
}
