using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour {
    [System.Serializable]
    public class DropItem
    {
        public string name;
        public GameObject item;
        public int dropRarity;
    }

    public List<DropItem> LootTable = new List<DropItem>();
    public int dropChance;
   
   
    public void calculateLoot()
    {
        int cal_dropChance = Random.Range(0, 101);
        if (cal_dropChance>dropChance)
        {
            return;
        }

        if (cal_dropChance<=dropChance)
        {
            int itemWeight = 0;
            for (int i = 0; i < LootTable.Count; i++)
            {
                itemWeight += LootTable[i].dropRarity;
            }
          
            int randomValue = Random.Range(0, itemWeight);
            for (int j = 0; j < LootTable.Count; j++)
            {
                if (randomValue<=LootTable[j].dropRarity)
                {                
                    Instantiate(LootTable[j].item, transform.position, Quaternion.identity);
                }
                randomValue -= LootTable[j].dropRarity;
            }
        }
    }
}
