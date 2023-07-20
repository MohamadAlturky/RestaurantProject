using System;
using System.Collections.Generic;

namespace ScafoldingProject.Models
{
    public partial class MealEntry
    {
        public MealEntry()
        {
            Reservations = new HashSet<Reservation>();
        }

        public long Id { get; set; }
        public long MealId { get; set; }
        public bool CustomerCanCancel { get; set; }
        public DateTime AtDay { get; set; }
        public int ReservationsCount { get; set; }
        public int PreparedCount { get; set; }
        public int LastNumberInQueue { get; set; }

        public virtual Meal Meal { get; set; } = null!;
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
