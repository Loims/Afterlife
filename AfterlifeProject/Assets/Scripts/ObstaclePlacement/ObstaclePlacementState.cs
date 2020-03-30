using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePlacementState : MonoBehaviour
{
    #region GenerationVariables
    private static ObstaclePlacementState instance;

    private PlayerMovement playerMovementScript;
    private PlaneMovement planeMovementScript;

    [SerializeField] private PlayerMovement.State playerGenerationState;

    private OceanPlacement oceanScript;
    private SkyPlacement skyScript;
    private CavePlacement caveScript;
    #endregion

    private void OnEnable()
    {
        //Instance generation
        InstantiateSingleton();

        playerMovementScript = transform.parent.GetComponentInChildren<PlayerMovement>();
        playerGenerationState = playerMovementScript.playerState;

        planeMovementScript = transform.parent.GetComponent<PlaneMovement>();

        oceanScript = GetComponent<OceanPlacement>();
        skyScript = GetComponent<SkyPlacement>();
        caveScript = GetComponent<CavePlacement>();
    }

    private void InstantiateSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if(playerGenerationState != playerMovementScript.playerState)
        {
            playerGenerationState = playerMovementScript.playerState;
        }

        GenerationStateMachine();
    }

    private void GenerationStateMachine()
    {
        switch (playerGenerationState)
        {
            case PlayerMovement.State.WHALE:
                oceanScript.enabled = true;
                skyScript.enabled = false;
                caveScript.enabled = false;
                break;

            case PlayerMovement.State.PLANE:
                oceanScript.enabled = false;
                skyScript.enabled = true;
                caveScript.enabled = false;
                break;

            case PlayerMovement.State.FLARE:
                oceanScript.enabled = false;
                skyScript.enabled = false;
                caveScript.enabled = true;
                break;
        }
    }

    public void ClearObjectsInChildren()
    {
        switch (playerGenerationState)
        {
            case PlayerMovement.State.WHALE:
                oceanScript.ClearObjects();
                break;

            case PlayerMovement.State.PLANE:
                skyScript.ClearObjects();
                break;

            case PlayerMovement.State.FLARE:
                //caveScript.ClearObjects();
                break;
        }
    }
}
