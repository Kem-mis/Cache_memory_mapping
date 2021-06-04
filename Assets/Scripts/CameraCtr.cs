using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtr : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float speed = 0.01f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //滚轮向后
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            
                Camera.main.fieldOfView += 2;
        }
        //Zoom in滚轮向前
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            
          
               Camera.main.fieldOfView -= 2;
        }
        //↑↓方向键
        float h = Input.GetAxis("Horizontal");
        //←→方向键
        float v = Input.GetAxis("Vertical");
        Vector3 offset = Vector3.zero;
        offset.x = h;
        offset.y = v;
        transform.position += offset;
    }
}
