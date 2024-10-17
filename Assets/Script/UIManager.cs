using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager UI_Manager {get; private set;}
    void Awake()
    {
        if (UI_Manager != this && UI_Manager != null)
        {
            Destroy(this);
        }
        else
        {
            UI_Manager = this;
        }
    }
    private int TotalScore = 0;
    [SerializeField]
    private TMP_Text Score_text;
    [SerializeField]
    private GameObject Lives = null;
    [SerializeField]
    private GameObject Bombs = null;

    public void AddScore(int Score)
    {
        TotalScore += Score;
        Score_text.text = TotalScore.ToString();
    }

    public void useBomb()
    {
        Debug.Log("Use Bomb and make the sprite invisible");
    }

    public void takeDamage(Player player)
    {
        Debug.Log("Player's life had deducted !\n" + player.HP.currentHP + " HP remaining!");
        Debug.Log("Bombs have been recovered to " + player.bomb.maxBomb + "!");

        Lives.transform.GetChild(player.HP.currentHP - 1).gameObject.SetActive(false);
       
        foreach(GameObject bomb in player.bombs)
        {
            bomb.SetActive(true);
        }
    }
}
