using UnityEngine;
using System.Collections;

public class Spin02 : MonoBehaviour
{
    public Transform CenObj;//Χ�Ƶ�����
    private Vector3 Rotion_Transform;
    private new Camera camera;
    void Start()
    {
        camera = GetComponent<Camera>();
        Rotion_Transform = CenObj.position;
    }
    void Update()
    {
        Ctrl_Cam_Move();
        Cam_Ctrl_Rotation();
    }
    //��ͷ��Զ��ͽӽ�
    public void Ctrl_Cam_Move()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            transform.Translate(Vector3.forward * 1f);//�ٶȿɵ�  ���е���
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            transform.Translate(Vector3.forward * -1f);//�ٶȿɵ�  ���е���
        }
    }
    //���������ת
    public void Cam_Ctrl_Rotation()
    {
        var mouse_x = Input.GetAxis("Mouse X");//��ȡ���X���ƶ�
        var mouse_y = -Input.GetAxis("Mouse Y");//��ȡ���Y���ƶ�
        if (Input.GetKey(KeyCode.Mouse1))
        {
            transform.RotateAround(Rotion_Transform, Vector3.up, mouse_x * 5);
            transform.RotateAround(Rotion_Transform, transform.right, mouse_y * 5);
        }
    }
   
}
