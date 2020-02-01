using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupManager : MonoBehaviour
{

    private CanvasGroup group;

    private void Start()
    {
        group = GetComponent<CanvasGroup>();
    }

    public void SetVisibility(bool visible)
    {
        group.alpha = visible ? 1f : 0f;
        group.interactable = visible;
    }
}
