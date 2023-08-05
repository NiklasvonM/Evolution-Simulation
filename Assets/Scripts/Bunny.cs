using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : Earthling
{
    private BunnyGenome BunnyGenome { get; set; }

    // We must use IGenome here, since this is an overridden property.
    public override IGenome Genome
    {
        get { return BunnyGenome; }
        set
        {
            if (value is BunnyGenome bg)
            {
                BunnyGenome = bg;
            }
            else
            {
                throw new ArgumentException("Value must be a BunnyGenome.");
            }
        }
    }

    public Bunny(Sex sex)
    {
        BunnyGenome = new BunnyGenome();
        BaseCharacteristics = new Dictionary<string, float>
        {
            { "jumping_strength", 100f },
            { "jumping_frequency", 5f },
            { "feeding_frequency", 100f },
            { "stamina", 10f },
            { "stamina_recovery", 1f },
            { "reproduction_frequency", 100f },
            { "maximum_age", 1000f },
            { "adulthood_age", 100f },
            { "drinking_frequency", 5f },
            { "size", 1f },
        };
        Sex = sex;
    }
}
