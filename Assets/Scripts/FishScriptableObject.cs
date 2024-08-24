using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FishScriptableObject", order = 1)]
public class FishScriptableObject : ScriptableObject
{
    public string fishName;
    public int fishValue;
    public float[] bobberChance = new float[4];
    public float reactionTime;
    public bool isAd = false;
}
