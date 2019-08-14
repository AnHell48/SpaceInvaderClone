using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace SpaceInvaderClone2D
{
    class ScreenManager
    { 
        /*
         * Manage Splash and press start screen
         */
        ContentManager contentS;
        Texture2D splashImg;
        Vector2 splashPosition;
        float timerSound;
        bool first; // first time calling the methos. First time loading all new so it don't keep repeating.
        SoundEffect soundEffect;
        SoundEffectInstance soundEffectInst;
 
        public ScreenManager() 
        {
            splashPosition = new Vector2(150, 250);
            first = true;
            timerSound = 1.5f;
        }

        ~ScreenManager()
        {
            //soundEffectInst.Stop();
        }

        public void LoadScreen(ContentManager content,string splashIMG)
        {
            contentS = content;
             splashImg = content.Load<Texture2D>(splashIMG);
            
        }


        private void BackgroundSound()
        {
            if(first)
                soundEffect = contentS.Load<SoundEffect>("Audio/coqui_short");
                soundEffectInst = soundEffect.CreateInstance();
            if (timerSound < 0)
            {
                soundEffectInst.Play();
                soundEffectInst.Volume = 0.8f;
                timerSound = 3f;
            } 
        }

        public void BackgroundMusic()
        {
            if (first)
            {
                soundEffect = contentS.Load<SoundEffect>("Audio/xfiles");
                soundEffectInst = soundEffect.CreateInstance();
            }
            soundEffectInst.Play();
            soundEffectInst.Volume = 0.5f;
        }

        public void UpdateScreen(GameTime gameTime,bool isSplashScreen)
        {
            if (isSplashScreen)
            {
                timerSound -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                BackgroundSound();
            }
            //if (!isSplashScreen)
            //{
            //    BackgroundMusic();
            //}
            first = false;
        }

        public void DrawScreen(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(splashImg, position, Color.White);
            spriteBatch.End();
        }
        
    }
}
