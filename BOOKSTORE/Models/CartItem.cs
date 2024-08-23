using System.ComponentModel.DataAnnotations;

namespace BOOKSTORE.Models
{
	public class CartItem
	{
       [Display(Name = "Mã sản phẩm")]
       [Required]
       public string ProductId { get; set; }

	   public string Image { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Giá bán")]
        [Range(1000, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 1000")]
        [Required]
        public double UnitPrice { get; set; }

        [Display(Name = "Số lượng")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        [Required]
        public int Quantity { get; set; }
	   public double Total
	   {
			get { return Quantity * UnitPrice; }
	   }

	}
}
