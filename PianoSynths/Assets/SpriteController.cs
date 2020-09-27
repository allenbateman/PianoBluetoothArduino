using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public Sprite sprite;
    Bluetooth data;
    float distance;
    void Start()
    {
        data = FindObjectOfType<Bluetooth>();
    }

    // Update is called once per frame
    void Update()
    {

            distance = data.incomingDistance;
            transform.localScale = new Vector3(1,(distance/100 )*  1 +1,0);
        


    }
}
