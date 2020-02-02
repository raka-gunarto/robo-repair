using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigRobotBuilder : MonoBehaviour
{
    public GameObject builder;

    private int progressIndex = 0;
    private List<List<int>> partRequirements;

    private GameObject canvasGroup;

    public string name = "P0";
    public Vector2 initialUIPosition = new Vector2(20, 20);
    public Color color = Color.black;

    private Sprite woodItemIcon;
    private Sprite ironItemIcon;
    private Sprite copperItemIcon;

    private GameObject woodItem;
    private GameObject ironItem;
    private GameObject copperItem;

    private GameObject text;
    private GameObject woodText;
    private GameObject ironText;
    private GameObject copperText;

    private Font font;

    // Start is called before the first frame update
    void Start()
    {
        partRequirements = new List<List<int>>();
        partRequirements.Add(new List<int>() { 1, 2, 3 });
        partRequirements.Add(new List<int>() { 2, 2, 2 });
        partRequirements.Add(new List<int>() { 3, 3, 3 });
        partRequirements.Add(new List<int>() { 4, 4, 4 });
        partRequirements.Add(new List<int>() { 5, 5, 5 });
        partRequirements.Add(new List<int>() { 6, 6, 6 });
        partRequirements.Add(new List<int>() { 7, 7, 7 });

        canvasGroup = new GameObject();
        canvasGroup.transform.parent = GameObject.Find("Canvas").transform;
        canvasGroup.transform.position = initialUIPosition;

        woodItemIcon   = Resources.Load<Sprite>("Icons/Wood");
        ironItemIcon   = Resources.Load<Sprite>("Icons/Iron");
        copperItemIcon = Resources.Load<Sprite>("Icons/Copper");

        font = Resources.Load<Font>("Fonts/BPreplayBoldItalics");

        text = new GameObject();
        woodText = new GameObject();
        ironText = new GameObject();
        copperText = new GameObject();
        woodItem = new GameObject();
        ironItem = new GameObject();
        copperItem = new GameObject();

        text.transform.parent = canvasGroup.transform;
        woodText.transform.parent = canvasGroup.transform;
        ironText.transform.parent = canvasGroup.transform;
        copperText.transform.parent = canvasGroup.transform;
        woodItem.transform.parent   = canvasGroup.transform;
        ironItem.transform.parent   = canvasGroup.transform;
        copperItem.transform.parent = canvasGroup.transform;


        text.transform.localPosition        = new Vector2(4, 0);
        woodText.transform.localPosition = new Vector2(27, 0);
        ironText.transform.localPosition = new Vector2(60, 0);
        copperText.transform.localPosition = new Vector2(90, 0);

        woodItem.transform.localPosition    = new Vector2(30 , 0);
        ironItem.transform.localPosition    = new Vector2(63, 0);
        copperItem.transform.localPosition  = new Vector2(93, 0);

        Text textComponent = text.AddComponent<Text>();
        Text woodTextComponent = woodText.AddComponent<Text>();
        Text ironTextComponent = ironText.AddComponent<Text>();
        Text copperTextComponent = copperText.AddComponent<Text>();

        Image woodItemImage = woodItem.AddComponent<Image>();
        Image ironItemImage = ironItem.AddComponent<Image>();
        Image copperItemImage = copperItem.AddComponent<Image>();

        textComponent.text = name+":";
        textComponent.font = font;
        textComponent.color = color;
        textComponent.fontSize = 12;
        textComponent.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        textComponent.alignment = TextAnchor.MiddleLeft;

        woodTextComponent.text = partRequirements[progressIndex][0].ToString();
        woodTextComponent.font = font;
        woodTextComponent.color = color;
        woodTextComponent.fontSize = 12;
        woodTextComponent.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        woodTextComponent.alignment = TextAnchor.MiddleLeft;

        ironTextComponent.text = partRequirements[progressIndex][1].ToString();
        ironTextComponent.font = font;
        ironTextComponent.color = color;
        ironTextComponent.fontSize = 12;
        ironTextComponent.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        ironTextComponent.alignment = TextAnchor.MiddleLeft;

        copperTextComponent.text = partRequirements[progressIndex][2].ToString();
        copperTextComponent.font = font;
        copperTextComponent.color = color;
        copperTextComponent.fontSize = 12;
        copperTextComponent.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        copperTextComponent.alignment = TextAnchor.MiddleLeft;

        woodItemImage.sprite = woodItemIcon;
        ironItemImage.sprite = ironItemIcon;
        copperItemImage.sprite = copperItemIcon;

        woodItem.transform.localScale = new Vector2(0.2f, 0.2f);
        ironItem.transform.localScale = new Vector2(0.2f, 0.2f);
        copperItem.transform.localScale = new Vector2(0.2f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != builder) return;

        // TODO: check it actually has the part
        gameObject.GetComponent<BigRobotProgressManager>().ProgressForward();
    }
}
