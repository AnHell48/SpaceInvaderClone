using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SpaceInvaderClone2D
{
    class Sprite
    {
        public Texture2D spriteTexture;
        public Vector2 spritePosition;
        public float spriteHealth; // float so when damage is .5 or 1.5 etc etc
        public Rectangle spriteRectangle
        {
            get
            {
                return new Rectangle((int)spritePosition.X, (int)spritePosition.Y, spriteTexture.Width, spriteTexture.Height);
            }
        }

        public Sprite() { }
        public Sprite(float SpriteHealth) { this.spriteHealth = SpriteHealth; }

        public void Load(ContentManager content, string file_name)
        {
            spriteTexture = content.Load<Texture2D>(file_name);
        } 

        public void Update(GameTime gametime)
        {

        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(spriteTexture, spriteRectangle, Color.White);
            //spritebatch.Draw(texture_, position_, Color.White);
            spritebatch.End();
        }
    }
}
