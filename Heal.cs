using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame._2
{
    public class Heal
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle hitbox;

        public Rectangle Hitbox{
            get{return hitbox;}
        }

        public Heal(Texture2D texture){
            this.texture = texture;
            Random rng = new Random();
            position.X = rng.Next(400, 1500);
            position.Y = rng.Next(350, 700);

            hitbox = new Rectangle((int) position.X, (int) position.Y, 30, 30);
        }

        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Color.White);
        }
    }
}