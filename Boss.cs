using System;
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
        public Microsoft.Xna.Framework.Rectangle Hitbox{
            get{return hitbox;}
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

            hitbox.Location = position.ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Microsoft.Xna.Framework.Color.White);
        }
    }
}
