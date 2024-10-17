using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour, IBezierRoute
{
    public float MAX_HP = 10000.0f;
    public float HP = 10000.0f;
    public Transform fire_point = null;

    private Vector3 BossDefaultPosition = new Vector3(-11,-4,0);

    public Rigidbody2D rb;

    [Header("Bezier")]
    [SerializeField]
    private GameObject[] BezierPath;
    [SerializeField]
    float timeToMove = 0.05f;
    [SerializeField]
    private float tParameter = 0.0f;
    [SerializeField]
    private float speedModifier = 0.01f;

    float lastMovingTime = 0.0f;
    int i = 0;
    int children = 0;

    bool back = false;

    bool pathFinished = false;

    private Gun gun = null;

    private const int DEFAULT = 0;

    enum Curve 
    {
        Infinite,
        Loop,
        BackandForth
    }
    void Start()
    {
        HP = MAX_HP;
        gun = GameObject.Find("GunForBoss").GetComponent<Gun>();
        gun.setShooter(this.transform);
    }

    void FixedUpdate()
    {    
        
    }

    private void Update()
    {
        if (!pathFinished)
            followBezierCurve(IBezierRoute.Path.BackandForth, DEFAULT);
        gun.shoot(GameObject.Find("Player").transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            float damage = GameObject.FindWithTag("Bullet").GetComponent<Bullet>().damage;
            HP = HP - damage;
            Debug.Log("HP deducted! Remaining " + HP.ToString());
            Destroy(collision.gameObject);

            // Score.GetComponent<UIManager>().AddScore(10);
            int HitScore = 10;

            UIManager.UI_Manager.AddScore(HitScore);

            if (isDead())
            {
                Destroy(gameObject);
                Debug.Log("Destroyed!");
            }
        }
    }

    private bool isDead()
    {
        return (HP < 0)? true : false;
    }

    Vector3 GetPointOnCubicBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        /*
            Formula is retrieved from wikipedia 
            Link: https://en.wikipedia.org/wiki/B%C3%A9zier_curve (Cubic Bezier Curve)
        */
          
        float u = 1 - t;

        Vector3 result =
            (u * u * u)          * p0 +   // u^3 * p0 +
            (u * u * t * 3f)     * p1 +   // 3(u^2 * t) * p1 + 
            (u * t * t * 3f)     * p2 +   // 3(u * t^2) * p2 + 
            (t * t * t)          * p3;    // t^3 * p3
    
        return result;
    }

    public void followBezierCurve(IBezierRoute.Path path, int PathNum)
    {
        bool followingSubPath = true;

        // Don't but children variable into the loop because it is somehow expensive
        // Try to find the childCount before enter the function
        int children = BezierPath[PathNum].transform.childCount;

        while (followingSubPath && ((Time.time - lastMovingTime) > timeToMove) && (i < children - 2))
        {
            Vector3 p0 = BezierPath[PathNum].transform.GetChild(i).transform.position;
            Vector3 p1 = BezierPath[PathNum].transform.GetChild(i + 1).transform.position;
            Vector3 p2 = BezierPath[PathNum].transform.GetChild(i + 2).transform.position;
            Vector3 p3 = BezierPath[PathNum].transform.GetChild(i + 3).transform.position;


            // if the distance of last position and current position is smaller than or larger than some values 
            // then update the position more or don't need to update
            if (!back)      // moving forward                                                                          
                gameObject.transform.position = GetPointOnCubicBezierCurve(p0,p1,p3,p2,tParameter);
            else            // moving backward
                gameObject.transform.position = GetPointOnCubicBezierCurve(p2,p3,p1,p0,tParameter);


            followingSubPath = (tParameter >= 1)? false : true;
            tParameter = followingSubPath? (tParameter + speedModifier) : 0;
            i = followingSubPath? i : i + 2;
            
            lastMovingTime = Time.time;
        }

        if (!followingSubPath)
        {
            if (path == IBezierRoute.Path.LoopFromFirstPoint && i >= (children - 2))
            {
                i = 0;
            }
            else if (path == IBezierRoute.Path.BackandForth)
            {
                i = 0;
                back = !back;
            }
            else if (path == IBezierRoute.Path.OneTime)
            {
                pathFinished = true;
            }
        }
    } 

}