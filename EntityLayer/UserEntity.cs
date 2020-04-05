using System;

namespace EntityLayer
{
    public class UserEntity
    {
        public int UserID { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }

        public int UserRoleID { get; set; }

    }
}
