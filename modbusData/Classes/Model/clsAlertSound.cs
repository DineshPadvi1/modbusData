// ClsAlertSound.cs
using System.Media;

public class clsAlertSound
{
    private SoundPlayer alertSoundPlayer;

    // Constructor: Initialize the SoundPlayer with the path to the alert sound file
    public clsAlertSound()
    {
        //string soundFilePath = "E:\\Bhavesh T\\Sound Files\\Alert wav\\error-2-126514.wav";
        string soundFilePath = "E:\\Bhavesh T\\Sound Files\\Alert wav\\error-3.wav";

        alertSoundPlayer = new SoundPlayer(soundFilePath);
    }

    // Method to play the alert sound
    public void PlayAlertSound()
    {
        alertSoundPlayer.Play();
    }
}

