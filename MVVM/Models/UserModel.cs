using System.ComponentModel.DataAnnotations;

namespace CadwiseAutomaticTellerMachine.MVVM.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Surname { get; set; } = string.Empty;

        public ICollection<CardModel>? Cards { get; set; }
    }
}
