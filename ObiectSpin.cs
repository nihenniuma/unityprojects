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
    { //���̧��
        isClick = false;
    }

    void OnMouseDown()
    { //��갴��

        isClick = true;
    }

    void Update()
    {
        nowPos = Input.mousePosition;
        offset = nowPos - oldPos;
        if (isClick)
        { //��갴�²�����
            transform.Rotate(0, -offset.x, 0);
            transform.Rotate(-offset.y, 0, 0);
            
        }
        oldPos = Input.mousePosition;
    }

    /// <summary>
    /// ���Collider��Ϊ���������
    /// </summary>
    public void AddBoxCollider()
    {
        BoxCollider box = gameObject.AddComponent<BoxCollider>();
        //����
        box.center = new Vector3(0, 1.2f, 0);
        box.size = new Vector3(2.4f, 2.4f, 2.4f);
    }
}
