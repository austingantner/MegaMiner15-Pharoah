using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Pharaoh
{
    enum MissionType
    {
        goTo
    }
    class Mission
    {
        public Thief thief;
        public MissionType type;
        public List<Point> targets;

        public Mission(Thief theThief, MissionType mType, List<Point> pts)
        {
            thief = theThief;
            type=mType;
            targets = pts;
        }
    }
}
