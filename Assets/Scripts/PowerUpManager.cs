using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField]
    GameObject powerUpPrefab;

    [SerializeField]
    List<PowerUp> availablePowerUpList = new List<PowerUp>();

    public static PowerUpManager Instance { get; private set; }

    private void Awake()
    {
        int sessionCount = FindObjectsOfType<PowerUpManager>().Length;
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


    public static PowerUp Create(GameObject prefab, Vector3 position)
    {
        GameObject newObject = Instantiate(prefab, position, Quaternion.identity) as GameObject;
        PowerUp result = newObject.GetComponent<PowerUp>();
        return result;
    }


    public void PopRandomPowerUp(Vector3Int cellPosition)
    {
        int index = Random.Range(0, availablePowerUpList.Count);


    }
}
