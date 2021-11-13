using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject explosionPrefab;
    List<BombController> bombs = new List<BombController>();

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        int sessionCount = FindObjectsOfType<GameManager>().Length;
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


    public void CreateExplosion(Vector3 position, Player player, bool isSource = false)
    {
        var go = Instantiate(explosionPrefab, position, Quaternion.identity);
        go.GetComponent<Explosion>().isSourceOfExplosion = isSource;
    }

    public void CreateBomb(Player player, Vector3 position)
    {

    }

}
