﻿using System.Text.Json.Serialization;

namespace PRN231_GroupProject_LearningOnline.Models.DTO
{
    public class QuizDTO
    {
        public int questionNo { get; set;}
        public string question { get; set; }
        public string answerA { get; set; }
        public string answerB { get; set; }
        public string answerC { get; set; }
        public string answerD { get; set; }
        [JsonIgnore]
        public string correctAnswer { get; set; }
        public string answer { get; set; }
    }
    public class QuizToGradeDTO
    {
        public int questionNo { get; set; }
        public string question { get; set; }
        public string answerA { get; set; }
        public string answerB { get; set; }
        public string answerC { get; set; }
        public string answerD { get; set; }
        public string correctAnswer { get; set; }
        public string answer { get; set; }
    }
}
