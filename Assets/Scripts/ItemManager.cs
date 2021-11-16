using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    GameObject powerUpPrefab;

    [SerializeField]
    List<AbstractItemSO> availablePowerUpList = new List<AbstractItemSO>();

    public static ItemManager Instance { get; private set; }

    private void Awake()
    {
        int sessionCount = FindObjectsOfType<ItemManager>().Length;
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
        //AbstractItemSO result = newObject.GetComponent<AbstractItemSO>();
        return newObject;
    }


    public void PopRandomPowerUp(Vector3Int cellPosition)
    {
        int index = Random.Range(-1, availablePowerUpList.Count);
        if (index == -1)
            return;
        var item = ItemManager.Create(powerUpPrefab, GameManager.Instance.ConvertTileToWorld(cellPosition));
        item.GetComponent<SpriteRenderer>().sprite = availablePowerUpList[index].GetSprite();

    }
}
