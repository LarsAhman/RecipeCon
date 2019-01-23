using Caliburn.Micro;
using RecipeWPFUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeWPFUI.ViewModels
{
    public class IngredientViewModel : Screen
    {
        private IngredientModel _ingredient;
        private string[] _relatedUnits;
        private int _selectedIndex;


        public IngredientViewModel(IngredientModel ingredient)
        {
            Ingredient = ingredient;
        }

        public IngredientModel Ingredient
        {
            get { return _ingredient; }
            set { _ingredient = value; }
        }

        public string[] RelatedUnits
        {
            get { return _relatedUnits; }
            set { _relatedUnits = value; }
        }
        private double _amount;

        public double Amount // purpose is to round the ingredients' amounts to be easier to read
        {
            get { return _amount; }
            set
            {
                Ingredient.Amount = value;
                _amount = Math.Round(value, 2);
                NotifyOfPropertyChange(() => Amount);
            }
        }

        private string _selectedValue;

        public string SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                Amount = UnitConverter.ConvertFromTo(Ingredient.Amount, Ingredient.Unit, value, Ingredient.UnitType);
                _selectedValue = value;
                Ingredient.Unit = value;
                NotifyOfPropertyChange(() => Ingredient);
            }
        }


        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
            }
        }


        public IngredientViewModel()
        {

        }

        public static List<IngredientViewModel> TextToIngredientViewModels(string recipeText)
        {
            List<IngredientViewModel> ingredientViewModels = new List<IngredientViewModel>();

            List<IngredientModel> ingredients = IngredientParser.TextToIngredients(recipeText);

            foreach (IngredientModel ingredient in ingredients)
            {
                if (ingredient.GetType() == typeof(AmountIngredientModel))
                {
                    ingredientViewModels.Add(new AmountIngredientViewModel(ingredient));
                    continue;
                }

                if (ingredient.GetType() == typeof(AmountUnitIngredientModel))
                {
                    ingredientViewModels.Add(new AmountUnitIngredientViewModel(ingredient));
                    continue;
                }

                ingredientViewModels.Add(new IngredientViewModel(ingredient));
            }
            return ingredientViewModels;
        }

        internal void ConvertToMetric()
        {
            Ingredient.ConvertToMetric();
            SelectedIndex = Array.IndexOf(RelatedUnits, Ingredient.Unit);
            NotifyOfPropertyChange(() => SelectedIndex);
            NotifyOfPropertyChange(() => Ingredient);
        }

        internal void ConvertToUS()
        {
            Ingredient.ConvertToUS();
            SelectedIndex = Array.IndexOf(RelatedUnits, Ingredient.Unit);
            NotifyOfPropertyChange(() => SelectedIndex);
            NotifyOfPropertyChange(() => Ingredient);
        }

        internal void Multiply(double multiplier)
        {
            if (this.GetType() == typeof(AmountIngredientViewModel) || this.GetType() == typeof(AmountUnitIngredientViewModel))
            {
                Amount *= multiplier;
                NotifyOfPropertyChange(() => Ingredient);
            }
        }
    }

    public class AmountIngredientViewModel : IngredientViewModel
    {
        public AmountIngredientViewModel(IngredientModel ingredient)
        {
            Ingredient = ingredient;
            Amount = Ingredient.Amount;
        }
    }

    public class AmountUnitIngredientViewModel : IngredientViewModel
    {
        public AmountUnitIngredientViewModel(IngredientModel ingredient)
        {
            Ingredient = ingredient;
            Amount = Ingredient.Amount;
            RelatedUnits = UnitConverter.GetUnitNamesByType(Ingredient.UnitType);
            SelectedIndex = Array.IndexOf(RelatedUnits, Ingredient.Unit);
        }
    }
}
