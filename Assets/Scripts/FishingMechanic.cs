using System.Collections;
using UnityEngine;

public class FishingMechanic : MonoBehaviour
{
    [Header("Fishing Mechanic - stage 1")]
    bool firstChange = true;
    bool waitingForFish = false;
    bool fishNibbled = false;
    [SerializeField] int secondsBetweenBobbing = 3;
    float reactionTime = 3;

    void Update()
    {
        if (GameState.Instance.GetState() == GameState.State.GameFishing)
        {
            if (firstChange) //trigger once
            {
                firstChange = false;
                waitingForFish = true;

                WaitingForFish();
            }

            //Check for tap when fish nibbled
            if (GestureManager.Instance.OnTap())
            {
                if (fishNibbled)
                {
                    //Get fish
                    GameManager.Instance.AddFish();
                    Debug.Log("Wawa fish");
                    ResetEverything();
                }
                else if (waitingForFish)
                {
                    Debug.Log("You reeled in too early");
                    ResetEverything();
                }
            }
        }
    }

    void ResetEverything()
    {
        StopAllCoroutines();
        GameState.Instance.SetState((int)GameState.State.GameIdle);

        firstChange = true;
        waitingForFish = false;
        fishNibbled = false;
        GameManager.Instance.SwitchRippleAnim(GameManager.Instance.GetActivePond(), (int)GameManager.BobberState.Nothing);

        UIManager.Instance.ShowAllPonds();
    }

    void WaitingForFish()
    {
        GameManager.Instance.GetWhichFishType();
        reactionTime = GameManager.Instance.GetReactionTime();
        Debug.Log("Fish lookin time");

        //Bobbing blub blub
        StartCoroutine(WaitingForFishTime());
    }

    IEnumerator WaitingForFishTime()
    {
        yield return new WaitForSeconds(secondsBetweenBobbing);

        float catchChance = Random.Range(0f, 100f);
        int animState = 0;

        if (catchChance <= GameManager.Instance.GetBobberState(GameManager.BobberState.Nothing))
        {
            animState = (int)GameManager.BobberState.Nothing;
            Debug.Log("Nothing");
        }
        else if (catchChance <= GameManager.Instance.GetBobberState(GameManager.BobberState.Small)) //Bobber moves
        {
            animState = (int)GameManager.BobberState.Small;
            Debug.Log("Blub");
        }
        else if (catchChance <= GameManager.Instance.GetBobberState(GameManager.BobberState.Big)) //Bobber moves wildly
        {
            animState = (int)GameManager.BobberState.Big;
            Debug.Log("BLUB BLUB BLUB");
        }
        else if (catchChance <= GameManager.Instance.GetBobberState(GameManager.BobberState.Catch)) //Fish takes bait
        {
            animState = (int)GameManager.BobberState.Catch;
            Debug.Log("NYOM");
            waitingForFish = false;
            fishNibbled = true;
        }

        GameManager.Instance.SwitchRippleAnim(GameManager.Instance.GetActivePond(), animState);

        if (waitingForFish)
        {
            StartCoroutine(WaitingForFishTime());
        }
        else if (fishNibbled)
        {
            waitingForFish = false;
            StartCoroutine(GrabFish());
        }
    }

    IEnumerator GrabFish()
    {
        //Give reaction time
        yield return new WaitForSeconds(reactionTime);

        if (fishNibbled)
        {
            //If player doesn't tap in time
            Debug.Log("Fish Got Away, boohoo");
            ResetEverything();
        }
    }

}
