using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Pharaoh
{
    class TestAI
    {
        int playerID;
        Executor executor;// = new Executor();
        Target target;

        public TestAI(int id)
        {
            executor = new Executor();
            playerID = id;
            target = new Target(id);
            target.init();
        }

        public void runAI()
        {
            List<Mission> missions = new List<Mission>();

            foreach (Thief t in BaseAI.thieves)
            {
                if (t.Owner == playerID && t.ThiefType == ThiefType.SLAVE)
                {
                    //for(int i = 0; i < 2; i++)
                        missions.Add(new Mission(t, MissionType.goTo, target.EnemySarcophagi));
                }
            }
            foreach (Thief t in BaseAI.thieves)
            {
                if (t.Owner == playerID && t.ThiefType == ThiefType.BOMBER)
                {
                    //for (int i = 0; i < 3; i++)
                        missions.Add(new Mission(t, MissionType.goTo, target.EnemySarcophagi));
                }
            }
            foreach (Thief t in BaseAI.thieves)
            {
                if (t.Owner == playerID && t.ThiefType != ThiefType.BOMBER && t.ThiefType != ThiefType.SLAVE)
                {
                    //for (int i = 0; i < 5; i++)
                        missions.Add(new Mission(t, MissionType.goTo, target.EnemySarcophagi));
                }
            }
            executor.execute(missions);
        }
    }
}
