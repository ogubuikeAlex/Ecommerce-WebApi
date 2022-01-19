using System;
using KingsStoreApi.Model.ModelHelpers;
using Microsoft.AspNetCore.Identity;

namespace KingsStoreApi.Model.Entities
{
    public class User : IdentityUser, IDelete
    {
        public string FullName { get; set; }
        public bool isAdmin { get; set; }
        public bool isVendor { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string Bio { get; set; }
        public bool isActive { get; set; }
        public bool IsDeleted { get; set; }        
        public bool HasBeenRemovedOver30Days => throw new NotImplementedException();
        public Cart Cart { get; set; }
    }
}
