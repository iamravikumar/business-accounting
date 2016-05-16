﻿using System.ComponentModel.DataAnnotations;

namespace BusinessAccounting.Model.Entity
{
    public class JobTitle : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public bool IsActive { get; set; }
        public bool IsBillable { get; set; }

        public JobTitle()
        {
            IsActive = true;
            IsBillable = true;
        }
    }
}