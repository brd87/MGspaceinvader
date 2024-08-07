using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace SpaceInvaderPlusPlus.Utilities
{
    internal class Settings
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

    internal class PlayerRecord
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }

        public PlayerRecord(string playerName, int score)
        {
            PlayerName = playerName;
            Score = score;
        }
    }

    internal class TopPlayers
    {
        public List<PlayerRecord> Players { get; set; } = new List<PlayerRecord>();
    }

    internal class AssetLibrary
    {
        public Texture2D tPlayer { get; set; }
        public Texture2D tPlayer_Front { get; set; }
        public Texture2D tPlayer_Left { get; set; }
        public Texture2D tPlayer_Right { get; set; }

        public Texture2D tWep_Gun { get; set; }
        public Texture2D tWep_Gun_Bul { get; set; }
        public Texture2D tWep_Gun_Fire { get; set; }
        public Texture2D tWep_Rail { get; set; }
        public Texture2D tWep_Rail_Bul { get; set; }
        public Texture2D tWep_Rail_Fire { get; set; }
        public Texture2D tWep_Saw { get; set; }
        public Texture2D tWep_Saw_Bul { get; set; }
        public Texture2D tWep_Saw_Fire { get; set; }
        public Texture2D tWep_Duo { get; set; }
        public Texture2D tWep_Duo_Bul { get; set; }
        public Texture2D tWep_Duo_Fire { get; set; }
        public Texture2D tWep_Nail { get; set; }
        public Texture2D tWep_Nail_Bul { get; set; }
        public Texture2D tWep_Nail_Fire { get; set; }
        public Texture2D tWep_Orb { get; set; }
        public Texture2D tWep_Orb_Bul { get; set; }
        public Texture2D tWep_Orb_Fire { get; set; }

        public Texture2D tPack_UltAbility { get; set; }
        public Texture2D tPack_Medpack { get; set; }
        public Texture2D tPack_Energy { get; set; }

        public Texture2D tEnv_Med { get; set; }
        public Texture2D tEnv_Mine { get; set; }
        public Texture2D tEnv_Rock { get; set; }

        public Texture2D tEne_Wall { get; set; }
        public Texture2D tEne_Wall_Ani { get; set; }
        public Texture2D tEne_Wall_Dmg { get; set; }
        public Texture2D tEne_Wall_Ani_Dmg { get; set; }
        public Texture2D tEne_Rusher { get; set; }
        public Texture2D tEne_Rusher_Ani { get; set; }
        public Texture2D tEne_Rusher_Dmg { get; set; }
        public Texture2D tEne_Rusher_Ani_Dmg { get; set; }
        public Texture2D tEne_Spewer { get; set; }
        public Texture2D tEne_Spewer_Ani { get; set; }
        public Texture2D tEne_Spewer_Dmg { get; set; }
        public Texture2D tEne_Spewer_Ani_Dmg { get; set; }

        public Texture2D tEneProjectile { get; set; }

        public Texture2D tRem_p1 { get; set; }
        public Texture2D tRem_p2 { get; set; }
        public Texture2D tRem_p3 { get; set; }
        public Texture2D tRem_p4 { get; set; }
        public Texture2D tRem_p5 { get; set; }
        public Texture2D tRem_p6 { get; set; }
        public Texture2D tRem_p7 { get; set; }

        public Texture2D tStar_w { get; set; }
        public Texture2D tStar_r { get; set; }
        public Texture2D tStar_y { get; set; }
        public Texture2D tStar_b { get; set; }

        public Texture2D tOther_bg_rock { get; set; }
        public Texture2D tOther_dmgeye { get; set; }
        public Texture2D tOther_ultShock { get; set; }
        public Texture2D tOther_wreck { get; set; }
        public Texture2D tOther_gaze { get; set; }
        public Texture2D tOther_plasma { get; set; }
        public Texture2D tOther_spark { get; set; }
        public Texture2D tOther_shellGun { get; set; }
        public Texture2D tOther_shellDuo { get; set; }
        

        public Song bg_music_menu { get; set; }
        public Song bg_music_combat { get; set; }

        public SoundEffect eff_Gun { get; set; }
        public SoundEffect eff_Rail { get; set; }
        public SoundEffect eff_Saw { get; set; }
        public SoundEffect eff_Select { get; set; }
        public SoundEffect eff_Death { get; set; }
        public SoundEffect eff_Ult { get; set; }

        public AssetLibrary(ref General general)
        {
            tPlayer = LoadTexture("player/player", ref general);
            tPlayer_Front = LoadTexture("player/player_front", ref general);
            tPlayer_Left = LoadTexture("player/player_lwing", ref general);
            tPlayer_Right = LoadTexture("player/player_rwing", ref general);

            tWep_Gun = LoadTexture("wep/wep_thegun", ref general);
            tWep_Gun_Bul = LoadTexture("wep/wep_thegun_bullet", ref general);
            tWep_Gun_Fire = LoadTexture("wep/wep_thegun_fire", ref general);
            tWep_Rail = LoadTexture("wep/wep_therail", ref general);
            tWep_Rail_Bul = LoadTexture("wep/wep_therail_bullet", ref general);
            tWep_Rail_Fire = LoadTexture("wep/wep_therail_fire", ref general);
            tWep_Saw = LoadTexture("wep/wep_thesaw", ref general);
            tWep_Saw_Bul = LoadTexture("wep/wep_thesaw_bullet", ref general);
            tWep_Saw_Fire = LoadTexture("wep/wep_thesaw_fire", ref general);
            tWep_Duo = LoadTexture("wep/wep_theduo", ref general);
            tWep_Duo_Bul = LoadTexture("wep/wep_theduo_bullet", ref general);
            tWep_Duo_Fire = LoadTexture("wep/wep_theduo_fire", ref general);
            tWep_Nail = LoadTexture("wep/wep_thenail", ref general);
            tWep_Nail_Bul = LoadTexture("wep/wep_thenail_bullet", ref general);
            tWep_Nail_Fire = LoadTexture("wep/wep_thenail_fire", ref general);
            tWep_Orb = LoadTexture("wep/wep_theorb", ref general);
            tWep_Orb_Bul = LoadTexture("wep/wep_theorb_bullet", ref general);
            tWep_Orb_Fire = LoadTexture("wep/wep_theorb_fire", ref general);


            tPack_UltAbility = LoadTexture("pack/pack_ult", ref general);
            tPack_Medpack = LoadTexture("pack/pack_med", ref general);
            tPack_Energy = LoadTexture("pack/pack_energy", ref general);

            tEnv_Med = LoadTexture("env/env_med", ref general);
            tEnv_Mine = LoadTexture("env/env_mine", ref general);
            tEnv_Rock = LoadTexture("env/env_rock", ref general);

            tEne_Wall = LoadTexture("ene/ene_thewall", ref general);
            tEne_Wall_Ani = LoadTexture("ene/ene_thewall_wings", ref general);
            tEne_Wall_Dmg = LoadTexture("ene/ene_thewall_dmg", ref general);
            tEne_Wall_Ani_Dmg = LoadTexture("ene/ene_thewall_wings_dmg", ref general);
            tEne_Rusher = LoadTexture("ene/ene_therusher", ref general);
            tEne_Rusher_Ani = LoadTexture("ene/ene_therusher_claws", ref general);
            tEne_Rusher_Dmg = LoadTexture("ene/ene_therusher_dmg", ref general);
            tEne_Rusher_Ani_Dmg = LoadTexture("ene/ene_therusher_claws_dmg", ref general);
            tEne_Spewer = LoadTexture("ene/ene_thespewer", ref general);
            tEne_Spewer_Ani = LoadTexture("ene/ene_thespewer_pipes", ref general);
            tEne_Spewer_Dmg = LoadTexture("ene/ene_thespewer_dmg", ref general);
            tEne_Spewer_Ani_Dmg = LoadTexture("ene/ene_thespewer_pipes_dmg", ref general);

            tEneProjectile = LoadTexture("ene/ene_thespewer_bullet", ref general);

            tRem_p1 = LoadTexture("rem/rem_p1", ref general);
            tRem_p2 = LoadTexture("rem/rem_p2", ref general);
            tRem_p3 = LoadTexture("rem/rem_p3", ref general);
            tRem_p4 = LoadTexture("rem/rem_p4", ref general);
            tRem_p5 = LoadTexture("rem/rem_p5", ref general);
            tRem_p6 = LoadTexture("rem/rem_p6", ref general);
            tRem_p7 = LoadTexture("rem/rem_p7", ref general);

            tStar_w = LoadTexture("star/star_white", ref general);
            tStar_r = LoadTexture("star/star_red", ref general);
            tStar_y = LoadTexture("star/star_yellow", ref general);
            tStar_b = LoadTexture("star/star_blue", ref general);

            tOther_bg_rock = LoadTexture("other/bg_rock", ref general);
            tOther_dmgeye = LoadTexture("other/dmgeye", ref general);
            tOther_ultShock = LoadTexture("other/ult_shock", ref general);
            tOther_wreck = LoadTexture("other/wreck", ref general);
            tOther_gaze = LoadTexture("other/gaze", ref general);
            tOther_plasma = LoadTexture("other/plasma_trail", ref general);
            tOther_spark = LoadTexture("other/volt_spark", ref general);
            tOther_shellGun = LoadTexture("other/thegun_shell", ref general);
            tOther_shellDuo = LoadTexture("other/theduo_shell", ref general);

            bg_music_menu = general.CONTENT.Load<Song>("music/menu");
            bg_music_combat = general.CONTENT.Load<Song>("music/combat");

            eff_Gun = general.CONTENT.Load<SoundEffect>("eff/eff_gun");
            eff_Rail = general.CONTENT.Load<SoundEffect>("eff/eff_rail");
            eff_Saw = general.CONTENT.Load<SoundEffect>("eff/eff_saw");
            eff_Select = general.CONTENT.Load<SoundEffect>("eff/eff_select");
            eff_Ult = general.CONTENT.Load<SoundEffect>("eff/eff_ult");
        }
        private Texture2D LoadTexture(string spriteName, ref General general)
        {
            if (spriteName != null)
                return general.CONTENT.Load<Texture2D>(spriteName);
            else
                return general.CONTENT.Load<Texture2D>("other/error");
        }
    }
}
