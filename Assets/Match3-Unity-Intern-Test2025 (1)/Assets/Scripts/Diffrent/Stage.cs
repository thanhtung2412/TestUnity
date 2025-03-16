using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private Collider2D col;
    private Camera cam;
    [SerializeField] private float timeAiAction = 0.5f;
    private float currentTimeAiAction;
    private float timeAttackMode;
    private void Start()
    {
        cam = Camera.main;
        currentTimeAiAction = timeAiAction;
        timeAttackMode = StageManager.instance.setting.timerAttackMode;
    }
    private void Update()
    {
        if(GameManager.instance.gameStates == GameStates.gameAutoPlay)
        {
            currentTimeAiAction -= Time.deltaTime;
            if(currentTimeAiAction <= 0)
            {
                GameAutoPlay();
                currentTimeAiAction = timeAiAction;
            }
            return;
        }
        if (GameManager.instance.gameStates == GameStates.gameAutoLose)
        {
            currentTimeAiAction -= Time.deltaTime;
            if (currentTimeAiAction <= 0)
            {
                GameAutoLost();
                currentTimeAiAction = timeAiAction;
            }
            return;
        }
        if (GameManager.instance.gameStates == GameStates.gameStarted)
        {
            GamePlay();
            return;
        }
        if (GameManager.instance.gameStates == GameStates.gameAttackMode)
        {
            GameAttackMode();           
        }
    }
    public void GamePlay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider != null)
            {
                col = hit.collider;
                CellBoard cell = col.GetComponent<CellBoard>();
                if(cell != null && cell.HaveFishItem())
                {
                    FishItem newFish = cell.GetFishItem();
                    CheckWhenMoveFishToCellBottom(newFish);
                    cell.RemoveFishItem();
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ResetRaycast();
        }
    }
    private void GameAutoPlay()
    {        
        int num = StageManager.instance.fishSpawner.allFishHaveSpawn.Count;
        if(num <= 0)
        {
            return;
        }
        int rand = Random.Range(0, num);
        FishItem newFish = StageManager.instance.fishSpawner.allFishHaveSpawn[rand];
        CheckWhenMoveFishToCellBottom(newFish);

    }
    private void GameAutoLost()
    {
        int num = StageManager.instance.fishSpawner.allFishHaveSpawn.Count;
        if (num <= 0)
        {
            return;
        }
        int rand = Random.Range(0, num);
        FishItem newFish = StageManager.instance.fishSpawner.allFishHaveSpawn[rand];
        CheckWhenMoveFishToCellBottom(newFish);
        StageManager.instance.board.CheckFishBottomAlwaysLose();
    }
    private void GameAttackMode()
    {
        if(timeAttackMode > 0 && StageManager.instance.fishSpawner.allFishHaveSpawn.Count > 0)
        {
            timeAttackMode -= Time.deltaTime;
            UIManager.instance.UpdateTimerUI(timeAttackMode);
        }
        else if(StageManager.instance.fishSpawner.allFishHaveSpawn.Count > 0)
        {
            timeAttackMode = 0;
            UIManager.instance.UpdateTimerUI(timeAttackMode);
            GameManager.instance.gameStates = GameStates.gameOver;
            UIManager.instance.ShowPanelLose();
        }
        if (Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                col = hit.collider;
                CellBoard cell = col.GetComponent<CellBoard>();
                if (cell != null && cell.HaveFishItem())
                {
                    FishItem newFish = cell.GetFishItem();
                    CheckWhenMoveFishToCellBottom(newFish);
                    cell.RemoveFishItem();
                    StageManager.instance.board.AddFishForCellBotton();
                }

                CellBottom cellBottom = col.GetComponent<CellBottom>();
                if(cellBottom != null && cellBottom.HaveFishItem())
                {                 
                    FishItem fish = cellBottom.GetFishItem();
                    cellBottom.RemoveFishItem();
                    StageManager.instance.board.RemoveBottomFish(fish);
                    fish.MoveFirstPosition();
                    fish.SetFishForFirstCell();
                    StageManager.instance.board.UpdateFishPositionInCellBottom();
                    StageManager.instance.board.UpdateFishForCellBottom();
                }            
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ResetRaycast();
        }
    }

    private void CheckWhenMoveFishToCellBottom(FishItem newFish)
    {    
        StageManager.instance.board.AddBottomFishs(newFish);
        StageManager.instance.fishSpawner.ReMoveFishHaveSpawn(newFish);
        newFish.MoveToTarget(StageManager.instance.board.GetPositionCellBottom());      
        StageManager.instance.board.CheckNumberFishBottom();
        StageManager.instance.board.UpdateFishPositionInCellBottom();
        CheckGameOver();
    }
    private void CheckGameWin()
    {
        if(StageManager.instance.fishSpawner.allFishHaveSpawn.Count <= 0)
        {
            GameManager.instance.gameStates = GameStates.gameOver;
            UIManager.instance.ShowPanelWin();
        }
    }
    private void CheckGameLose()
    {
        if(StageManager.instance.board.bottomFishs.Count >= 5)
        {
            GameManager.instance.gameStates = GameStates.gameOver;
            UIManager.instance.ShowPanelLose();
        }
    }
    private void CheckGameOver()
    {
        CheckGameWin();
        CheckGameLose();
    }
    public void ResetRaycast()
    {
        col = null;
    }
}
