using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX;

namespace Monogame._2
{
    public class Boss
    {
        private Texture2D texture;
        private Microsoft.Xna.Framework.Vector2 position;
        private Microsoft.Xna.Framework.Rectangle hitbox;
        private float speed;
        private float velocityX = 3f;
        private List<Bullet> bullets = new List<Bullet>();
        public Microsoft.Xna.Framework.Rectangle Hitbox{
            get{return hitbox;}
        }
        public List<Bullet> Bullets{
            get{return bullets;}
        }

        public Boss(Texture2D texture, Microsoft.Xna.Framework.Vector2 position){
            this.texture = texture;   
            this.position = position;
            int size = 120;
            speed = 15f;
            hitbox = new ((int) position.X, (int) position.Y, size, size);
        }

        public void Update(){
            position.Y += speed*1f/60f;
            position.X += velocityX;
            if(position.X <= 0 || position.X>=1801){
                velocityX *= -1.2f;
            }
            Shoot();

            foreach(Bullet b in bullets){
                b.Update(); 
            }

            hitbox.Location = position.ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Microsoft.Xna.Framework.Color.DarkViolet);
            foreach(Bullet b in bullets){
                b.Draw(spriteBatch);
            }
        }
        private void Shoot(){
            Random rng = new Random();
            int chance = rng.Next(0,50);
            if(5>chance){
                Bullet bullet = new Bullet(texture, position, new Microsoft.Xna.Framework.Vector2(0, 1));
                bullets.Add(bullet);
            }
        }
    }
}
