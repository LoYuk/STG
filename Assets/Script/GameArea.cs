using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCollisionExit2D (Collision2D collision)
    {
        Destroy(collision.gameObject);
        Debug.Log("Destroy");
    }
}
