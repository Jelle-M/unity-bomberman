using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject bomb;
    private int n_bombs = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && n_bombs > 0)
        {
            Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
            Vector3 centerPosition = tilemap.GetCellCenterWorld(cellPosition);
            Instantiate(bomb, centerPosition, Quaternion.identity);
            n_bombs--;
            Invoke("addBomb", 1); //RemoveLater
        }
        
    }

    void addBomb()
    {
        n_bombs++;
    }
}
