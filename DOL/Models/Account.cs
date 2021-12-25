using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.Models
{
    public class Login
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Please enter your username")]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    

}
