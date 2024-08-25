using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] List<CanvasGroup> ponds;
    [SerializeField] TextMeshProUGUI fishAmt;
    [SerializeField] TextMeshProUGUI fishCaught;
    [SerializeField] float alphaPond = 0.1f;

    private void Update()
    {
        fishAmt.text = GameManager.Instance.GetFishAmount().ToString();
    }

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

    public void ShowOnlyThisPond(int index)
    {
        GameManager.Instance.SetActivePond(index);

        for (int i = 0; i < ponds.Count; i++)
        {
            if (i != index)
            {
                ponds[i].interactable = false;
                ponds[i].alpha = alphaPond;
            }
            else
            {
                ponds[i].interactable = false; //Only show, but can't interact
                ponds[i].alpha = 1f;
            }
        }
    }
    public void ShowAllPonds()
    {
        GameManager.Instance.SetActivePond(-1);

        for (int i = 0; i < ponds.Count; i++)
        {
            ponds[i].interactable = true;
            ponds[i].alpha = 1;
        }
    }
    public void ShowCaughtFish()
    {
        fishCaught.text = ("You Caught " + GameManager.Instance.GetFishObject(-1).fishName);
    }
}
