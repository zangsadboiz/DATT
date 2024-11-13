using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fshop.Models
{
	[Table("Review")]
	public class Review
	{
		[Key]
		public int ReviewID { get; set; }
		public string? Author { get; set; }
		public string? Title { get; set; }
		public string? Contents { get; set; }
		public string? Images { get; set; }
		public string? Link { get; set; }
		public bool? IsActive { get; set; }
		public int ReviewOrder { get; set; }
		public int Status { get; set; }

	}
}
