using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FishingMechanic : MonoBehaviour
{
    [Header("Fishing Mechanic - stage 1")]
    bool firstChange = true;
    bool waitingForFish = false;
    bool fishNibbled = false;
    [SerializeField] int secondsBetweenBobbing = 3;
    [SerializeField] int reactionTime = 1;

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
                    GameManager.Instance.ModifyFish(1);
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

        UIManager.Instance.ShowAllPonds();
    }

    void WaitingForFish()
    {
        //Generate fish HERE
        Debug.Log("Fish lookin time");

        //Bobbing blub blub
        StartCoroutine(WaitingForFishTime());
    }

    IEnumerator WaitingForFishTime()
    {
        yield return new WaitForSeconds(secondsBetweenBobbing);

        int catchChance = Random.Range(0, 10);
        /* CATCH PERCENTAGES:
         * 0-1 = nothing
         * 2-4 = bobber moves
         * 5-7 = bobber moves wildly
         * 8-10 = fish takes bait
         */

        if (catchChance <= 1)
        {
            Debug.Log("Nothing");
        }
        else if (catchChance <= 4) //Bobber moves
        {
            Debug.Log("Blub");
        }
        else if (catchChance <= 7) //Bobber moves wildly
        {
            Debug.Log("BLUB BLUB BLUB");
        }
        else if (catchChance <= 10) //Fish takes bait
        {
            Debug.Log("NYOM");
            waitingForFish = false;
            fishNibbled = true;
        }

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
