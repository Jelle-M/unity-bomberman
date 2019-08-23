using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    [SerializeField]
    private float speed = 1;
    private Rigidbody2D rb2d;
    private Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))).normalized;
        setAnimator(movement);
    }

    void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        rb2d.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    void setAnimator(Vector2 direction)
    {
        float horizontal = movement[0];
        float vertical = movement[1];
        Debug.LogFormat("H {0} V {1}", horizontal, vertical);
        animator.SetFloat("speed", horizontal*horizontal + vertical*vertical);
        if (horizontal < 0)
        {
            animator.SetInteger("direction", 2);
        }
        else if (vertical < 0)
        {
            animator.SetInteger("direction", 3);
        }
        else if (vertical > 0)
        {
            animator.SetInteger("direction", 16);
        }
        else
        {
            animator.SetInteger("direction", 0);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.gameObject.tag)
        {
            case "BombPowerUp":
                Debug.Log("BombPowerUp!");
                break;
            case "SpeedPowerUp":
                Debug.Log("SpeedPowerUp!");
                break;
            case "ShieldPowerUp":
                Debug.Log("ShieldPowerUp!");
                break;
            case "BlastPowerUp":
                Debug.Log("BlastPowerUp!");
                break;
            case "DeadZone":
                Debug.Log("DeadZone!");
                break;
            default:
                break;
        }
    }
}
