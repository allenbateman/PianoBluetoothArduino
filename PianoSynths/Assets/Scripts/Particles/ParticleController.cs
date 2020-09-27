using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleController : MonoBehaviour
{
    ParticleSystem particles;
    Bluetooth data;
    float distance;
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        data = FindObjectOfType<Bluetooth>();
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = data.incomingDistance / 100;
        var shape = particles.shape;
        shape.radius = distance;
        var main = particles.main;
       // main.startColor = new Color((distance/85) / 255,0.2f, 0.2f);
    }
}
