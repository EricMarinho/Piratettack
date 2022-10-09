using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private MoveCharacter moveCharacter;
    private Rigidbody2D rb;
    private float horizontalInput;
    private float verticalInput;
    private PlayerController playerControllerInstance;
    private ObjectPooler objectPoolerInstance;
    private SessionTimeHandler sessionTimeHandlerInstance;
    private Vector3 bulletSpawnPosition;
    private float bulletSpawnPositionModifier = 0.2f;
    private float frontalTimer = 0f;
    private float leftSideTimer = 0f;
    private float rightSideTimer = 0f;

    private bool isFrontalReady = true;
    private bool isRightSideReady = true;
    private bool isLeftSideReady = true;

    private void Start()
    {
        objectPoolerInstance = ObjectPooler.instance;
        sessionTimeHandlerInstance = SessionTimeHandler.Instance;
        playerControllerInstance = PlayerController.instance;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (!isFrontalReady)
        {
            frontalTimer += Time.deltaTime;
            if (frontalTimer >= playerControllerInstance.playerData.frontalShotReloadTime)
            {
                frontalTimer = 0f;
                isFrontalReady = true;
            }
        }

        if (!isLeftSideReady)
        {
            leftSideTimer += Time.deltaTime;
            if (leftSideTimer >= playerControllerInstance.playerData.sideShotReloadTime)
            {
                leftSideTimer = 0f;
                isLeftSideReady = true;
            }
        }

        if (!isRightSideReady)
        {
            rightSideTimer += Time.deltaTime;
            if (rightSideTimer >= playerControllerInstance.playerData.sideShotReloadTime)
            {
                rightSideTimer = 0f;
                isRightSideReady = true;
            }
        }
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (isFrontalReady)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                bulletSpawnPosition = rb.transform.position + (rb.transform.up * bulletSpawnPositionModifier);
                objectPoolerInstance.poolSpawner.SpawnFromPool(playerControllerInstance.playerData.frontalPoolTag, bulletSpawnPosition, rb.transform.rotation * Quaternion.Euler(0, 0, 0));
                isFrontalReady = false;
            }
        }

        if (isLeftSideReady)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                objectPoolerInstance.poolSpawner.SpawnFromPool(playerControllerInstance.playerData.frontalPoolTag, rb.transform.position + (rb.transform.up * bulletSpawnPositionModifier), rb.transform.rotation * Quaternion.Euler(0, 0, 90));
                objectPoolerInstance.poolSpawner.SpawnFromPool(playerControllerInstance.playerData.frontalPoolTag, rb.transform.position, rb.transform.rotation * Quaternion.Euler(0, 0, 90));
                objectPoolerInstance.poolSpawner.SpawnFromPool(playerControllerInstance.playerData.frontalPoolTag, rb.transform.position - (rb.transform.up * bulletSpawnPositionModifier), rb.transform.rotation * Quaternion.Euler(0, 0, 90));
                isLeftSideReady = false;
            }
        }

        if (isRightSideReady)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                objectPoolerInstance.poolSpawner.SpawnFromPool(playerControllerInstance.playerData.frontalPoolTag, rb.transform.position + (rb.transform.up * bulletSpawnPositionModifier), rb.transform.rotation * Quaternion.Euler(0, 0, -90));
                objectPoolerInstance.poolSpawner.SpawnFromPool(playerControllerInstance.playerData.frontalPoolTag, rb.transform.position, rb.transform.rotation * Quaternion.Euler(0, 0, -90));
                objectPoolerInstance.poolSpawner.SpawnFromPool(playerControllerInstance.playerData.frontalPoolTag, rb.transform.position - (rb.transform.up * bulletSpawnPositionModifier), rb.transform.rotation * Quaternion.Euler(0, 0, -90));
                isRightSideReady = false;
            }
        }

    }

    private void FixedUpdate()
    {
        if (horizontalInput != 0)
        {
            moveCharacter.Rotate(rb, playerControllerInstance.playerData.rotationSpeed * -horizontalInput);
        }
        if (verticalInput > 0)
        {
            moveCharacter.Accelerate(rb, playerControllerInstance.playerData.speed);
        }
        if (verticalInput < 0)
        {
            moveCharacter.Stop(rb, playerControllerInstance.playerData.reverse);
        }
    }
}
