using System;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    private Quaternion FACE_UP_ROTATION = Quaternion.Euler(0, 0, 180);
    private Quaternion FACE_DOWN_ROTATION = Quaternion.identity;
    private float ROTATION_SPEED = 1;

    public int pairNumber = 0;

    private bool faseUp = false;
    private float rotationProgress = -1;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationProgress >= 0 && rotationProgress < 1)
        {
            if (faseUp)
            {
                TurnCard(FACE_DOWN_ROTATION, FACE_UP_ROTATION);
            } else
            {
                TurnCard(FACE_UP_ROTATION, FACE_DOWN_ROTATION);
            }
        }
    }

    private void TurnCard(Quaternion startRotation, Quaternion endRotation)
    {
        rotationProgress += Time.deltaTime * ROTATION_SPEED;
        transform.rotation = Quaternion.Lerp(startRotation, endRotation, Math.Min(1, rotationProgress));
    }

    public bool flipCard()
    {
        Debug.Log("flip");
        if (faseUp)
        {
            return false;
        }

        faseUp = true;
        rotationProgress = 0;

        return true;
    }

    public void returnCard()
    {
        faseUp = false;
        rotationProgress = 0;
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
