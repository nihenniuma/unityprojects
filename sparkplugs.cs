using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class sparkplugs : MonoBehaviour
{
	public GameObject 
		Object
		;
	public Text lableText;
	public Vector3 lablePosition;
	// Start is called before the first frame update
	void Start()
    {
		lableText.text = gameObject.name;
		lableText.transform.position = gameObject.transform.position;
	}
	public void OnMouseOver()
	{
		lableText.text = gameObject.name;
		
		
	}
	public void OnMouseExit()
	{
		lableText.text = "";
	}
	// Update is called once per frame
	void Update()
    {
        
    }
}
