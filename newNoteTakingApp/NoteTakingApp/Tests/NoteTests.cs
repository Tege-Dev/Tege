using System;
using NoteTakingApp;

namespace Tests
{
    [TestClass]
    public class NoteTests
    {
        [TestMethod]
        public void NoteCompareTo_SmallerNumber_ReturnsNegative()
        {
            var note1 = new Note(1, "AuthorOne", "Theme1", "Content1");
            var note2 = new Note(2, "AuthorTwo", "Theme2", "Content2");
            int result = note1.CompareTo(note2);
            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void NoteCompareTo_EqualNumbers_ReturnsZero()
        {
            var note1 = new Note(1, "AuthorOne", "Theme1", "Content1");
            var note2 = new Note(1, "AuthorTwo", "Theme2", "Content2");
            int result = note1.CompareTo(note2);
            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void NoteCompareTo_GreaterNumber_ReturnsPositive()
        {
            var note1 = new Note(2, "AuthorOne", "Theme1", "Content1");
            var note2 = new Note(1, "AuthorTwo", "Theme2", "Content2");
            int result = note1.CompareTo(note2);
            Assert.IsTrue(result > 0);
        }
    }
}