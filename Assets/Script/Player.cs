using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public struct HealthPoint
    {
        public int currentHP { get; set; }
        public int maxHP { get; set; }
    }
    public struct Bomb
    {
        public int currentBomb { get; set; }
        public int maxBomb { get; set; }
    }

    private Transform player = null;
    private Gun gun = null;
    public HealthPoint HP;
    public Bomb bomb;
    public float speed;

    public GameObject[] bombs;
    public GameObject[] HPs;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        Vector3 defaultPosition = new Vector3(-6.5f, -7f, 0);
        player.position = defaultPosition;
        gun = GameObject.Find("Gun").GetComponent<Gun>();
        gun.setShooter(this.transform);

        HP.currentHP = 3;
        HP.maxHP = 3;
        bomb.currentBomb = 3;
        bomb.maxBomb = 3;
        this.speed = 10;
    }

    void Update()
    {
        updatePlayerPosition();

        bool useBombButtonPressed = Input.GetKeyDown(KeyCode.X);
        if (useBombButtonPressed && (bomb.currentBomb > 0))
        {
            useBomb();
        }
    }

    private void FixedUpdate()
    {
        bool shooting_button_pressed = Input.GetKey(KeyCode.Space);
        if (shooting_button_pressed)
        {
            gun.shoot();
        }
    }

    public void takeDamage(int damage = 1)
    {
        HPs[HP.currentHP - damage].SetActive(false);
        HP.currentHP -= damage;
        bomb.currentBomb = bomb.maxBomb;

        UIManager.UI_Manager.takeDamage(this);
    }

    public bool isDead(int damage = 0)
    {
        return ((HP.currentHP - damage) <= 0) ? true : false;
    }

    public void useBomb()
    {
        bombs[bomb.currentBomb - 1].SetActive(false);
        bomb.currentBomb--;
        Debug.Log("You have used a bomb!\n" + bomb.currentBomb + " bombs remaining! ");
        
        UIManager.UI_Manager.useBomb();
    }


    private void updatePlayerPosition()
    {
        if (Input.GetKey(KeyCode.W) && player.position.y < 9.5f)
        {
            player.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) && player.position.x > -15f)
        {
            player.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) && player.position.y > -9f)
        {
            player.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) && player.position.x < 2.3f)
        {
            player.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            takeDamage();
            if (isDead())
            {
                GameManager.gameManager.GameOver();
            }
        }
    }
}
