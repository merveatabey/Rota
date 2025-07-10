using System;
namespace Entities
{
	public class Reservation
	{
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int TourId { get; set; }

        public int GuestCount { get; set; }
        public string Status { get; set; } // "Bekliyor", "Onaylandı", "İptal"
        public decimal TotalPrice { get; set; }
        public DateTime ReservationDate { get; set; }

        public User User { get; set; }
        public Tour Tour { get; set; }
        public Payment Payment { get; set; }
    }
}

