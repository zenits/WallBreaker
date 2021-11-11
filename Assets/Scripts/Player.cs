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

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
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
        Debug.Log(myRigidbody2D.velocity.x + " - " + myRigidbody2D.velocity.y);
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
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
