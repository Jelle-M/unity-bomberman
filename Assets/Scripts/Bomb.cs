using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    public float countdown = 5f;
    public Rigidbody rb;

    IEnumerator Example()
    {
        DisableRagdoll();
        yield return new WaitForSeconds(5);
        EnableRagdoll();
    }
     void Start() {
         rb = GetComponent<Rigidbody>();
         StartCoroutine(Example());
     }
     void EnableRagdoll() {
         rb.isKinematic = false;
         rb.detectCollisions = true;
     }
     void DisableRagdoll() {
         rb.isKinematic = true;
         rb.detectCollisions = false;
     }
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

   void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("COLLIDER TRIGGER");
        switch(col.gameObject.tag)
        {
            case "DeadZone":
                Debug.Log("FIRE TRIGGERED");
                Explode();
                break;
            default:
                break;
        }
    }
}