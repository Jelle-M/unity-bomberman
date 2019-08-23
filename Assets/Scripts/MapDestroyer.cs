using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{
    public Tilemap tilemap;

    public Tile wallTile;
    public Tile destructibleTile;
    public GameObject flame;
    private float timeFlame = 3f;

    public void Explode(Vector2 worldPos, int power = 1)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);
        ExplodeCell(originCell);
        ExplodeCell(originCell + new Vector3Int(1, 0, 0));
        ExplodeCell(originCell + new Vector3Int(2, 0, 0));
        ExplodeCell(originCell + new Vector3Int(0, 1, 0));
        ExplodeCell(originCell + new Vector3Int(0, 2, 0));
        ExplodeCell(originCell + new Vector3Int(-1, 0, 0));
        ExplodeCell(originCell + new Vector3Int(-2, 0, 0));
        ExplodeCell(originCell + new Vector3Int(0, -1, 0));
        ExplodeCell(originCell + new Vector3Int(0, -2, 0));
    }

    void ExplodeCell(Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);
        if (tile == wallTile)
        {
            return;
        }
        if (tile == destructibleTile)
        {
            // Remove tile
            tilemap.SetTile(cell, null);
        }
        // Create explosion
        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        GameObject flameObject = Instantiate(flame, pos, Quaternion.identity);
        Destroy(flameObject, timeFlame);
    }
}