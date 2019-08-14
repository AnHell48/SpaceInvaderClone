using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using System.Diagnostics;

namespace SpaceInvaderClone2D 
{
    class Bullets
    {
        /*
            Custom bullet class for invader game BUT with a few chances can be a general class for generating bullet lists
         */
        ContentManager Content;
        public List<Sprite> bulletShipList, bulletInvList; 
        float timeBetweenBullet = 1.5f;

        public Bullets()  
        {
            bulletShipList = new List<Sprite>();
            bulletInvList = new List<Sprite>();
        }

        public void LoadBullets(ContentManager content )
        {
            Content = content;
        }

        public void CreateNewPlayerBullet(Vector2 startingPosition, float timeBetweenBullets)
        {
            if (timeBetweenBullet > timeBetweenBullets)
            {
                Sprite Bullet = new Sprite();
                bulletShipList.Add(Bullet);
                Bullet.Load(Content, "bullet");
                Bullet.spritePosition = startingPosition;
                timeBetweenBullet = 0;
            } 
        }

        // 2 methods that do the same because on the collition part was mixing all bullets and putting them from the same list
        // i know the problem is creating a list here blabla ... 
        // a way to fix it is to pass a list ...?
        public void CreateNewInvaderBullet(Vector2 startingPosition, float timeBetweenBullets)
        {
            if (timeBetweenBullet > timeBetweenBullets)
            {
                Sprite Bullet = new Sprite();
                bulletInvList.Add(Bullet);
                Bullet.Load(Content, "bullet");
                Bullet.spritePosition = startingPosition ;
                timeBetweenBullet = 0;
            }
        }

        private void MoveBulletsUp()
        {                    
            foreach (Sprite bullet in bulletShipList)
            {
                bullet.spritePosition.Y -= 3f;
            }
        }

        private void MoveBulletsDown()
        {
            foreach (Sprite bullet in bulletInvList)
            {
                bullet.spritePosition.Y += 3f;
            }
        }

        public void UpdateBullets(GameTime gameTime)
        {
            timeBetweenBullet += (float)gameTime.ElapsedGameTime.TotalSeconds;
            MoveBulletsUp();
            MoveBulletsDown();
             
        }

        public void DrawBullets(SpriteBatch spritebatch)
        {
            foreach (Sprite bullet in bulletShipList)
            {
                bullet.Draw(spritebatch);
            }
            foreach (Sprite bullet in bulletInvList)
            {
                bullet.Draw(spritebatch);
            }

        }
    }
}
