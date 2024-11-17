using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    // TMP_Dropdowns for resolution and fullscreen mode
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown fullscreenModeDropdown;

    // Array of supported resolutions
    private Resolution[] availableResolutions;

    void Start()
    {
        // Get available resolutions from the system
        availableResolutions = Screen.resolutions;

        // Populate the resolution dropdown
        resolutionDropdown.ClearOptions();
        int currentResolutionIndex = 0;

        for (int i = 0; i < availableResolutions.Length; i++)
        {
            string resolutionOption = availableResolutions[i].width + " x " + availableResolutions[i].height;
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolutionOption));

            // Check if this is the current resolution
            if (availableResolutions[i].width == Screen.currentResolution.width &&
                availableResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Populate the fullscreen mode dropdown
        fullscreenModeDropdown.ClearOptions();
        fullscreenModeDropdown.AddOptions(new System.Collections.Generic.List<string> { "Fullscreen", "Windowed", "Borderless" });

        // Set the initial fullscreen mode
        fullscreenModeDropdown.value = GetCurrentFullscreenMode();
        fullscreenModeDropdown.RefreshShownValue();
    }

    public void ApplyResolutionSettings()
    {
        // Get the selected resolution from the dropdown
        int selectedResolutionIndex = resolutionDropdown.value;
        Resolution selectedResolution = availableResolutions[selectedResolutionIndex];

        // Get the selected fullscreen mode
        int fullscreenModeIndex = fullscreenModeDropdown.value;

        // Apply the selected resolution and fullscreen mode
        FullScreenMode fullscreenMode = FullScreenMode.FullScreenWindow;

        switch (fullscreenModeIndex)
        {
            case 0:
                fullscreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                fullscreenMode = FullScreenMode.Windowed;
                break;
            case 2:
                fullscreenMode = FullScreenMode.MaximizedWindow;
                break;
        }

        Screen.SetResolution(selectedResolution.width, selectedResolution.height, fullscreenMode);
    }

    private int GetCurrentFullscreenMode()
    {
        if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
            return 0; // Fullscreen
        if (Screen.fullScreenMode == FullScreenMode.Windowed)
            return 1; // Windowed
        if (Screen.fullScreenMode == FullScreenMode.MaximizedWindow)
            return 2; // Borderless

        return 0; // Default to Fullscreen
    }
}
