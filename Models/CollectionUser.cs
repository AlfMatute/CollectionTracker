using Microsoft.AspNetCore.Identity;

namespace CollectionTrackerAPI.Models
{
    public class CollectionUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
