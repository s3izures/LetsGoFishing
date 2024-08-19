using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] List<CanvasGroup> ponds;
    [SerializeField] TextMeshProUGUI fishAmt;

    private void Update()
    {
        fishAmt.text = GameManager.Instance.GetFish().ToString();
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
        for (int i = 0; i < ponds.Count; i++)
        {
            if (i != index)
            {
                ponds[i].interactable = false;
                ponds[i].alpha = 0.5f;
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
        for (int i = 0; i < ponds.Count; i++)
        {
            ponds[i].interactable = true;
            ponds[i].alpha = 1;
        }
    }
}
