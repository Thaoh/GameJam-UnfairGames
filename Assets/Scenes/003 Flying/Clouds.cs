using UnityEngine;

public class Clouds : MonoBehaviour
{
    private float sizeModifier = 0.7f;

    public void Duplicate()
    {
        if (transform.localScale.x > 0.5f)
        {
            // Change size to 70%
            transform.localScale *= sizeModifier;
            // Duplicate the gameObject
            Instantiate(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
