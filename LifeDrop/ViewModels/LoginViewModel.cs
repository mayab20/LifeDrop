using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Username or Email")]
    public required string UserNameOrEmail { get; set; }

    [Required, DataType(DataType.Password)]
    public required string Password { get; set; }
}
