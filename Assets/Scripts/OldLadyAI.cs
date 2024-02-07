using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Ink.Runtime;

public class OldLadyAI : MonoBehaviour
{
    // public float moveSpeed = 0.5f;
    // public float collisionOffset = 0f;
    // public Vector2 targetDirection;
    private Rigidbody2D rb; 
    SpriteRenderer spriteRenderer;
    // public ContactFilter2D movementFilter;
    private Animator animator;
    // private float changeDirectionCooldown;
    // List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    // public int visionRange = 10;
    // public LayerMask playerLayer;
    // public bool isFollowing = false;
    // public bool isSelling = false;
    // public bool canFollow = true;
    GameObject playerObject;

    // private bool hasCrossedJunction = false;
    // private bool hasReachedJunction = false;

    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    private bool playerInRange;

    private Story currentStory;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerInRange = false;
        visualCue.SetActive(false);
    }

    // // In the beginning, old lady walks towards the junctions and wait
    // void WalkToJunction()
    // {

    // }

    // void GiveQuest()
    // {

    // }

    
    // public void DetectPlayer(Vector2 rayDirection) {
    //     RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, visionRange, playerLayer);

    //     if (hit.collider != null && hit.collider.CompareTag("Player")) {
    //         // Player is within the line of sight
    //         Debug.Log("Player detected! Following");
    //         playerObject = hit.collider.gameObject;
    //         FollowPlayer();
    //     }
    // }

    // private bool TryMove(Vector2 targetDirection) {
    //     int count = rb.Cast(
    //             targetDirection,
    //             movementFilter,
    //             castCollisions,
    //             moveSpeed * Time.fixedDeltaTime + collisionOffset);

    //     //Player Doesn't count
    //     for (int i = 0; i < castCollisions.Count; i++)
    //     {
    //         RaycastHit2D hit = castCollisions[i];
    //         if (hit.collider != null && hit.collider.CompareTag("Player")) {
    //             count--;
    //         }
    //     }
        
    //     if (count <= 1) {
    //         rb.MovePosition(rb.position + targetDirection * moveSpeed * Time.fixedDeltaTime);

    //         if (targetDirection.x < 0) {
    //             spriteRenderer.flipX = true;
    //             animator.SetInteger("MovingDirection", 1);
    //         } else if (targetDirection.x > 0) {
    //             spriteRenderer.flipX = false;
    //             animator.SetInteger("MovingDirection", 1);
    //         } else if (targetDirection.y > 0) { 
    //             animator.SetInteger("MovingDirection", 2);
    //         } else if (targetDirection.y < 0) { 
    //             animator.SetInteger("MovingDirection", 3);
    //         }     

    //         return true;


    //     } else {
    //         return false;
    //     }
    // }

    // void FollowPayer()
    // {
    //     isFollowing = true;
    //     Vector2 newDirection = (playerObject.transform.position - transform.position).normalized;
    //     moveSpeed = 0.5f;
    //     playerObject.moveSpeed = 0.5f; // player becomes slow to help old lady
    //     TryMove(newDirection);
    //     if (TryMove(new Vector2(0, newDirection.y))) return;
    //     TryMove(new Vector2(newDirection.x, 0));
    // }

    // void UnfollowPlayer()
    // {
    //     // player back to original speed
    //     playerObject.moveSpeed = 1f;
    //     isFollowing = false;
    // }

    // Update is called once per frame
    void Update()
    {
        // if (!hasReachedJunction) {
        //     WalkToJunction();
        // }

        // if (isFollowing) {
        //     FollowPlayer();
        //     return;
        // }

        // if (hasReachedJunction && hasCrossedJunction) {
        //     UnfollowPlayer();
        // }

        if (DialogueManager.GetInstance().dialogueIsPlaying) {
            return;
        }

        if (playerInRange) {
            visualCue.SetActive(true);
            if (Keyboard.current.FindKeyOnCurrentKeyboardLayout("f").wasPressedThisFrame) {
                currentStory = new Story(inkJSON.text);
                DialogueManager.GetInstance().EnterDialogueMode(currentStory);
                transform.position = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y + 1); 
                gameObject.transform.SetParent(playerObject.transform);
                PlayerControls playercontrols = playerObject.GetComponent<PlayerControls>();
                playercontrols.moveSpeed = 3f;
            }
        } else {
            visualCue.SetActive(false);
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerObject = collision.gameObject;
            playerInRange = true;            
        }
    }

     void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;            
        }
    }

    private void OnDisable()
    {
       if (playerObject != null) {
            PlayerControls playercontrols = playerObject.GetComponent<PlayerControls>();
            playercontrols.moveSpeed = 7f;
        }
    }
}
