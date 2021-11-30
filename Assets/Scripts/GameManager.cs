using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject explosionPrefab;

    public static GameManager Instance { get; private set; }


    List<Tuple<Vector3Int, BombController>> bombs = new List<Tuple<Vector3Int, BombController>>();

    Grid cellgrid;
    Tilemap bgTilemap;
    private void Awake()
    {
        int sessionCount = FindObjectsOfType<GameManager>().Length;
        if (sessionCount > 1)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        var maps = FindObjectsOfType<Tilemap>();
        bgTilemap = maps.Where(x => x.tag == "Background").SingleOrDefault();
        cellgrid = FindObjectOfType<Grid>();
    }


    public void CreateExplosion(Vector3 position, Player player, bool isSource = false)
    {
        var go = Instantiate(explosionPrefab, position, Quaternion.identity);
        go.GetComponent<Explosion>().isSourceOfExplosion = isSource;
    }


    public BombController CreateBomb(GameObject bombPrefab, Vector3Int cellPosition, int explosionRange)
    {
        var pos = bgTilemap.CellToWorld(cellPosition) + (cellgrid.cellSize / 2);
        var bc = BombController.Create(bombPrefab, pos, explosionRange);
        bc.OnExplode += OnBombExplode;
        bombs.Add(new Tuple<Vector3Int, BombController>(cellPosition, bc));
        return bc;
    }

    public void OnBombExplode(BombController bombController)
    {
        var bc = bombs.First(i => i.Item2 == bombController);
        bombs.Remove(bc);
    }

    public bool ExistsBombOnCell(Vector3Int cellPosition)
    {
        return bombs.Exists(i => i.Item1.Equals(cellPosition));
    }

    public Vector3 ConvertTileToWorld(Vector3Int cellPosition)
    {
        var maps = FindObjectsOfType<Tilemap>();
        var wallTilemap = maps.Where(x => x.tag == "Walls").SingleOrDefault();
        var pos = transform.position + new Vector3(wallTilemap.cellSize.x / 2 + wallTilemap.cellSize.x * cellPosition.x, wallTilemap.cellSize.y / 2 + wallTilemap.cellSize.y * cellPosition.y, 0f);
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
