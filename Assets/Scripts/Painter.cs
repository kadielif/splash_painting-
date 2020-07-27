using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
  
    public Camera mainCamera;
    public Texture2D splashTexture;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit))
            {
                Drawing script = hit.collider.gameObject.GetComponent<Drawing> (); 
                if (null != script)
                    script.PaintOn(hit.textureCoord, splashTexture);
            }

        }
    }
}


