using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestDatabase", menuName = "ScriptableObjects/QuestDatabase")]
public class QuestDatabase : ScriptableObject
{
    public List<Quest> Quests;
}