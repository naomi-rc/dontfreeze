using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private InputReader inputReader = default;

    [SerializeField]
    private ThirdPersonController playerPrefab;

    [SerializeField]
    private Transform spawnLocation;

    public VoidEventChannel onSceneReady = default;

    private void Awake()
    {
        // onSceneReady.OnEventRaised += InstantiatePlayer;
        InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        ThirdPersonController player = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);

        inputReader.EnableGameplayInput();
    }
}
