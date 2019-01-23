using Caliburn.Micro;
using RecipeWPFUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecipeWPFUI.ViewModels
{
    public class RecipeViewModel : Screen
    {
        private BindableCollection<IngredientViewModel> _ingredientViewModels;
        public BindableCollection<IngredientViewModel> IngredientViewModels
        {
            get { return _ingredientViewModels; }
            set
            {
                _ingredientViewModels = value;
                NotifyOfPropertyChange(() => IngredientViewModels);
            }
        }

        public RecipeViewModel()
        {
            _canExecute = true;
            Multiplier = 1;
            ConvertSelectedIndex = -1;
        }

        private string _recipeText;
        private double _multiplier;
        private bool _canExecute;
        private int _convertSelectedIndex;

        public int ConvertSelectedIndex
        {
            get { return _convertSelectedIndex; }
            set
            {
                if (value == 0) //Metric
                {
                    ConvertToMetric();
                }

                if (value == 1) // US customary
                {
                    ConvertToUS();
                }
                _convertSelectedIndex = -1;
            }
        }

        private void ConvertToUS()
        {
            foreach (IngredientViewModel ingredientViewModel in IngredientViewModels)
            {
                ingredientViewModel.ConvertToUS();
            }
        }

        private void ConvertToMetric()
        {
            foreach (IngredientViewModel ingredientViewModel in IngredientViewModels)
            {
                ingredientViewModel.ConvertToMetric();
            }
        }

        public double Multiplier
        {
            get { return _multiplier; }
            set
            {
                _multiplier = value;
                NotifyOfPropertyChange(() => Multiplier);
            }
        }

        public string RecipeText
        {
            get { return _recipeText; }
            set { _recipeText = value; }
        }


        private ICommand _multiplyClickCommand;
        public ICommand MultiplyClickCommand
        {
            get
            {
                return _multiplyClickCommand ?? (_multiplyClickCommand = new CommandHandler(() => MultiplyIngredients(), _canExecute));
            }
        }

        private ICommand _processClickCommand;
        public ICommand ProcessClickCommand
        {
            get
            {
                return _processClickCommand ?? (_processClickCommand = new CommandHandler(() => ProcessTextToIngredients(), _canExecute));
            }
        }

        public void ProcessTextToIngredients()
        {
            IngredientViewModels = new BindableCollection<IngredientViewModel>(IngredientViewModel.TextToIngredientViewModels(RecipeText));
        }

        public void MultiplyIngredients()
        {
            foreach (IngredientViewModel ingredientViewModel in IngredientViewModels)
            {
                ingredientViewModel.Multiply(Multiplier);
            }
            Multiplier = 1;
        }
    }


}
