using System;
using Microsoft.Xna.Framework.Graphics;
using SharpDX;

namespace Monogame._2
{
    public class Enemy2
    {
        private Texture2D texture;
        private Microsoft.Xna.Framework.Vector2 position;
        private Microsoft.Xna.Framework.Rectangle hitbox;
        public Microsoft.Xna.Framework.Rectangle Hitbox{
            get{return hitbox;}
        }

        public Enemy2(Texture2D texture){
            this.texture = texture;
            Random rand = new Random();   
            position.X = rand.NextFloat(0, 1870);
            position.Y = 1200;
            hitbox = new ((int) position.X, (int) position.Y, 60, 60);
        }

        public void Update(){
            position.Y -= 15*1f/60f;

            hitbox.Location = position.ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Microsoft.Xna.Framework.Color.YellowGreen);
        }
    }
}