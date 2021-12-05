using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    // -------------------------------------------------
    // Private fields
    Vector2 moveInput;
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    int droppedBomb = 0;


    // -------------------------------------------------
    // Serialized property
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] PlayerStats stats;



    // -------------------------------------------------
    // Unity Methods
    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        UpdateAnimation();
    }

    // -------------------------------------------------
    // Collision Events
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Bomb")
        {
            var bc = other.GetComponent<BombController>();
            if (bc != null && bc.playerOwner == this)
                bc.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    // -------------------------------------------------
    // Private Methods
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

    public void IncreaseAvailableBombQuantity(BombController bc)
    {
        this.droppedBomb--;
    }

    // -------------------------------------------------
    // Input System Events
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire()
    {
        if (droppedBomb < stats.maxBombCount)
        {
            var pos = transform.position;

            var cellPosition = GameManager.Instance.ConvertWorldToCell(transform.position);
            if (GameManager.Instance.ExistsBombOnCell(cellPosition))
                return;
            var bc = GameManager.Instance.CreateBomb(bombPrefab, cellPosition, stats.explosionRange);
            bc.playerOwner = this;
            bc.OnExplode += IncreaseAvailableBombQuantity;
            droppedBomb++;
        }
    }

    // -------------------------------------------------
    // Property Getter
    public PlayerStats GetStats() { return stats; }
}
