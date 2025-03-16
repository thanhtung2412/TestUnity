using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public int countFishType = 3;
    private List<FishTypes> fishTypes = new List<FishTypes>();
    public List<FishItem> numberFishPrefabs;
    public List<FishItem> allFishHaveSpawn;
    private void Start()
    {
        for(int i = 0; i < System.Enum.GetValues(typeof(FishTypes)).Length; i++)
        {
            for(int j = 0; j < countFishType; j++)
            {
                fishTypes.Add((FishTypes)i);
            }
        }
    }
    public void SpawnFish(Vector3 pos, out FishItem items)
    {
        int rand = Random.Range(0, fishTypes.Count);
        FishItem newFishItem = GetFish(fishTypes[rand]);
        items = Instantiate(newFishItem, pos,Quaternion.identity);
        items.SetFishTypes(fishTypes[rand]);
        AddAllFishHaveSpawn(items);
        fishTypes.RemoveAt(rand);
    }
    public void AddAllFishHaveSpawn(FishItem item)
    {
        allFishHaveSpawn.Add(item);
    }
    public void ReMoveFishHaveSpawn(FishItem items)
    {
        allFishHaveSpawn.Remove(items);
    }
    private FishItem GetFish(FishTypes fishType)
    {
        return numberFishPrefabs[(int)fishType];
    }
}
