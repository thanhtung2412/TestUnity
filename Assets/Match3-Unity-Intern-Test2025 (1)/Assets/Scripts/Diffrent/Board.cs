using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private int boardSizeX;
    private int boardSizeY;
    private int boardBottomSize;
    [SerializeField] private GameObject cellBoardPrefab;
    [SerializeField] private GameObject cellBottomPrefab;
    private CellBoard[,] cellBoards;
    public List<FishItem> bottomFishs = new List<FishItem>();
    public List<CellBottom> cellBottoms = new List<CellBottom>();
    private void Start()
    {
        boardSizeX = StageManager.instance.setting.boardSizeX;
        boardSizeY = StageManager.instance.setting.boardSizeY;
        boardBottomSize = StageManager.instance.setting.sizeBoardBottom;
        cellBoards = new CellBoard[boardSizeX, boardSizeY];
        
    }
    public void CreateBoard()
    {
        if (cellBoardPrefab == null) return;
        Vector3 pos = new Vector3(-boardSizeX * 0.5f + 0.5f, -boardSizeY * 0.5f + 0.5f, 0f);
        for(int i = 0; i < boardSizeX; i++)
        {
            for(int j = 0;j < boardSizeY; j++)
            {
                GameObject newCellBoard = Instantiate(cellBoardPrefab);
                newCellBoard.transform.position = pos + new Vector3(i,j, 0);
                cellBoards[i, j] = newCellBoard.GetComponent<CellBoard>();
                FishItem fish;
                StageManager.instance.fishSpawner.SpawnFish(newCellBoard.transform.position, out fish);
                fish.SetCellBoard(cellBoards[i, j]);
                cellBoards[i, j].SetFishItem(fish);
            }
        }
    }
  
    public void CreateCellBottom()
    {
        Vector3 pos = new Vector3(-boardSizeX * 0.5f, -boardSizeY * 0.5f - 1f, 0);
        for(int i = 0; i < boardBottomSize; i++)
        {
            GameObject newCellBottom = Instantiate(cellBottomPrefab);
            newCellBottom.transform.position = pos + new Vector3(i * 1f, 0f , 0f);
            CellBottom obj = newCellBottom.GetComponent<CellBottom>();
            cellBottoms.Add(obj);
        }
    }
    public Vector3 GetPositionCellBottom()
    {
        int num = bottomFishs.Count;
        Vector3 pos = new Vector3(-boardSizeX * 0.5f, -boardSizeY * 0.5f - 1f, 0);
        Vector3 newPos = pos + new Vector3((num - 1) * 1f, 0f, 0f);
        return newPos;
    }
    public void AddBottomFishs(FishItem fish)
    {
        bottomFishs.Add(fish);
    }
    public void RemoveBottomFish(FishItem fish)
    {
        bottomFishs.Remove(fish);
    }
    public void AddFishForCellBotton()
    {
        if (bottomFishs.Count <= 0) return;
        int i = bottomFishs.Count;
        cellBottoms[i - 1].SetFishItem(bottomFishs[i - 1]);
    }
    public void UpdateFishForCellBottom()
    {
        if (bottomFishs.Count <= 0) return;
        for(int i =0;i < bottomFishs.Count;i++)
        {
            cellBottoms[i].SetFishItem(bottomFishs[i]);
        }
    }
    public void CreateMap()
    {
        CreateBoard();
        CreateCellBottom();
    }
    public void CheckNumberFishBottom()
    {
        Dictionary<FishTypes, List<FishItem>> fishGroups = new Dictionary<FishTypes, List<FishItem>>();
        foreach(FishItem fish in bottomFishs)
        {
            if (!fishGroups.ContainsKey(fish.GetFishType()))
            {
                fishGroups[fish.GetFishType()] = new List<FishItem>();
            }
            fishGroups[fish.GetFishType()].Add(fish);
        }
        foreach(KeyValuePair<FishTypes, List<FishItem>> group in fishGroups)
        { 
            if(group.Value.Count >= 3) 
            {
                foreach(FishItem fish in group.Value)
                {
                    fish.AnimationDestroyFish();
                }
                bottomFishs.RemoveAll(f => group.Value.Contains(f));
                break;
            }
                
        }
    }
    public void CheckFishBottomAlwaysLose()
    {
        Dictionary<FishTypes, List<FishItem>> fishGroup = new Dictionary<FishTypes, List<FishItem>>();
        foreach(FishItem fish in bottomFishs)
        {
            if (!fishGroup.ContainsKey(fish.GetFishType()))
            {
                fishGroup[fish.GetFishType()] = new List<FishItem>();
            }
            fishGroup[fish.GetFishType()].Add(fish);
        }
        foreach(KeyValuePair<FishTypes, List<FishItem>> group in fishGroup)
        {
            if(group.Value.Count >= 2)
            {
                StageManager.instance.fishSpawner.allFishHaveSpawn.RemoveAll(f => group.Value.Contains(f));
            }
        }
    }
    public void UpdateFishPositionInCellBottom()
    {
        Vector3 pos = new Vector3(-boardSizeX * 0.5f, -boardSizeY * 0.5f - 1f, 0);
        for(int i = 0; i < bottomFishs.Count; i++)
        {
            bottomFishs[i].MoveToTarget(pos + new Vector3(i * 1f, 0f,0f));
        }
    }
}
