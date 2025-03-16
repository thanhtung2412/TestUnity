using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public Setting setting;
    public Board board;
    public FishSpawner fishSpawner;
    private void Awake()
    {
        instance = this;
    }
    public void GameStarted()
    {
        board.CreateMap();
    }
}
