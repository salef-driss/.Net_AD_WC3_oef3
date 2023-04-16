using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Points
    {
        

        [Display(Name = "Student")]
        public int StudentId { get; set; }
        [Display(Name ="Course")]
        public int CourseId { get; set; }
        public Student? Student { get; set; }
        public Course? Course { get; set; }
        [Range(0,20)]
        public int Grade { get; set; }
    }
}
