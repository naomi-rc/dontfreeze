using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class LocationLoader : MonoBehaviour
{
    [SerializeField] private SceneEventChannel sceneEventChannel = default;

    [SerializeField] private VoidEventChannel onRestartEvent = default;

    private void OnEnable()
    {
        sceneEventChannel.OnEventRaised += Load;
        onRestartEvent.OnEventRaised += Reload;
    }

    private void OnDisable()
    {
        sceneEventChannel.OnEventRaised -= Load;
        onRestartEvent.OnEventRaised -= Reload;
    }

    public void Load(SceneObject sceneToLoad)
    {
        StartCoroutine(TransitionToLocation(sceneToLoad));
    }

    public void Reload()
    {
        StartCoroutine(ReloadLocation());
    }

    private IEnumerator ReloadLocation()
    {
        yield return new WaitForSecondsRealtime(2f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

        yield return operation;
    }

    private IEnumerator TransitionToLocation(SceneObject sceneToLoad)
    {
        yield return new WaitForSecondsRealtime(1f);

        AsyncOperationHandle<SceneInstance> operation = sceneToLoad.reference.LoadSceneAsync(LoadSceneMode.Single, true);
        yield return operation;

        if (operation.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.LogFormat("{0} successfully loaded.", operation.Result.Scene.name);
        }
    }
}
