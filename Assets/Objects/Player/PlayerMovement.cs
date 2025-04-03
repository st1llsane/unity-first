using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
  private Rigidbody2D body;
  private SpriteRenderer sprite;
  private bool isLastMoveDirectionRight = true;
  [SerializeField] private float moveSpeed;

  private void Awake() {
    body = GetComponent<Rigidbody2D>();
    sprite = GetComponent<SpriteRenderer>();
    body.transform.position = new Vector2(0, -1.85f);
    body.mass = 10;
    body.gravityScale = 6;
  }

  private void Update() {
    float horizontalInput = Input.GetAxis("Horizontal") * moveSpeed;

    body.linearVelocity = new Vector2(horizontalInput, body.linearVelocity.y);
    HandleSpriteFlip();

    if (Input.GetKey(KeyCode.Space)) {
      body.linearVelocity = new Vector2(body.linearVelocity.x, moveSpeed);
    }

    HandleRestartGame();
    HandleQuitGame();
  }

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