using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame._2
{
    public class Bullet
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle hitbox;
        private Vector2 direction;

        public Rectangle Hitbox{
            get{return hitbox;}
        }

        public Bullet(Texture2D texture, Vector2 spawnPosition, Vector2 direction){
            this.texture = texture;
            position = spawnPosition;
            this.direction = direction;
            hitbox = new Rectangle((int) position.X, (int) position.Y, 10, 10);
        }

        public void Update(){
            position.Y += direction.Y * 200 * 1f/60f;
  
            hitbox.Location = position.ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Color.LimeGreen);
        }
    }
}