using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Product
{
    public class ImgPath
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PathId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Path")]
        public string Path { get; set; }
    }
}
