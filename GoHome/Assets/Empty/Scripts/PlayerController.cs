using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int SENSITIVETY;
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(-Input.GetAxis("Horizontal") * SENSITIVETY, 0, 0);
        }
    }
}

