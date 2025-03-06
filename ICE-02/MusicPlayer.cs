using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using TagLib;

namespace ICE_02
{
    class MusicPlayer
    {
        private SoundPlayer _player;
        private List<Song> _songs;

        public MusicPlayer()
        {
            Console.WriteLine("Welcome to the Music Player!");
            initializePlayer();
            initializeSongs();
        }

        private void initializePlayer()
        {
            _player = new SoundPlayer();
        }

        private void initializeSongs()
        {
            _songs = new List<Song>();
            string[] songPaths = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Music"));
            foreach (string songPath in songPaths)
            {
                if (songPath.Contains("shapeOfYou"))
                {
                    _songs.Add(new Song("Shape of You", "Ed Sheeran", "Deluxe", "Pop", 2017, songPath));
                }
                else if (songPath.Contains("lookWhatYouMadeMeDo"))
                {
                    _songs.Add(new Song("Look what you made me do", "Taylor Swift", "Reputation", "Pop", 2017, songPath));
                }
            }
            displaySongs();
        }

        private void displaySongs()
        {
            for (int i = 0; i < _songs.Count; i++)
            {
                var file = TagLib.File.Create(_songs[i].FilePath);
                Console.WriteLine($"{i + 1}. {_songs[i].Title} by {_songs[i].Artist} ({_songs[i].Year}) - Duration: {file.Properties.Duration}");
            }
            int songIndex = int.Parse(Console.ReadLine()) - 1;
            playSong(_songs[songIndex].FilePath);
        }

        private void playSong(string songPath)
        {
            _player.SoundLocation = Path.Combine(Directory.GetCurrentDirectory(), "Music", songPath);
            _player.Load();
            _player.PlaySync();
        }
    }
}
