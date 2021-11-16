using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

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




    public Vector3 ConvertTileToWorld(Vector3Int cellPosition)
    {
        var maps = FindObjectsOfType<Tilemap>();
        var wallTilemap = maps.Where(x => x.tag == "Walls").SingleOrDefault();
        var pos = transform.position + new Vector3(wallTilemap.cellSize.x * cellPosition.x, wallTilemap.cellSize.y  * cellPosition.y, 0f);
        return pos;
    }
    public Vector3Int ConvertWorldToCell(Vector3 worldPosition)
    {
        var maps = FindObjectsOfType<Tilemap>();
        var wallTilemap = maps.Where(x => x.tag == "Walls").SingleOrDefault();
        Vector3Int currentCell = wallTilemap.WorldToCell(worldPosition);
        return currentCell;
    }
}
