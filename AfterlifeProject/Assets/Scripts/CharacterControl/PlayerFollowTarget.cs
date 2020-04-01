using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowTarget : MonoBehaviour
{
    private PlaneMovement planeComp;
    private PlayerMovement movementComp;
    private PlayerDeath deathComp;
    private ObstaclePlacementState obstacleComp;

    [SerializeField] private Transform target;

    private float smoothSpeed;
    private float rotateSpeed;
    private float rollSpeed;
    private float rollProgress;

    private bool barrelRolling = false;
    private bool rollLeft = false;
    private bool rollRight = false;

    public float formTimer;

    [SerializeField] public bool hasHitObstacle = false;

    //TEMP
    private bool spawnedEnd = false;

    /// <summary>
    /// Instantiate component references and variables
    /// </summary>
    private void OnEnable()
    {
        planeComp = transform.parent.GetComponent<PlaneMovement>();
        movementComp = transform.parent.GetComponentInChildren<PlayerMovement>();
        deathComp = transform.parent.GetComponentInChildren<PlayerDeath>();
        obstacleComp = transform.parent.GetComponentInChildren<ObstaclePlacementState>();

        target = transform.parent.GetChild(2).transform;
        smoothSpeed = 0.125f;
        rotateSpeed = 2f;
        rollSpeed = 800f;

        formTimer = 0f;
    }

    /// <summary>
    /// Update used for allowing barrel rolling in the plane state
    /// </summary>
    private void Update()
    {
        //Allows barrel rolling to execute if in plane state
        if (movementComp.GetCurrentState() == PlayerMovement.State.PLANE)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!barrelRolling)
                {
                    if (!rollLeft)
                    {
                        rollLeft = true;
                        barrelRolling = true;
                        movementComp.xyspeed = 20f;
                    }
                }
            }

            //Initiate barrel roll on mouse press
            if (Input.GetMouseButtonDown(1))
            {
                if (!barrelRolling)
                {
                    if (!rollRight)
                    {
                        rollRight = true;
                        barrelRolling = true;
                        movementComp.xyspeed = 20f;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Controls player movement and form timing. When form timer reaches certain value, form naturally progresses. 
    /// </summary>
    private void FixedUpdate()
    {
        formTimer += Time.deltaTime;

        if (movementComp.playerState != PlayerMovement.State.FLARE)
        {
            if (formTimer >= 120f)
            {
                planeComp.ResetPlane();
                movementComp.ChangeStateData();
                deathComp.DeathEvent();
                obstacleComp.ClearObjectsInChildren();
                formTimer = 0f;
            }
        }
        else
        {
            if (formTimer >= 120f)
            {
                if(!spawnedEnd)
                {
                    Instantiate(Resources.Load<GameObject>("End"), new Vector3(transform.position.x, transform.position.y, transform.position.z + 40f), Quaternion.identity);
                    spawnedEnd = true;
                }
            }
        }

        MoveToTarget();
        RotateToTarget();
        if(rollLeft)
        {
            RollLeft();
        }
        if(rollRight)
        {
            RollRight();
        }
    }

    /// <summary>
    /// Moves the player object to the invisible mouse target. Used to get smooth moving.
    /// </summary>
    private void MoveToTarget()
    {
        Vector3 desiredPos = new Vector3(target.position.x, target.position.y, target.position.z - 2);
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothedPos;
    }

    /// <summary>
    /// Keeps the player object rotated towards the invisible mouse target. Used alongside MoveToTarget()
    /// </summary>
    private void RotateToTarget()
    {
        Vector3 targetPosition = target.transform.position - transform.position;
        float rotateStep = rotateSpeed * Time.deltaTime;

        Vector3 direction = Vector3.RotateTowards(transform.forward, targetPosition, rotateStep, 0f);
        transform.rotation = Quaternion.LookRotation(direction);
        
    }

    /// <summary>
    /// Used to roll the player left when a barrel roll is initiated
    /// </summary>
    private void RollLeft()
    {
        rollProgress += rollSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rollProgress, Space.Self);
        if(rollProgress >= 360)
        {
            rollLeft = false;
            barrelRolling = false;
            rollProgress = 0;
            movementComp.xyspeed = 12f;
        }
    }

    /// <summary>
    /// Used to roll the player right when a barrel roll is initiated
    /// </summary>
    private void RollRight()
    {
        rollProgress -= rollSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rollProgress, Space.Self);
        if (rollProgress <= -360)
        {
            rollRight = false;
            barrelRolling = false;
            transform.Rotate(0, 0, 0);
            rollProgress = 0;
            movementComp.xyspeed = 12f;
        }
    }

    /// <summary>
    /// Collision event that is called when the player collides. Performs form changing, plane resetting, and has events for death in the future
    /// </summary>
    private void CollisionEvent()
    {
        if (!hasHitObstacle)
        {
            hasHitObstacle = true;
        }
        planeComp.ResetPlane();
        movementComp.ChangeStateData();
        deathComp.DeathEvent();
        obstacleComp.ClearObjectsInChildren();
        formTimer = 0f;
    }

    /// <summary>
    /// Temp method. Ends the game with a loss
    /// </summary>
    private void StopGameWithLoss()
    {
        Debug.Log("YOU LOSE");
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Temp method. Ends the game with a win
    /// </summary>
    private void StopGameWithWin()
    {
        Debug.Log("YOU WIN");
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "MainCliff")
        {
            CollisionEvent();
        }

        if(other.tag == "SkyObstacle")
        {
            CollisionEvent();
        }

        if(other.tag == "CaveObstacle")
        {
            //THIS IS PLACEHOLDER
            CollisionEvent();
            StopGameWithLoss();
        }

        if(other.tag == "EndObstacle")
        {
            //PLACEHOLDER
            StopGameWithWin();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NearMiss")
        {
            Debug.Log("Exit with " + other.gameObject);
        }
    }
}
