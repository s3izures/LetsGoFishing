using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    int fishCollected = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        GameState.Instance.SetState(1); //temp since we have no main menu
    }

    public void ModifyFish(int amt)
    {
        fishCollected += amt;
    }
    public int GetFish()
    {
        return fishCollected;
    }
}
