using System.ComponentModel.DataAnnotations;

namespace CHOA.Entities
{
    public class Sessions
    {
        public int Id { get; set; }
  
        [Required]
        public string AlgorithmIds { get; set; }
        [Required]
        public string FitnessFunctionIds { get; set; }
        [Required]
        public string State { get; set; }
    }

}
