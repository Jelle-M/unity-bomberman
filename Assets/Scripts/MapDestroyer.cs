using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile indestructibleTile;
    public Tile destructibleTile;
    public GameObject flame;
    [SerializeField]
    private float timeFlame = 3.5f;
    [SerializeField]
    private float delayExplosions = 0.8f;

    private IEnumerator ExplodeCells(Vector3Int originCell, int power)
    {
        for (int i = 1; i < power + 1; i++)
        {
            ExplodeCell(originCell + new Vector3Int(i, 0, 0));
            ExplodeCell(originCell + new Vector3Int(-i, 0, 0));
            ExplodeCell(originCell + new Vector3Int(0, i, 0));
            ExplodeCell(originCell + new Vector3Int(0, -i, 0));
            yield return new WaitForSeconds(delayExplosions);
        }
    }

    public void Explode(Vector2 worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);
        ExplodeCell(originCell);
        StartCoroutine(ExplodeCells(originCell, PlayerController.power) ) ;
    }

    void ExplodeCell(Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);
        if (tile == indestructibleTile)
        {
            return;
        }
        if (tile == destructibleTile)
        {
            tilemap.SetTile(cell, null);
        }
        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        GameObject flameObject = Instantiate(flame, pos, Quaternion.identity);
        Destroy(flameObject, timeFlame);
    }
}