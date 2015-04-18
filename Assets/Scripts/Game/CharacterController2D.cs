﻿using UnityEngine;
using System.Collections;


/// <summary>
/// References:
/// https://unity3d.com/pt/learn/tutorials/modules/beginner/2d/2d-controllers
/// </summary>

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : GameBehaviour {

    public float speed = 5f;
    public float gripTime = .5f;
    public LayerMask groundLayers;
    public LayerMask wallLayers;

    readonly Vector2 feetA = new Vector2(-.23f, .05f);
    readonly Vector2 feetB = new Vector2(.23f, -.01f);
    readonly Vector2 handA = new Vector2(.20f, .02f);
    readonly Vector2 handB = new Vector2(.26f, .48f);

    Animator animator;
    bool isGrounded;
    bool isGripping;
    bool canAirJump;
    public int TotalJumps;
    public int RemainingJumps;
    public bool AirJumpEnabled = true;

    Vector3 checkpointPosition;

    Collider2D[] dumbColliders = new Collider2D[1];

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();

        ResetMultipleJump();

    }

	protected override void FixedUpdate () 
    {
        base.FixedUpdate();

        var pos = (Vector2)transform.position;

        var wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapAreaNonAlloc(pos + feetA, pos + feetB, dumbColliders, groundLayers) > 0;
        
        if(isGrounded)
        {
            transform.parent = dumbColliders[0].transform;
            // Reset Remaining Jumps
            RemainingJumps = TotalJumps -1;
        }
        else
        {
            transform.parent = null;
        }

        var mirror = new Vector2(-1, 1);

        var isGrippingRight = Physics2D.OverlapAreaNonAlloc(pos + handA, pos + handB, dumbColliders, groundLayers) > 0;
        var isGrippingLeft = Physics2D.OverlapAreaNonAlloc(pos + Vector2.Scale(handA, mirror), pos + Vector2.Scale(handB, mirror), dumbColliders, groundLayers) > 0;

        if (isGrounded || RemainingJumps > 0)
        {
            canAirJump = true;
        }

        var horizontal =  Input.GetAxis("Horizontal");
        if (isGrippingRight)
        {
            horizontal = Mathf.Clamp(horizontal, float.NegativeInfinity, 0);
        }

        if (isGrippingLeft)
        {
            horizontal = Mathf.Clamp(horizontal, 0, float.PositiveInfinity);
        }

        var sign = Mathf.Sign(horizontal);

        animator.SetBool("Mirror", sign < 0);
        animator.SetBool("IsWalking", Mathf.Abs(horizontal) > 0.3f);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("Vertical Speed", rigidbody2D.velocity.y);
        landSpeed = Mathf.Min(rigidbody2D.velocity.y, landSpeed);
        
        if (isGrounded && !wasGrounded)
        {
            animator.SetFloat("Land Speed", landSpeed);
            landSpeed = 0;
        }

        rigidbody2D.velocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y);
	}
    float landSpeed;

    protected override void Update()
    {
        base.Update();

        if (Input.GetButtonDown("Fire1"))
        {
            transform.parent = null;
			rigidbody2D.WakeUp();
			World.ShiftDimension();
        }

		if (Input.GetKeyUp(KeyCode.LeftShift))
        {
			transform.parent = null;
			rigidbody2D.WakeUp();
			World.ShiftTo(Dimensions.Green);
        }

        
        if ((isGrounded || canAirJump && AirJumpEnabled) && Input.GetButtonDown("Jump"))
        {
            if (!isGrounded)
            {
                // Decreses amount of Air Jumps
                RemainingJumps -= 1;
                
                // If player ran out of air Jumps,he can no longer jump
                if(RemainingJumps <=0) canAirJump = false;
                
                // Air Jump Velocity
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            }

            // Ground Jump Force
            rigidbody2D.AddForce(new Vector2(0, 700));
        }


        sadLayerWeight = Mathf.Lerp(sadLayerWeight, World.Dimension == Dimensions.Red && !isGrounded ? 1 : 0, Time.deltaTime * 8);
        dragWalkLayerWeight = Mathf.Lerp(dragWalkLayerWeight, World.Dimension == Dimensions.Red && isGrounded ? 1 : 0, Time.deltaTime * 8);

        animator.SetLayerWeight(1, sadLayerWeight); //1 = Sad
        animator.SetLayerWeight(2,dragWalkLayerWeight); // 2 = Sad Drag Walk
    }
    float sadLayerWeight = 0;
    float dragWalkLayerWeight = 0;

    public override void SetCheckpoint()
    {
        base.SetCheckpoint();

        checkpointPosition = transform.position;
    }

    public override void LoadCheckpoint()
    {
        base.LoadCheckpoint();

        transform.position = checkpointPosition;
        rigidbody2D.velocity = Vector2.zero;
    }

    public void Interact(GameBehaviour behaviour)
    {
        if (behaviour.GetType() == typeof(AbysmalPit))
        {
            World.LoadCheckpoint();
            ResetMultipleJump();
        }

		if (behaviour.GetType() == typeof(Spike))
		{
			World.LoadCheckpoint();
            ResetMultipleJump();
		}

		if (behaviour.GetType() == typeof(FallingPlatform))
		{
			World.LoadCheckpoint();
            ResetMultipleJump();
		}
        if (behaviour.GetType() == typeof(CollectableDoubleJump))
        {
            if (!AirJumpEnabled) AirJumpEnabled = true;
        }

        if (behaviour.GetType() == typeof(CollectableAddJump))
        {
            if (TotalJumps < 9)
            {
                TotalJumps += 1;
                RemainingJumps += 1;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position + (Vector3)handA + new Vector3(0, 0, -2), transform.position + (Vector3)handB + new Vector3(0, 0, -2));
    }

    void ResetMultipleJump()
    {
        // Default Values
        TotalJumps = 2;
        RemainingJumps = TotalJumps - 1;
    }
}
