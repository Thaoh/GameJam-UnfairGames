using UnityEngine;
using TMPro;

public class Interactables : MonoBehaviour
{
    public GameObject dialougeBox;
    public TMP_Text dialougeText;
    public bool isDialougeActive = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (dialougeBox !=null)
        {
            dialougeBox.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (dialougeBox != null)
        {
            isDialougeActive = !isDialougeActive;
            dialougeBox.SetActive(isDialougeActive);
        }
    }
}
