using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharaoh
{
    class Executor
    {
        public void execute(List<Mission> missions)
        {
             foreach(Mission m in missions)
             {
                 execute(m);
             }
        }

        public void execute(Mission mission)
        {
            if(mission.type == MissionType.goTo)
            {
                if (mission.thief.Alive == 1 && mission.thief.FrozenTurnsLeft == 0 && mission.thief.MovementLeft > 0)
                {
                    Point p = new Point(mission.thief.X, mission.thief.Y);
                    Queue<Point> path = findPath(mission.thief.position, mission.targets[0]);
                    // If a path exists then move forward on the path
                    int i = 0;
                    while (path.Count > 0 && mission.thief.MovementLeft > 0 && i++ < 25)
                    {
                        Point nextMove = path.Dequeue();
                        mission.thief.move(nextMove.x, nextMove.y);
                    }
                }
            }
        }

        // Returns the tile at the given x,y position or null otherwise
        Tile getTile(int x, int y)
        {
            if (x < 0 || x >= 50 || y < 0 || y >= 25)
            {
                return null;
            }
            return BaseAI.tiles[y + x *25];
        }

        //returns a path from start to end, or nothing if no path is found.
        public Queue<Point> findPath(Point start, Point end)
        {
            Stack<Point> reversedReturn = new Stack<Point>();
            Queue<Point> toReturn = new Queue<Point>();
            // The set of open tiles to look at
            Queue<Tile> openSet = new Queue<Tile>();
            // Points back to parent tile
            Dictionary<Tile, Tile> parent = new Dictionary<Tile, Tile>();
            // Push back the starting tile
            openSet.Enqueue(getTile(start.x, start.y));
            // The start tile has no parent
            parent[getTile(start.x, start.y)] = null;
            // The end tile
            Tile endTile = getTile(end.x, end.y);
            // As long as the end tile has no parent
            while (!parent.ContainsKey(endTile))
            {
                // If there are no tiles in the openSet then there is no path
                if (openSet.Count == 0)
                {
                    return toReturn;
                }
                // Check tiles from the front and remove
                Tile curTile = openSet.Dequeue();

                int[] xChange = new int[] { 0, 0, -1, 1 };
                int[] yChange = new int[] { -1, 1, 0, 0 };
                // Look in all directions
                for (int i = 0; i < 4; i++)
                {
                    Point loc = new Point(curTile.X + xChange[i], curTile.Y + yChange[i]);
                    Tile toAdd = getTile(loc.x, loc.y);
                    // If a tile exists there
                    if (toAdd != null)
                    {
                        // If it's an open tile and it doesn't have a parent
                        if (toAdd.Type == Tile.EMPTY && !parent.ContainsKey(toAdd))
                        {
                            // Add the tile to the open set; and mark its parent as the current tile
                            openSet.Enqueue(toAdd);
                            parent[toAdd] = curTile;
                        }
                    }
                }
            }
            // Find the path back
            for (Tile tile = endTile; parent[tile] != null; tile = parent[tile])
            {
                reversedReturn.Push(new Point(tile.X, tile.Y));
            }
            // Convert stack to a queue
            while (reversedReturn.Count > 0)
            {
                toReturn.Enqueue(reversedReturn.Pop());
            }
            return toReturn;
        }
    }
}
