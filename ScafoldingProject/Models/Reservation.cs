using System;
using System.Collections.Generic;

namespace ScafoldingProject.Models
{
    public partial class Reservation
    {
        public long Id { get; set; }
        public DateTime AtDay { get; set; }
        public long CustomerId { get; set; }
        public long MealEntryId { get; set; }
        public string ReservationStatus { get; set; } = null!;
        public int NumberInQueue { get; set; }
        public int Price { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual MealEntry MealEntry { get; set; } = null!;
    }
}
