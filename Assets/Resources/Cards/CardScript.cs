using System;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public int pairNumber = 0;

    private bool faseUp = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool flipCard()
    {
        if (faseUp)
        {
            return false;
        }

        faseUp = true;
        transform.Rotate(0.0f, 0.0f, 0.0f);

        return true;
    }

    public void returnCard()
    {
        faseUp = false;
        transform.Rotate(0.0f, 0.0f, 90);
    }

    public void assassingPair(int number) 
    {
        pairNumber = number;
        var mat = Resources.Load<Material>(String.Format("Cards/TestMaterials/m{0}", number));
        if (mat != null)
        {
            gameObject.GetComponent<Renderer>().material = mat;
        }
    }
}
