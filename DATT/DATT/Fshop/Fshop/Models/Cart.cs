using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fshop.Models
{
	[Table("Cart")]
	public class Cart
	{
		[Key]
		public long CartID { get; set; }
		public long ProductID { get; set; }
		public string? ProductName { get; set; }
		public int Price { get; set; }
		public int Discount { get; set; }
		public int Quantity { get; set; }
		public int MenuID { get; set; }
		public bool IsActive { get; set; }
		public string? Images { get; set; }

	}
}
