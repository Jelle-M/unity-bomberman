using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile indestructibleTile;
    public Tile destructibleTile;
    [SerializeField]
    private float chancePowerUp = 1f;
    public GameObject flame;
    public GameObject speed;
    public GameObject power;
    public GameObject bomb;
    [SerializeField]
    private float timeFlame = 3.5f;
    [SerializeField]
    private float timePower = 8f;
    [SerializeField]
    private float delayExplosions = 0.8f;

    private IEnumerator ExplodeCells(Vector3Int originCell, int power)
    {
        bool up = true, down = true, left = true, right = true;
        for (int i = 1; i < power + 1; i++)
        {
            if (right)
            {
                right = ExplodeCell(originCell + new Vector3Int(i, 0, 0));
            }
            if (left)
            {
                left = ExplodeCell(originCell + new Vector3Int(-i, 0, 0));
            }
            if (up)
            {
                up = ExplodeCell(originCell + new Vector3Int(0, i, 0));
            }
            if (down)
            {
                down = ExplodeCell(originCell + new Vector3Int(0, -i, 0));
            }
            yield return new WaitForSeconds(delayExplosions);
        }
    }

    public void Explode(Vector2 worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);
        ExplodeCell(originCell);
        StartCoroutine(ExplodeCells(originCell, PlayerController.power) ) ;
    }

    void spawnPowerUp(Vector3 pos)
    {
        float randValue = Random.value;
        if (Random.value < chancePowerUp)
        {
            GameObject powerUp;
            if (randValue < 0.33f)
            {
                powerUp = Instantiate(bomb, pos, Quaternion.identity);
            }
            else if (randValue < 0.66f)
            {
                powerUp = Instantiate(speed, pos, Quaternion.identity);
            }
            else
            {
                powerUp = Instantiate(power, pos, Quaternion.identity);
            }
            Destroy(powerUp, timePower);
        }
    }

    bool ExplodeCell(Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);
        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        bool continueExplosion = true;
        if (tile == indestructibleTile)
        {
            return false;
        }
        if (tile == destructibleTile)
        {
            tilemap.SetTile(cell, null);
            spawnPowerUp(pos);
            continueExplosion = false;
        }
        GameObject flameObject = Instantiate(flame, pos, Quaternion.identity);
        Destroy(flameObject, timeFlame);
        return continueExplosion;
    }
}