using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.CrossPlatformInput;

public class Character : MonoBehaviour
{

    private Rigidbody2D rb;
    public Inventory inventory;
    public Animator animator;
    public ItemList itemList;

    public float moveThreshhold = 0.1f;
    public bool isGrounded;
    public bool facingRight;
    public float jumpSpeed;
    public float climbSpeed;
    public float ladderJumpSpeed;
    public float speed;

    public bool isClimbing;
    public bool isLadderJumping;
    public float ladderJumpCooldown;
    public LayerMask ladderMask;
    public LayerMask groundMask;
    public LayerMask waterMask;

    public float groundLength;
    public float ladderLength;
    public float slowLadderLength;
    public float legWidth;

    [Header("Item Drop")]
    public LayerMask itemDropMask;
    public float itemDropLength;
    public Vector3 dropOffset = new Vector3(1,1,0);

    [Header("Audio")]
    public AudioClip jumpClip;
    
    private float ladderJumpTimer;
    private Interactable interactable;

    [Header("Coyote Time")]
    public float coyoteTime = 0.15f;
    private float lastGroundedTime;
    public float ladderCoyoteTime = 0.2f;
    private float lastClimbTime;

    [Header("Controls")]
    public Joystick joystick;
    public Button jumpBtn;
    public Button interactBtn;

    public TextMeshPro inventoryFullText;
    public Vector3 inventoryFullTextOffset = new Vector3(0,1,0);


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();

    }
    void Start()
    {
        inventory.onInventoryFullCallback += HandleOnInventoryFull;
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //bool jumpRequest = Input.GetKeyDown(KeyCode.Z);
        //bool interactRequest = Input.GetKeyDown(KeyCode.X);

        float horizontal = 0;
        float vertical = 0;
        if (joystick.Horizontal > moveThreshhold)
        {
            horizontal = joystick.Horizontal;
        }
        else if (joystick.Horizontal < -moveThreshhold)
        {
            horizontal = joystick.Horizontal;
        }

        if (joystick.Vertical > moveThreshhold)
        {
            vertical = joystick.Vertical;
        }
        else if (joystick.Vertical < -moveThreshhold)
        {
            vertical = joystick.Vertical;
        }

        Vector3 direction = new Vector3(horizontal, vertical);

        bool jumpRequest = CrossPlatformInputManager.GetButtonDown("Z");
        bool interactRequest = CrossPlatformInputManager.GetButtonDown("X");

        // Move
        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            facingRight = true;
        }else if(direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            facingRight = false;
        }

        bool hasLadder = Physics2D.Raycast(transform.position, Vector3.up, ladderLength, ladderMask);
        bool hasLadderLong = Physics2D.Raycast(transform.position + Vector3.up * slowLadderLength, Vector3.up, 0.4f, ladderMask);

        bool leftGrounded = Physics2D.Raycast(transform.position + Vector3.left * legWidth, Vector3.down, groundLength, groundMask);
        bool rightGrounded = Physics2D.Raycast(transform.position + Vector3.right * legWidth, Vector3.down, groundLength, groundMask);
        isGrounded = leftGrounded || rightGrounded;
        lastGroundedTime = isGrounded ? Time.time : lastGroundedTime;

        if (hasLadder && direction.y > 0)
        {
            isClimbing = true;
        }

        if (!hasLadder)
        {
            isClimbing = false;
        }
        lastClimbTime = isClimbing ? Time.time : lastClimbTime;

        bool canJump = isGrounded || (lastGroundedTime + coyoteTime > Time.time);
        bool canLadderJump = (isClimbing || lastClimbTime + coyoteTime > Time.time) && ladderJumpTimer <= 0;

        if (jumpRequest)
        {
            if (canJump)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpSpeed);
                GameManager.instance.soundFx.Play(jumpClip);
            }
            else if (canLadderJump)
            {
                isLadderJumping = true;
                ladderJumpTimer = ladderJumpCooldown;
                rb.velocity = new Vector3(rb.velocity.x, ladderJumpSpeed);
                GameManager.instance.soundFx.Play(jumpClip);
            }
        }

        if (isClimbing && !isLadderJumping)
        {
            if (hasLadderLong)
            {
                rb.velocity = new Vector3(rb.velocity.x, direction.y * climbSpeed);
                rb.gravityScale = 0;
            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x, (direction.y > 0 ? 0 : direction.y )* climbSpeed);
                rb.gravityScale = 0;
            }
        }
        else
        {
            rb.gravityScale = 2;
        }

        if (isClimbing)
        {
            rb.velocity = new Vector3(direction.x * climbSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector3(direction.x * speed, rb.velocity.y);
        }

        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsClimbing", isClimbing);
        animator.SetFloat("Horizontal", Mathf.Abs(direction.x));
        animator.SetFloat("Vertical", Mathf.Abs(direction.y));
        animator.SetFloat("VerticalSpeed", rb.velocity.y);

        if (interactRequest)
        {
            if(interactable != null)
            {
                interactable.Interact();
            }
        }

        //Timers
        if(ladderJumpTimer > 0)
        {
            ladderJumpTimer -= Time.deltaTime;
        }
        else
        {
            isLadderJumping = false;
        }
    }

    public void HandleOnInventoryFull()
    {
        Debug.Log("Inventory Full");
        Instantiate(inventoryFullText, transform.position + inventoryFullTextOffset, Quaternion.identity);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Item item = collision.gameObject.GetComponent<Item>();
        //if(item != null)
        //{
        //    bool success = item.Grab(inventory);
        //    if (success)
        //    {
        //        soundFX.Play(pickupClip);
        //    }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable _interactable = collision.GetComponent<Interactable>();
        if(_interactable != null)
        {
            interactable = _interactable;
            interactBtn.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(interactable == null)
        {
            Interactable _interactable = collision.GetComponent<Interactable>();
            if (_interactable != null)
            {
                interactable = _interactable;
                interactBtn.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interactable _interactable = collision.GetComponent<Interactable>();
        if (_interactable != null)
        {
            interactable = null;
            interactBtn.gameObject.SetActive(false);
        }
    }

    public void DropItem(Item item, bool dropRight)
    {
        Vector3 dropPosition = transform.position + (dropRight ? dropOffset : new Vector3(-dropOffset.x, dropOffset.y));
        bool canDrop = !Physics2D.Raycast(transform.position, dropPosition - transform.position, itemDropLength, itemDropMask);
        if (canDrop)
        {
            item.Drop(inventory, dropPosition);
        }
    }

    public void DropItem(Item item)
    {
        Vector3 dropPosition = transform.position + (facingRight ? dropOffset : new Vector3(-dropOffset.x, dropOffset.y));
        bool canDrop = !Physics2D.Raycast(transform.position, dropPosition - transform.position, itemDropLength, itemDropMask);
        if (canDrop)
        {
            item.Drop(inventory, dropPosition);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine((transform.position + Vector3.left * legWidth), (transform.position + Vector3.left * legWidth) + Vector3.down * groundLength);
        Gizmos.DrawLine((transform.position + Vector3.right * legWidth), (transform.position + Vector3.right * legWidth) + Vector3.down * groundLength);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up* ladderLength);
    }

    public void Save()
    {
        SaveSystem.SavePlayer(this);
    }

    public void Load()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if(data != null)
        {
            Vector3 savePos = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
            transform.position = savePos;
            for (int i = 0; i < data.itemKeys.Length; i++)
            {
                int key = data.itemKeys[i];
                Item prefab = itemList.items[key];
                if(prefab != null)
                {
                    Item item = Instantiate(prefab);
                    item.Grab(inventory);
                }
            }
        }
    }

    public void SetJumpSpeed(float _jumpSpeed)
    {
        jumpSpeed = _jumpSpeed;
    }

    public void AddJumpSpeed(float _addJumpSpeed)
    {
        jumpSpeed += _addJumpSpeed;
        ladderJumpSpeed += _addJumpSpeed;
    }

    public void SubtractJumpSpeed(float _subtractJumpSpeed)
    {
        jumpSpeed -= _subtractJumpSpeed;
        ladderJumpSpeed -= _subtractJumpSpeed;
    }
}
