using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Audio Sources for SFX and Music
    public AudioSource sfxSource;
    public AudioSource musicSource;

    // Current states for SFX and Music
    private bool isSfxOn = true;
    private bool isMusicOn = true;

    // Turn SFX On
    public void TurnSFXOn()
    {
        isSfxOn = true;
        sfxSource.mute = false;
    }

    // Turn SFX Off
    public void TurnSFXOff()
    {
        isSfxOn = false;
        sfxSource.mute = true;
    }

    // Turn Music On
    public void TurnMusicOn()
    {
        isMusicOn = true;
        musicSource.mute = false;
    }

    // Turn Music Off
    public void TurnMusicOff()
    {
        isMusicOn = false;
        musicSource.mute = true;
    }
}
