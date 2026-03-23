using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public class FloorTile : MapTile
    {
        public FloorTile()
        {
            Mark = Constants.FloorMark;
        }
    }
}
