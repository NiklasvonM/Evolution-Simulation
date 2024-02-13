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
            { "jumping_strength", 50f },
            { "jumping_frequency", 1f },
            { "stamina_required_to_jump", 10f },
            { "feeding_frequency", 100f },
            { "maximum_stamina", 100f },
            { "stamina_recovery", 0.1f },
            { "reproduction_frequency", 100f },
            { "maximum_age", 1000f },
            { "adulthood_age", 10f },
            { "drinking_frequency", 50f },
            { "size", 1f },
            { "vision_cone_degrees", 90f },
            { "vision_distance", 10f }
        };
        Sex = sex;
    }
}
