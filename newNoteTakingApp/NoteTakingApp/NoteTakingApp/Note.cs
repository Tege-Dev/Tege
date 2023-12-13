using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Author { get; set; } = LoginWindow.Username;
        [Required]
        public string Theme { get; set; }
        [Required]
        public string Content { get; set; }

        public string Tag { get; set; }

        public PrivacySetting Privacy { get; set; }

        public Note(string theme, string content, PrivacySetting privacy = PrivacySetting.Private, string tag = null!)
        {
            Theme = theme;
            Content = content;
            Privacy = privacy;
            Tag = tag;
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
