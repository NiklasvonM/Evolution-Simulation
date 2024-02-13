using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sex
{
    male,
    female
}

public abstract class Earthling
{
    public abstract IGenome Genome { get; set; }
    public Dictionary<string, float> BaseCharacteristics { get; protected set; }
    public Sex Sex;

    // Modify the base characteristics based on the multipliers given by the genes
    public Dictionary<string, float> GetModifiedCharacteristics()
    {
        var modifiedCharacteristics = new Dictionary<string, float>(this.BaseCharacteristics);

        foreach (var gene in Genome.Genes)
        {
            foreach (var characteristic in gene.Characteristics.Keys)
            {
                if (modifiedCharacteristics.ContainsKey(characteristic))
                {
                    float geneCharacteristicMultiplier = gene.Characteristics[characteristic];
                    modifiedCharacteristics[characteristic] *= geneCharacteristicMultiplier < 1f ? Mathf.Lerp(
                        geneCharacteristicMultiplier,
                        1f,
                        1 - gene.Strength
                    ) : Mathf.Lerp(
                        1f,
                        geneCharacteristicMultiplier,
                        gene.Strength
                    );
                }
            }
        }

        return modifiedCharacteristics;
    }

    public T Breed<T>(T partner) where T : Earthling
    {
        // Checking if T has a constructor that takes a Sex argument
        var constructor = typeof(T).GetConstructor(new[] { typeof(Sex) });
        if (constructor == null)
        {
            throw new InvalidOperationException("Type T must have a constructor that takes a Sex parameter");
        }
        // Using the constructor to create a new instance of T
        var child = (T)constructor.Invoke(new object[] { Utils.RandomEnumValue<Sex>() });

        for (int i = 0; i < this.Genome.Genes.Count; i++)
        {
            Gene geneFromFather = this.Genome.Genes[i];
            Gene geneFromMother = partner.Genome.Genes[i];

            Gene inheritedGene = new Gene(
                name: geneFromFather.Name,
                // Randomly use the gene strength from either father or mother
                strength: UnityEngine.Random.value > 0.5f ? geneFromFather.Strength : geneFromMother.Strength,
                characteristics: geneFromFather.Characteristics
            );

            // Slight random adjustment for mutation
            if (UnityEngine.Random.value < 0.2f)
            {
                inheritedGene.Strength += Mathf.Clamp(UnityEngine.Random.Range(-0.1f, 0.1f), 0, 1);
            }
            child.Genome.Genes.Add(inheritedGene);
        }

        return child;
    }
}

