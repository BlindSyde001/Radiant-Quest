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
    public bool canWalkOnGround;
    public bool canWalkOnSea;
    public bool canWalkOnMountain;

    public float playerSpeed = 5f;
    public GameObject vehicle;

    [SerializeField]
    private bool isMoving = false;
    [SerializeField]
    private Vector3 targetPosition;

    // UPDATES
    private void Start()
    {
        SnapPlayerToClosestGroundTile();
    }
    private void FixedUpdate()
    {
        if (!isMoving)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            AttemptToMove(horizontalInput, verticalInput);
        }

        if (isMoving)
        {
            Debug.Log("Moving Towards");
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, playerSpeed * Time.fixedDeltaTime);
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }

    // METHODS
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

            if (IsTileWalkable(newPosition))
            {
                if (vehicle != null && newPosition == vehicle.transform.position)
                {
                    // Player is trying to enter a vehicle
                    StartCoroutine(EnterVehicleCoroutine(vehicle.GetComponent<Vehicle>()));
                }
                else
                {
                    // Player is not trying to enter a vehicle
                    targetPosition = groundTilemap.GetCellCenterWorld(groundTilemap.WorldToCell(newPosition));
                    isMoving = true;
                }
            }
        }
    }
    private bool IsTileWalkable(Vector3 position)
    {
        // find all the tiles on that space in the world
        TileBase groundTile = groundTilemap.GetTile(groundTilemap.WorldToCell(position));
        TileBase seaTile = seaTilemap.GetTile(seaTilemap.WorldToCell(position));
        TileBase mountainTile = mountainTilemap.GetTile(mountainTilemap.WorldToCell(position));

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

        // check if vehicle is present at the position
        if (vehicle == null)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(position, new Vector2(0.5f, 0.5f), 0);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.GetComponent<Vehicle>() != null)
                {
                    canWalkOnCurrentTile = true;
                    vehicle = collider.gameObject;
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
        Debug.Log("Get out of the vehicle");
        yield return null;
    }
}
