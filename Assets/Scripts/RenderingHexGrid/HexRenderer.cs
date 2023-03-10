using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public struct Face
{
    public List<Vector3> vertices { get; private set; }
    public List<int> triangles { get; private set; }
    public List<Vector2> uvs { get; private set; }

    public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
    {
        this.vertices = vertices;
        this.triangles = triangles;
        this.uvs = uvs;

    }

}



[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class HexRenderer : MonoBehaviour
{

    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    public Material material;
    private List<Face> faces;
    public float innerSize;
    public float outerSize;
    public float height;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        mesh = new Mesh();
        mesh.name = "Hex";
        meshFilter.mesh = mesh;
        meshRenderer.material = material;
    }

    // Start is called before the first frame update
    void Start()
    {
        DrawMesh();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DrawMesh()
    {
        DrawFaces();
        CombineFaces();
    }

    private void DrawFaces()
    {
        faces = new List<Face>();

        for(int point = 0; point < 6; point++) 
        {
            faces.Add(CreateFace(innerSize, outerSize, height / 2f, height / 2f, point));
        
        
        }

    }
    private void CombineFaces()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i =0; i<faces.Count; i++) 
        {
            vertices.AddRange(faces[i].vertices);
            uvs.AddRange(faces[i].uvs);
            int offset = (4 * i);
            foreach(int triangle in faces[i].triangles) 
            {
                tris.Add(triangle + offset);
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

    }

    protected Face CreateFace(float innerRad, float outerRad, float heightA, float heightB, int point) 
    {
        Vector3 pointA = GetPoint(innerRad, heightB, point);
        Vector3 pointB = GetPoint(innerRad, heightB, (point < 5) ? point + 1 : 0 );
        Vector3 pointC = GetPoint(outerRad, heightA, (point < 5) ? point + 1 : 0);
        Vector3 pointD = GetPoint(outerRad, heightA, point);
        List<Vector3> vertices = new List<Vector3>() { pointA, pointB, pointC, pointD };
        List<int> triangles = new List<int>() { 0, 1, 2, 2, 3, 0 };
        List<Vector2> uvs = new List<Vector2>() { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };

            return new Face(vertices, triangles, uvs);
    }

    protected Vector3 GetPoint(float size, float height, int index) 
    {
        float angleDeg = 60 * index;
        float angleRad = Mathf.PI / 180f * angleDeg;
        return new Vector3((size * Mathf.Cos(angleRad)), height, size * Mathf.Sin(angleRad));
    }


}