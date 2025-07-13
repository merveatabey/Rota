using System;
namespace Rota.Entities.DTOs
{
	public class ReservationDto
	{
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int TourId { get; set; }

        public int GuestCount { get; set; }
        public string Status { get; set; }  // "Bekliyor", "Onaylandı", "İptal"
        public decimal TotalPrice { get; set; }
        public DateTime ReservationDate { get; set; }

        public UserDto User { get; set; }         
        public TourDto Tour { get; set; }         
        public PaymentDto Payment { get; set; }   
    }
}

