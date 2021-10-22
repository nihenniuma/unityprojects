using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ViewButtons : MonoBehaviour
{
    //Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 4, 5);
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }
    public void FrontFunc()
    {
        transform.position = new Vector3(0, 4, 5);
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }
    public void LeftFunc()
    {
        transform.position = new Vector3(5, 4, 0);
        transform.Rotate(0, -90, 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
    }
    public void RightFunc()
    {
        transform.position = new Vector3(-5, 4, 0);
        
        transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
    }
    public void AboveFunc()
    {
        transform.position = new Vector3(0, 9, 0);
        
        transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
    }
    public void BelowFunc()
    {
        transform.position = new Vector3(0, -1, 0);
        
        transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
    }
    public void BehindFunc()
    {
        transform.position = new Vector3(0, 4, -5);
        
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0,90,0);
    }
}
