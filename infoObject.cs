using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infoObject : MonoBehaviour
{
	public Text infoText;
	public GameObject infoobject;

	// Start is called before the first frame update
	void Start() 
	{
	
	
	
	
	}
	private void OnMouseOver()
	{
		infoText.text = gameObject.name;
		infoText.transform.position = gameObject.transform.position;


	}
	private void OnMouseExit()
	{
		infoText.text = "";
	}
}
