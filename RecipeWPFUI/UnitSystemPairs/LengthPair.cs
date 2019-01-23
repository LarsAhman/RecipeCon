namespace RecipeWPFUI
{
    internal class LengthPair : UnitSystemPair
    {
        // {0, x} --> SI
        // {1, x} --> Imperial

        public LengthPair()
        {
            Type = UnitType.Length;
        }


        public override Unit[][] Pair => new Unit[][]
        {
            new Unit[]
            {
                new Unit(new string[] { "mm" }, -1.0, 10.0, 25.4, "in"),
                new Unit(new string[] { "cm" }, 0.1, 100.0, 2.54, "in"),
                new Unit(new string[] { "m" }, 0.01, 1000, 0.0254, "ft"),
                new Unit(new string[] { "km" }, 0.001, -1, 0.0000254, "miles")},

            new Unit[]
            {
                new Unit(new string[] { "in" }, -1.0, 12.0, 0.03937, "cm"),
                new Unit(new string[] { "ft" }, 0.083333, 3, 0.003281, "cm"),
                new Unit(new string[] { "yd" }, 0.33333, 1760, 0.001094, "m"),
                new Unit(new string[] { "miles" }, 0.00568, -1, 0.000000621, "km")}
        };

    }
}