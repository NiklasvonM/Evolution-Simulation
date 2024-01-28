using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HexGrid : MonoBehaviour
{
    public float HexRadius = 1f; // The radius of a hexagon

    // Define orientation (Pointy or Flat topped)
    public bool PointyTopped = false;

    // This method converts world position to hex coordinates
    public Vector2Int WorldToHex(Vector3 worldPosition)
    {
        float q, r;

        if (PointyTopped)
        {
            q = (worldPosition.x * Mathf.Sqrt(3) / 3 - worldPosition.z / 3) / HexRadius;
            r = worldPosition.z * 2 / 3 / HexRadius;
        }
        else
        {
            q = worldPosition.x * 2 / 3 / HexRadius;
            r = (-worldPosition.x * Mathf.Sqrt(3) / 3 + worldPosition.z / 3) / HexRadius;
        }

        return HexRound(new Vector2(q, r));
    }

    /// <summary>
    /// Rounds the fractional hexagonal coordinates to the nearest valid hexagonal grid coordinates.
    /// </summary>
    /// <param name="fractionalHex">The fractional q,r coordinates representing a position on the hexagonal grid.</param>
    /// <returns>The rounded q,r coordinates of the hexagon on the grid.</returns>
    private Vector2Int HexRound(Vector2 fractionalHex)
    {
        int q = Mathf.RoundToInt(fractionalHex.x);
        int r = Mathf.RoundToInt(fractionalHex.y);
        int s = Mathf.RoundToInt(-fractionalHex.x - fractionalHex.y);

        float q_diff = Mathf.Abs(q - fractionalHex.x);
        float r_diff = Mathf.Abs(r - fractionalHex.y);
        float s_diff = Mathf.Abs(s + fractionalHex.x + fractionalHex.y);

        if (q_diff > r_diff && q_diff > s_diff)
        {
            q = -r - s;
        }
        else if (r_diff > s_diff)
        {
            r = -q - s;
        }

        return new Vector2Int(q, r);
    }

    /// <summary>
    /// Gets the hexagonal coordinates for all neighboring hexagons for a given hexagon.
    /// </summary>
    /// <param name="hexCoords">The q,r coordinates of the hexagon for which to find neighbors.</param>
    /// <returns>An array of neighboring q,r coordinates.</returns>
    public Vector2Int[] GetNeighbors(Vector2Int hexCoords)
    {
        // Define directions based on the orientation of the hexagons (Pointy or Flat topped)
        Vector2Int[] directions = PointyTopped ?
            new Vector2Int[]
            {
            new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, -1),
            new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, 1)
            }
            :
            new Vector2Int[]
            {
            new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(-1, 1),
            new Vector2Int(-1, 0), new Vector2Int(0, -1), new Vector2Int(1, -1)
            };

        Vector2Int[] neighbors = new Vector2Int[6];
        for (int i = 0; i < 6; i++)
        {
            neighbors[i] = new Vector2Int(hexCoords.x + directions[i].x, hexCoords.y + directions[i].y);
        }

        return neighbors;
    }

}
