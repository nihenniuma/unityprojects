using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpinByMouse : MonoBehaviour
{
   
    public Vector3 offsetMousePositon;//屏幕坐标变化量
    public Vector3
        normalized,
        mousePositionOriginal,
        mouseWorldPositionOriginal,
        mousePositionNow,
        mouseWorldPositonNow,
        a,
        b,
        c;
    public float 
        normalDistance = 3,
        mouseZoomMax = 5,
        mouseZoomMin = 1,
        mouseSensitivity = 1;
    // Start is called before the first frame update//下面滚轮调节的时候，用float类型地变量就会出问题，用int型就不会出问题？？？？
    private float
        xSpeed = 250.0f,
        ySpeed = 120.0f,
        x = 0.0f,
        y = 0.0f;
    public Transform target;
    private int MouseWheelSensitivity = 1;
    private int MouseZoomMin = 1;
    private int MouseZoomMax = 5;

    


    private int yMinLimit = -20;
    private int yMaxLimit = 80;

    

    private Vector3 screenPoint;
    private Vector3 offset;

    private Quaternion rotation = Quaternion.Euler(new Vector3(30f, 0f, 0f));
    private Vector3 CameraTarget;
    void Start()
    {
        CameraTarget = target.position;

       
        

        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        mousePositionOriginal = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(1))
        {
            screenPoint = Camera.main.WorldToScreenPoint(target.transform.position);
            target.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            
        
        } else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            normalized = (transform.position - CameraTarget).normalized;
            
            if (MouseZoomMax >= normalDistance && MouseZoomMin <= normalDistance) 
            {
                normalDistance -= Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity;
            } else if (MouseZoomMin > normalDistance) 
            {
                normalDistance = MouseZoomMin;


            } else if (MouseZoomMax < normalDistance) 
            {
                normalDistance = MouseZoomMax;
            }
            transform.position = normalized * normalDistance;
        } else if (Input.GetMouseButton(0)) 
        {
            mouseWorldPositionOriginal = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionOriginal.x,mousePositionOriginal.y,-4));
            mousePositionNow = Input.mousePosition;
            mouseWorldPositonNow = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionNow.x, mousePositionNow.y, -4));
            a = new Vector3(0, 0, -4);
            b = mouseWorldPositonNow - mouseWorldPositionOriginal;
            c = Vector3.Cross(a, b).normalized;
            Debug.Log(mousePositionOriginal);
            Debug.Log(mousePositionNow);
            Debug.Log(mouseWorldPositionOriginal);
            Debug.Log(mouseWorldPositonNow);
            Debug.Log(c);
            target.transform.Rotate(new Vector3(c.x, c.y, c.z),Space.World);
            

           
        }

        mousePositionOriginal = mousePositionNow;
        

    }
    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
