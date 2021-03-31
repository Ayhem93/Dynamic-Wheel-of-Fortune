using System.Collections.Generic;
using UnityEngine;

public class PartCircle : MonoBehaviour
{
    [SerializeField] private Material mat = null;
    [SerializeField] private float startAngle = 0;
    [SerializeField] private float addedAngle = 0;
    [SerializeField] private float radius = 100;
    [SerializeField] private int NomberOfEdges = 3;
    [SerializeField] private bool updateMesh = true;

    private void Update()
    {
        if (updateMesh)
        {
            updateMesh = false;
            DrawAngleWithMultipleTriangles(NomberOfEdges);
            radius = GetComponent<RectTransform>().rect.height / 2;
        }
    }

    private void DrawAngleWithMultipleTriangles(int edgesNb)
    {
        Mesh mesh = new Mesh();
        float stepAngle = addedAngle / (edgesNb + 1);

        //Verticies
        List<Vector3> verticiesList = new List<Vector3>();
        float x;
        float y;
        verticiesList.Add(new Vector3(0, 0, 0));
        for (int i = 0; i < edgesNb + 2; i++)
        {
            float angle = startAngle + (stepAngle * i);
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            verticiesList.Add(new Vector3(x, y, 0));
        }

        //Triangles
        List<int> trianglesList = new List<int> { };
        for (int i = 0; i < (edgesNb + 1); i++)
        {
            trianglesList.Add(0);
            trianglesList.Add(i + 1);
            trianglesList.Add(i + 2);
        }

        //normals
        List<Vector3> normalsList = new List<Vector3> { };
        for (int i = 0; i < verticiesList.Count; i++)
        {
            normalsList.Add(-Vector3.forward);
        }
        Vector3[] normals = normalsList.ToArray();

        //initialise
        mesh.vertices = verticiesList.ToArray();
        mesh.triangles = trianglesList.ToArray();
        mesh.normals = normals;

        CanvasRenderer rend = GetComponent<CanvasRenderer>();
        rend.SetMaterial(mat, null);
        rend.SetMesh(mesh);
    }
}