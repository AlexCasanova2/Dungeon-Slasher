using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public string questName, questDescription;
    public int questGold;
}
