using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class LocationLoader : MonoBehaviour
{
    [SerializeField]
    private SceneEventChannel sceneEventChannel = default;

    private void OnEnable()
    {
        sceneEventChannel.OnEventRaised += Load;
    }

    private void OnDisable()
    {
        sceneEventChannel.OnEventRaised -= Load;
    }

    public void Load(SceneObject sceneToLoad)
    {
        StartCoroutine(TransitionToLocation(sceneToLoad));
    }

    private IEnumerator TransitionToLocation(SceneObject sceneToLoad)
    {
        AsyncOperationHandle<SceneInstance> operation = sceneToLoad.reference.LoadSceneAsync(LoadSceneMode.Single, true);
        yield return operation;

        if (operation.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.LogFormat("{0} successfully loaded.", operation.Result.Scene.name);
        }
    }
}
