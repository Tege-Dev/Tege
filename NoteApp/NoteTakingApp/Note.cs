using System;
using System.ComponentModel.DataAnnotations;

namespace NoteTakingApp
{
    public enum PrivacySetting
    {
        Public = 0,
        Private = 1
    }

    public enum SharingSetting
    {
        Viewing = 0,
        Commenting = 1,
        Editing = 2
    }

    public class Note : IComparable<Note>, IEquatable<Note>
    {
        [Key]
        public int Number { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        public PrivacySetting Privacy { get; set; }
        public SharingSetting Sharing { get; set; }

        public Note() { }

        public Note(string author, string title, string content, PrivacySetting privacy = PrivacySetting.Private, SharingSetting sharing = SharingSetting.Viewing)
        {
            Author = author;
            Title = title;
            Content = content;
            Privacy = privacy;
            Sharing = sharing;
        }

        public int CompareTo(Note other)
        {
            if (other == null)
            {
                return 1;
            }
            return Number.CompareTo(other.Number);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Note);
        }

        public bool Equals(Note other)
        {
            if (other == null)
            {
                return false;
            }

            return Number == other.Number &&
                   Author == other.Author &&
                   Title == other.Title &&
                   Content == other.Content &&
                   Privacy == other.Privacy;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Number, Author, Title, Content, Privacy);
        }
    }
}
