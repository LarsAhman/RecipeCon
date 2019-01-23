using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeWPFUI.Models
{
    public class IngredientModel
    {

        public string Name { get; set; }
        public double Amount { get; set; }

        private string _unit;

        public string Unit
        {
            get { return _unit; }
            set
            {
                _unit = value;
            }
        }


        public UnitType UnitType { get; set; }
        public string[] RelatedUnits { get; set; }

        public IngredientModel(string name)
        {
            this.Name = name;
        }

        public IngredientModel()
        {

        }

        internal void ConvertToUS()
        {
            if (this.GetType() == typeof(AmountUnitIngredientModel))
            {
                UnitConverter.ConvertToUS(this);
            }
        }

        internal void ConvertToMetric()
        {
            if (this.GetType() == typeof(AmountUnitIngredientModel))
            {
                UnitConverter.ConvertToMetric(this);
            }
        }
    }

    public class AmountIngredientModel : IngredientModel
    {
        public AmountIngredientModel(double amount, string name)
        {
            Amount = amount;
            Name = name;
        }
    }

    public class AmountUnitIngredientModel : IngredientModel
    {
        public AmountUnitIngredientModel(double amount, string unit, string name, UnitType type)
        {
            Amount = amount;
            Unit = unit;
            Name = name;
            UnitType = type;
        }
    }
}