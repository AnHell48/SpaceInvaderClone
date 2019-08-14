using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceInvaderClone2D
{
    /*----------------------------------
     *By:  
     *      Angel A. Robles
     *      4/may/2018
     * 
     *---------------------------------*/
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Bullets bullets;
        Invaders invaderS;
        Player P1;
        Covers coverS;
        Score score;
        ScreenManager Splash,PStartScreen,GameOverScreen;
        KeyboardState keybState;
        Texture2D layoutBckd;
        float splashTimer;
        bool loadStart;

        enum GameStates
        {
            SplashScreen,PressStart,MainMenu,GameStart,GameOver,Credits
        }

        GameStates currentState;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            Splash = new ScreenManager();
            PStartScreen = new ScreenManager();
            currentState = new GameStates();

            currentState = GameStates.SplashScreen; 

            base.Initialize();
        }        

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            if (currentState == GameStates.SplashScreen) { Splash.LoadScreen(Content, "backgrounds/splashscreen"); }

            if (currentState == GameStates.GameStart) { LoadNewGame(); }

        }

        private void LoadNewGame()
        {
            invaderS = new Invaders();
            P1 = new Player();
            coverS = new Covers();
            score = new Score();
            bullets = new Bullets(); 
            P1.LoadPlayer(Content);
            invaderS.LoadInvaders(Content);
            bullets.LoadBullets(Content);
            coverS.LoadCovers(Content);
            layoutBckd = Content.Load<Texture2D>("backgrounds/layout");
            loadStart = false;

        }

        #region Collitions

        protected void CollitionPlayerBulletsInvaders()
        {
           bool somethingGotDeleted = false;
            foreach (Sprite bullet in bullets.bulletShipList)
            {
                foreach (Sprite inv in invaderS.invaderList)
                {
                    if (bullet.spriteRectangle.Intersects(inv.spriteRectangle))
                    {
                        bullets.bulletShipList.Remove(bullet);
                        //invaderS.invaderList.Remove(inv);     
                        invaderS.CollitionPlayerBulletINvader(inv);                  
                        somethingGotDeleted = true;
                        break; // break out of invader loop so it can modify list 
                    }
                }
                // check if the any of the list were modify, if it did
                // then break the loop
                if (somethingGotDeleted)
                {
                    somethingGotDeleted = false;
                    break;// breakout of bullet loop so it can modify list
                }
            }
        }

        protected void CollitionInvaderBulletsCovers()
        {
            bool collitionHappened = false ;
            foreach (Sprite bullet in bullets.bulletInvList)
            {
                foreach (Sprite cover in coverS.coverList)
                {
                    if (bullet.spriteRectangle.Intersects(cover.spriteRectangle))
                    {
                        bullets.bulletInvList.Remove(bullet);
                        cover.spriteHealth--;
                        if (cover.spriteHealth == 0)
                        {
                            coverS.coverList.Remove(cover);
                        }
                        collitionHappened = true;
                        break; // break out of invader loop so it can modify list 
                    }
                }
                // check if the any of the list were modify, if it did
                // then break the loop
                if (collitionHappened)
                {
                    collitionHappened = false;
                    break;// breakout of bullet loop so it can modify list
                }
            }
        }

        private void CollitionInvaderbulletsPlayer()
        {
            foreach (Sprite inv in bullets.bulletInvList)
            {
                if (inv.spriteRectangle.Intersects(P1.playerRect))
                {
                    // player hit invader
                    bullets.bulletInvList.Remove(inv);
                    //invaders hit player
                    P1.CollitionInvaderbullets();
                    break;
                }
            }
        }

        private void CollitionInvaderBulletsPlayerBullet()
        {
            // when bullets crash with eachother
            // destroy eachother
        }

        private void CollitionInvaderPlayer()
        {
            // for if invader keep going down and touch player
            // gameover
        }

        private void CollitionInvaderCovers()
        {
            // if the invader touch the covers IF they r still there
        }

        private void CollitionPlayerBulletsCover()
        {
            // player bullets hit the cover
            // get deleted
        }

        #endregion

        #region GAMEOVER
        private bool IsGameOver()
        {
            if (invaderS.invaderList.Count <= 0 || P1.playerHealth == 0)
                return true;
            else
                return false;
        }
        #endregion

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            keybState = Keyboard.GetState();
            splashTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (currentState == GameStates.SplashScreen)
            { 
                SplashScreenManager();
                Splash.UpdateScreen(gameTime,true); 
            }

            if (currentState == GameStates.PressStart) 
            {
                PressStartScreenManager(); 
                PStartScreen.UpdateScreen(gameTime, false);
            }

            if (currentState == GameStates.GameStart)
            {
                if (loadStart)
                    LoadNewGame();
                if (!IsGameOver())
                {
                    GameStartManager(gameTime);
                }
                else
                {
                    currentState = GameStates.GameOver;
                }
            }
            if (currentState == GameStates.GameOver)
            {

            }


            base.Update(gameTime);
        }

        private void SplashScreenManager()
        {
            if (splashTimer >= 4f)
            {
                currentState = GameStates.PressStart;
            }
        }

        private void PressStartScreenManager()
        {
            PStartScreen.LoadScreen(Content, "backgrounds/pressstart");
            PStartScreen.BackgroundMusic();
            if (keybState.IsKeyDown(Keys.Enter) || keybState.IsKeyDown(Keys.Space))
            {
                currentState = GameStates.GameStart; 
                loadStart = true;
            }
            //PStartScreen.UpdateScreen(gameTime, false);
            
        }

        private void GameStartManager(GameTime gameTime)
        {
            if (P1.Fire())
            {
                bullets.CreateNewPlayerBullet(P1.playerPos, 0.4f);
            }

            if (invaderS.Shoot())
            {
                bullets.CreateNewInvaderBullet(invaderS.invaderPos, 2f);
            }

            P1.UpdatePlayer(gameTime);
            invaderS.InvadersUpdate(gameTime);
            bullets.UpdateBullets(gameTime);

            CollitionPlayerBulletsInvaders();
            CollitionInvaderBulletsCovers();
            CollitionInvaderbulletsPlayer();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            if (currentState == GameStates.SplashScreen)
            {
                Splash.DrawScreen(spriteBatch,new Vector2(150, 250));
            }

            if (currentState == GameStates.PressStart)
            { PStartScreen.DrawScreen(spriteBatch, Vector2.Zero); }

            if (currentState == GameStates.GameStart)
            {
                invaderS.DrawInvaders(spriteBatch);
                P1.DrawPlayer(spriteBatch);
                bullets.DrawBullets(spriteBatch);
                coverS.DrawCover(spriteBatch);

                spriteBatch.Begin();
                spriteBatch.Draw(layoutBckd, new Vector2(0, 0), Color.White);
                spriteBatch.End();
            }
            if (currentState == GameStates.GameOver)
            {
                GameOverScreen = new ScreenManager();
                GameOverScreen.LoadScreen(Content, "backgrounds/Gover");
                GameOverScreen.DrawScreen(spriteBatch, Vector2.Zero);
            }

            base.Draw(gameTime);
        }
    }
}
