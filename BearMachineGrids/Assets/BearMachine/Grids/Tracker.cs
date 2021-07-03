using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearMachine
{
    public interface Tracker<V>
    {
        void MoveSignal(int signal);
        void Load(V value);
        void Write();
        void Read();
        V GetValue();
    }

    public class GridTracker : Tracker<GridUnit>
    {
        public int x;
        public int y;
        public GridUnit unit;
        public Grids tape;

        public GridUnit GetValue()
        {
            return unit;
        }

        public void Load(GridUnit value)
        {
            unit = value;
        }

        public void MoveSignal(int signal)
        {
            GridMoveSignal gridSignal = (GridMoveSignal)signal;
            switch (gridSignal) {
                case GridMoveSignal.Right:
                    x += 1;
                    break;

                case GridMoveSignal.Up:
                    y += 1;
                    break;

                case GridMoveSignal.Left:
                    x -= 1;
                    break;

                case GridMoveSignal.Down:
                    x -= 1;
                    break;

                default:
                    break;
            }
        }

        public void Read()
        {
            unit = tape.ReadUnit(x, y);
        }

        public void Write()
        {
            tape.WriteUnit(x, y, unit);
        }

        public Vector2 GetPosition() {
            return new Vector2(x,y);
        }

        
    }

    


}