using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    // VARIABLES
    public Tilemap groundTilemap;
    public Tilemap seaTilemap;
    public Tilemap mountainTilemap;
    public Tilemap shipExitTilemap;
    public LayerMask collisionTileLayers;

    public bool canWalkOnGround = true;
    public bool canWalkOnSea = false;
    public bool canWalkOnMountain = false;

    public float playerSpeed = 5f;
    public GameObject currentVehicleInUse;

    [SerializeField]
    private bool isMoving = false;
    [SerializeField]
    private Vector3 targetPosition;

    private List<InteractableController> nearInteractable = new List<InteractableController> { };

    public Animator animator;
    public GameObject spriteGameObject;
    private bool facingRight = true;
    private Vector2 lastDir;

    // UPDATES
    private void Start()
    {
        SnapPlayerToClosestGroundTile();
        animator = spriteGameObject.GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (isMoving) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, playerSpeed * Time.fixedDeltaTime);
            if (transform.position == targetPosition) {
                isMoving = false;
            }
        } else {
            IdleAnimation(lastDir);
        }

    }

    private void Update()
    {
        PlayerInputs();
    }

    // METHODS
    private void PlayerInputs() {
        if (Input.GetKeyDown(KeyCode.E)) {
            PlayerInteracts();
        }

        if (GameManager.state != GameManager.GameStates.Playing) return;
        // Bellow here won't execute if player is interacting with anything or the game is paused

        // Movement inputs
        if (!isMoving) {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            AttemptToMove(horizontalInput, verticalInput);
        }
    }
        

        private void PlayerInteracts()
    {
        if (UIDialogue.Instance.isDialogueActive())
        {
            UIDialogue.Instance.NextLine();
        }
        else if (nearInteractable.Count > 0)
        {
            InteractableController interactable = nearInteractable[nearInteractable.Count - 1];
            interactable.PlayerInteract();
        }
    }

    private void SnapPlayerToClosestGroundTile()
    {
        Vector3Int cellPosition = groundTilemap.WorldToCell(transform.position);
        Vector3 cellCenterPosition = groundTilemap.GetCellCenterWorld(cellPosition);
        transform.position = cellCenterPosition;
    }

    private void AttemptToMove(float horizontalInput, float verticalInput)
    {
        if (horizontalInput != 0f || verticalInput != 0f)
        {
            Vector2 inputDirection = new Vector2(horizontalInput, verticalInput);

            // Normalize the input direction so that diagonal movement is not faster than horizontal or vertical movement.
            inputDirection.Normalize();

            // Clamp the input direction to the horizontal or vertical axis.
            if (Mathf.Abs(inputDirection.x) > Mathf.Abs(inputDirection.y))
            {
                inputDirection.y = 0f;
            }
            else
            {
                inputDirection.x = 0f;
            }

            // New tile I am moving to
            Vector3 newPosition = transform.position + new Vector3(inputDirection.x, inputDirection.y, 0f);

            if (CheckNextTileProperties(newPosition))
            {
                if (currentVehicleInUse != null && newPosition == currentVehicleInUse.transform.position)
                {
                    // Player is trying to enter a vehicle
                    StartCoroutine(EnterVehicleCoroutine(currentVehicleInUse.GetComponent<Vehicle>()));
                }
                else
                {
                    // Player is not trying to enter a vehicle
                    targetPosition = groundTilemap.GetCellCenterWorld(groundTilemap.WorldToCell(newPosition));
                    isMoving = true;
                }
            }

            if (isMoving) { //Player Animation based on direction
                if ((inputDirection.x != 0f) || (inputDirection.y != 0f)) {
                    if (inputDirection.x != 0f) {
                        if (inputDirection.x > 0f && !facingRight || inputDirection.x < 0f && facingRight) FlipSprite();
                        animator.Play("Player_walkSide");
                        lastDir = inputDirection;
                    } else if (inputDirection.y > 0f) {
                        animator.Play("Player_walkNorth");
                        lastDir = inputDirection;
                    } else if (inputDirection.y < 0f) {
                        animator.Play("Player_walkSouth");
                        lastDir = inputDirection;
                    }
                }
            }
        }
    }
    private bool CheckNextTileProperties(Vector3 position)
    {
        // find all the tiles on that space in the world
        TileBase groundTile = groundTilemap.GetTile(groundTilemap.WorldToCell(position));
        TileBase seaTile = seaTilemap.GetTile(seaTilemap.WorldToCell(position));
        TileBase mountainTile = mountainTilemap.GetTile(mountainTilemap.WorldToCell(position));
        TileBase vehicleExitTile = shipExitTilemap.GetTile(shipExitTilemap.WorldToCell(position)); // assuming you have a vehicle exit tilemap

        // check if tile is walkable based on boolean variables
        bool canWalkOnCurrentTile = false;

        if (groundTile != null)
        {
            if (canWalkOnGround)
            {
                canWalkOnCurrentTile = true;
            }
            else canWalkOnCurrentTile = false;
        }

        if (seaTile != null)
        {
            if (canWalkOnSea)
            {
                canWalkOnCurrentTile = true;
            }
            else canWalkOnCurrentTile = false;
        }

        if (mountainTile != null)
        {
            if (canWalkOnMountain)
            {
                canWalkOnCurrentTile = true;
            }
            else canWalkOnCurrentTile = false;
        }

        if (vehicleExitTile != null && currentVehicleInUse != null)
        {
            StartCoroutine(ExitVehicleCoroutine());
        }

        // check if vehicle is present at the position
        if (currentVehicleInUse == null)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(position, new Vector2(0.5f, 0.5f), 0, collisionTileLayers);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.GetComponent<Vehicle>() != null)
                {
                    canWalkOnCurrentTile = true;
                    currentVehicleInUse = collider.gameObject;
                    break;
                }
            }
        }
        return canWalkOnCurrentTile;
    }
    private IEnumerator EnterVehicleCoroutine(Vehicle vehicle)
    {
        canWalkOnGround = vehicle.canDriveOnGround;
        canWalkOnSea = vehicle.canDriveOnSea;
        canWalkOnMountain = vehicle.canDriveOnMountain;

        while (transform.position != vehicle.transform.position)
        {
            targetPosition = vehicle.transform.position;
            isMoving = true;
            yield return new WaitForFixedUpdate();
        }

        isMoving = false;
        vehicle.transform.SetParent(transform);
        vehicle.GetComponent<BoxCollider2D>().enabled = false;
    }
    private IEnumerator ExitVehicleCoroutine()
    {
        canWalkOnGround = true;
        canWalkOnSea = false;
        canWalkOnMountain = false;

        while (transform.position != currentVehicleInUse.transform.position)
        {
            targetPosition = currentVehicleInUse.transform.position;
            isMoving = true;
            yield return new WaitForFixedUpdate();
        }

        isMoving = false;
        currentVehicleInUse.transform.SetParent(null);
        currentVehicleInUse.GetComponent<BoxCollider2D>().enabled = true;
        currentVehicleInUse = null;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Interactable"))
        {
            nearInteractable.Add(collider.GetComponent<InteractableController>());
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Interactable"))
        {
            nearInteractable.Remove(collider.GetComponent<InteractableController>());
        }
    }

    public void FlipSprite() { //flips the sprite to face the other way
            facingRight = !facingRight;
            this.transform.localScale = new Vector3(this.transform.localScale.x * -1, 1, 1);
        }

    public void IdleAnimation(Vector2 lastDir) {
        if (lastDir.y > 0) {
            animator.Play("Player_idleNorth");
        } else if (lastDir.y < 0) {
            animator.Play("Player_idleSouth");
        } else {
            animator.Play("Player_idleSide");
        }
    }
}
