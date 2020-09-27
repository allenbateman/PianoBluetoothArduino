using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private PianoController pianoController;
    Envelope envelope;

    private void Start()
    {
        pianoController = FindObjectOfType<PianoController>();
        envelope = FindObjectOfType<Envelope>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {       
        pianoController.PlayOscillators(int.Parse(gameObject.name));


        pianoController.keyPressed = true;

        pianoController.timeKeyPressed = 0;

        //set envelope gain
        envelope.currentGain = pianoController.oscillator1.amplitude;
        envelope.maxGain = pianoController.oscillator1.maxAmplitude;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        pianoController.keyPressed = false;

        pianoController.timeKeyRelease = 0;

        envelope.releaseLevel = pianoController.oscillator1.amplitude;
        envelope.releaseLevel = pianoController.oscillator2.amplitude;
        envelope.releaseLevel = pianoController.oscillator3.amplitude;
    }

}
