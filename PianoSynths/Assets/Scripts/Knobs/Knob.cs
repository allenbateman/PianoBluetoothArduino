using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum KnobType
{
    volume = 0,
    pitch = 1,
    octave = 2
}

public class Knob : MonoBehaviour
{
    public KnobType type;
    [HideInInspector]
    public float value;
}
