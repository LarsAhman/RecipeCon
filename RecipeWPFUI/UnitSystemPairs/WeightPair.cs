namespace RecipeWPFUI
{
    internal class WeightPair : UnitSystemPair
    {
        public WeightPair()
        {
            Type = UnitType.Weight;
        }

        public override Unit[][] Pair => new Unit[][]
        {
            new Unit[]
            {
                new Unit(new string[] { "g" }, -1.0, 1000.0, 28.3495231, "oz"),
                new Unit(new string[] { "kg" }, 0.001, -1, 0.0283495231, "lb"),
            },

            new Unit[]
            {
                new Unit(new string[] { "oz" }, -1.0, 16.0, 0.0352739619, "g"),
                new Unit(new string[] { "lb" }, 0.0625, -1, 0.00220462262, "g"),
            }

        };
    }
}