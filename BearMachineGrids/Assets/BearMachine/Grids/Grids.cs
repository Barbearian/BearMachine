using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearMachine
{
    public interface Grids
    {
        GridUnit ReadUnit(int x,int y);
        void WriteUnit(int x, int y, GridUnit unit);
    }

    public class MyGrids:Grids {
        public Dictionary<Vector2, GridUnit> dic = new Dictionary<Vector2, GridUnit>();

        public GridUnit ReadUnit(int x, int y)
        {
            Vector2 key = new Vector2(x, y);
            if (dic.ContainsKey(key))
            {
                return dic[key];

            }
            else
            {
                GridUnit unit = new GridUnit();
                dic[key] = unit;
                return unit;
            }
        }

        public void WriteUnit(int x, int y, GridUnit value)
        {
            dic[new Vector2(x, y)] = value;
        }
    }


    public class GridUnit {
        public MyNode value;
        public bool isUnknown;
    }

    public enum GridMoveSignal
    {
        Right,
        Left,
        Up,
        Down
    }


}