using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBottom : MonoBehaviour
{
    private FishItem fishItem;
    public void SetFishItem(FishItem fishItem)
    {
        this.fishItem = fishItem;
    }
    public void RemoveFishItem()
    {
        this.fishItem = null;
    }
    public bool HaveFishItem()
    {
        if (this.fishItem == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public FishItem GetFishItem()
    {
        return this.fishItem;
    }
}
