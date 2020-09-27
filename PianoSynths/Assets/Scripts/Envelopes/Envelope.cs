using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envelope : MonoBehaviour
{
    Oscillator oscillator;
    //[HideInInspector]
    public float time;

    [Range(0,3)]
    public float attack, delay, release;
    [Range(0, 1)]
    public float attackLevel, sustainLevel, releaseLevel;
    //[HideInInspector]
    public float currentGain, maxGain;



    public float Attack(float _time)
    {
       return Slope(attackLevel, 0 , attack, 0 )*_time;
    }
    public float Delay(float _time)
    {
        return Slope(sustainLevel,attackLevel, attack + delay,  attack ) * (_time-attack) + attackLevel;
    }
    public float Sustain()
    {
        return sustainLevel;
    }
    public float Release(float _time)
    {
        return Slope(0, releaseLevel, release , 0) * _time + releaseLevel;
    }
    float Slope(float y2, float y1,float x2,float x1)
    {
        return (y2 - y1) / (x2 - x1);
    }
}
