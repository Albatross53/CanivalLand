using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "ScroptableObj/DialogueData", order = int.MaxValue)]
public class DialogueData : ScriptableObject
{
    [TextArea] public string[] Sentence;
}
