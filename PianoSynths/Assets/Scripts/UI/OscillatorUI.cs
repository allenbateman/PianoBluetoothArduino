using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OscillatorUI : MonoBehaviour
{
    public Oscillator oscillator;
    private AudioSource audioSource;
    private Toggle toggle;


    List<Knob> knobs = new List<Knob>();


    void Start()
    {
        if (oscillator!=null)
         audioSource =  oscillator.GetComponent<AudioSource>();

        toggle = gameObject.GetComponentInChildren<Toggle>();
        //knobs.AddRange(FindObjectsOfType<Knob>());
        knobs = new List<Knob>(gameObject.GetComponentsInChildren<Knob>());
    }
    void Update()
    {
       if( toggle.isOn)
       {
            oscillator.gameObject.SetActive(true);
       }
       else
       {
            oscillator.gameObject.SetActive(false);
       }
       foreach(Knob k in knobs)
       {
            if(k.type ==  KnobType.volume)
            {
                audioSource.volume = k.value;
            }else if(k.type == KnobType.pitch)
            {
                audioSource.pitch = k.value;
            }
       }
    }
    public void DisableOSC()
    {
        oscillator.gameObject.SetActive(!oscillator.gameObject.activeSelf);
    }
    public void OscillatorWaveForm(int val)
    {
        if(val == 0)
        {
            oscillator.synthType = SynthType.Sin;
        }
        if (val == 1)
        {
            oscillator.synthType = SynthType.Square;
        }
        if (val == 2)
        {
            oscillator.synthType = SynthType.Triangle;
        }
        if(val  == 3)
        {
            oscillator.synthType = SynthType.SawWave;
        }
    }
}
