using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    [SerializeField] int bombCount = 1;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] int explosionRange = 1;


    Grid cellgrid;
    int droppedBomb = 0;
    BoxCollider2D boxColliderLeft;
    BoxCollider2D boxColliderRight;
    BoxCollider2D boxColliderTop;
    BoxCollider2D boxColliderBottom;

    Tilemap bgTilemap;

    private void Awake()
    {
        var maps = FindObjectsOfType<Tilemap>();
        bgTilemap = maps.Where(x => x.tag == "Background").SingleOrDefault();

        cellgrid = FindObjectOfType<Grid>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        var colliders = GetComponents<BoxCollider2D>();
        foreach (var collider in colliders)
        {
            if (collider.offset.x == 1)
                boxColliderRight = collider;
            else if (collider.offset.x == -1)
                boxColliderLeft = collider;
            else if (collider.offset.y == 1)
                boxColliderTop = collider;
            else if (collider.offset.y == -1)
                boxColliderBottom = collider;
        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void FixedUpdate()
    {
        Move();
        UpdateAnimation();
    }
    void Update()
    {
    }

    void Move()
    {
        myRigidbody2D.velocity = moveInput * moveSpeed;
    }

    void UpdateAnimation()
    {
        {
            myAnimator.SetBool("IsWalkingUp", false);
            myAnimator.SetBool("IsWalkingDown", false);
            myAnimator.SetBool("IsWalkingRight", false);
            myAnimator.SetBool("IsWalkingLeft", false);
        }
        if (myRigidbody2D.velocity.y < 0)
        {
            myAnimator.SetBool("IsWalkingUp", false);
            myAnimator.SetBool("IsWalkingDown", true);
        }
        if (myRigidbody2D.velocity.x > 0)
        {
            myAnimator.SetBool("IsWalkingRight", true);
            myAnimator.SetBool("IsWalkingLeft", false);
        }
        if (myRigidbody2D.velocity.x < 0)
        {
            myAnimator.SetBool("IsWalkingRight", false);
            myAnimator.SetBool("IsWalkingLeft", true);
        }
        if (myRigidbody2D.velocity.y > 0)
        {
            myAnimator.SetBool("IsWalkingUp", true);
            myAnimator.SetBool("IsWalkingDown", false);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Bomb")
        {
            var bc = other.GetComponent<BombController>();
            if (bc != null && bc.playerOwner == this)
                bc.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire()
    {
        if (droppedBomb < bombCount)
        {
            var pos = transform.position;

            var cellPosition = bgTilemap.WorldToCell(transform.position);
            if (GameManager.Instance.ExistsBombOnCell(cellPosition))
                return;
            pos = bgTilemap.CellToWorld(cellPosition) + (cellgrid.cellSize / 2);
            var bc = GameManager.Instance.CreateBomb(bombPrefab, cellPosition, explosionRange);
            //var bomb = Instantiate(bombPrefab, pos, Quaternion.identity);
            //bomb.GetComponent<BombController>().playerOwner = this;
            //var bc = BombController.Create(bombPrefab, pos, explosionRange);
            bc.playerOwner = this;
            bc.OnExplode += IncreaseAvailableBombQuantity;
            droppedBomb++;
        }
    }

    public void IncreaseAvailableBombQuantity(BombController bc)
    {
        this.droppedBomb--;
    }
}
