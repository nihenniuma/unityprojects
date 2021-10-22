using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EngineValve
{
	public GameObject
		ValveObjects,
		SpringObjects;
	public float
		OpenPhase,
		ClosePhase;
	[HideInInspector]
	public Vector3 DefPos;
}
 [System.Serializable]
public class EngineObjects
{
	public EngineValve[]
		intakeValves,
		ExhaustValves;

	public GameObject
		Flywheel,
		Crankshaft,
		CamshaftIntake,
		CamshaftExhaust,
		Rod1,
		Rod2,
		Rod3,
		Rod4,
		Rod1Target,
		Rod2Target,
		Rod3Target,
		Rod4Target,
		Piston1,
		Piston2,
		Piston3,
		Piston4;




	
}



public class InterEngine : MonoBehaviour
{
	// Start is called before the first frame update
	
	public EngineObjects elements;
	public float
			IntakePhase,
			ExhaustPhase,
			Piston1Pos,
			DeltPos;
	public Vector3
			offset,
			formerValvePos,
			formerSpringPos,
			ValveOffset,
			ValveSpringOffset;
	public string objectNameLable;
	public GameObject 
		
		hitObject;
	public Vector3 lablePosition;
	public Text lableText;
	public Text infoText;
	public GameObject infoobject;

	void Start()
    {
		lableText.text = "";
		DeltPos = elements.Piston1.transform.position.y - elements.Rod1.transform.position.y;
		ValveOffset = new Vector3(0, 0, 0.01f);
		ValveSpringOffset = new Vector3(0, 0, 0.29f);
		foreach (var valve in elements.intakeValves)
		{
			valve.DefPos = valve.ValveObjects.transform.localPosition;
		}
		foreach (var valve in elements.ExhaustValves)
		{
			valve.DefPos = valve.ValveObjects.transform.localPosition;
		}
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



	// Update is called once per frame
	void Update()
    {

		lablePosition = Input.mousePosition;
		IntakePhase = elements.CamshaftIntake.transform.localEulerAngles.z;
		ExhaustPhase = elements.CamshaftExhaust.transform.localEulerAngles.z;
		//飞轮上的轴
		elements.Crankshaft.transform.Rotate(new Vector3(0, 0, 1f));
		//活塞下的连杆
		elements.Rod1.transform.LookAt(elements.Rod1Target.transform, elements.Rod1.transform.up);
		elements.Rod2.transform.LookAt(elements.Rod2Target.transform, elements.Rod2.transform.up);
		elements.Rod3.transform.LookAt(elements.Rod3Target.transform, elements.Rod3.transform.up);
		elements.Rod4.transform.LookAt(elements.Rod4Target.transform, elements.Rod4.transform.up);
		//进气杆和排气杆
		elements.CamshaftExhaust.transform.Rotate(new Vector3(0, 0, 0.5f));
		elements.CamshaftIntake.transform.Rotate(new Vector3(0, 0, 0.5f));
		//活塞
		elements.Piston1.transform.position =
			new Vector3(elements.Piston1.transform.position.x, elements.Rod1.transform.position.y + DeltPos, elements.Piston1.transform.position.z);
		elements.Piston2.transform.position =
			new Vector3(elements.Piston2.transform.position.x, elements.Rod2.transform.position.y + DeltPos, elements.Piston2.transform.position.z);
		elements.Piston3.transform.position =
			new Vector3(elements.Piston3.transform.position.x, elements.Rod3.transform.position.y + DeltPos, elements.Piston3.transform.position.z);
		elements.Piston4.transform.position =
			new Vector3(elements.Piston4.transform.position.x, elements.Rod4.transform.position.y + DeltPos, elements.Piston4.transform.position.z);
		foreach (var valve in elements.ExhaustValves)
		{
			float t = 0;
			if (ExhaustPhase > valve.OpenPhase && ExhaustPhase < valve.ClosePhase)
			{
				t = ((ExhaustPhase - valve.OpenPhase) / (valve.ClosePhase - valve.OpenPhase)) * 2;

				if (t > 1)
				{
					t = 1 - (t - 1);
				}
				float b = Mathf.SmoothStep(0, 1, t);
				valve.ValveObjects.transform.localPosition = Vector3.Lerp(valve.DefPos, valve.DefPos - ValveOffset, b);
				valve.SpringObjects.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one - ValveSpringOffset, b);
			}
		}
		foreach (var valve in elements.intakeValves)
		{
			float t = 0;
			if (IntakePhase > valve.OpenPhase && IntakePhase < valve.ClosePhase)
			{
				t = ((IntakePhase - valve.OpenPhase) / (valve.ClosePhase - valve.OpenPhase)) * 2;

				if (t > 1)
				{
					t = 1 - (t - 1);
				}
				float b = Mathf.SmoothStep(0, 1, t);
				valve.ValveObjects.transform.localPosition = Vector3.Lerp(valve.DefPos, valve.DefPos - ValveOffset, b);
				valve.SpringObjects.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one - ValveSpringOffset, b);
			}



		}

	}




		//elements.Piston1.transform.position = elements.Rod1.transform.position;
}

