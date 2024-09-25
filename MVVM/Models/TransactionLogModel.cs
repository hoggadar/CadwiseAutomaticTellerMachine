using CadwiseAutomaticTellerMachine.MVVM.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadwiseAutomaticTellerMachine.MVVM.Models
{
    public class TransactionLogModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public TypeEnum Type { get; set; }

        [Required]
        public StatusEnum Status { get; set; }

        [ForeignKey("CardId")]
        public int CardId { get; set; }
        public CardModel? Card { get; set; }

    }
}
