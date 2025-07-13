using System;
namespace Rota.Entities.DTOs
{
	public class TourActivityDto
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int TourDayId { get; set; }  //  aktivitenin hangi güne ait olduğunu belirtir. tur ilişkisi burası üzerinden yapılacak
        public string ActivityImage { get; set; }
    }
}

