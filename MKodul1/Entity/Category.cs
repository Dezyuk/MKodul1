﻿namespace MKodul1.Entity
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public List<Question> Questions { get; set; } = new();
    }
}
