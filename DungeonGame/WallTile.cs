using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public class WallTile : MapTile
    {
        public WallTile()
        {
            Mark = Constants.WallMark;
        }
    }
}
