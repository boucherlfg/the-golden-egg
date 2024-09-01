using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor {
    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        if(GUILayout.Button("CreateMesh")) {
            (target as FieldOfView).GenerateMesh();
        }
    }
}
#endif

public class FieldOfView : MonoBehaviour 
{
    [SerializeField]
    private bool followMouse;
    [SerializeField]
    Transform toFollow;
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    float fov = 90;
    [SerializeField]
    int rayCount = 2;
    [SerializeField]
    float viewDistance = 50f;
    Mesh mesh;
    private void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }
    private void Update() {
        GenerateMesh();
    }
    public void GenerateMesh() {
        #if UNITY_EDITOR
        if(!Application.isPlaying) {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
        }
        #endif
        var mousePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - toFollow.position).normalized;
        var orientation = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        if(orientation < 0) orientation += 360;
        if(!followMouse) orientation = 0;
        
        float angle = orientation + fov/2;
        float angleIncrease = fov / rayCount;
        Vector3 origin = toFollow.position;

        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uvs = new Vector2[vertices.Length];
        int[] triangles = new int[3 * rayCount];

        vertices[0] = origin;
        int vertexIndex = 1;
        int triangleIndex = 0;

        for(int i = 0; i <= rayCount; i++) {
            Vector3 destination = GetVectorFromAngle(angle);
            var hit = Physics2D.Raycast(origin, destination, viewDistance, layerMask);

            Vector3 vertex =  hit.collider ? hit.point : origin + destination * viewDistance;
            vertices[vertexIndex] = vertex;

            if(i > 0) {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                
                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
    }
    private Vector2 GetVectorFromAngle(float angle) {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }
}