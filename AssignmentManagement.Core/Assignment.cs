using System;
using AssignmentManagement.Core;

namespace AssignmentManagement.Core
{
    public class Assignment
    {
        private static int _nextId = 1;

        public int Id { get; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsCompleted { get; private set; }
        public Priority Priority { get; private set; }

        public Assignment(string title, string description, Priority priority = Priority.Medium)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty.");

            Title = title;
            Description = description;
            Priority = priority;
        }

        public void MarkComplete() => IsCompleted = true;

        public void Update(string newTitle, string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newTitle)) throw new ArgumentException("Title is required.");
            if (string.IsNullOrWhiteSpace(newDescription)) throw new ArgumentException("Description is required.");

            Title = newTitle;
            Description = newDescription;
        }

        // 🆕 Optional: Add method to update priority if needed
        public void UpdatePriority(Priority newPriority)
        {
            Priority = newPriority;
        }
    }
}