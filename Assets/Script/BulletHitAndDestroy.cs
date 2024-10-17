using UnityEngine;

public class BulletHitAndDestroy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        Debug.Log("Destroy");
    }
}
