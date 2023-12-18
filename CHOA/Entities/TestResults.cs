﻿using System.ComponentModel.DataAnnotations;

namespace CHOA.Entities
{
    public class TestResults
    {
        public int Id { get; set; }
        [Required]
        public int TestId { get; set; }
        public string XBest{ get; set; }
        public double FBest { get; set; }
        [Required]
        public string Parameters { get; set; }
    }
}
