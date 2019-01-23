using RecipeWPFUI.Models;
using System;
using System.Linq;

namespace RecipeWPFUI
{
    abstract class UnitSystemPair
    {
        public abstract Unit[][] Pair { get; }
        public UnitType Type { get; protected set; }

        public UnitSystemPair()
        {

        }

        public double ConvertFromTo(double amount, string fromUnit, string toUnit)
        {
            double newAmount = amount;
            Tuple<int, int> fromIndex = IndexOfUnitName(fromUnit);
            Tuple<int, int> toIndex = IndexOfUnitName(toUnit);

            int iNow = fromIndex.Item1;
            int jNow = fromIndex.Item2;

            if (fromIndex.Item1 != toIndex.Item1) //If they are of different unitsystem
            {
                newAmount /= Pair[fromIndex.Item1][fromIndex.Item2].SwitchUnits;
                iNow = 1 - iNow; //Update position in conversion table
                jNow = 0;
            }

            if (jNow < toIndex.Item2)
            {
                while (!Pair[iNow][jNow].UnitNames.Contains(toUnit))
                {
                    newAmount = newAmount / Pair[iNow][jNow].Next;
                    jNow++;
                }
            }

            if (toIndex.Item2 < jNow)
            {
                while (!Pair[iNow][jNow].UnitNames.Contains(toUnit))
                {
                    newAmount = newAmount / Pair[iNow][jNow].Previous;
                    jNow--;
                }
            }
            return newAmount;
        }

        public Tuple<int, int> IndexOfUnitName( string name)
        {
            for (int i = 0; i < Pair.Length; i++)
            {
                for (int j = 0; j < Pair[i].Length; j++)
                    if (Pair[i][j].UnitNames.Contains(name))
                    {
                        return Tuple.Create(i, j);
                    }
            }
            return Tuple.Create(-1, -1);
        }

        internal bool IsUnit(string s, out UnitType type)
        {
            type = UnitType.None;
            foreach (Unit[] unitArray in Pair)
            {
                foreach (Unit unit in unitArray)
                {
                    if (unit.UnitNames.Contains(s))
                    {
                        type = Type;
                        return true;
                    }
                }
            }
            return false;
        }

        internal Unit[][] GetTable()
        {
            return Pair;
        }

        internal string[] GetNames()
        {
            string[] names = new string[GetLength()];
            int index = 0;
            for (int i = 0; i < Pair.Length; i++)
            {
                for (int j = 0; j < Pair[i].Length; j++)
                {
                    names[index] = Pair[i][j].UnitNames[0];
                    index += 1;
                }
            }
            return names;
        }

        internal int GetLength()
        {
            int count = 0;
            foreach (Unit[] unitArray in Pair)
            {
                foreach (Unit unit in unitArray)
                {
                    count += 1;
                }
            }
            return count;
        }
    }
}