using KingsStoreApi.Model.ModelHelpers;
using Microsoft.AspNetCore.Identity;

namespace KingsStoreApi.Model.Entities
{
    public class Role : IdentityRole, IDelete
    {
        public bool IsDeleted { get; set; }

    }
}
