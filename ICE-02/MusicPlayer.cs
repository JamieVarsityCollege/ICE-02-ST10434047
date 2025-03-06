using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using TagLib;
using Spectre.Console;
using System.Threading;

namespace ICE_02
{
    class MusicPlayer
    {
        private SoundPlayer _player;
        private List<Song> _songs;
        private string[] _songsAsStrings = new string[4];

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
                else if (songPath.Contains("ride"))
                {
                    _songs.Add(new Song("Ride", "TwentyOnePilots", "BlurryFace", "Pop", 2015, songPath));
                }
                else if (songPath.Contains("stressedOut"))
                {
                    _songs.Add(new Song("Stressed Out", "TwentyOnePilots", "BlurryFace", "Pop", 2015, songPath));
                }
            }
            displaySongs();
        }

        private void convertSongsToStringArray()
        {
            for (int i = 0; i < _songs.Count; i++)
            {
                var file = TagLib.File.Create(_songs[i].FilePath);
                _songsAsStrings[i] = $"{i + 1}. {_songs[i].Title} by {_songs[i].Artist} ({_songs[i].Year}) - Duration: {file.Properties.Duration}";
            }
        }

        private void displaySongs()
        {
            convertSongsToStringArray();
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>().Title("Use [red]arrow keys[/] to select a song: ")
                .PageSize(4)
                .AddChoices(_songsAsStrings));
            int index = 0;
            for (int i = 0; i < _songs.Count; i++)
            {
                if (selection.Contains(_songs[i].Title))
                {
                    index = i;
                    break;
                }
            }
            playSong(_songs[index].FilePath);
        }

        private void playSong(string songPath)
        {
            _player.SoundLocation = Path.Combine(Directory.GetCurrentDirectory(), "Music", songPath);
            _player.Load();
            Console.WriteLine("Press enter to stop the song:");
            _player.Play();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyPressed = Console.ReadKey(true); // true to not display the entered character.
                    if (keyPressed.Key == ConsoleKey.Enter)
                    {
                        _player.Stop();
                        Console.WriteLine("Song stopped.");
                        Thread.Sleep(1000);
                        break;
                    }
                }
            }
            Console.Clear();
            initializeSongs();

        }
    }
}
