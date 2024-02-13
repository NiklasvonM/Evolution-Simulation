using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyGenome : IGenome
{

    public List<Gene> Genes { get; set; }

    public BunnyGenome()
    {
        Genes = new List<Gene>
        {
            new Gene(
                name: "Agility",
                strength: UnityEngine.Random.Range(0f, 1f),
                characteristics: new Dictionary<string, float> {
                    { "jumping_frequency", 2f },
                    { "feeding_frequency", 0.5f}
                }
            ),
            new Gene(
                name: "Strength",
                strength: UnityEngine.Random.Range(0f, 1f),
                characteristics: new Dictionary<string, float> {
                    { "jumping_frequency", 2f },
                    { "feeding_frequency", 0.5f}
                }
            ),
            new Gene(
                name: "VisionCone",
                strength: UnityEngine.Random.Range(0f, 1f),
                characteristics: new Dictionary<string, float> {
                    { "vision_cone_degrees", 2f },
                    { "vision_distance", 0.5f}
                }
            ),
        };
    }
}
