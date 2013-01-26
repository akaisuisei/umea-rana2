using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Umea_rana
{
     public class objet
    {
         public Rectangle rectangle_C, rectangle;
         protected int decallageX, decallageY, largeurX, hauteurY;
         public bool tombe;

         public void Update_rec_collision()
         {
             rectangle_C.X = rectangle.X + decallageX;
             rectangle_C.Y = rectangle.Y + decallageY;
             rectangle_C.Height = hauteurY;
             rectangle_C.Width = largeurX;
         }
    }
}
