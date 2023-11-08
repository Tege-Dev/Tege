using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp
{
    public enum PrivacySetting
    {
        Public = 0,
        Private = 1
    }

    public class Note : IComparable<Note>
    {
        public int Number { get; set; }
        public string Author { get; set; }
        public string Theme { get; set; }
        public string Content { get; set; }
        public string Tag { get; set; }
        public PrivacySetting Privacy { get; set; }

        public Note(int number, string author, string theme, string content, PrivacySetting privacy = PrivacySetting.Private, string tag = null)
        {
            Number = number;
            Author = author;
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

        public int CompareTo(Note other)
        {
            return this.Number.CompareTo(other.Number);
        }
    }

}
