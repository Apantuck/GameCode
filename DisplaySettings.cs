using UnityEngine;

public class DisplaySettings : Saveable
{
    public enum WindowTypes { fullscreen, borderless, windowed};
    public struct resolution { public int width; public int height; };
    public struct ScreenSettings { public resolution dimensions; public bool fullscreen; public int framerate; };

    [Header("Resolution")]
    public resolution[] supportedResolutions;
    private resolution defaultResolution;
    private bool defaultFullscreen = true;

    [Header("Framerate")]
    public int[] supportedFramerates = { 30, 60 };
    public int defaultFramerate = 0; // (0 is unlocked)

    private ScreenSettings CurSelectedSettings;

    public override void Awake()
    {
        base.Awake();
        defaultResolution.width = Screen.width;
        defaultResolution.height = Screen.height;
        CurSelectedSettings.dimensions = defaultResolution;
        CurSelectedSettings.fullscreen = defaultFullscreen;
        CurSelectedSettings.framerate = defaultFramerate;
    }

    public void Confirm()
    {
        Screen.SetResolution(CurSelectedSettings.dimensions.width, CurSelectedSettings.dimensions.height,
            CurSelectedSettings.fullscreen, CurSelectedSettings.framerate);
    }

    public void Reset_Defaults()
    {
        Screen.SetResolution(defaultResolution.width, defaultResolution.height, defaultFullscreen, defaultFramerate);
    }

    public void Select_Fullscreen(int selection)
    {
        if (selection == TRUE) CurSelectedSettings.fullscreen = true;
        else CurSelectedSettings.fullscreen = false;
    }

    public void Select_Resolution(resolution res)
    {
        CurSelectedSettings.dimensions = res;
    }

    public void Select_Framerate(int framerate)
    {
        CurSelectedSettings.framerate = framerate;
    }
}
