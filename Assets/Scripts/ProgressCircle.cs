using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressCircle : MonoBehaviour
{

    public float rate = 1;

    private Color initial        = new Color(0.863f, 0.208f, 0.271f);
    private Color intermediate   = new Color(0.749f, 0.569f, 0.024f);
    private Color final          = new Color(0.184f, 0.749f, 0.306f);

    public PlayerController owner;

    private UnityEngine.UI.Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        image = GetComponent<UnityEngine.UI.Image>();

        if ((image.fillAmount + Time.deltaTime * rate) > 1)
            owner.finishMining();

        image.fillAmount += Time.deltaTime * rate;

        image.color = initial;

        if (image.fillAmount < .5f)
        {
            image.color = Color.Lerp(initial, intermediate, (image.fillAmount - .3f) * 5);
        }
        else
        {
            image.color = Color.Lerp(intermediate, final, (image.fillAmount - .6f) * 5);
        }
        
    }
}
