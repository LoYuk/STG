using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb = null;
    [SerializeField]
    private float speed;
    public float damage;
    [SerializeField]
    private int hitScore = 10;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with " + collision.gameObject.tag);

        switch (collision.gameObject.tag)
        {
            case "GameBoundaries":
                break;
            case "Enemy":
                UIManager.UI_Manager.AddScore(hitScore);
                break;
            case "Player":
                UIManager.UI_Manager.takeDamage(GameObject.Find("Player").GetComponent<Player>());
                break;
        }
        Destroy(gameObject);
     }
}
