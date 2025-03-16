using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Set")]
public class Setting : ScriptableObject
{
    public int boardSizeX = 5;
    public int boardSizeY = 5;
    public int sizeBoardBottom = 5;
    public float timerAttackMode = 60;
}
