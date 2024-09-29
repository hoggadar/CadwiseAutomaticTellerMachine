using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CadwiseAutomaticTellerMachine.MVVM.Models
{
    public class StorageModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, 100)]
        public int Quantity { get; set; }

        [ForeignKey("BanknoteId")]
        public int BanknoteId { get; set; }
        public BanknoteModel? Banknote { get; set; }
    }
}
