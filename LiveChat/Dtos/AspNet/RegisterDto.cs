using System.ComponentModel.DataAnnotations;

namespace Dtos.AspNet
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "{0} اجباری است")]
        [EmailAddress(ErrorMessage ="فرمت ورودی صحیح نیست")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} اجباری است")]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} اجباری است")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه عبور")]
        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار آن با هم متفاوت هستند.")]
        public string ConfirmPassword { get; set; }
    }
}
