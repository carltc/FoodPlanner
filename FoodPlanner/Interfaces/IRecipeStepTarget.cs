﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPlanner.Interfaces
{
    public interface IRecipeStepTarget
    {
        public string Name { get; }

        public string Category { get; }
    }
}
