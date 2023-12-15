/*using NoteTakingApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Tests
{
    [TestClass]
    public class MainWindowTests
    {
        [TestMethod]
        public void Note_Constructors_ShouldInitializeProperties()
        {
            var note = new Note("Theme", "Content", PrivacySetting.Private);
            Assert.AreEqual(1, note.Number);
            Assert.AreEqual("Author", note.Author);
            Assert.AreEqual("Theme", note.Title);
            Assert.AreEqual("Content", note.Content);
            Assert.AreEqual(PrivacySetting.Private, note.Privacy);
        }

        [TestMethod]
        public void LoadNotesFromDatabase_ShouldLoadNotesFromDatabase()
        {
            // Arrange
            MainWindow mainWindow = null;
            var expectedNote = new Note("Theme", "Content", PrivacySetting.Private);
            var manualResetEvent = new ManualResetEvent(false);

            // Act
            var thread = new Thread(() =>
            {
                // Initialize and run the UI-related code on the STA thread
                mainWindow = new MainWindow();
                mainWindow.Notes.Add(expectedNote);
                mainWindow.SaveNoteToDatabase();

                // Signal the test thread that the UI-related code has completed
                manualResetEvent.Set();

                // Use Application.Run to start the WPF application on the STA thread
                var application = new Application();
                application.Run(mainWindow);
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            // Wait for the UI-related code to complete before continuing with the test
            manualResetEvent.WaitOne();

            // Assert
            Assert.IsNotNull(mainWindow); // Ensure that the UI-related code ran successfully

            var loadedNotes = mainWindow.Notes;

            Assert.AreEqual(1, loadedNotes.Count);
            Assert.AreEqual(expectedNote.Number, loadedNotes[0].Number);
            Assert.AreEqual(expectedNote.Author, loadedNotes[0].Author);
            Assert.AreEqual(expectedNote.Title, loadedNotes[0].Title);
            Assert.AreEqual(expectedNote.Content, loadedNotes[0].Content);
            Assert.AreEqual(expectedNote.Privacy, loadedNotes[0].Privacy);
        }

        [TestMethod]
        public void SaveNotesToDatabase_ShouldSaveNotesToDatabase()
        {
            // Arrange
            MainWindow mainWindow = null;
            var expectedNote = new Note("Theme", "Content", PrivacySetting.Private);

            // Act
            var thread = new Thread(() =>
            {
                mainWindow = new MainWindow();
                mainWindow.Notes.Add(expectedNote);
                mainWindow.SaveNoteToDatabase();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            // Assert
            using (var context = new NoteDbContext())
            {
                var savedNote = context.Notes.FirstOrDefault();
                Assert.IsNotNull(savedNote);
                Assert.AreEqual(expectedNote.Number, savedNote.Number);
                Assert.AreEqual(expectedNote.Author, savedNote.Author);
                Assert.AreEqual(expectedNote.Title, savedNote.Title);
                Assert.AreEqual(expectedNote.Content, savedNote.Content);
                Assert.AreEqual(expectedNote.Privacy, savedNote.Privacy);
            }
        }


    }
}
*/