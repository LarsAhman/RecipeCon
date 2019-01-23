namespace RecipeWPFUI
{
    public class Unit
    {
        public string[] UnitNames { get; }
        public double Previous { get; }
        public double Next { get; }
        public double SwitchUnits { get; }

        public string DefaultSwitchTo { get; }

        public Unit(string[] unitNames, double previous, double next, double switchUnits, string defaultSwitchTo)
        {
            UnitNames = unitNames;
            Previous = previous;
            Next = next;
            SwitchUnits = switchUnits;
            DefaultSwitchTo = defaultSwitchTo;
        }
    }
}