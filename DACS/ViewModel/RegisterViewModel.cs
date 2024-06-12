using DACS.Models;
using System.ComponentModel.DataAnnotations;

namespace DACS.ViewModel
{
    public class RegisterViewModel
    {
        // Các thuộc tính khác

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public User Register { get; set; }

        public RegisterViewModel()
        {
            Register = new User();
        }
    }
}
