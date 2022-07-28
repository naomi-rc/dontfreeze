using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class SettingsMenuHandler : MonoBehaviour
{
    public UnityAction OnSettingsBackButtonClicked = delegate { };
    public UnityAction<AudioClip> SubmitSoundAction = delegate { };

    private Button backButton;
    private Toggle fullscreenToggle;
    private DropdownField resolutionDropdown;
    private DropdownField qualityDropdown;
    private Toggle vsyncToggle;
    private Slider masterVolumeSlider;
    private Button generalTab;
    private Button graphicsTab;
    private Button audioTab;
    private VisualElement generalMenu;
    private VisualElement graphicsMenu;
    private VisualElement audioMenu;

    [SerializeField]
    private AudioClip onHoverSound = default;

    [SerializeField]
    private AudioClip onClickSound = default;

    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        backButton = rootVisualElement.Q<Button>("CloseButton");
        generalTab = rootVisualElement.Q<Button>("GeneralTab");
        graphicsTab = rootVisualElement.Q<Button>("GraphicsTab");
        audioTab = rootVisualElement.Q<Button>("AudioTab");
        fullscreenToggle = rootVisualElement.Q<Toggle>("FullscreenToggle");
        resolutionDropdown = rootVisualElement.Q<DropdownField>("ResolutionDropdown");
        vsyncToggle = rootVisualElement.Q<Toggle>("VsyncToggle");
        qualityDropdown = rootVisualElement.Q<DropdownField>("QualityDropdown");
        masterVolumeSlider = rootVisualElement.Q<Slider>("MasterVolumeSlider");
        generalMenu = rootVisualElement.Q<VisualElement>("General");
        graphicsMenu = rootVisualElement.Q<VisualElement>("Graphics");
        audioMenu = rootVisualElement.Q<VisualElement>("Audio");

        backButton.RegisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
        backButton.clicked += OnBackButtonClicked;
        backButton.clicked += OnButtonClicked;

        generalTab.RegisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
        generalTab.clicked += OnButtonClicked;
        generalTab.clicked += () => OnTabClicked("General");

        graphicsTab.RegisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
        graphicsTab.clicked += OnButtonClicked;
        graphicsTab.clicked += () => OnTabClicked("Graphics");

        audioTab.RegisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
        audioTab.clicked += OnButtonClicked;
        audioTab.clicked += () => OnTabClicked("Audio");

        fullscreenToggle.RegisterCallback<ChangeEvent<bool>>(OnFullscreenToggleChanged);
        fullscreenToggle.RegisterCallback<ChangeEvent<bool>>((evt) => OnButtonClicked());
        fullscreenToggle.value = Screen.fullScreen;

        resolutionDropdown.choices = new List<string>();
        foreach (Resolution resolution in Screen.resolutions.Reverse())
        {
            var resolutionString = $"{resolution.width}x{resolution.height}";
            var currentResolutionString = $"{Screen.width}x{Screen.height}";
            resolutionDropdown.choices.Add(resolutionString);
            if (resolutionString == currentResolutionString)
            {
                resolutionDropdown.value = resolutionString;
            }
        }
        resolutionDropdown.RegisterCallback<ChangeEvent<string>>(OnResolutionDropdownChanged);
        resolutionDropdown.RegisterCallback<ChangeEvent<string>>((evt) => OnButtonClicked());

        qualityDropdown.choices = new List<string>();
        foreach (var quality in QualitySettings.names.Select((name, index) => new { name, index }))
        {
            qualityDropdown.choices.Add(quality.name);
            if (quality.index == QualitySettings.GetQualityLevel())
            {
                qualityDropdown.value = quality.name;
            }
        }
        qualityDropdown.RegisterCallback<ChangeEvent<string>>(OnQualityDropdownChanged);
        qualityDropdown.RegisterCallback<ChangeEvent<string>>((evt) => OnButtonClicked());

        vsyncToggle.RegisterCallback<ChangeEvent<bool>>(OnVsyncToggleChanged);
        vsyncToggle.RegisterCallback<ChangeEvent<bool>>((evt) => OnButtonClicked());
        vsyncToggle.value = QualitySettings.vSyncCount > 0;

        masterVolumeSlider.RegisterCallback<ChangeEvent<float>>(OnMasterVolumeSliderChanged);
        masterVolumeSlider.value = AudioListener.volume;
    }

    void OnDisable()
    {
        backButton.UnregisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
        backButton.clicked -= OnBackButtonClicked;
        backButton.clicked -= OnButtonClicked;
    }

    void OnBackButtonClicked()
    {
        OnSettingsBackButtonClicked.Invoke();
    }

    void OnTabClicked(string tabName)
    {
        generalMenu.style.display = tabName == "General" ? DisplayStyle.Flex : DisplayStyle.None;
        graphicsMenu.style.display = tabName == "Graphics" ? DisplayStyle.Flex : DisplayStyle.None;
        audioMenu.style.display = tabName == "Audio" ? DisplayStyle.Flex : DisplayStyle.None;
    }

    void OnButtonMouseEnter(MouseEnterEvent _)
    {
        if (onHoverSound != null)
        {
            SubmitSoundAction.Invoke(onHoverSound);
        }
    }

    void OnButtonClicked()
    {
        if (onClickSound != null)
        {
            SubmitSoundAction.Invoke(onClickSound);
        }
    }

    void OnFullscreenToggleChanged(ChangeEvent<bool> value)
    {
        Screen.fullScreen = value.newValue;
    }

    void OnResolutionDropdownChanged(ChangeEvent<string> value)
    {
        var resolutionString = value.newValue;
        var resolution = resolutionString.Split('x');
        Screen.SetResolution(int.Parse(resolution[0]), int.Parse(resolution[1]), Screen.fullScreen);
    }

    void OnQualityDropdownChanged(ChangeEvent<string> value)
    {
        QualitySettings.SetQualityLevel(QualitySettings.names.ToList().IndexOf(value.newValue));
    }

    void OnVsyncToggleChanged(ChangeEvent<bool> value)
    {
        QualitySettings.vSyncCount = value.newValue ? 1 : 0;
    }

    void OnMasterVolumeSliderChanged(ChangeEvent<float> value)
    {
        AudioListener.volume = value.newValue;
    }
}
