using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public partial class SceneTransitionHandler : MonoBehaviour
{
    private VisualElement screen;
    private IEnumerable<string> screenClassList = new List<string>();

    private void OnEnable()
    {
        var rootElement = GetComponent<UIDocument>().rootVisualElement;
        rootElement.SendToBack();

        screen = rootElement.Q<VisualElement>("Screen");
        foreach (string className in screenClassList)
            screen.AddToClassList(className);
    }

    private void OnDisable()
    {
        screenClassList = screen.GetClasses();
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutAnimation());
    }

    public IEnumerator FadeInAnimation()
    {
        yield return new WaitForSeconds(2.0f);
        screen.AddToClassList("animation-fade");
    }

    public IEnumerator FadeOutAnimation()
    {
        yield return new WaitForSeconds(0f);
        screen.RemoveFromClassList("animation-fade");
    }
}
