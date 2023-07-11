
// (Coded and Validated by: Muhammad, with help from Ali)

using System.ComponentModel.DataAnnotations;
using TravelExpertsData;

namespace TravelExpertsMVC.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Please enter your first name.")]
        public string CustFirstName { get; set; } = null!;
        [StringLength(25)]
        [Required(ErrorMessage = "Please enter your last name.")]
        public string CustLastName { get; set; } = null!;
        [StringLength(75)]
        [Required(ErrorMessage = "Please enter your address.")]
        public string CustAddress { get; set; } = null!;
        [StringLength(50)]
        [Required(ErrorMessage = "Please enter your city.")]
        public string CustCity { get; set; } = null!;
        [StringLength(2)]
        [Required(ErrorMessage = "Please enter your province.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Province can only contain letters.")]
        public string CustProv { get; set; } = null!;
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Postal code must be 6 characters long.")]
        [RegularExpression(@"^[a-zA-Z]\d[a-zA-Z]\d[a-zA-Z]\d$", ErrorMessage = "Postal code must be in the format 'X1X1X1' where X is a letter and 1 is a digit.")]
        [Required(ErrorMessage = "Please enter your postal code.")]
        public string CustPostal { get; set; } = null!;
        [StringLength(25)]
        [Required(ErrorMessage = "Please enter you country.")]
        public string? CustCountry { get; set; }
        [StringLength(10, MinimumLength = 10)]
        [Required(ErrorMessage = "Please enter a phone number.")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Phone number must be 10 digits including area code.")]
        public string? CustHomePhone { get; set; }
        [StringLength(10, MinimumLength = 10)]
        [Required(ErrorMessage = "Please enter a phone number. If you do not have a business, please enter the same number as homephone.")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Phone number must be 10 digits inmcluding area code.")]
        public string CustBusPhone { get; set; } = null!;
        [StringLength(50)]
        [Required(ErrorMessage = "Please enter a valid email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string CustEmail { get; set; } = null!;
        public int? AgentId { get; set; }
        [StringLength(25)]
        [Required(ErrorMessage = "Please enter a Username.")]
        public string? Username { get; set; }
        [StringLength(25)]
        [Required(ErrorMessage = "Please enter a Password.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string? Password { get; set; }
    }
}
