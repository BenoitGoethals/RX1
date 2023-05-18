namespace Rx.Blazor.Helpers
{
    public class UserDto
    {
    
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string Email { get; set; }

      
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string Password { get; set; }
  
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string FirstName { get; set; }
  
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string LastName { get; set; }
   
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string Role { get; set; }
    }
}
