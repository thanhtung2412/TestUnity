using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishItem : MonoBehaviour
{
    private FishTypes fishTypes;
    private CellBoard firstCellBoard;
    [SerializeField] private float timeMoveToTarget = 0.2f;
    
    public void SetCellBoard(CellBoard value)
    {
        firstCellBoard = value;
    }
    public void SetFishTypes(FishTypes type)
    {
        fishTypes = type;
    }
    public FishTypes GetFishType()
    {
        return fishTypes;
    }
    public void SetFishForFirstCell()
    {
        firstCellBoard.SetFishItem(this);
    }
    public void MoveFirstPosition()
    {
        if (firstCellBoard == null) return;
        transform.DOMove(firstCellBoard.gameObject.transform.position, timeMoveToTarget);
    }
    public void MoveToTarget(Vector3 pos)
    {
        transform.DOMove(pos, timeMoveToTarget);
        AnimationMoveToTarget();
    }
    public void AnimationMoveToTarget()
    {
        transform.DOScale(transform.localScale * 0.7f, 0.05f).OnComplete(() =>
        {
            transform.DOScale(Vector3.one, timeMoveToTarget - 0.05f);
        });
    }
    public void AnimationDestroyFish()
    {
        transform.DOScale(0.1f, 0.1f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
