using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnobController : MonoBehaviour
{
    [SerializeField] Transform handle;
    [SerializeField] Image fill;
    [SerializeField] Text valTxt;

    float value;

    Vector3 mousePos;
    Vector3 knobPos;

    private Knob knob;

    private void Start()
    {
        knob = GetComponent<Knob>();
    }

    public void OnHandleDrag()
    {
        mousePos = Input.mousePosition;
        knobPos = Camera.main.WorldToScreenPoint(handle.position);

        double angle = 234 - getAngle(knobPos);
        angle = (angle <= 0) ? (360 + angle) : angle;

        if (angle >= 0 && angle <= 285)
        {
            Quaternion r = Quaternion.AngleAxis((float)-angle, Vector3.forward);
            handle.rotation = r;
            value = (float)(angle / 285);           
            knob.value = value;
            fill.fillAmount = value;
            valTxt.text =(angle/285*100).ToString();
        }
    }
    public double getAngle(Vector3 screenPoint)
    {
        double dx = mousePos.x - screenPoint.x;
        double dy = -(mousePos.y - screenPoint.y);

        double inRads = Mathf.Atan2((float)dy, (float)dx);

        if (inRads < 0)
            inRads =  Mathf.Abs((float)inRads);
        else
            inRads = 2 * Mathf.PI - inRads;

        return inRads * Mathf.Rad2Deg;
    }
}
