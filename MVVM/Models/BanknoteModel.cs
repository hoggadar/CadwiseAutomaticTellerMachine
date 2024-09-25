using System.ComponentModel.DataAnnotations;

namespace CadwiseAutomaticTellerMachine.MVVM.Models
{
    public class BanknoteModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Denomination { get; set; }

        public ICollection<ATMModel>? ATMs { get; set; }
    }
}
