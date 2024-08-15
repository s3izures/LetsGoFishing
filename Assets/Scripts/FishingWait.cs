using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingWait : MonoBehaviour
{
    bool firstChange = true;
    bool waitingForFish = false;
    bool fishNibbled = false;
    [SerializeField] int secondsBetweenBobbing = 3;
    [SerializeField] int reactionTime = 1;

    private void Start()
    {
        firstChange = true;
        waitingForFish = false;
        fishNibbled = false;
    }

    void Update()
    {
        if (firstChange && GameState.Instance.GetState() == GameState.State.GameFishing)
        {
            waitingForFish = true;
            firstChange = false;

            if (waitingForFish)
            {
                WaitingForFish();
            }
            else if (fishNibbled)
            {
                GrabFish();
            }
        }
    }

    void WaitingForFish()
    {
        //Generate fish
        Debug.Log("Fish lookin time");

        //Waiting for fish time
        StartCoroutine(WaitingForFishTime());

        //Fish catch time = tap at the right moment
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
            StopCoroutine(WaitingForFishTime());
        }

        if (waitingForFish)
        {
            WaitingForFishTime();
        }
    }

    IEnumerator GrabFish()
    {
        //Check for tap


        //Give reaction time
        yield return new WaitForSeconds(reactionTime);


        //If Player doesn't tap in time
        Debug.Log("Fish Got Away, boohoo");
        firstChange = false; //Reset
    }
}
