using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaderPlusPlus.Enemies;
using SpaceInvaderPlusPlus.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaderPlusPlus
{
    public class MainWorld
    {

        private Player _player;
        private Weapon _weapon;
        private List<Enemy> _enemyRusher;

        public void LoadContent(int dif)
        {
            Holder.score = 0;
            Holder.font = Holder.content.Load<SpriteFont>("Score");

            _player = new Player(dif, new Vector2(Holder.width / 2, Holder.height / 4 * 3));
            _weapon = new TheGun(new Vector2(Holder.width / 2, Holder.height / 4 * 3));
            _enemyRusher = new List<Enemy>();
            while (_enemyRusher.Count < 3)
                _enemyRusher.Add(new TheRusher(new Vector2(Holder.random.Next(0, Holder.width), Holder.random.Next(-500, 0))));
        }

        public void Update(GameTime gameTime)
        {
            //Update

            foreach (Enemy enemy in _enemyRusher)
                enemy.Update(_player.Position, _player, _weapon.Projetiles, _weapon.Damage);
            _player.Update(Holder.kState);
            _weapon.Update(_player.AskToFire, _player.Position, gameTime);
            _weapon.ProjectileUpdate();


            //Despawn / Spawns
            for (int i = 0; i < _enemyRusher.Count; i++)
            {
                if (_enemyRusher[i].Position.Y > Holder.height || _enemyRusher[i].Health <= 0)
                    _enemyRusher.RemoveAt(i);
            }
            while (_enemyRusher.Count < 3)
                _enemyRusher.Add(new TheRusher(new Vector2(Holder.random.Next(0, Holder.width), Holder.random.Next(-500, 0))));
            for (int i = 0; i < _weapon.Projetiles.Count; i++)
            {
                if (_weapon.Projetiles[i].Position.Y < 0 || (_weapon.Projetiles[i].CollisionMark && !_weapon.Penetration))
                    _weapon.Projetiles.RemoveAt(i);
            }

            Holder.score++;
        }

        public void Draw()
        {
            Holder.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            _weapon.DrawAll();
            _player.DrawAll();
            foreach (Enemy enemy in _enemyRusher)
                enemy.DrawAll();

            Holder.spriteBatch.DrawString(Holder.font, $"SCORE: {Holder.score} HEALTH: {_player.Health} SHIELDS: {_player.Shields}", new Vector2(50, 50), Color.White);
            Holder.spriteBatch.End();
        }
    }
}
