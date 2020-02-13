using System;
using System.ComponentModel.DataAnnotations;

namespace SULS.App.Models
{
    public class Submission
    {
        public Submission()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [MaxLength(800), Required]
        public string Code { get; set; }

        [Required]
        public int AchievedResult { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string ProblemId { get; set; }

        public Problem Problem { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
