using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Eye_of_the_Bovine
{
    public class Music
    {
        private List<SoundEffect> SoundEffects;
        public List<SoundEffect> WAVS
        {
            get
            {
                return SoundEffects;
            }
        }
        private List<Song> Songs;
        public List<Song> SONGS
        {
            get
            {
                return Songs;
            }
        }
        public int playslot;
        bool hassong;
        bool haswav;
        private App ownerapp;

        public Music(App owner)
        {
            ownerapp = owner;
            Songs = new List<Song>();
            SoundEffects = new List<SoundEffect>();
            hassong = false;
            haswav = false;
            playslot = 0;
        }

        public void LoadSong(string title)
        {
            Songs.Add(ownerapp.Content.Load<Song>(title));
            hassong = true;
        }

        public void LoadWav(string title)
        {
            SoundEffects.Add(ownerapp.Content.Load<SoundEffect>(title));
            haswav = true;
        }

        public Music(App owner, List<Song> music)
        {
            ownerapp = owner;
            SoundEffects = new List<SoundEffect>();
            Songs = music;
            hassong = true;
            haswav = false;
            playslot = 0;
        }

        public void AddSong(Song track)
        {
            Songs.Add(track);
            hassong = true;
        }

        public Music(App owner, List<SoundEffect> wavs)
        {
            ownerapp = owner;
            Songs = new List<Song>();
            SoundEffects = wavs;
            hassong = false;
            haswav = true;
            playslot = 0;
        }

        public void AddWav(SoundEffect sound)
        {
            SoundEffects.Add(sound);
            haswav = true;
        }

        public Music(App owner, List<Song> music, List<SoundEffect> wavs)
        {
            ownerapp = owner;
            Songs = music;
            SoundEffects = wavs;
            hassong = true;
            haswav = true;
            playslot = 0;
        }

        public void PlayMusic(int slot)
        {
            if (hassong)
            {
                if (slot < 0)
                {
                    slot = 0 - slot;
                }
                if (slot >= Songs.Count)
                {
                    slot = 0;
                }
                if (slot < Songs.Count)
                {
                    MediaPlayer.Stop();
                }
                if (slot < Songs.Count)
                {
                    MediaPlayer.Play(Songs[slot]);
                    playslot = slot;
                }
            }
        }

        public void PlayWav(int slot)
        {
            if (haswav)
            {
                if (slot < 0)
                {
                    slot = 0 - slot;
                }
                if (slot < SoundEffects.Count)
                {
                    SoundEffects[slot].Play();
                }
            }
        }

        public void Update()
        {
            if (playslot <= Songs.Count)
            {
                if (((MediaPlayer.PlayPosition.Minutes * 60) + MediaPlayer.PlayPosition.Seconds) >
                    ((((float)(Songs[playslot].Duration.Minutes * 60)) + ((float)Songs[playslot].Duration.Seconds)) -
                ((((float)(Songs[playslot].Duration.Minutes * 60)) + ((float)Songs[playslot].Duration.Seconds) * .01f))))
                {

                    if (playslot < Songs.Count)
                    {
                        playslot++;
                        MediaPlayer.Play(Songs[playslot]);
                    }
                    else
                    {
                        MediaPlayer.Play(Songs[0]);
                        playslot = 0;
                    }


                }
            }
        }

        public void Update(int last, int progress)
        {
            if (playslot <= Songs.Count)
            {
                if (last == progress)
                {
                    if (((MediaPlayer.PlayPosition.Minutes * 60) + MediaPlayer.PlayPosition.Seconds) >
                        ((((float)(Songs[playslot].Duration.Minutes * 60)) + ((float)Songs[playslot].Duration.Seconds)) -
                    ((((float)(Songs[playslot].Duration.Minutes * 60)) + ((float)Songs[playslot].Duration.Seconds) * .01f))))
                    {
                        MediaPlayer.Play(Songs[playslot]);
                    }
                }
                else
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(Songs[(playslot + (progress - last))]);
                }
            }
        }
    }
}
