using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


[System.Serializable]
public class EngineVariation{
	public GameObject[] gameObjects;
}


[System.Serializable]
public class Tweaks{
	public float 
		TransparencyEnablingTime,
		TransparencyDisablingTime;

	[Range(0,1)]
	public float TransparencyValue=0.1f;
}
	

[System.Serializable]
public class Valve{
	public GameObject 
		ValveGameobject,
		SpringGameobject;

	public float 
		OpenPhase,
		ClosePhase;

	[HideInInspector]
	public Vector3 DefPos;
}


[System.Serializable]
public class EngineElements{
	public Valve[] 
		intakeValves, 
		exhaustValves;

	public GameObject 
		EngineBlock,
		CylinderHead,
		Gearbox,
		Clutch,
		Flywheel,
		IntakeManifolds,
		FuelRail,
		ExhaustManifolds,
		CylinderHeadCovers,
		SparkPlugWires,
		SparkPlugs,
		OilPan,
		Crankshaft,
		CamshaftIntake1,
		CamshaftIntake2,
		CamshaftExhaust1,
		CamshaftExhaust2,
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
		Piston4,
		GearboxPrimaryShaft,
		GearboxSecondaryShaft,
		Gear2,
		Gear3,
		Gear4,
		Gear5,
		StarterGear1,
		StarterGear2,
		DistributorGear,
		TensionPulley,
		WaterPumpPulley,
		GeneratorPulley,
		TimingBelt,
		GeneratorBelt,
		TurboFan;
}


public class Enginei4 : MonoBehaviour {
	[Header("System")]
	public EngineElements elements;

	public EngineVariation[] engineVariations;

	public GameObject[] TransparentGameobjects;

	public Material 
		FadeMaterial, 
		OpaqueMaterial;


	[Header("Contorls")]

	public bool ShowGUI;

	[Range(0,30)]
	public float RPM;

	public Tweaks tweaks;

	private Vector3 
		Piston1DefPos,
		Piston2DefPos,
		Piston3DefPos,
		Piston4DefPos,
		Rod1DefPos,
		Rod2DefPos,
		Rod3DefPos,
		Rod4DefPos,
		ValveIntake1DefPos,
		ValveIntake2DefPos,
		ValveExhaust1DefPos,
		ValveExhaust2DefPos,
		ValveSpringOffset,
		ValveOffset;

	private float 
		IntakePhase,
		ExhaustPhase,
		Piston1Delta,
		Piston2Delta,
		Piston3Delta,
		Piston4Delta;

	private Material 
		TimingBeltMaterial,
		GeneratorBeltMaterial;
		

	void Start () {
		ValveOffset = new Vector3 (0, 0, 0.01f);
		ValveSpringOffset = new Vector3 (0, 0, 0.29f);

		TimingBeltMaterial = elements.TimingBelt.GetComponent<MeshRenderer> ().material;
		GeneratorBeltMaterial = elements.GeneratorBelt.GetComponent<MeshRenderer> ().material;

		Piston1DefPos = elements.Piston1.transform.localPosition;
		Piston2DefPos = elements.Piston2.transform.localPosition;
		Piston3DefPos = elements.Piston3.transform.localPosition;
		Piston4DefPos = elements.Piston4.transform.localPosition;

		Rod1DefPos = transform.InverseTransformPoint (elements.Rod1.transform.position);
		//函数返回把transform作为世界中心以后Rod1的位置坐标
		Rod2DefPos = transform.InverseTransformPoint (elements.Rod2.transform.position);
		Rod3DefPos = transform.InverseTransformPoint (elements.Rod3.transform.position);
		Rod4DefPos = transform.InverseTransformPoint (elements.Rod4.transform.position);

		foreach (var valve in elements.intakeValves)
			//首先说一下foreach有的也叫增强for循环，foreach其实是for循环的一个特殊简化版。
			//再说一下foreach的书写格式：
			//for（元素类型  元素名称 ： 遍历数组（集合）（或者能进行迭代的））{语句}
			//foreach虽然是for循环的简化版本，但是并不是说foreach就比for更好用，foreach适用于循环次数未知，或者计算循环次数比较麻烦情况下使用效率更高，但是更为复杂的一些循环还是需要用到for循环效率更高。
			valve.DefPos = valve.ValveGameobject.transform.localPosition;

		foreach (var valve in elements.exhaustValves)
			valve.DefPos = valve.ValveGameobject.transform.localPosition;

	}
	/*C# 中的var关键字
	var 是3.5新出的一个定义变量的类型，其实也就是弱化类型的定义，VAR可代替任何类型

	编译器会根据上下文来判断你到底是想用什么类型的，

	至于什么情况下用到VAR ，我想就是你无法确定自己将用的是什么类型，

	就可以使用VAR 类似 OBJECT 但是效率比OBJECT高点。



	或者通俗的讲：

	  var可以理解为匿名类型，我们可以认为它是一个声明变量的占位符。

	   它主要用于在声明变量时，无法确定数据类型时使用。



	使用var定义变量时有以下四个特点:

	1. 必须在定义时初始化。也就是必须是var s = “abcd”形式，而不能是如下形式: var s; s = “abcd”;

	2. 一但初始化完成，就不能再给变量赋与初始化值类型不同的值了。

	3. var要求是局部变量。

	4. 使用var定义变量和object不同，它在效率上和使用强类型方式定义变量完全一样。*/


	//OnGUI绘制了一个带有调节速度与功能按键的UI界面
	void OnGUI(){
		if (!ShowGUI) return;

		GUI.Box(new Rect(Screen.width-207,5,207,380)," ");
	/*在 GUI 层上创建一个框。
	通过使用 GUIContent 参数， 框可以包含文本和/或图像，以及可选的工具提示。
	您也可以使用 GUIStyle 调整框中项目的布局、文本颜色和其他属性。*/
		GUI.Label (new Rect (Screen.width-200, 5, 80, 20), "RPM");

		RPM = GUI.HorizontalSlider (new Rect (Screen.width-170, 10, 160, 20), RPM, 0, 30);
		
		if (GUI.Button (new Rect (Screen.width-200, 30, 95, 20), "Tuning 1")) SetVariation (0);
		if (GUI.Button (new Rect (Screen.width-100, 30, 95, 20), "Tuning 2")) SetVariation (1);
		if (GUI.Button (new Rect (Screen.width-200, 55, 95, 20), "Tuning 3")) SetVariation (2);
		if (GUI.Button (new Rect (Screen.width-100, 55, 95, 20), "Tuning 4")) SetVariation (3);

		if (GUI.Button (new Rect (Screen.width-200, 80, 195, 20), "Enable transparency")) EnableTransparency ();
		if (GUI.Button (new Rect (Screen.width-200, 105, 195, 20), "Disable transparency")) DisableTransparency ();
		//SetActive激活/停用 GUI.Toggle创建一个打开/关闭的开关按钮
		elements.EngineBlock.SetActive(GUI.Toggle (new Rect (Screen.width-200, 130, 200,20), elements.EngineBlock.activeSelf, "Engine block"));
		elements.CylinderHead.SetActive(GUI.Toggle (new Rect (Screen.width-200, 150, 200, 20), elements.CylinderHead.activeSelf, "Cylinder head"));
		elements.CylinderHeadCovers.SetActive(GUI.Toggle (new Rect (Screen.width-200, 170, 200, 20), elements.CylinderHeadCovers.activeSelf, "Cylinder head covers"));
		elements.Gearbox.SetActive(GUI.Toggle (new Rect (Screen.width-200, 190, 200, 20), elements.Gearbox.activeSelf, "Gearbox"));
		elements.ExhaustManifolds.SetActive(GUI.Toggle (new Rect (Screen.width-200, 210, 200, 20), elements.ExhaustManifolds.activeSelf, "Exhaust manifold"));
		elements.FuelRail.SetActive(GUI.Toggle (new Rect (Screen.width-200, 230, 200, 20), elements.FuelRail.activeSelf, "Fuel rail"));
		elements.IntakeManifolds.SetActive(GUI.Toggle (new Rect (Screen.width-200, 250, 200, 20), elements.IntakeManifolds.activeSelf, "Intake manifold"));
		elements.Flywheel.SetActive(GUI.Toggle (new Rect (Screen.width-200, 270, 200, 20), elements.Flywheel.activeSelf, "Flywheel"));
		elements.Clutch.SetActive(GUI.Toggle (new Rect (Screen.width-200, 290, 200, 20), elements.Clutch.activeSelf, "Clutch"));
		elements.OilPan.SetActive(GUI.Toggle (new Rect (Screen.width-200, 310, 200, 20), elements.OilPan.activeSelf, "Oil pan"));
		elements.SparkPlugWires.SetActive(GUI.Toggle (new Rect (Screen.width-200, 330, 200, 20), elements.SparkPlugWires.activeSelf, "Spark plug wires"));
		elements.SparkPlugs.SetActive(GUI.Toggle (new Rect (Screen.width-200, 350, 200, 20), elements.SparkPlugs.activeSelf, "Spark plugs"));

		
	}
		

	public void SetVariation(int variation){
		ActivateAllObjects ();

		foreach (var _variation in engineVariations)
			foreach (var gameobject in _variation.gameObjects)
				gameobject.SetActive (false);

		foreach (var gameobject in engineVariations[variation].gameObjects)
			gameobject.SetActive (true);
	}


	public void ActivateAllObjects(){
		foreach (var mr in transform.GetComponentsInChildren<MeshRenderer>(true))
			mr.gameObject.SetActive (true);
	}


	public void EnableTransparency(){   //调节透不透明
		if (!Application.isPlaying) {
			Debug.LogWarning ("Transparency works only in playing mode");
			return;
		}

		foreach (var go in TransparentGameobjects)
			StartCoroutine (EnableTransparencyCor (go));
	}


	public void DisableTransparency(){
		if (!Application.isPlaying) {
			Debug.LogWarning ("Transparency works only in playing mode");
			return;
		}

		foreach (var go in TransparentGameobjects)
			StartCoroutine (DisableTransparencyCor (go));
	}


	IEnumerator EnableTransparencyCor(GameObject go){
		Material thisMaterial = go.GetComponent<MeshRenderer> ().material;
		Material fadeMaterial=(Material)Instantiate(FadeMaterial);

		go.GetComponent<MeshRenderer>().material=fadeMaterial;
//设置材质贴图和纹理
		fadeMaterial.SetTexture ("_MainTex", thisMaterial.GetTexture ("_MainTex"));
		fadeMaterial.SetTexture ("_OcclusionMap", thisMaterial.GetTexture ("_OcclusionMap"));
		fadeMaterial.SetTexture ("_BumpMap", thisMaterial.GetTexture ("_BumpMap"));

		fadeMaterial.SetFloat ("_BumpScale", thisMaterial.GetFloat ("_BumpScale"));
		fadeMaterial.SetFloat ("_Metallic", thisMaterial.GetFloat ("_Metallic"));
		fadeMaterial.SetFloat ("_OcclusionStrength", thisMaterial.GetFloat ("_OcclusionStrength"));
		fadeMaterial.SetFloat ("_Glossiness", thisMaterial.GetFloat ("_Glossiness"));

		Color tempColor = fadeMaterial.color;

		for (float f = tweaks.TransparencyEnablingTime; f >= tweaks.TransparencyValue; f -= 0.1f) {
				tempColor.a = f;
				fadeMaterial.color = tempColor;
				yield return null;
			}
	}


	IEnumerator DisableTransparencyCor(GameObject go){
		Material tempMaterial = go.GetComponent<MeshRenderer> ().material;

		Color tempColor = tempMaterial.color;

		for (float f = tweaks.TransparencyValue; f <= tweaks.TransparencyDisablingTime; f += 0.1f) {
			tempColor.a = f;
			tempMaterial.color = tempColor;
			yield return null;
		}

		Material opaqMaterial=(Material)Instantiate(OpaqueMaterial);
		go.GetComponent<MeshRenderer>().material=opaqMaterial;

		opaqMaterial.SetTexture ("_MainTex", tempMaterial.GetTexture ("_MainTex"));
		opaqMaterial.SetTexture ("_OcclusionMap", tempMaterial.GetTexture ("_OcclusionMap"));
		opaqMaterial.SetTexture ("_BumpMap", tempMaterial.GetTexture ("_BumpMap"));

		opaqMaterial.SetFloat ("_BumpScale", tempMaterial.GetFloat ("_BumpScale"));
		opaqMaterial.SetFloat ("_Metallic", tempMaterial.GetFloat ("_Metallic"));
		opaqMaterial.SetFloat ("_OcclusionStrength", tempMaterial.GetFloat ("_OcclusionStrength"));
		opaqMaterial.SetFloat ("_Glossiness", tempMaterial.GetFloat ("_Glossiness"));
		opaqMaterial.EnableKeyword ("_NORMALMAP");
	}



//unity中物体形变需要用网格过滤器（mesh filter）
//动画效果
	void Update () {

		float CorrectedRPM=RPM * Time.timeScale;
		
		IntakePhase =elements.CamshaftIntake1.transform.localEulerAngles.z;
		ExhaustPhase =elements.CamshaftExhaust1.transform.localEulerAngles.z;

		TimingBeltMaterial.mainTextureOffset += new Vector2 ( 0, CorrectedRPM/85);
		GeneratorBeltMaterial.mainTextureOffset += new Vector2 (0, CorrectedRPM / 180);
//传送带的传送效果是通过将传送带贴图进行移动得到的
		elements.Crankshaft.transform.Rotate (new Vector3 (0, 0, CorrectedRPM));
//进出气阀门的控制轴的旋转速度是Crankshaft的一半，这时候由四冲程内燃机的性质决定的
		elements.CamshaftExhaust1.transform.Rotate (new Vector3 (0, 0, CorrectedRPM/2));
		elements.CamshaftExhaust2.transform.Rotate (new Vector3 (0, 0, CorrectedRPM/2));
		elements.CamshaftIntake1.transform.Rotate (new Vector3 (0, 0, CorrectedRPM/2));
		elements.CamshaftIntake2.transform.Rotate (new Vector3 (0, 0, CorrectedRPM/2));

		elements.GearboxSecondaryShaft.transform.Rotate (new Vector3 (0, CorrectedRPM*1.47f, 0));
		elements.GearboxPrimaryShaft.transform.Rotate(new Vector3 (0, -CorrectedRPM,0 ));

		elements.Gear2.transform.Rotate (new Vector3 (0,0 , -CorrectedRPM*1.47f));
		elements.Gear3.transform.Rotate (new Vector3 (0,0 , -CorrectedRPM*1.33f));
		elements.Gear4.transform.Rotate (new Vector3 (0,0 , -CorrectedRPM*0.9996f));
		elements.Gear5.transform.Rotate (new Vector3 (0,0 , -CorrectedRPM*0.525f));

		elements.StarterGear1.transform.Rotate(new Vector3 (0,0 , -CorrectedRPM*5.13f));
		elements.StarterGear2.transform.Rotate(new Vector3 (0,0 , CorrectedRPM*4.8f));

		elements.TurboFan.transform.Rotate (new Vector3 (0, 0, CorrectedRPM));

		elements.DistributorGear.transform.Rotate (new Vector3 (0, 0, -CorrectedRPM));

		elements.TensionPulley.transform.Rotate (new Vector3 (0, 0, -CorrectedRPM));

		elements.GeneratorPulley.transform.Rotate (new Vector3 (0, -CorrectedRPM*2, 0));
		elements.WaterPumpPulley.transform.Rotate (new Vector3 (0,0 , -CorrectedRPM*2));
//LookAt函数：旋转Rod1物体，使其的up（vector 3）向量指向Rod1Target
		elements.Rod1.transform.LookAt (elements.Rod1Target.transform,elements.Rod1.transform.up);
		elements.Rod2.transform.LookAt (elements.Rod2Target.transform,elements.Rod2.transform.up);
		elements.Rod3.transform.LookAt (elements.Rod3Target.transform,elements.Rod3.transform.up);
		elements.Rod4.transform.LookAt (elements.Rod4Target.transform,elements.Rod4.transform.up);

		Piston1Delta = Rod1DefPos.y - transform.InverseTransformPoint (elements.Rod1.transform.position).y;
		Piston2Delta = Rod2DefPos.y - transform.InverseTransformPoint (elements.Rod2.transform.position).y;
		Piston3Delta = Rod3DefPos.y - transform.InverseTransformPoint (elements.Rod3.transform.position).y;
		Piston4Delta = Rod4DefPos.y - transform.InverseTransformPoint (elements.Rod4.transform.position).y;

		elements.Piston1.transform.localPosition = Piston1DefPos - new Vector3 (0, Piston1Delta, 0);
		elements.Piston2.transform.localPosition = Piston2DefPos - new Vector3 (0, Piston2Delta, 0);
		elements.Piston3.transform.localPosition = Piston3DefPos - new Vector3 (0, Piston3Delta, 0);
		elements.Piston4.transform.localPosition = Piston4DefPos - new Vector3 (0, Piston4Delta, 0);
		//以下这些不怎么看得懂
		foreach (var valve in elements.intakeValves) {
			float t = 0;

			if (IntakePhase > valve.OpenPhase && IntakePhase < valve.ClosePhase) 
				t = ((IntakePhase - valve.OpenPhase) / (valve.ClosePhase - valve.OpenPhase)) * 2;

			if (t > 1) t = 1 - (t - 1);

			float b = Mathf.SmoothStep (0, 1, t);

			valve.ValveGameobject.transform.localPosition = Vector3.Lerp (valve.DefPos, valve.DefPos - ValveOffset, b);
			valve.SpringGameobject.transform.localScale = Vector3.Lerp (Vector3.one, Vector3.one - ValveSpringOffset, b);
		}

		foreach (var valve in elements.exhaustValves) {
			float t = 0;

			if (ExhaustPhase > valve.OpenPhase && ExhaustPhase < valve.ClosePhase) 
				t = ((ExhaustPhase - valve.OpenPhase) / (valve.ClosePhase - valve.OpenPhase)) * 2;

			if (t > 1) t = 1 - (t - 1);

			float b = Mathf.SmoothStep (0, 1, t);

			valve.ValveGameobject.transform.localPosition = Vector3.Lerp (valve.DefPos, valve.DefPos - ValveOffset, b);
			valve.SpringGameobject.transform.localScale = Vector3.Lerp (Vector3.one, Vector3.one - ValveSpringOffset, b);
		}
	}
}
