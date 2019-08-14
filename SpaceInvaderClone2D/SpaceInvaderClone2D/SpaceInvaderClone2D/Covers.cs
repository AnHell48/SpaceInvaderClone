using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SpaceInvaderClone2D
{
    class Covers
    {
        Sprite cover;
        public List<Sprite> coverList;
        Vector2 coverOriginPos;
        float coverHealth;

        /*
         need to add
         * after certain hits from invader (change img to taken damage) if health 0 then destroy/dissapear
         * collition between bullets from invaders         
         */

        public Covers()
        {
            coverList = new List<Sprite>();
            coverOriginPos = new Vector2(180, 450);
            coverHealth = 2f;
        }

        public void COverInit()
        {
        }

        public void LoadCovers(ContentManager content)
        {
            for (int n = 0; n < 4; n++)
            {
                cover = new Sprite(coverHealth);
                cover.Load(content, "cover0");
                coverList.Add(cover);
                cover.spritePosition = coverOriginPos;
                coverOriginPos.X += 80;
            }
        }



        public void DrawCover(SpriteBatch spritebatch)
        {
            foreach (Sprite covers in coverList)
            {
                covers.Draw(spritebatch);
            }
        }

    }
}
