using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject itemPrefab;

    [SerializeField]
    List<ItemSO> availablePowerUpList = new List<ItemSO>();

    public static ItemSpawner Instance { get; private set; }

    private void Awake()
    {
        int sessionCount = FindObjectsOfType<ItemSpawner>().Length;
        if (sessionCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public static GameObject Create(GameObject prefab, Vector3 position)
    {
        GameObject newObject = Instantiate(prefab, position, Quaternion.identity) as GameObject;
        return newObject;
    }


    public void PopRandomPowerUp(Vector3Int cellPosition)
    {
        int index = Random.Range(-1, availablePowerUpList.Count);
        if (index == -1)
            return;
        var item = ItemSpawner.Create(itemPrefab, GameManager.Instance.ConvertTileToWorld(cellPosition));
        item.GetComponent<SpriteRenderer>().sprite = availablePowerUpList[index].GetSprite();
        item.GetComponent<Item>().item = availablePowerUpList[index];

    }
}
