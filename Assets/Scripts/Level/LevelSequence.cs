using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Sequence", menuName = "Level Sequence")]
public class LevelSequence : ScriptableObject
{
    public Level[] levels;
}
