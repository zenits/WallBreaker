using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class Explosion : MonoBehaviour
{

    Animator myAnimator;

    Tilemap wallTilemap;
    Tilemap stoneTilemap;
    [SerializeField] float propageDelay = 0.2f;
    float elapsedDelay = 0f;

    int offsetTop = 1;
    int offsetBottom = -1;
    int offsetRight = 1;
    int offsetLeft = -1;

    public bool isSourceOfExplosion { get; set; } = false;

    int explosionRange = 1;
    int currentRange = 0;

    BoxCollider2D myCollider;


    public static Explosion Create(GameObject prefab, Vector3 position, int explosionRange = 1, bool isSource = false)
    {
        GameObject newObject = Instantiate(prefab, position, Quaternion.identity) as GameObject;
        Explosion result = newObject.GetComponent<Explosion>();
        result.explosionRange = explosionRange;
        result.isSourceOfExplosion = isSource;
        return result;
    }

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();

        var maps = FindObjectsOfType<Tilemap>();
        wallTilemap = maps.Where(x => x.tag == "Walls").SingleOrDefault();
        stoneTilemap = maps.Where(x => x.tag == "Stones").SingleOrDefault();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Propagate()
    {
        if (!isSourceOfExplosion || currentRange >= explosionRange)
            return;
        offsetRight = (PropagateExplosion(offsetRight, 0)) ? offsetRight + 1 : 0;
        offsetLeft = PropagateExplosion(offsetLeft, 0) ? offsetLeft - 1 : 0;
        offsetTop = PropagateExplosion(0, offsetTop) ? offsetTop + 1 : 0;
        offsetBottom = PropagateExplosion(0, offsetBottom) ? offsetBottom - 1 : 0;
        currentRange++;
    }

    private bool PropagateExplosion(int offsetX, int offsetY)
    {
        if (offsetX == 0 && offsetY == 0)
            return false; ;

        Vector3Int currentCell = wallTilemap.WorldToCell(transform.position);
        currentCell.x += offsetX;
        currentCell.y += offsetY;
        var isFree = wallTilemap.GetTile(currentCell) == null && stoneTilemap.GetTile(currentCell) == null;
        if (isFree)
        {
            var pos = transform.position + new Vector3(wallTilemap.cellSize.x * offsetX, wallTilemap.cellSize.y * offsetY, 0f);
            Explosion.Create(this.gameObject, pos);
            //GameManager.Instance.CreateExplosion(pos, null, false);
            return true;
        }
        else if (stoneTilemap.GetTile(currentCell) != null)
            DestroyStone(currentCell);
        return false;
    }

    void DestroyStone(Vector3Int cell)
    {
        stoneTilemap.SetTile(cell, null);
        ItemManager.Instance.PopRandomPowerUp(cell);
    }
    // Update is called once per frame
    void Update()
    {
        if (elapsedDelay >= propageDelay)
        {
            elapsedDelay = 0f;
            Propagate();
        }
        if (myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            Destroy(gameObject);

        elapsedDelay += Time.deltaTime;
    }

}
