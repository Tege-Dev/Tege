using NoteTakingApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tests
{
    [TestClass]
    public class MainWindowTests
    {
        [TestMethod]
        public void Note_Constructors_ShouldInitializeProperties()
        {
            var note = new Note(1, "Author", "Theme", "Content", PrivacySetting.Private, "Tag");
            Assert.AreEqual(1, note.Number);
            Assert.AreEqual("Author", note.Author);
            Assert.AreEqual("Theme", note.Theme);
            Assert.AreEqual("Content", note.Content);
            Assert.AreEqual(PrivacySetting.Private, note.Privacy);
            Assert.AreEqual("Tag", note.Tag);
        }
    }
}
