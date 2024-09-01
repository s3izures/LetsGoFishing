using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FishObject", order = 0)]
public class FishObject : ScriptableObject
{
    public string fishName;
    public string fishDesc;
    public int fishValue;
    public Sprite fishImage;

    public float[] bobberChance = new float[4];
    public float reactionTime;
    public bool isAd = false;
}
