using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticInvader.Interfaces
{
    interface IAnimate
    {
        Vector2 dimension { get; set; }
        List<Rectangle> frames { get; set; }
        int frameIndex { get; set; }
        int delay { get; set; }
        int delayCounter { get; set; }

        void CreateFrames();
        void PlayFrames(int forward, int backward);
    }
}
