using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Game.Characters
{
    interface IKillable
    {
        void TakeDamage();
        void Kill();
    }
}
