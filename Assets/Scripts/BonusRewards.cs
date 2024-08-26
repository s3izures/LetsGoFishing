using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRewards : MonoBehaviour
{
    static public BonusRewards Instance;
    [SerializeField] int multiplier = 2;
    [SerializeField] float multiplerDuration = 60;
    bool multiplierIsActive = false;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance)
        {
            Destroy(this);
        }
    }

    public bool isAdActive()
    {
        if (multiplierIsActive)
        {
            return true;
        }
        return false;
    }
    public void LimitedMultiplier()
    {
        multiplierIsActive = true;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(multiplerDuration);
        multiplierIsActive = false;
    }

    public int GetMultipliers()
    {
        if (multiplierIsActive)
        {
            return multiplier;
        }
        return 1;
    }
}
