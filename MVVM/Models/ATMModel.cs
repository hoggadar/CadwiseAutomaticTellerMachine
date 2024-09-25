using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadwiseAutomaticTellerMachine.MVVM.Models
{
    public class ATMModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("BanknoteId")]
        public int BanknoteId { get; set; }
        public BanknoteModel? Banknote { get; set; }
    }
}
