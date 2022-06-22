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

    private string currentSceneName;

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void Load(SceneObject sceneToLoad)
    {
        StartCoroutine(TransitionToLocation(sceneToLoad));
    }

    private IEnumerator TransitionToLocation(SceneObject sceneToLoad)
    {
        AsyncOperationHandle<SceneInstance> operation = sceneToLoad.reference.LoadSceneAsync(LoadSceneMode.Single, true);
        operation.Completed += OnLoadComplete;

        yield return null;
    }

    private void OnLoadComplete(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.LogFormat("{0} successfully loaded.", obj.Result.Scene.name);
        }
    }
}
