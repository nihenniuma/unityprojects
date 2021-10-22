using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObiectSpin : MonoBehaviour
{
    private bool isClick = false;
    private Vector3 nowPos;
    private Vector3 oldPos;
    private Vector3 offset;
    float length = 1;

    void Start()
    {
        AddBoxCollider();
    }

    void OnMouseUp()
    { //鼠标抬起
        isClick = false;
    }

    void OnMouseDown()
    { //鼠标按下

        isClick = true;
    }

    void Update()
    {
        nowPos = Input.mousePosition;
        offset = nowPos - oldPos;
        if (isClick)
        { //鼠标按下不松手
            transform.Rotate(0, -offset.x, 0);
            transform.Rotate(-offset.y, 0, 0);
            
        }
        oldPos = Input.mousePosition;
    }

    /// <summary>
    /// 添加Collider作为鼠标检测区域
    /// </summary>
    public void AddBoxCollider()
    {
        BoxCollider box = gameObject.AddComponent<BoxCollider>();
        //参数
        box.center = new Vector3(0, 1.2f, 0);
        box.size = new Vector3(2.4f, 2.4f, 2.4f);
    }
}
