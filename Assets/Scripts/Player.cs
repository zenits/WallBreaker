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
    Grid cellgrid;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("boxColliderLeft" + boxColliderLeft.IsTouching(other).ToString());
        //Debug.Log("boxColliderRight" + boxColliderRight.IsTouching(other).ToString());
        //Debug.Log("boxColliderTop" + boxColliderTop.IsTouching(other).ToString());
        //Debug.Log("boxColliderBottom" + boxColliderBottom.IsTouching(other).ToString());
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("boxColliderLeft" + boxColliderLeft.IsTouching(other).ToString());
        //Debug.Log("boxColliderRight" + boxColliderRight.IsTouching(other).ToString());
        //Debug.Log("boxColliderTop" + boxColliderTop.IsTouching(other).ToString());
        //Debug.Log("boxColliderBottom" + boxColliderBottom.IsTouching(other).ToString());
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire()
    {
        if (bombCount > 0)
        {
            var pos = transform.position;

            var cellPosition = bgTilemap.WorldToCell(transform.position);

            pos = new Vector3((int)System.Math.Truncate(transform.position.x / cellgrid.cellSize.x) * cellgrid.cellSize.x - cellgrid.cellSize.x / 2,
                            (int)System.Math.Truncate(transform.position.y / cellgrid.cellSize.y) * cellgrid.cellSize.y + cellgrid.cellSize.x / 2,
                            0);

            pos = bgTilemap.CellToWorld(cellPosition) + (cellgrid.cellSize / 2);
            //GameManager.Instance.CreateBomb(this, pos);
            Instantiate(bombPrefab, pos, Quaternion.identity);
        }
    }

}
