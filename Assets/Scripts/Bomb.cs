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
            FindObjectOfType<MapDestroyer>().Explode(transform.position); // TODO: Replace by Singleton 
            Destroy(gameObject);
        }
    }

    public void Explode()
    {
        countdown = 0f;
    }
}