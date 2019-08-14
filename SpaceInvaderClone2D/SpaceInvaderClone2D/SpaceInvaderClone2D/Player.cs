using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace SpaceInvaderClone2D
{
    class Player
    {
        ContentManager contentP;
        Sprite Player1;
        Bullets Bullet;
        KeyboardState keyboardCurrentState, keybPrevState;

        //----------------------------
        Vector2 prev_position;
        public Rectangle playerRect { get { return Player1.spriteRectangle; } }
        public Vector2 playerPos { get { return Player1.spritePosition; } }
        public float playerHealth;
        bool fire;

        public Player()
        {
            playerHealth = 3;
            Player1 = new Sprite(playerHealth);
            Player1.spritePosition = new Vector2(200, 480);
            Bullet =  new Bullets(); 
        }

        public void LoadPlayer(ContentManager content)
        {
            contentP = content;
            Player1.Load(content, "ship");
        }

        private void Movement()
        {
            if (keyboardCurrentState.IsKeyDown(Keys.Right))
            {
                Player1.spritePosition.X += 4f;
            }
            if (keyboardCurrentState.IsKeyDown(Keys.Left))
            {
                Player1.spritePosition.X -= 4f;
            }

            fire = false;
            if (keyboardCurrentState.IsKeyDown(Keys.Space))
            {
                if(KeyPressed(Keys.Space))                
                    fire = true;
            }
        }

        protected bool KeyPressed(Keys key)
        {
            return keyboardCurrentState.IsKeyDown(key) && keybPrevState.IsKeyUp(key);
        }

        public bool Fire()
        { 
            return fire;
        }

        private void ScreenBoundaries()
        {
            if (Player1.spritePosition.X <= 50 || Player1.spritePosition.X >= 500)
            {
                Player1.spritePosition = prev_position;
            }
        }

        public void CollitionInvaderbullets()
        {
            playerHealth--;
            if (playerHealth == 0)
            {
                Player1.Load(contentP, "ship_destroy");
                Debug.Write("GAMEOVER!");
            }
        }

        public void UpdatePlayer(GameTime gameTime)
        {
            keyboardCurrentState = Keyboard.GetState();

            //playerPos = Player1.spritePosition;
            prev_position = Player1.spritePosition;
            //playerRect = Player1.spriteRectangle;

            Movement();

            keybPrevState = keyboardCurrentState;

            ScreenBoundaries(); 

        }

        public void DrawPlayer(SpriteBatch spriteBatch)
        {
            Player1.Draw(spriteBatch);
        }

    }
}
