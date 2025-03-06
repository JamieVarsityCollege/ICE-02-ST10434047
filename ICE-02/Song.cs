using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICE_02
{
    class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public string FilePath { get; set; }

        public Song(string title, string artist, string album, string genre, int year, string filePath)
        {
            Title = title;
            Artist = artist;
            Album = album;
            Genre = genre;
            Year = year;
            FilePath = filePath;
        }

    }
}
