using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public enum Sound
    {
        Shoot = 0,
        EnemyDie = 1
    }

    public enum GameStateType
    {
        PAUSE,
        PLAY,
        STOP,
    }
    public enum PickUpType
    {
        Health,
        Armour
    }
}
