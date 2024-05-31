using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaderPlusPlus.Enemies;
using SpaceInvaderPlusPlus.Environments;
using SpaceInvaderPlusPlus.Pickups;
using SpaceInvaderPlusPlus.Players;
using SpaceInvaderPlusPlus.Utilities;
using SpaceInvaderPlusPlus.Weapons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SpaceInvaderPlusPlus
{
    public class MainWorld
    {
        private Player _player { get; set; }
        private Weapon _weapon { get; set; }
        private List<Enemy> _enemyWall { get; set; }
        private List<Enemy> _enemyRusher { get; set; }
        private List<Enemy> _enemySpewer { get; set; }
        private List<Pickup> _pickups { get; set; }
        private List<Environmental> _enviroments { get; set; }
        private List<UltAbility> _ultAbility { get; set; }
        private Entity _damageScr { get; set; }
        private SpriteFont _hudFont { get; set; }
        private SpriteFont _hudFontAux { get; set; }
        private TimeSpan _lastTimeWall { get; set; }
        private TimeSpan _lastTimeRusher { get; set; }
        private TimeSpan _lastTimeSpewer { get; set; }
        private TimeSpan _lastTimePickup { get; set; }
        private TimeSpan _lastTimeEnviroment { get; set; }
        private int _previusScale { get; set; }
        private float _cooldawnWall { get; set; }
        private float _cooldawnRusher { get; set; }
        private float _cooldawnSpewer { get; set; }
        private float _cooldawnPickup { get; set; }
        private float _cooldawnEnviroment { get; set; }
        private int _enemySawnHeightMax { get; set; }
        private int _enemySpawnHeightMin { get; set; }
        private int _otherSawnHeightMax { get; set; }
        private int _otherSpawnHeightMin { get; set; }
        private int _despawnHeight { get; set; }


        public MainWorld()
        {
            _enemyWall = new List<Enemy>();
            _enemyRusher = new List<Enemy>();
            _enemySpewer = new List<Enemy>();
            _pickups = new List<Pickup>();
            _enviroments = new List<Environmental>();
            _ultAbility = new List<UltAbility>();
        }

        public void RanNew()
        {
            Holder.STARTNEW = false;
            Holder.RUNWORLD = true;
            Holder.SCORE_TRAVEL = 0;
            Holder.SCORE_DMG = 0;
            Holder.SCORE_DMGPLAYER = 0;
            Holder.SCORE_PICKUPS = 0;
            Holder.SCORE_AMMOWASTE = 0;

            _hudFont = Holder.CONTENT.Load<SpriteFont>("font/font_hudmain");
            _hudFontAux = Holder.CONTENT.Load<SpriteFont>("font/font_hudaux");
            _enemySawnHeightMax = -1100;
            _enemySpawnHeightMin = -100;
            _otherSawnHeightMax = -200;
            _otherSpawnHeightMin = -100;
            _despawnHeight = Holder.HEIGHT + 100;

            _player = new Player(new Vector2(Holder.WIDTH / 2, Holder.HEIGHT / 4 * 3));
            if (Holder.SETTINGS.LastWeaponType == 0)
                _weapon = new TheGun(new Vector2(Holder.WIDTH / 2, Holder.HEIGHT / 4 * 3));
            else if (Holder.SETTINGS.LastWeaponType == 1)
                _weapon = new TheSaw(new Vector2(Holder.WIDTH / 2, Holder.HEIGHT / 4 * 3));
            else if (Holder.SETTINGS.LastWeaponType == 2)
                _weapon = new TheRail(new Vector2(Holder.WIDTH / 2, Holder.HEIGHT / 4 * 3));
            else
                _weapon = new TheGun(new Vector2(Holder.WIDTH / 2, Holder.HEIGHT / 4 * 3));
            //_weapon = new TheDrill(new Vector2(Holder.WIDTH / 2, Holder.HEIGHT / 4 * 3));
            //_weapon = new TheGauss(new Vector2(Holder.WIDTH / 2, Holder.HEIGHT / 4 * 3));

            _damageScr = new Entity(new Vector2(Holder.WIDTH / 2, Holder.HEIGHT / 2), 0.0f, "other/dmgeye", 1);

            _enemyWall.Clear();
            _enemyRusher.Clear();
            _enemySpewer.Clear();
            _enviroments.Clear();
            _pickups.Clear();

            _lastTimeWall = TimeSpan.FromSeconds(0.0f);
            _lastTimeRusher = TimeSpan.FromSeconds(0.0f);
            _lastTimeSpewer = TimeSpan.FromSeconds(0.0f);
            _lastTimeEnviroment = TimeSpan.FromSeconds(0.0f);
            _lastTimePickup = TimeSpan.FromSeconds(0.0f);

            if (Holder.SETTINGS.LastDifficulty == 0)
            {
                Holder.SCORE_MULTIPLAYER = 0.4f;
                Holder.SCORE_TRAVEL = 0;
                _cooldawnWall = 25;
                _cooldawnRusher = 3;
                _cooldawnSpewer = 20;
                _cooldawnEnviroment = 15;
                _cooldawnPickup = 10;
            }
            else if (Holder.SETTINGS.LastDifficulty == 1)
            {
                Holder.SCORE_MULTIPLAYER = 1;
                Holder.SCORE_TRAVEL = 0;
                _cooldawnWall = 25;
                _cooldawnRusher = 2;
                _cooldawnSpewer = 15;
                _cooldawnEnviroment = 15;
                _cooldawnPickup = 10;
            }
            else if (Holder.SETTINGS.LastDifficulty == 2)
            {
                Holder.SCORE_MULTIPLAYER = 1.25f;
                Holder.SCORE_TRAVEL = 1000;
                _cooldawnWall = 20;
                _cooldawnRusher = 2;
                _cooldawnSpewer = 10;
                _cooldawnEnviroment = 10;
                _cooldawnPickup = 15;
            }
            else
            {
                Holder.SCORE_MULTIPLAYER = 1.5f;
                Holder.SCORE_TRAVEL = 2000;
                _cooldawnWall = 15;
                _cooldawnRusher = 1;
                _cooldawnSpewer = 10;
                _cooldawnEnviroment = 10;
                _cooldawnPickup = 20;
            }

            _previusScale = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (Holder.KSTATE.IsKeyDown(Keys.Escape))
                Holder.RUNWORLD = false;

            //Update / Input / Movement / Actions
            HandleUpdates(gameTime);

            //Despawn / Spawns
            HandleSpawnDespawn(gameTime);


            //Handle difficulity
            if (_previusScale != (int)(Holder.SCORE_TRAVEL / 500))
                DiffScaling();

            Holder.SCORE_TRAVEL++;
        }

        public void HandleDeath()
        {
            Holder.RUNWORLD = false;
            int finalScore = (int)((Holder.SCORE_TRAVEL + Holder.SCORE_PICKUPS + Holder.SCORE_DMG - Holder.SCORE_AMMOWASTE - Holder.SCORE_DMGPLAYER) * Holder.SCORE_MULTIPLAYER);
            PlayerRecord newRecord = new PlayerRecord(Holder.SETTINGS.LastSavedPilotName, finalScore);
            if (Holder.TOP_PLAYERS.Players.Count == 0 && finalScore > 0)
                Holder.TOP_PLAYERS.Players.Add(newRecord);
            else
            {
                bool inserted = false;
                for (int i = 0; i < Holder.TOP_PLAYERS.Players.Count; i++)
                {
                    if (finalScore > Holder.TOP_PLAYERS.Players[i].Score)
                    {
                        Holder.TOP_PLAYERS.Players.Insert(i, newRecord);
                        inserted = true;
                        break;
                    }
                }
                if (!inserted && Holder.TOP_PLAYERS.Players.Count < 20 && finalScore > 0)
                    Holder.TOP_PLAYERS.Players.Add(newRecord);
                if (Holder.TOP_PLAYERS.Players.Count > 20)
                    Holder.TOP_PLAYERS.Players.RemoveAt(Holder.TOP_PLAYERS.Players.Count - 1);
            }

            string gameFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SIPP");
            string settingsPath = Path.Combine(gameFolder, "top.json");
            string json = JsonSerializer.Serialize(Holder.TOP_PLAYERS, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(settingsPath, json);
        }

        public void Draw()
        {
            foreach (Environmental env in _enviroments)
                env.DrawEntity();
            _weapon.DrawAll();
            _player.DrawAll();


            foreach (Enemy enemy in _enemyWall)
                enemy.DrawAll();
            foreach (Enemy enemy in _enemyRusher)
                enemy.DrawAll();
            foreach (Enemy enemy in _enemySpewer)
                enemy.DrawAll();


            foreach (Pickup pickup in _pickups)
                pickup.DrawEntity();

            foreach (UltAbility ult in _ultAbility)
                ult.DrawEntity();

            if (_player.CollisionMark)
                _damageScr.DrawEntity();

            DrawHUD(_player, _weapon);
        }

        private void HandleUpdates(GameTime gameTime)
        {
            _player.Update();
            _weapon.Update(_player.AskToFire, _player.Position, gameTime);
            _weapon.ProjectileUpdate(_player.Position);

            foreach (Enemy enemy in _enemyWall)
                enemy.Update(_player, _weapon);
            foreach (Enemy enemy in _enemyRusher)
                enemy.Update(_player, _weapon);
            foreach (Enemy enemy in _enemySpewer)
                enemy.Update(_player, _weapon, gameTime);


            foreach (Pickup pickup in _pickups)
                pickup.Update(_player, _weapon);


            foreach (Environmental env in _enviroments)
                env.Update(_player, _weapon.Projetiles);

            foreach (UltAbility ult in _ultAbility)
                ult.Update(new List<List<Enemy>> { _enemyWall, _enemyRusher, _enemySpewer });
        }

        private void HandleSpawnDespawn(GameTime gameTime)
        {
            //_player
            if (_player.Health <= 0)
                HandleDeath();
            //_weapon
            for (int i = 0; i < _weapon.Projetiles.Count; i++)
            {
                if (_weapon.Projetiles[i].Position.Y < 0 || (_weapon.Projetiles[i].CollisionMark && !_weapon.Penetration) || _weapon.Projetiles[i].CollisionHardMark)
                {
                    if (!_weapon.Projetiles[i].CollisionMark)
                        Holder.SCORE_AMMOWASTE += _weapon.AmmoScoreCost;
                    _weapon.Projetiles.RemoveAt(i);
                    i--;
                }
            }

            //_enemyWall
            for (int i = 0; i < _enemyWall.Count; i++)
            {
                if (_enemyWall[i].Position.Y > _despawnHeight || _enemyWall[i].Health <= 0)
                {
                    _enemyWall.RemoveAt(i);
                    i--;
                }
            }
            if (gameTime.TotalGameTime - _lastTimeWall >= TimeSpan.FromSeconds(_cooldawnWall))
            {
                _lastTimeWall = gameTime.TotalGameTime;
                _enemyWall.Add(new TheWall(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(_enemySawnHeightMax, _enemySpawnHeightMin))));
            }

            //_enemyRusher
            for (int i = 0; i < _enemyRusher.Count; i++)
            {
                if (_enemyRusher[i].Position.Y > _despawnHeight || _enemyRusher[i].Health <= 0)
                {
                    _enemyRusher.RemoveAt(i);
                    i--;
                }
            }
            if (gameTime.TotalGameTime - _lastTimeRusher >= TimeSpan.FromSeconds(_cooldawnRusher))
            {
                _lastTimeRusher = gameTime.TotalGameTime;
                _enemyRusher.Add(new TheRusher(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(_enemySawnHeightMax, _enemySpawnHeightMin))));
            }

            //_enemySpewer
            for (int i = 0; i < _enemySpewer.Count; i++)
            {
                if (_enemySpewer[i].Position.Y > _despawnHeight || _enemySpewer[i].Health <= 0)
                {
                    _enemySpewer.RemoveAt(i);
                    i--;
                }
            }
            if (gameTime.TotalGameTime - _lastTimeSpewer >= TimeSpan.FromSeconds(_cooldawnSpewer))
            {
                _lastTimeSpewer = gameTime.TotalGameTime;
                _enemySpewer.Add(new TheSpewer(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(_enemySawnHeightMax, _enemySpawnHeightMin))));
            }

            //_pickups
            for (int i = 0; i < _pickups.Count; i++)
            {
                if (_pickups[i].Position.Y > _despawnHeight || _pickups[i].CollisionMark)
                {
                    _pickups.RemoveAt(i);
                    i--;
                }
            }
            if (gameTime.TotalGameTime - _lastTimePickup >= TimeSpan.FromSeconds(_cooldawnPickup))
            {
                _lastTimePickup = gameTime.TotalGameTime;
                int type = Holder.RANDOM.Next(0, 3);
                if (type == 0)
                    _pickups.Add(new MedPack(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(_otherSawnHeightMax, _otherSpawnHeightMin))));
                if (type == 1)
                    _pickups.Add(new EnergyPack(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(_otherSawnHeightMax, _otherSpawnHeightMin))));
                if (type == 2)
                    _pickups.Add(new UltPack(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(_otherSawnHeightMax, _otherSpawnHeightMin))));
            }

            //_enviroments
            for (int i = 0; i < _enviroments.Count; i++)
            {
                if (_enviroments[i].Position.Y > _despawnHeight || (_enviroments[i].CollisionMark && _enviroments[i].DespawnOnHit))
                {
                    _enviroments.RemoveAt(i);
                    i--;
                }
            }
            if (gameTime.TotalGameTime - _lastTimeEnviroment >= TimeSpan.FromSeconds(_cooldawnEnviroment))
            {
                _lastTimeEnviroment = gameTime.TotalGameTime;
                int type = Holder.RANDOM.Next(0, 3);
                if (type == 0)
                    _enviroments.Add(new TrapMed(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(_otherSawnHeightMax, _otherSpawnHeightMin))));
                if (type == 1)
                    _enviroments.Add(new BigRock(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(_otherSawnHeightMax, _otherSpawnHeightMin))));
                if (type == 2)
                    _enviroments.Add(new AcidMine(new Vector2(Holder.RANDOM.Next(0, Holder.WIDTH), Holder.RANDOM.Next(_otherSawnHeightMax, _otherSpawnHeightMin))));
            }

            //_ultAbility
            for (int i = 0; i < _ultAbility.Count; i++)
            {
                if (_ultAbility[i].Position.Y > _despawnHeight || _ultAbility[i].Done)
                {
                    _ultAbility.RemoveAt(i);
                    i--;
                }
            }
            if (_player.UltAbility && Holder.KSTATE.IsKeyDown(Keys.LeftControl))
            {
                _ultAbility.Add(new UltAbility());
                _player.UltAbility = false;
            }
        }

        public void DiffScaling()
        {
            _previusScale = (int)(Holder.SCORE_TRAVEL / 500);
            _cooldawnWall *= 1 - 0.01f * _previusScale;
            _cooldawnRusher *= 1 - 0.01f * _previusScale;
            _cooldawnSpewer *= 1 - 0.01f * _previusScale;
        }

        public void DrawHUD(Player player, Weapon weapon)
        {
            Holder.SPRITE_BATCH.DrawString(_hudFont, $"HEALTH: {player.Health}%", new Vector2(20, 20), Color.White);
            Holder.SPRITE_BATCH.DrawString(_hudFont, $"SHIELDS: {player.Shields}%", new Vector2(360, 20), Color.White);
            Holder.SPRITE_BATCH.DrawString(_hudFont, $"AMMO {weapon.Ammunition}|{weapon.MaxAmmunition}", new Vector2(720, 20), Color.White);
            if (_player.UltAbility)
                Holder.SPRITE_BATCH.DrawString(_hudFontAux, $"POWER WEAPON READY", new Vector2(380, 50), Color.LightSkyBlue);
            if (!_weapon.Loaded && _weapon.Ammunition > 0)
                Holder.SPRITE_BATCH.DrawString(_hudFontAux, $"LOADING...", new Vector2(720, 50), Color.IndianRed);
            Holder.SPRITE_BATCH.DrawString(_hudFont, $"Travel: {Holder.SCORE_TRAVEL}", new Vector2(400, 850), Color.White);

            Holder.SPRITE_BATCH.DrawString(_hudFontAux, $"Bonuses:", new Vector2(20, 782), Color.IndianRed);
            Holder.SPRITE_BATCH.DrawString(_hudFontAux, $"+DMG: {Holder.SCORE_DMG}", new Vector2(20, 800), Color.IndianRed);
            Holder.SPRITE_BATCH.DrawString(_hudFontAux, $"-DMG: {Holder.SCORE_DMGPLAYER}", new Vector2(20, 818), Color.IndianRed);
            Holder.SPRITE_BATCH.DrawString(_hudFontAux, $"AMEF: {Holder.SCORE_AMMOWASTE}", new Vector2(20, 836), Color.IndianRed);
            Holder.SPRITE_BATCH.DrawString(_hudFontAux, $"PICK: {Holder.SCORE_PICKUPS}", new Vector2(20, 854), Color.IndianRed);
        }
    }
}
