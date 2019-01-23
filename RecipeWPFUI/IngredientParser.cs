using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using RecipeWPFUI.Models;

namespace RecipeWPFUI
{
    class IngredientParser
    {
        internal static List<IngredientModel> TextToIngredients(string recipeText)
        {
            List<IngredientModel> ingredients = new List<IngredientModel>();
            string[] lines = recipeText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                ingredients.Add(LineToIngredient(line));
            }
            return ingredients;
        }

        private static IngredientModel LineToIngredient(string line)
        {
            float amount = 0;
            string unit = null;
            string ingredientName = null;
            string[] ingredientParts = line.Trim().Split(new[] { ' ' }, 4, System.StringSplitOptions.RemoveEmptyEntries);
            if (!float.TryParse(ingredientParts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out amount)) //test if the first entry is a number
            {
                float numberWord = UnitConverter.NumeralToNumber(ingredientParts[0]); //Check if it is instead a word for a number ie. "one", "a" or "dozen"
                if (numberWord == -1)
                {
                    return new IngredientModel(string.Join(" ", ingredientParts)); //if neither: Ingredient is probably in the style of: "some salt" or "olive oil to taste" or just "pepper" which cannot be expressed numerically and doesn't require unit or amount conversion
                }
                amount = numberWord;
            }

            //TODO:
            // 1dl of cabbage, 2kg mämmi
            if (ingredientParts.Length == 2) //Ingredient is probably in the style of: "1 banana" "a cucumber" "a pair of eggs" ie. doesn't contain a unit of measurement
            {
                ingredientName = ingredientParts[1];
                return new AmountIngredientModel(amount, ingredientName);
            }

            //We now know the length of the parts array is x >= 3
            string part1 = ingredientParts[1];
            string part2 = ingredientParts[2];

            UnitType type;

            if (UnitConverter.IsUnit(part1, out type))
            {
                unit = part1;
                ingredientName = part2;
                return new AmountUnitIngredientModel(amount, unit, ingredientName, type);
            }

            string partsJoined = string.Join(" ", part1, part2);

            if (UnitConverter.IsUnit(partsJoined, out type)) //Such as "1 fl. oz. cream"
            {
                unit = partsJoined;
                ingredientName = ingredientParts[3];
                return new AmountUnitIngredientModel(amount, unit, ingredientName, type);
            }

            // ingredient is really a two-part ingredient name, such as "(1) (clove of garlic)"
            ingredientName = string.Join(" ", ingredientParts.Skip(1));
            return new AmountIngredientModel(amount, ingredientName);

        }
    }
}
