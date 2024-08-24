using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] List<FishScriptableObject> fishes;
    [SerializeField] List<Animator> rippleAnim;
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
    }

    public void SetActivePond(int index)
    {
        pondActive = index;
    }
    public int GetActivePond()
    {
        return pondActive;
    }

    public void GetWhichFishType()
    {
        fishToCatch = Random.Range(0, fishes.Count);
        Debug.Log(fishes[fishToCatch].fishName);
    }

    public void AddFish()
    {
        fishCollected += fishes[fishToCatch].fishValue;
    }
    public void TakeFish(int amt)
    {
        fishCollected -= amt;
    }
    public int GetFish()
    {
        return fishCollected;
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
