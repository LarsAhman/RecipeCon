namespace RecipeWPFUI
{
    internal class VolumePair : UnitSystemPair
    {
        public VolumePair()
        {
            Type = UnitType.Volume;
        }
        //string[] unitNames, double previous, double next, double switchUnits, int min, int max
        public override Unit[][] Pair => new Unit[][]
        {
            new Unit[]
            {
                new Unit(new string[] { "ml" }, -1.0, 10.0, 29.5735296, "fl oz"),
                new Unit(new string[] { "cl" }, 0.1, 10.0, 2.95735296, "fl oz"),
                new Unit(new string[] { "dl", "deciliter" }, 0.1, 10, 0.295735296, "cup"),
                new Unit(new string[] { "l" }, 0.1, -1, 0.0295735296, "cup")
            },

            new Unit[]
            {
                new Unit(new string[] { "fl oz", "fl. oz." }, -1, 1.5, 0.0338140227, "ml"),
                new Unit(new string[] { "shot", "shots" }, 2/3, 5.333333333, 0.0225426818, "cl"),
                new Unit(new string[] { "cup" }, 0.1875, 2, 0.00422675284, "dl"),
                new Unit(new string[] { "pint", "liquid pint", "pt" }, 0.5, 2, 0.00211337642, "l"),
                new Unit(new string[] { "quart", "liquid quart", "qt" }, 0.5, 4, 0.00105668821, "l"),
                new Unit(new string[] { "gallon", "liquid gallon", "gal" }, 0.25, -1, 0.000264172052, "l")
            }
        };
    }
}