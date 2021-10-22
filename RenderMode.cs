using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RenderMode : MonoBehaviour
{
	Material renderMaterial;
	List<Vector3> renderVertices;
	Mesh mesh;
	Shader shader;
	public bool flag = false;

	private void Start()
    {
		mesh = GetComponent<MeshFilter>().mesh;
		renderVertices = new List<Vector3>();
		shader = Shader.Find("project/WireFrameRendering");
		renderMaterial = new Material(shader);
		renderMaterial.hideFlags = HideFlags.HideAndDontSave;
		renderMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
		//mesh.vertices
		for (int i = 0; i < mesh.triangles.Length; ++i)
		{
			renderVertices.Add(mesh.vertices[mesh.triangles[i]]);
		}
	}

    private void OnRenderObject()
	{
		if (flag)
        {
			GetComponent<MeshRenderer>().enabled = false;
			renderMaterial.SetPass(0);

			GL.MultMatrix(transform.localToWorldMatrix);
			GL.Begin(GL.LINES);

			for (int i = 0; i < renderVertices.Count / 3; ++i)
			{
				GL.Vertex(renderVertices[3 * i + 0]);
				GL.Vertex(renderVertices[3 * i + 1]);

				GL.Vertex(renderVertices[3 * i + 1]);
				GL.Vertex(renderVertices[3 * i + 2]);

				GL.Vertex(renderVertices[3 * i + 2]);
				GL.Vertex(renderVertices[3 * i + 0]);
			}
			GL.End();
		}
	}

	public void LightRendering()
    {
		shader = Shader.Find("project/WireFrameRendering");
		GetComponent<MeshRenderer>().enabled = true;
		GetComponent<MeshRenderer>().material = new Material(shader);
		flag = false;
	}
	//public void WireFrameRendering()
	//{
	//	shader = Shader.Find("project/WireFrameRendering");  BlinnPhongRendering
	//	GetComponent<MeshRenderer>().enabled = true;
	//	GetComponent<MeshRenderer>().material = new Material(shader);
	//	flag = true;


	//}
}