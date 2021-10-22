using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarRotate : MonoBehaviour
{
    // speed of rotating
    public float xSpeed = 2.0f;
    public float ySpeed = 2.0f;
    private Vector3 newPos;
    private Vector3 oldPos;
    private Vector3 offset;
    private float x = 0.0f;
    private float y = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            //when click the left button on the mouse
            x = Input.GetAxis("Mouse X") * xSpeed*0.02f;
            y = Input.GetAxis("Mouse Y") * ySpeed*0.02f;
            transform.Rotate(-y, -x, 0);
        }
        
        else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //鼠标滚动滑轮 值就会变化
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                //范围值限定
                if (Camera.main.fieldOfView <= 100)
                    Camera.main.fieldOfView += 2;
                if (Camera.main.orthographicSize <= 20)
                    Camera.main.orthographicSize += 0.5F;
            }
            //Zoom in  
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                //范围值限定
                if (Camera.main.fieldOfView > 2)
                    Camera.main.fieldOfView -= 2;
                if (Camera.main.orthographicSize >= 1)
                    Camera.main.orthographicSize -= 0.5F;
            }
            oldPos = Input.mousePosition;







        }
        
    }
}
