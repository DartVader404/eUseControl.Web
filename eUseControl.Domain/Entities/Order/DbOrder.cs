using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Order
{
    public class DbOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int AddressId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "ProductName")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Price")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [DataType(DataType.Date)]
        public DateTime AddedDate { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        public string OrderNote { get; set; }
    }
}
