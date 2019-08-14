using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SpaceInvaderClone2D
{
    class Score
    {
        SpriteFont scoreFont;
        Vector2 scorePos;
        int score;

        public Score( )
        {
            scorePos = new Vector2(80,130);
        }

        public void LoadFonts(ContentManager content)
        {
            scoreFont = content.Load<SpriteFont>("Fonts");
        }

        public void DrawFonts(SpriteBatch spriteBatch,string textToDisplay)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(scoreFont,"SCORE: "+textToDisplay, scorePos, Color.LimeGreen);
            spriteBatch.End();
        }

    }
}
