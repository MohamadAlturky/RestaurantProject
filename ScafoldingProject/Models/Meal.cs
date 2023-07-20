using System;
using System.Collections.Generic;

namespace ScafoldingProject.Models
{
    public partial class Meal
    {
        public Meal()
        {
            MealEntries = new HashSet<MealEntry>();
        }

        public long Id { get; set; }
        public int NumberOfCalories { get; set; }
        public string Type { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImagePath { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<MealEntry> MealEntries { get; set; }
    }
}
