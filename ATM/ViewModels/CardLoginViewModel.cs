
using System.ComponentModel.DataAnnotations;

namespace ATM.Models.ViewModels
{
    public class CardLoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập số thẻ ATM.")]
        [RegularExpression(@"^\d{16}$",
            ErrorMessage = "Số thẻ phải gồm đúng 16 chữ số.")]
        [Display(Name = "Số thẻ ATM")]
        public string CardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập mã PIN.")]
        [RegularExpression(@"^\d{4,6}$",
            ErrorMessage = "Mã PIN phải gồm 4 đến 6 chữ số.")]
        [Display(Name = "Mã PIN")]
        public string PIN { get; set; } = string.Empty;
    }
}
