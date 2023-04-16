﻿namespace WebApplication1.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        public int CoursePoint { get; set; }    
        public List<Points>? Points { get; set; }

    }
}
