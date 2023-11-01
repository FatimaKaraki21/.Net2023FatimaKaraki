using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class UserEdit
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password),MinLength(4,ErrorMessage ="Minimum length must be 4")]
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required, Display(Name = "Address")]
        public string Address { get; set; }
        public string Password { get; set; }

        public UserEdit() { }

        public UserEdit(AppUser appUser)
        {
            FirstName = appUser.FirstName;
            LastName = appUser.LastName;
            Address = appUser.Address;
            Email= appUser.Email;
            Password = appUser.PasswordHash;
        }
    }
}
