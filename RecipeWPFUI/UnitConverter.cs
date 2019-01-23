using System;
using System.Linq;
using RecipeWPFUI.Models;

namespace RecipeWPFUI
{
    public enum UnitType
    {
        None,
        Volume,
        Weight,
        Length
    }
    internal class UnitConverter
    {
        protected static UnitSystemPair[] UnitSystemPairs = { new LengthPair(), new WeightPair(), new VolumePair() };

        protected static Tuple<string, int>[] WordNumberPairs =
        {
            new Tuple<string,int>("one", 1),
            new Tuple<string,int>("a", 1 ),
            new Tuple<string,int>("two", 2),
            new Tuple<string,int>("pair", 2),
            new Tuple<string,int>("three", 3),
            new Tuple<string,int>("four", 4),
            new Tuple<string,int>("five", 5),
            new Tuple<string,int>("six", 6),
            new Tuple<string,int>("seven", 7),
            new Tuple<string,int>("eight", 8),
            new Tuple<string,int>("nine", 9),
            new Tuple<string,int>("ten", 10),
            new Tuple<string,int>("dozen", 12)
        };

        public static void ConvertToUS(IngredientModel ingredientModel)
        {
            foreach (UnitSystemPair unitSystemPair in UnitSystemPairs)
            {
                if (unitSystemPair.Type == ingredientModel.UnitType)
                {
                    Tuple<int,int> index = unitSystemPair.IndexOfUnitName(ingredientModel.Unit);

                    if (index.Item1 == 1)
                    {
                        return;
                    }
                    string unit = ingredientModel.Unit;
                    string defaultSwitchTo = unitSystemPair.Pair[index.Item1][index.Item2].DefaultSwitchTo;
                    ingredientModel.Amount = ConvertFromTo(ingredientModel.Amount, unit, defaultSwitchTo, ingredientModel.UnitType);
                    ingredientModel.Unit = defaultSwitchTo;
                    return;
                }
            }
        }

        public static void ConvertToMetric(IngredientModel ingredientModel)
        {
            foreach (UnitSystemPair unitSystemPair in UnitSystemPairs)
            {
                if (unitSystemPair.Type == ingredientModel.UnitType)
                {
                    Tuple<int, int> index = unitSystemPair.IndexOfUnitName(ingredientModel.Unit);

                    if (index.Item1 == 0)
                    {
                        return;
                    }
                    string unit = ingredientModel.Unit;
                    string defaultSwitchTo = unitSystemPair.Pair[index.Item1][index.Item2].DefaultSwitchTo;
                    ingredientModel.Amount = ConvertFromTo(ingredientModel.Amount, unit, defaultSwitchTo, ingredientModel.UnitType);
                    ingredientModel.Unit = defaultSwitchTo;
                    return;
                }
            }
        }

        public static double ConvertFromTo(double amount, string fromUnit, string toUnit, UnitType type)
        {
            foreach (UnitSystemPair unitSystemPair in UnitSystemPairs)
            {
                if (unitSystemPair.Type == type)
                {
                    double newAmount = unitSystemPair.ConvertFromTo(amount, fromUnit, toUnit);
                    return newAmount;
                }
            }
            return 0;
        }


        public static UnitSystemPair GetLengthPair()
        {
            return UnitSystemPairs[0];
        }

        public static UnitSystemPair GetWeightPair()
        {
            return UnitSystemPairs[1];
        }

        public static UnitSystemPair GetVolumePair()
        {
            return UnitSystemPairs[2];
        }

        public static string[] GetLengthNames()
        {
            return UnitSystemPairs[0].GetNames();
        }

        public static string[] GetWeightNames()
        {
            return UnitSystemPairs[1].GetNames();
        }

        public static string[] GetVolumeNames()
        {
            return UnitSystemPairs[2].GetNames();
        }

        public static float NumeralToNumber(string word)
        {
            foreach (Tuple<string, int> pair in WordNumberPairs)
            {
                if (word == pair.Item1)
                {
                    return pair.Item2;
                }
            }
            return -1;
        }

        public static bool IsUnit(string s, out UnitType type)
        {
            type = UnitType.None;
            foreach (UnitSystemPair pair in UnitSystemPairs)
            {
                if (pair.IsUnit(s, out type))
                {
                    return true;
                }
            }
            return false;
        }

        public static string[] GetUnitNamesByTypeAndSystem(UnitType unitType, int index)
        {
            string[] unitNames = null;
            foreach (UnitSystemPair unitSystemPair in UnitSystemPairs)
            {
                if (unitSystemPair.Type == unitType)
                {
                    unitNames = new string[unitSystemPair.Pair[index].Length];

                    for (int i = 0; i < unitSystemPair.Pair[index].Length; i++)
                    {
                        unitNames[i] = unitSystemPair.Pair[index][i].UnitNames[0];
                    }
                    break;
                }
            }
            return unitNames;
        }


        internal static string[] GetUnitNamesByType(UnitType type)
        {
            foreach (UnitSystemPair pair in UnitSystemPairs)
            {
                if(pair.Type == type)
                {
                    return pair.GetNames();
                }
            }
            return null;
        }
    }
}