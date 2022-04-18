using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace eUseControl.Domain.Entities.Product
{
    public class UpdateProductData
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string PreImgPath { get; set; }
        public List<ImgPath> Paths { get; set; }
        public HttpPostedFileBase PreviewImg { get; set; }
        public HttpPostedFileBase[] ProductImg { get; set; }
        public bool[] status { get; set; }
    }
}
