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
using System.Threading;

namespace SpaceInvaderPlusPlus
{
    internal class MainWorld
    {
        private Player _player;
        private Weapon _weapon;
        private List<Particles> _particles;
        private List<EnemyProjectile> _enemyProjectiles;
        private List<Enemy> _enemyWall;
        private List<Enemy> _enemyRusher;
        private List<Enemy> _enemySpewer;
        private List<Pickup> _pickups;
        private List<Environmental> _enviroments;
        private List<UltAbility> _ultAbility;
        private Entity _damageScr;
        private Hud _hud;
        private TimeSpan _lastTimeWall;
        private TimeSpan _lastTimeRusher;
        private TimeSpan _lastTimeSpewer;
        private TimeSpan _lastTimePickup;
        private TimeSpan _lastTimeEnviroment;
        private int _previusScale;
        private float _scaling;
        private float _cooldawnWall;
        private float _cooldawnRusher;
        private float _cooldawnSpewer;
        private float _cooldawnPickup;
        private float _cooldawnEnviroment;
        private int _enemySawnHeightMax;
        private int _enemySpawnHeightMin;
        private int _otherSawnHeightMax;
        private int _otherSpawnHeightMin;
        private int _despawnHeight;


        public MainWorld(ref General general)
        {
            _particles = new List<Particles>();
            _enemyProjectiles = new List<EnemyProjectile>();
            _enemyWall = new List<Enemy>();
            _enemyRusher = new List<Enemy>();
            _enemySpewer = new List<Enemy>();
            _pickups = new List<Pickup>();
            _enviroments = new List<Environmental>();
            _ultAbility = new List<UltAbility>();
            _hud = new Hud(ref general);
        }

        public void CleanUp()
        {
            _particles.Clear();
            _enemyWall.Clear();
            _enemyRusher.Clear();
            _enemySpewer.Clear();
            _enviroments.Clear();
            _pickups.Clear();
            _ultAbility.Clear();
        }

        public void RanNew(ref General general)
        {
            //CleanUp();
            general.SCORE_TRAVEL = 0;
            general.SCORE_DMG = 0;
            general.SCORE_DMGPLAYER = 0;
            general.SCORE_PICKUPS = 0;
            general.SCORE_AMMOWASTE = 0;

            _enemySawnHeightMax = -1100;
            _enemySpawnHeightMin = -100;
            _otherSawnHeightMax = -200;
            _otherSpawnHeightMin = -100;
            _despawnHeight = general.HEIGHT + 100;

//            _progressEnt.Clear();
            _enemyProjectiles.Clear();

            Vector2 spawnVector = new Vector2(general.WIDTH / 2, general.HEIGHT / 4 * 3);
            _player = new Player(ref general, ref spawnVector);
            if (general.SETTINGS.LastWeaponType == 0)
                _weapon = new TheGun(ref general, ref spawnVector);
            else if (general.SETTINGS.LastWeaponType == 1)
                _weapon = new TheSaw(ref general, ref spawnVector);
            else if (general.SETTINGS.LastWeaponType == 2)
                _weapon = new TheRail(ref general, ref spawnVector);
            else
                _weapon = new TheGun(ref general, ref spawnVector);

            _damageScr = new Entity(ref general, new Vector2(general.WIDTH / 2, general.HEIGHT / 2), 0.0f, general.ASSETLIBRARY.tOther_dmgeye, 1);

            _lastTimeWall = TimeSpan.FromSeconds(0.0f);
            _lastTimeRusher = TimeSpan.FromSeconds(0.0f);
            _lastTimeSpewer = TimeSpan.FromSeconds(0.0f);
            _lastTimeEnviroment = TimeSpan.FromSeconds(0.0f);
            _lastTimePickup = TimeSpan.FromSeconds(0.0f);

            if (general.SETTINGS.LastDifficulty == 0)
            {
                general.SCORE_MULTIPLAYER = 0.4f;
                general.SCORE_TRAVEL = 0;
                _scaling = 0.99f;
                _cooldawnWall = 25;
                _cooldawnRusher = 3;
                _cooldawnSpewer = 20;
                _cooldawnEnviroment = 15;
                _cooldawnPickup = 10;
            }
            else if (general.SETTINGS.LastDifficulty == 1)
            {
                general.SCORE_MULTIPLAYER = 1;
                general.SCORE_TRAVEL = 0;
                _scaling = 0.98f;
                _cooldawnWall = 25;
                _cooldawnRusher = 2;
                _cooldawnSpewer = 15;
                _cooldawnEnviroment = 10;
                _cooldawnPickup = 10;
            }
            else if (general.SETTINGS.LastDifficulty == 2)
            {
                general.SCORE_MULTIPLAYER = 1.4f;
                general.SCORE_TRAVEL = 1000;
                _scaling = 0.97f;
                _cooldawnWall = 20;
                _cooldawnRusher = 2;
                _cooldawnSpewer = 10;
                _cooldawnEnviroment = 10;
                _cooldawnPickup = 15;
            }
            else
            {
                general.SCORE_MULTIPLAYER = 1.6f;
                general.SCORE_TRAVEL = 2000;
                _scaling = 0.96f;
                _cooldawnWall = 15;
                _cooldawnRusher = 1;
                _cooldawnSpewer = 10;
                _cooldawnEnviroment = 7;
                _cooldawnPickup = 20;
            }

            _previusScale = 0;
        }

        public void Update(ref GameTime gameTime, ref General general)
        {
            if (general.KSTATE.IsKeyDown(Keys.Escape))
                general.GAMESTATE = 3;

            //Update / Input / Movement / Actions
            HandleUpdates(ref general, ref gameTime);

            //Despawn / Spawns
            HandleSpawnDespawn(ref gameTime, ref general);


            //Handle difficulity
            if (_previusScale != (int)(general.SCORE_TRAVEL / 500))
                DiffScaling(ref general);

            general.SCORE_TRAVEL++;
        }

        private void HandleDeath(ref General general)
        {
            general.GAMESTATE = 3;

            int finalScore = (int)((general.SCORE_TRAVEL + general.SCORE_PICKUPS + general.SCORE_DMG - general.SCORE_AMMOWASTE - general.SCORE_DMGPLAYER) * general.SCORE_MULTIPLAYER);
            PlayerRecord newRecord = new PlayerRecord(general.SETTINGS.LastSavedPilotName, finalScore);
            if (general.TOP_PLAYERS.Players.Count == 0 && finalScore > 0)
                general.TOP_PLAYERS.Players.Add(newRecord);
            else
            {
                bool inserted = false;
                for (int i = 0; i < general.TOP_PLAYERS.Players.Count; i++)
                {
                    if (finalScore > general.TOP_PLAYERS.Players[i].Score)
                    {
                        general.TOP_PLAYERS.Players.Insert(i, newRecord);
                        inserted = true;
                        break;
                    }
                }
                if (!inserted && general.TOP_PLAYERS.Players.Count < 20 && finalScore > 0)
                    general.TOP_PLAYERS.Players.Add(newRecord);
                if (general.TOP_PLAYERS.Players.Count > 20)
                    general.TOP_PLAYERS.Players.RemoveAt(general.TOP_PLAYERS.Players.Count - 1);
            }

            string gameFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SIPP");
            string settingsPath = Path.Combine(gameFolder, "top.json");
            string json = JsonSerializer.Serialize(general.TOP_PLAYERS, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(settingsPath, json);
        }

        public void Draw(ref General general)
        {
            General generalLoc = general;

//            foreach (var entity in _progressEnt)
//                entity.DrawEntity(ref general);

            foreach (Environmental env in _enviroments)
                env.EnvMain.DrawEntity(ref general);
            _weapon.DrawAll(ref general);
            _player.DrawAll(ref general);

            foreach (Particles part in _particles)
                part.DrawAll(ref general);

            foreach (EnemyProjectile enemyPro in _enemyProjectiles)
                enemyPro.ProMain.DrawEntity(ref general);

            foreach (Enemy enemy in _enemyWall)
                enemy.DrawAll(ref general);
            foreach (Enemy enemy in _enemyRusher)
                enemy.DrawAll(ref general);
            foreach (Enemy enemy in _enemySpewer)
                enemy.DrawAll(ref general);


            foreach (Pickup pickup in _pickups)
                pickup.PicMain.DrawEntity(ref general);

            foreach (UltAbility ult in _ultAbility)
                ult.UltMain.DrawEntity(ref general);

            if (_player.PlMain.CollisionMark)
                _damageScr.DrawEntity(ref general);

            _hud.DrawHUD(ref general, ref _player, ref _weapon);
        }

        private void HandleUpdates(ref General general, ref GameTime gameTime)
        {
            _player.Update(ref general);
            _weapon.Update(ref general, _player.AskToFire, _player.PlMain.Position, gameTime);
            _weapon.ProjectileUpdate(_player.PlMain.Position);

            foreach (Enemy enemy in _enemyWall)
                enemy.Update(ref general, ref _player, ref _weapon);
            foreach (Enemy enemy in _enemyRusher)
                enemy.Update(ref general, ref _player, ref _weapon);
            foreach (Enemy enemy in _enemySpewer)
                enemy.Update(ref general, ref _player, ref _weapon, gameTime);

            foreach (Pickup pickup in _pickups)
                pickup.Update(ref general, ref _player, ref _weapon);

            foreach (Environmental env in _enviroments)
                env.Update(ref general, ref _player, ref _weapon);

            foreach (Particles part in _particles)
                part.Update(gameTime);

            foreach (EnemyProjectile enemyPro in _enemyProjectiles)
                enemyPro.Update(ref general, ref _player);

            foreach (UltAbility ult in _ultAbility)
                ult.Update(new List<List<Enemy>> { _enemyWall, _enemyRusher, _enemySpewer });
        }

        private void HandleSpawnDespawn(ref GameTime gameTime, ref General general)
        {
            //_player
            if (_player.Health <= 0)
                HandleDeath(ref general);

            //_weapon
            for (int i = 0; i < _weapon.Projetiles.Count; i++)
            {
                if (_weapon.Projetiles[i].Position.Y < 0 || _weapon.Projetiles[i].CollisionMark)
                {
                    if (!_weapon.Projetiles[i].CollisionMark)
                        general.SCORE_AMMOWASTE += _weapon.AmmoScoreCost;
                    _weapon.Projetiles.RemoveAt(i);
                    i--;
                }
            }

            //_enemyWall
            for (int i = 0; i < _enemyWall.Count; i++)
            {
                if (_enemyWall[i].EnMain.Position.Y > _despawnHeight || _enemyWall[i].Health <= 0)
                {
                    _particles.Add(new Particles(ref general, gameTime, _enemyWall[i].MaxHealth, _enemyWall[i].EnMain.Position, 
                        _enemyWall[i].EnMain.EntityTexture.Height / 2, _enemyWall[i].EnMain.Velocity, _enemyWall[i].ParticleSetId));
                    _enemyWall.RemoveAt(i);
                    i--;
                }
            }
            if (gameTime.TotalGameTime - _lastTimeWall >= TimeSpan.FromSeconds(_cooldawnWall))
            {
                _lastTimeWall = gameTime.TotalGameTime;
                _enemyWall.Add(new TheWall(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(_enemySawnHeightMax, _enemySpawnHeightMin))));
            }

            //_enemyRusher
            for (int i = 0; i < _enemyRusher.Count; i++)
            {
                if (_enemyRusher[i].EnMain.Position.Y > _despawnHeight || _enemyRusher[i].Health <= 0)
                {
                    _particles.Add(new Particles(ref general, gameTime, _enemyRusher[i].MaxHealth, _enemyRusher[i].EnMain.Position, 
                        _enemyRusher[i].EnMain.EntityTexture.Height / 2, _enemyRusher[i].EnMain.Velocity));
                    _enemyRusher.RemoveAt(i);
                    i--;
                }
            }
            if (gameTime.TotalGameTime - _lastTimeRusher >= TimeSpan.FromSeconds(_cooldawnRusher))
            {
                _lastTimeRusher = gameTime.TotalGameTime;
                _enemyRusher.Add(new TheRusher(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(_enemySawnHeightMax, _enemySpawnHeightMin))));
            }

            //_enemySpewer
            for (int i = 0; i < _enemySpewer.Count; i++)
            {
                if (_enemySpewer[i].AskTofire)
                    _enemyProjectiles.Add(new EnemyProjectile(ref general, ref _enemySpewer[i].EnMain.Position));
                if (_enemySpewer[i].EnMain.Position.Y > _despawnHeight || _enemySpewer[i].Health <= 0)
                {
                    _particles.Add(new Particles(ref general, gameTime, _enemySpewer[i].MaxHealth, _enemySpewer[i].EnMain.Position, 
                        _enemySpewer[i].EnMain.EntityTexture.Height / 2, _enemySpewer[i].EnMain.Velocity, _enemySpewer[i].ParticleSetId));
                    _enemySpewer.RemoveAt(i);
                    i--;
                }
            }
            if (gameTime.TotalGameTime - _lastTimeSpewer >= TimeSpan.FromSeconds(_cooldawnSpewer))
            {
                _lastTimeSpewer = gameTime.TotalGameTime;
                _enemySpewer.Add(new TheSpewer(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(_enemySawnHeightMax, _enemySpawnHeightMin))));
            }

            //_pickups
            for (int i = 0; i < _pickups.Count; i++)
            {
                if (_pickups[i].PicMain.Position.Y > _despawnHeight || _pickups[i].PicMain.CollisionMark)
                {
                    _pickups.RemoveAt(i);
                    i--;
                }
            }
            if (gameTime.TotalGameTime - _lastTimePickup >= TimeSpan.FromSeconds(_cooldawnPickup))
            {
                _lastTimePickup = gameTime.TotalGameTime;
                int type = general.RANDOM.Next(0, 10);
                if (type <= 3)
                    _pickups.Add(new MedPack(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(_otherSawnHeightMax, _otherSpawnHeightMin))));
                else if (type <= 7)
                    _pickups.Add(new EnergyPack(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(_otherSawnHeightMax, _otherSpawnHeightMin))));
                else if (type <= 9)
                    _pickups.Add(new UltPack(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(_otherSawnHeightMax, _otherSpawnHeightMin))));
            }

            //_enviroments
            for (int i = 0; i < _enviroments.Count; i++)
            {
                if (_enviroments[i].EnvMain.Position.Y > _despawnHeight || (_enviroments[i].EnvMain.CollisionMark && _enviroments[i].DespawnOnHit))
                {
                    if (_enviroments[i].EnvMain.CollisionMark && _enviroments[i].DespawnOnHit)
                        _particles.Add(new Particles(ref general, gameTime, 3, _enviroments[i].EnvMain.Position,
                            _enviroments[i].EnvMain.EntityTexture.Height / 2, _enviroments[i].EnvMain.Velocity, _enviroments[i].ParticleSetId));

                    _enviroments.RemoveAt(i);
                    i--;
                }
            }
            if (gameTime.TotalGameTime - _lastTimeEnviroment >= TimeSpan.FromSeconds(_cooldawnEnviroment))
            {
                _lastTimeEnviroment = gameTime.TotalGameTime;
                int type = general.RANDOM.Next(0, 10);
                if (type <= 2)
                    _enviroments.Add(new TrapMed(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(_otherSawnHeightMax, _otherSpawnHeightMin))));
                else if (type <= 6)
                    _enviroments.Add(new BigRock(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(_otherSawnHeightMax, _otherSpawnHeightMin))));
                else if (type <= 9)
                    _enviroments.Add(new AcidMine(ref general, new Vector2(general.RANDOM.Next(0, general.WIDTH), general.RANDOM.Next(_otherSawnHeightMax, _otherSpawnHeightMin))));
            }

            //_particles
            for (int i = 0; i < _particles.Count; i++)
            {
                if (_particles[i].EndAll)
                {
                    _particles.RemoveAt(i);
                    i--;
                }
            }

            //_enemyProjectiles
            for (int i = 0; i < _enemyProjectiles.Count; i++)
            {
                if (_enemyProjectiles[i].ProMain.Position.Y > _despawnHeight || _enemyProjectiles[i].ProMain.CollisionMark)
                {
                    _enemyProjectiles.RemoveAt(i);
                    i--;
                }
            }

            //_ultAbility
            for (int i = 0; i < _ultAbility.Count; i++)
            {
                if (_ultAbility[i].UltMain.Position.Y > _despawnHeight || _ultAbility[i].Done)
                {
                    _ultAbility.RemoveAt(i);
                    i--;
                }
            }
            if (_player.UltAbility && general.KSTATE.IsKeyDown(Keys.LeftControl))
            {
                _ultAbility.Add(new UltAbility(ref general));
                _player.UltAbility = false;
            }
        }

        private void DiffScaling(ref General general)
        {
            _previusScale = (int)(general.SCORE_TRAVEL / 500);
            _cooldawnWall *= _scaling;
            _cooldawnRusher *= _scaling;
            _cooldawnSpewer *= _scaling;
        }
    }
}
