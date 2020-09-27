using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;


public class PianoController : MonoBehaviour
{
    public Oscillator oscillator1, oscillator2, oscillator3;
    public GameObject[] keys;
    private float interval = 1.059463094359f;
    private float rootFrequency = 261.63f;
    [Range(1,5)]
    public int octave;
    Bluetooth bluetooth;
    public float timeKeyPressed,timeKeyRelease;
    public bool keyPressed;
    
    Envelope envelope;


    float frequency;
    void Start()
    {
        bluetooth = FindObjectOfType<Bluetooth>();
        keys = new GameObject[transform.childCount];
        envelope = FindObjectOfType<Envelope>();

       for (int i = 0; i< transform.childCount; i++)
       {
            keys[i] = transform.GetChild(i).gameObject;
            keys[i].AddComponent<AudioSource>();
       }
        oscillator1.frequency = rootFrequency;
        oscillator2.frequency = rootFrequency;
        oscillator3.frequency = rootFrequency;
        octave = 1;
        SoundOFF();
    }
    private void Update()
    {
        if (keyPressed)
        {
            timeKeyPressed += Time.deltaTime;
            SoundON();
        }
        if (!keyPressed)
        {
            timeKeyRelease += Time.deltaTime;
            bluetooth.sendingData = false;
            SoundOFF();
        }
    }
    public void PlayOscillators(int _key)
    {
        //send oscillator frequency
        oscillator1.frequency = octave * rootFrequency * Mathf.Pow(interval, _key);
        oscillator2.frequency = octave * rootFrequency * Mathf.Pow(interval, _key);
        oscillator3.frequency = octave * rootFrequency * Mathf.Pow(interval, _key);

        //send bluetooth frequency
        frequency = octave * rootFrequency * Mathf.Pow(interval, _key);
        bluetooth.sendingData = true;
        bluetooth.SendFrequency(frequency.ToString());
    }
    public void SoundON()
    {

        if (timeKeyPressed < envelope.attack)
        {
           oscillator1.amplitude = envelope.Attack(timeKeyPressed);
           oscillator2.amplitude = envelope.Attack(timeKeyPressed);
           oscillator3.amplitude = envelope.Attack(timeKeyPressed);

        }else if (timeKeyPressed < envelope.attack + envelope.delay && timeKeyPressed > envelope.attack)
        {
            oscillator1.amplitude = envelope.Delay(timeKeyPressed);
            oscillator2.amplitude = envelope.Delay(timeKeyPressed);
            oscillator3.amplitude = envelope.Delay(timeKeyPressed);

        }else 
        {
            oscillator1.amplitude = envelope.Sustain();
            oscillator2.amplitude = envelope.Sustain();
            oscillator3.amplitude = envelope.Sustain();
        }
    }
    public void SoundOFF()
    {

        if (timeKeyRelease < envelope.release)
        {
            oscillator1.amplitude = envelope.Release(timeKeyRelease);
            oscillator2.amplitude = envelope.Release(timeKeyRelease);
            oscillator3.amplitude = envelope.Release(timeKeyRelease);
        }
    }

}
