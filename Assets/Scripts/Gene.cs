using System.Collections;
using System.Collections.Generic;

public class Gene
{
    public string Name { get; set; }
    /// <summary>
    /// Strength is a value between 0 and 1.
    /// Represents the multiplicative influence of the gene on the characteristics.
    /// 0 means the gene has no influence on the characteristics.
    /// 1 means the gene has full influence on the characteristics.
    /// </summary>
    public float Strength { get; set; }
    public Dictionary<string, float> Characteristics { get; set; }

    public Gene(string name, float strength, Dictionary<string, float> characteristics)
    {
        Name = name;
        Strength = strength;
        Characteristics = characteristics;
    }
}
