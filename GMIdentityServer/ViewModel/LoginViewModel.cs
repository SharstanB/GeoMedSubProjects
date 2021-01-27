using System.ComponentModel.DataAnnotations;

namespace GMIdentityServer.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [StringLength(255 , ErrorMessage = "يجب أن تكون كلمة المرور أطول من 6 محارف",
            MinimumLength = 6) ]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        //[Required(ErrorMessage = "هذا الحقل مطلوب")]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
