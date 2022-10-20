using System.ComponentModel.DataAnnotations;

namespace onlineshopping.Models
{
    public class Student
    {
        [Key]
        [Required]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        [MaxLength(15)]
        public string TellNumber { get; set; }

        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string FatherName { get; set; }

        [MaxLength(15)]
        public string CellPhone { get; set; }


    }
}
