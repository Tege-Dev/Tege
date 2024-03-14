using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NoteTakingApp
{
    public class UserNote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public int PublicNoteNumber { get; set; }

        public UserNote() { }

        public UserNote(string userName, int publicNoteNumber)
        {
            UserName = userName;
            PublicNoteNumber = publicNoteNumber;
        }
    }
}
