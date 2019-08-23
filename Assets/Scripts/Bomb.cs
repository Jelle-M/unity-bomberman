using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float countdown = 3f;

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            // explode
            Debug.Log("Explode!");
            Destroy(gameObject);
        }
        
    }
}