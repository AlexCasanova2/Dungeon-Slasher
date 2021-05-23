using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Props/Chest")]
public class ChestTest : ScriptableObject
{
    public bool haveGold;
    public int goldToGive;
    public bool haveSpecialItem;

    public enum SpecialItem { Key }
    public SpecialItem specialItemToGive;

    public Sprite sprite;

    public string rewardText;
}
