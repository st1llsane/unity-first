using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
  [SerializeField] private float moveSpeed;
  private Rigidbody2D body;
  private SpriteRenderer sprite;
  private Animator animator;
  private bool isLastMoveDirectionRight = true;
  private bool isMoving = false;

  // AWAKE

  private void Awake() {
    RegisterComponents();
    SetInitialValues();
  }

  private void RegisterComponents() {
    body = GetComponent<Rigidbody2D>();
    sprite = GetComponent<SpriteRenderer>();
    animator = GetComponent<Animator>();
  }

  private void SetInitialValues() {
    body.transform.position = new Vector2(0, -1.85f);
    body.mass = 10;
    body.gravityScale = 6;
  }

  // UPDATE

  private void Update() {
    HandleMovement();
    HandleAnimations();

    HandleRestartGame();
    HandleQuitGame();
  }

  // MOVEMENT

  private void HandleMovement() {
    HandleLeftRightMoves();
    HandleSpriteFlip();
    HandleJump();
  }

  private void HandleLeftRightMoves() {
    float horizontalInput = Input.GetAxis("Horizontal") * moveSpeed;
    body.linearVelocity = new Vector2(horizontalInput, body.linearVelocity.y);
    HandleIsMoving(horizontalInput);

  }

  private void HandleIsMoving(float horizontalInput) {
    isMoving = horizontalInput != 0;
  }

  // SPRITE FLIP

  private void HandleSpriteFlip() {
    HandleLastMoveDirection();
    FlipSpriteInRelationOnLastMoveDirection();
  }

  private void HandleLastMoveDirection() {
    if (Input.GetKey(KeyCode.A)) {
      isLastMoveDirectionRight = false;
    }
    else if (Input.GetKey(KeyCode.D)) {
      isLastMoveDirectionRight = true;
    }
  }

  private void FlipSpriteInRelationOnLastMoveDirection() {
    if (isLastMoveDirectionRight) {
      FlipSpriteRight();
    }
    else {
      FlipSpriteLeft();
    }
  }

  private void FlipSpriteRight() {
    sprite.flipX = false;
  }

  private void FlipSpriteLeft() {
    sprite.flipX = true;
  }

  // JUMP 

  private void HandleJump() {
    if (Input.GetKey(KeyCode.Space)) {
      body.linearVelocity = new Vector2(body.linearVelocity.x, moveSpeed);
    }
  }

  // ANIMATIONS

  private void HandleAnimations() {
    animator.SetBool("Move", isMoving);
  }


  // GAME STATE

  private void HandleRestartGame() {
    if (Input.GetKey(KeyCode.R)) {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  }

  private void HandleQuitGame() {
    if (Input.GetKey(KeyCode.Escape)) {
      Application.Quit();
    }
  }
}