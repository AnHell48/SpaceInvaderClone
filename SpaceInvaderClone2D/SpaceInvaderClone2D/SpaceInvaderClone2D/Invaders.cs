using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace SpaceInvaderClone2D
{
    class Invaders
    {
        Score Score;

        ContentManager contentInv;
        public List<Sprite> invaderList;
        Vector2 spriteOriginPosition;
        public Vector2 invaderPos;
        float timerMove,timerDissapear, timerRareInv;
        int counterX, changeImage,score;
        bool moveRight,moveDown,shoot;
        

        List<Bullets> bulletInvaderList;
        Random random;

        public Invaders() 
        {
            Score = new Score();
            invaderList = new List<Sprite>();
            spriteOriginPosition = new Vector2(170, 120);
            counterX = 0;
            moveRight = true;
            bulletInvaderList = new List<Bullets>();
            changeImage = 0;
            timerDissapear = 1f;
            timerRareInv = 3.0f;
        }

        public void LoadInvaders(ContentManager content)
        {
            contentInv = content;

            Score.LoadFonts(content);

            for (int columnY = 4; columnY >= 0; columnY--)
            {
                // sum on the column position everytime it finish a row
                spriteOriginPosition.Y += 40;
                // set row back to original to start from corner again
                spriteOriginPosition.X = 200;

                for (int rowX = 8; rowX >= 0; rowX--)
                {
                    Sprite newInvader = new Sprite();
                    invaderList.Add(newInvader);
                    // load different sprites depending in which row it is
                    if (columnY == 4)
                    {
                        newInvader.Load(content, "Invaders/inv_c"+changeImage);
                    }
                    if (columnY <= 3)
                    {
                        newInvader.Load(content, "Invaders/inv_b" + changeImage);
                    }
                    if (columnY == 0)
                    {
                        newInvader.Load(content, "Invaders/inv_a" + changeImage);
                    }
                    newInvader.spritePosition = spriteOriginPosition ;
                    // sum on X for separation between sprites
                    spriteOriginPosition.X += 30;
                }
            }
        }


        private void LoadRareInvader()
        {
            if(timerRareInv <= 0)
            {
                Sprite rareInvader = new Sprite();
                rareInvader.Load(contentInv, "Invaders/inv_rare");
                rareInvader.spritePosition = new Vector2(650, 120);
                invaderList.Add(rareInvader);
                timerRareInv = 30.0f;
            }
        }

        private void MoveRareInv()
        {
           // invaderList.ElementAt(invaderList.Count - 1).spritePosition.X -= 2.5f;
            foreach (Sprite inv in invaderList)
            {
                if (inv.spriteTexture.Width == 50) //with width we know is he rare one...
                {
                    inv.spritePosition.X -= 2.5f;
                    if (inv.spritePosition.X <= 0) // if outside of screen
                    {
                        invaderList.Remove(inv);
                        break;
                    }
                }
                
            }
        }

        private void MoveInvaders()
        {
            if (counterX == 6) { moveRight = false; moveDown = true; }
            if (counterX == -6) { moveRight = true; moveDown = true; }

            if (timerMove < 0)
            {
                // -------------------------MOVE LEFT/RIGHT -------------------------------
                if (moveRight)
                {
                    if (changeImage == 0)
                        changeImage = 1;
                    else
                        changeImage = 0;

                    foreach (Sprite inv in invaderList)
                    {
                        // go right ----->
                        inv.spritePosition.X += 5;
                        //if(inv.spriteTexture.Tag == "inv_a"+changeImage)
                        //if (changeImage == 0)
                        //{
                        //    inv.Load(contentInv, "Invaders/inv_a" + changeImage);
                        //    inv.Load(contentInv, "Invaders/inv_b" + changeImage);
                        //    inv.Load(contentInv, "Invaders/inv_c" + changeImage);
                        //}
                        //else
                        //{
                        //    inv.Load(contentInv, "Invaders/inv_a" + changeImage);
                        //    inv.Load(contentInv, "Invaders/inv_b" + changeImage);
                        //    inv.Load(contentInv, "Invaders/inv_c" + changeImage); 
                        //}
                    }
                    counterX++;
                    
                }
                else 
                {
                    foreach (Sprite inv in invaderList)
                    {
                        // <----- go left
                        inv.spritePosition.X -= 5; 
                    }
                    counterX--;
                }

                // ----------------------- MOVE DOWN ----------------------------------------

                if (moveDown)
                {
                    foreach (Sprite inv in invaderList)
                    {
                        inv.spritePosition.Y += 5; 
                    }
                    moveDown = false;
                }

                timerMove = 2.0f; 
            }
        }

        public bool Shoot()
        {
            random = new Random();
            //select a random numbet between 0 and any number this time 100, the bigger the less the chances of
            // been choose
            int ran = random.Next(0,100);
            // if ran is 24 then return true which means go ahead andfire
            if (ran == 48)
                return true;
            else
                return false;
        }


        public void CollitionPlayerBulletINvader(Sprite invaderHit)
        { 
            invaderHit.spriteTexture = contentInv.Load<Texture2D>("Invaders/inv_destroy");
            // need to add a timer or somethign so after certain time it delete the sprite so it show the ^ sprite

            // problem here
            if (invaderHit.spriteTexture.Width == 50) // is the rare ?
            {
                //special score
                score += 100;
                
                // play weird sound 
                //idk
            }
            else
            {
                // normal score for all other invaders
                score += 10;
            }
            invaderList.Remove(invaderHit);
            timerDissapear = 5f;
        }

        public void InvadersUpdate(GameTime gameTime)
        {
            timerMove -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            timerDissapear -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            timerRareInv -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            Debug.WriteLine(timerRareInv);
            LoadRareInvader();
            MoveRareInv();
            MoveInvaders(); 

            if (Shoot())
            {
                int pos; 
                // select a random invader which will be the one shooting
                pos = random.Next(0,invaderList.Count); 
                invaderPos = invaderList[pos].spritePosition;
            }

        }

        public void DrawInvaders(SpriteBatch spriteBatch)
        {
            foreach (Sprite inv in invaderList)
            {
                inv.Draw(spriteBatch);
            }
            Score.DrawFonts(spriteBatch, score.ToString());

        }

    }
}
