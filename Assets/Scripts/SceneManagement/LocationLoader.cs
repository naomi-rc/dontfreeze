using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class LocationLoader : MonoBehaviour
{
    [SerializeField]
    private VoidEventChannel onSceneReady = default;

    [SerializeField]
    private VoidEventChannel onLoadingRequest = default;

    private string currentSceneName;

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void Load(SceneSO sceneToLoad)
    {
        onLoadingRequest.Raise();
        StartCoroutine(TransitionToLocation(sceneToLoad));
    }

    private IEnumerator TransitionToLocation(SceneSO sceneToLoad)
    {
        yield return new WaitForSeconds(1f);

        AsyncOperationHandle<SceneInstance> operation = sceneToLoad.reference.LoadSceneAsync(LoadSceneMode.Single, true);

        yield return operation.WaitForCompletion();
        operation.Completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.LogFormat("{0} successfully loaded.", obj.Result.Scene.name);
        }
    }
}
