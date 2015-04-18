using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharaoh
{
    class Target
    {
        public Target(int id)
        {
            playerID = id;
            enemyID = (playerID + 1) % 2;
        }
        int playerID;
        int enemyID;
        public List<Point> EnemySarcophagi;
        public List<Point> OurSarcophagi;

        public void init()
        {
            OurSarcophagi = getTrapType(playerID, TrapType.SARCOPHAGUS);
            EnemySarcophagi = getTrapType(enemyID, TrapType.SARCOPHAGUS);
        }


        public List<Point> getTrapType(int player, int trapType)
        {
            List<Point> pts = new List<Point>();
            foreach (Trap t in BaseAI.traps)
            {
                if (t.TrapType == trapType && t.Owner == player)
                {
                    pts.Add(new Point(t.X, t.Y));
                }
            }
            return pts;
        }
    }
}
