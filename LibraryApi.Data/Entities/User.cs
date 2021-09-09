using Microsoft.AspNetCore.Identity;

namespace LibraryApi.Data.Entities
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalIdentificationNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}