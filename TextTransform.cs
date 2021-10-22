using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTransform : MonoBehaviour
{
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "Front";
    }
    public void Front()
    {
        text.text = "Front";
    }
    public void Left()
    {
        text.text = "Left";
    }
    public void Right()
    {
        text.text = "Right";
    }
    public void Above()
    {
        text.text = "Above";
    }
    public void Below()
    {
        text.text = "Below";
    }
    public void Behind()
    {
        text.text = "Behind";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
