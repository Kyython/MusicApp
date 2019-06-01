using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Documents;

namespace ThreadApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PlayMusic();
        }

        private void WindowClosed(object sender, System.EventArgs e)
        {
            var thread = new Thread(SaveTextInFile);
            thread.Start();
        }

        private void PlayMusic()
        {
            var demonThread = new Thread(mediaElement.Play)
            {
                IsBackground = true
            };

            demonThread.Start();
        }

        private void SaveTextInFile()
        {
            string text = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;

            using (var stream = new FileStream("UserText.txt", FileMode.Create))
            {
                byte[] bytes = Encoding.Unicode.GetBytes(text);
                stream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
