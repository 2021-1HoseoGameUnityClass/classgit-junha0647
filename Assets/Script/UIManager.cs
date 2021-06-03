using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance = null;
    public static UIManager instance { get { return _instance; } }

    [SerializeField] private GameObject[] playerHp_Objs = null;

    void Awake()
    {
        _instance = this;
    }

    public void PlayerHP()
    {
        int minusHP = 3 - DataManager.instance.playerHP;
        for(int i = 0; i < minusHP; i++)
        {
            playerHp_Objs[i].SetActive(false);
        }
    }
}
