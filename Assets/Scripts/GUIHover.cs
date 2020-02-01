using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIHover : MonoBehaviour
{

    public Image image;

    void Update()
    {
        Vector3 textPos = Camera.main.WorldToScreenPoint(this.transform.position);
        image.transform.position = textPos;
    }
}
