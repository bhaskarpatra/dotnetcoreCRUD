
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationDBManager.Models
{
    public class Order
    {
        [Required]
        [Key]
        public string Id { get; set; }

        [Required]
        [ForeignKey(nameof(Product.Id))]
        public string ProductId { get; set; }

        [Required]
        [ForeignKey(nameof(User.Id))]
        public string UserId { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
