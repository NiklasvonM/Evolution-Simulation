using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateHexGrid : MonoBehaviour
{
    public GameObject hexPrefab;
    private int width = 100;
    private int height = 100;

    float hexWidth = 1f;
    float hexHeight = 1f;
    private float gap = 0.0f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;

        //AddGap();
        CreateGrid();
    }

    void AddGap()
    {
        hexWidth += hexWidth * gap;
        hexHeight += hexHeight * gap;
    }

    void CreateGrid()
    {
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                float xPos = x * hexWidth;
                Debug.Log("x:" + x);
                if (z % 2 == 1)
                {
                    xPos += hexWidth / 2;
                }

                GameObject hex = Instantiate(hexPrefab, new Vector3(xPos, 0, z * hexHeight * 0.75f) + startPos, Quaternion.identity);
                hex.transform.parent = this.transform;
            }
        }
    }
}
