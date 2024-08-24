using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;
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


    State state;
    public enum State
    {
        MainMenu = 0,
        GameIdle = 1,
        GameFishing = 2
    }
    public State GetState()
    {
        return state;
    }
    public void SetState(int setState)
    {
        state = (State)setState;
    }

}
