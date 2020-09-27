using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawWave : MonoBehaviour
{
    LineRenderer line;
    public Oscillator oscillator1,oscillator2,oscillator3;
    public KeyController keyController;
    Bluetooth bluetooth;
    float timer;
    public int lengthOfLineRenderer = 100;
   public float amplitude,waveSpeed, waveFrequency;
    List<Vector3> linePositions;

    private Vector3[] points;



    private Vector3 startPos, endPos;
    void Start()
    {
        linePositions = new List<Vector3>();
        line = GetComponent<LineRenderer>();
        line.positionCount = lengthOfLineRenderer;
        line.useWorldSpace = false;
        points = new Vector3[lengthOfLineRenderer];

        amplitude = 3;
        startPos = new Vector3(0, 0, 0);
        endPos = new Vector3(200, 0, 0);
        waveFrequency = Vector3.Distance(startPos, endPos) / lengthOfLineRenderer;

        bluetooth = FindObjectOfType<Bluetooth>();
    }

    // Update is called once per frame
    void Update()
    {


        SinWaveGraph();
       // SquareWaveGraph();

    }
    void SinWaveGraph()
    {

        amplitude =( oscillator1.amplitude + oscillator2.amplitude + oscillator3.amplitude);
       
        for (int i = 0; i < points.Length; i++)
        {
            float x = waveFrequency * i;
            float y = Mathf.Sin(x* (bluetooth.incomingDistance/500 )* Time.time) * 5;
            //float y = Mathf.Sin((float)oscillator1.frequency + (float)oscillator2.frequency + (float)oscillator3.frequency * x * Time.time)* amplitude;
            //float y = Mathf.Sin(x * oscillator1.SquareWave() * Time.time)* amplitude;
            points[i] = new Vector3(x, y, 0);
        }
        line.SetPositions(points);
    }
    /*
    void SquareWaveGraph()
    {
        amplitude = oscillator.gain * 4;
        for (int i = 0; i < points.Length; i++)
        {
            float x = waveFrequency * i;
            float y = Mathf.Sin(x * oscillator.SquareWave() * Time.time) * amplitude;
            points[i] = new Vector3(x, y, 0);
        }
        line.SetPositions(points);
    }
    void TraingleWaveGraph()
    {
        amplitude = oscillator.gain * 4;
        for (int i = 0; i < points.Length; i++)
        {
            float x = waveFrequency * i;
            float y = Mathf.Sin(x * oscillator.TriangleWave() * Time.time) * amplitude;
            points[i] = new Vector3(x, y, 0);
        }
        line.SetPositions(points);
    }
    */
}
