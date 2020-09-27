using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SynthType { Sin,Triangle , Square ,SawWave}
public class Oscillator : MonoBehaviour
{

    AudioSource audioSource;

    [Range(50f, 1000f)]
    public double frequency; 
    private double increment;//step note
    private double phase;
    private double sampling_frequency = 48000.0;


    [Range(0.1f, 1f)]
    public float amplitude;
    [HideInInspector]
    public float maxAmplitude;

    public float volume;
    public float pitch;

    public int thisFreq;
    public SynthType synthType;

    float deltaTime;

    private void Start()
    {
        synthType = SynthType.Sin;
        audioSource = GetComponent<AudioSource>();
        maxAmplitude = 1;
        amplitude = 0;

        audioSource.volume = 0.1f;
    }
    private void Update()
    {
        deltaTime = Time.deltaTime;
    }
    private void OnAudioFilterRead(float[] data, int channels)
    {
        if(amplitude > 1)
        {
            amplitude = 1;
        }
        increment = frequency * 2.0 * Mathf.PI / sampling_frequency;
 

        for (int i =0; i < data.Length; i+= channels)
        {
            phase += increment;

            switch (synthType)
            {
                case SynthType.Sin:
                    data[i] = SinWave();
                    break;
                case SynthType.Triangle:
                    data[i] = TriangleWave();
                    break;
                case SynthType.Square:
                    data[i] = SquareWave();
                    break;
                case SynthType.SawWave:
                    data[i] = SawWave();
                    break;
            }
            if (channels ==2 )
            {
                data[i + 1] = data[i];
            }
            if(phase>(Mathf.PI *2 ))
            {
                phase = 0.0;
            }
        }
    }
    public float SinWave()
    {
        return (float)(amplitude * Mathf.Sin((float)phase));
    }
    public float TriangleWave()
    {
        return (float)(amplitude * (double)Mathf.PingPong((float)phase, 1.0f));
    }
    public float SquareWave()
    {
        if(amplitude * Mathf.Sin((float)phase)>=0 * amplitude)
        {
            return (float)amplitude * 0.6f;
        }
        else
        {
            return (-(float)amplitude) * 0.6f;
        }
    }
    public float SawWave()
    {

        float outPut = 0;

        for (float n = 1.0f; n < 100.0; n++)
            outPut += (Mathf.Sin(n * amplitude * deltaTime)) / n;

        return (float)outPut * (2/Mathf.PI);
    }
}
