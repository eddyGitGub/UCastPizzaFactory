using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCastPizzaFactory.Models;

namespace UCastPizzaFactory.Extensions;

public static class HelperExtension
{
    public static int ToPizzaBaseCookingTime(this int pizzabase, PizzaBaseCookingTime baseCookingTime)
    {
        return pizzabase switch
        {
            1 => baseCookingTime.DeepPan,
            2 => baseCookingTime.StuffedCrus,
            3 => baseCookingTime.ThinAndCrispy,
            var _ => throw new NotImplementedException(),
        };
    }
}
