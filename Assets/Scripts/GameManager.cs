using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] List<FishScriptableObject> fishes;
    [SerializeField] List<Animator> rippleAnim;
    int[] fishTypeInPond = new int[4];
    int pondActive = -1; //means none
    int fishToCatch = 0;
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

        GenerateFish();
    }

    public void SetActivePond(int index)
    {
        pondActive = index;
    }
    public int GetActivePond()
    {
        return pondActive;
    }

    public void GenerateFish()
    {
        for (int i = 0; i < UIManager.Instance.GetPondAmounts(); i++)
        {
            fishTypeInPond[i] = Random.Range(0, fishes.Count);
        }
    }
    public void SetCurrentFish(int pond)
    {
        fishToCatch = fishTypeInPond[pond];
    }

    public void ModifyFishAmt(int amt)
    {
        fishCollected += amt;
    }
    public int GetFishAmount()
    {
        return fishCollected;
    }
    public FishScriptableObject GetFishObject(int index)
    {
        if (index == -1)
        {
            return fishes[fishToCatch]; //Get current fish
        }
        return fishes[index];
    }

    public enum BobberState
    {
        Nothing,
        Small,
        Big,
        Catch
    }
    public float GetBobberState(BobberState state)
    {
        return fishes[fishToCatch].bobberChance[(int)state];
    }
    public void SwitchRippleAnim(int index, int state)
    {
        rippleAnim[index].SetInteger("Intensity", state);
    }
    public float GetReactionTime()
    {
        return fishes[fishToCatch].reactionTime;
    }
}
