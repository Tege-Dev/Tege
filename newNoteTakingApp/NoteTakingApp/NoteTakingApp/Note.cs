using System;
using System.ComponentModel.DataAnnotations;

namespace NoteTakingApp
{
    public enum PrivacySetting
    {
        Public = 0,
        Private = 1
    }

    public class Note : IComparable<Note>
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

        public Note(){}

        public Note(string author, string title, string content, PrivacySetting privacy = PrivacySetting.Private)
        {
            Author = author;
            Title = title;
            Content = content;
            Privacy = privacy;
        }
        public object NumberToObject()
        {
            object boxedNumber = Number;
            return boxedNumber;
        }

        public void NumberFromObject(object boxedNumber)
        {
            Number = (int)boxedNumber;
        }

        public int CompareTo(Note? other)
        {
            if (other == null)
                return 1;
            return this.Number.CompareTo(other.Number);
        }
    }

}
