using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Interactables : MonoBehaviour
{
    public GameObject dialougeBox;
    public TMP_Text dialougeText;
    public string[] dialouge;
    public bool isDialougeActive = false;
    public int lineNumber;

    public bool outlineEnabled = false;
    private Outline objectOutline;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialougeBox = GameObject.Find("DialogueBox");
        dialougeText = GameObject.Find("DialogueText").GetComponent<TMP_Text>();
        dialougeBox.SetActive(false);

        objectOutline = GetComponent<Outline>();
        if (objectOutline != null)
        {
            outlineEnabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lineNumber < dialouge.Length)
        {
            NextLine();
        }
        else
        {
            dialougeBox.GetComponent<RawImage>().enabled = false;
            dialougeText.enabled = false;
            lineNumber = 0;
        }
    }

    private void OnMouseDown()
    {
        if (dialougeBox != null)
        {
            if (!dialougeBox.activeSelf)
            {
                dialougeBox.SetActive(true);
                NextLine();
            }
            else
            {
                if (lineNumber < dialouge.Length)
                {
                    NextLine();
                }
                else
                {
                    dialougeBox.SetActive(false);
                    lineNumber = 0;
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        if (objectOutline != null)
        {
            objectOutline.enabled = true;
        }
    }

    private void OnMouseExit()
    {
        if (objectOutline != null)
        {
            objectOutline.enabled = false;
        }
    }


    void NextLine()
    {
        if (lineNumber < dialouge.Length)
        {
            dialougeBox.GetComponent<RawImage>().enabled = true;
            dialougeText.enabled = true;
            dialougeText.text = dialouge[lineNumber];
            lineNumber++;
        }
    }

}
