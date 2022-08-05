using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVP.Models
{
    public class PetModel
    {
        //fileds
        private int id;
        private String name;
        private String type;
        private String color;
        //proprites -- validation
        [DisplayName("Pet ID")]
        public int Id { get => id; set => id = value; }
        [DisplayName("Pet Name")]
        [Required(ErrorMessage ="Pet name is required") ]
        [StringLength (50,MinimumLength =3,ErrorMessage ="Pet name must be between 3 and 50")]
        public string Name { get => name; set => name = value; }
        [DisplayName("Pet type")]
        [Required(ErrorMessage = "Pet tyoe is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Pet type must be between 3 and 50")]
        public string Type { get => type; set => type = value; }
        [DisplayName("Pet color")]
        [Required(ErrorMessage = "Pet color is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = " Pet color must be between 3 and 50")]
        public string Color { get => color; set => color = value; }

    }
}
