using System;
namespace Rota.Entities.DTOs
{
	public class TourDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public decimal Price { get; set; }
		public int Capacity { get; set; }
		public string Category { get; set; }
		public Guid? GuideId { get; set; }
		public string GuideName { get; set; }
	}
}

