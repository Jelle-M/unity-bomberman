using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject bomb;
    private int bombs = 1;
    [SerializeField]
    private float bombRefreshTime = 5f;



    void Update()
    {
        if (Input.GetButtonDown("Jump") && bombs > 0)
        {
            Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
            Vector3 centerPosition = tilemap.GetCellCenterWorld(cellPosition);
            Instantiate(bomb, centerPosition, Quaternion.identity);
            bombs--;
            Invoke("addBomb", bombRefreshTime);
        }
        
    }

    public void incrementBomb()
    {
        bombs++;
    }

    private void addBomb()
    {
        bombs++;
    }
}
