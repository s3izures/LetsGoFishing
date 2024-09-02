using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] List<CanvasGroup> ponds;
    [SerializeField] TextMeshProUGUI fishAmt;
    [SerializeField] TextMeshProUGUI fishCaughtText;
    [SerializeField] TextMeshProUGUI fishCaughtValueText;
    [SerializeField] TextMeshProUGUI fishDescription;
    [SerializeField] Image fishCaughtImage;
    [SerializeField] float alphaPond = 0.1f;
    [SerializeField] Button shopChest;
    [SerializeField] CanvasGroup shop;
    [SerializeField] Animator shopAnim;
    [SerializeField] Animator fishCaughtAnim;
    [SerializeField] Button adButton;

    private void Start()
    {
        HideAdButton();
    }

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

    public int GetPondAmounts()
    {
        return ponds.Count;
    }
    public void ShowOnlyThisPond(int index)
    {
        GameManager.Instance.SetActivePond(index);
        GameManager.Instance.SetCurrentFish(index);

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

        shopChest.interactable = false;
    }
    public void ShowAllPonds()
    {
        GameManager.Instance.SetActivePond(-1);

        for (int i = 0; i < ponds.Count; i++)
        {
            ponds[i].interactable = true;
            ponds[i].alpha = 1;
        }

        shopChest.interactable = true;
    }
    public void ShowCaughtFish()
    {
        fishCaughtText.text = GameManager.Instance.GetFishObject(-1).fishName;
        fishDescription.text = GameManager.Instance.GetFishObject(-1).fishDesc;
        fishCaughtValueText.text = "+ " + GameManager.Instance.GetFishObject(-1).fishValue.ToString() + " fish point(s)";
        fishCaughtImage.sprite = GameManager.Instance.GetFishObject(-1).fishImage;
        fishCaughtAnim.Play("FishCaughtReveal");
    }

    public void OpenShop()
    {
        shopAnim.Play("OpenShop");
        shop.interactable = true;
        shop.blocksRaycasts = true;
    }
    public void CloseShop()
    {
        shop.interactable = false;
        shop.blocksRaycasts = false;
        shopAnim.Play("CloseShop");
    }

    public void ShowAdButton()
    {
        adButton.interactable = true;
        adButton.gameObject.SetActive(true);
    }
    public void HideAdButton()
    {
        adButton.interactable = false;
        adButton.gameObject.SetActive(false);
    }
}
