using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadwiseAutomaticTellerMachine.MVVM.Models
{
    public class CardModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(16)]
        public string Number { get; set; } = string.Empty;

        [Required]
        [StringLength(4)]
        public string PIN { get; set; } = string.Empty;

        [Required]
        public int Cash { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public UserModel? User { get; set; }

        public ICollection<TransactionLogModel>? TransactionLogs { get; set; }
    }
}
