using NAudio.Wave;

public class AudioManager
{
    private IWavePlayer outputDevice;
    private AudioFileReader audioFileReader;

    public AudioManager()
    {
        outputDevice = new WaveOutEvent();
    }

    public void PlayMusic(string filePath)
    {
        StopMusic(); 

        audioFileReader = new AudioFileReader(filePath);
        outputDevice.Init(audioFileReader);
        outputDevice.Play();
    }

    public void StopMusic()
    {
        if (outputDevice != null)
        {
            outputDevice.Stop();
        }

        audioFileReader?.Dispose();
    }

    public void Dispose()
    {
        StopMusic();
        outputDevice.Dispose();
    }
}
