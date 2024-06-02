using System;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus.Utilities
{
    public class Settings
    {
        public string LastSavedPilotName { get; set; }
        public int LastDifficulty { get; set; }
        public int LastWeaponType { get; set; }
        public float LastMusicVolume { get; set; }
        public float LastEffectsVolume { get; set; }
        public Settings(string lastSavedPilotName, int lastDifficulty, int lastWeaponType, float lastMusicVolume, float lastEffectsVolume)
        {
            LastSavedPilotName = lastSavedPilotName;
            LastDifficulty = lastDifficulty;
            LastWeaponType = lastWeaponType;
            LastMusicVolume = lastMusicVolume;
            LastEffectsVolume = lastEffectsVolume;
        }
    }

    public class PlayerRecord
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }

        public PlayerRecord(string playerName, int score)
        {
            PlayerName = playerName;
            Score = score;
        }
    }

    public class TopPlayers
    {
        public List<PlayerRecord> Players { get; set; } = new List<PlayerRecord>();
    }
}
