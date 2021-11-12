using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    [SerializeField] float moveSpeed = 5f;

    BoxCollider2D boxColliderLeft;
    BoxCollider2D boxColliderRight;
    BoxCollider2D boxColliderTop;
    BoxCollider2D boxColliderBottom;
    Vector3 cellSize;

    private void Awake()
    {
        cellSize = FindObjectOfType<Grid>().cellSize;
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
        Debug.Log("boxColliderBottom" + boxColliderBottom.IsTouching(other).ToString());
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("boxColliderLeft" + boxColliderLeft.IsTouching(other).ToString());
        //Debug.Log("boxColliderRight" + boxColliderRight.IsTouching(other).ToString());
        //Debug.Log("boxColliderTop" + boxColliderTop.IsTouching(other).ToString());
        Debug.Log("boxColliderBottom" + boxColliderBottom.IsTouching(other).ToString());
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
