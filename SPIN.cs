using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPIN : MonoBehaviour
{
    //��ת���Ƕ�
    public int yMinLimit = -20;
    public int yMaxLimit = 80;
    //��ת�ٶ�
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;
    //��ת�Ƕ�
    private float x = 0.0f;
    private float y = 0.0f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //����Ļ����ת��Ϊ��������  ScreenToWorldPoint������z�᲻��Ϊ0����Ȼ�����������λ�ã���Input.mousePosition��z��Ϊ0
            //z�����10��ԭ��������������ǣ�0��0��-10����������������ǣ�0��0��0�������Լ���10��������ת���������������ľ���
            Vector3 temp = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            transform.position = temp;
        }
        else if (Input.GetMouseButton(1))
        {
            //Input.GetAxis("MouseX")��ȡ����ƶ���X��ľ���
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            //ŷ����ת��Ϊ��Ԫ��
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            transform.rotation = rotation;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //���������� ֵ�ͻ�仯
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                //��Χֵ�޶�
                if (Camera.main.fieldOfView <= 100)
                    Camera.main.fieldOfView += 2;
                if (Camera.main.orthographicSize <= 20)
                    Camera.main.orthographicSize += 0.5F;
            }
            //Zoom in  
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                //��Χֵ�޶�
                if (Camera.main.fieldOfView > 2)
                    Camera.main.fieldOfView -= 2;
                if (Camera.main.orthographicSize >= 1)
                    Camera.main.orthographicSize -= 0.5F;
            }
        }
    }

    //�Ƕȷ�Χֵ�޶�
    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}

