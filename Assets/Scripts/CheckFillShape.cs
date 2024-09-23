using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFillShape : MonoBehaviour
{
    private LineRenderer _brushRender;
    private GameObject[] _traceArea;
    private bool isLineComplete=false;

    // Start is called before the first frame update
    void Start()
    {
        _brushRender = GetComponent<LineRenderer>();
        _traceArea = GameObject.FindGameObjectsWithTag("CircleCheck");
    }

    // Update is called once per frame
    void Update()
    {
        
       
        GenerateCollider();
    }
    public void GenerateCollider()
    {
        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
       
        if (collider == null)
        {
            collider =gameObject.AddComponent<PolygonCollider2D>();
           /* collider.convex = true;
            collider.isTrigger = true;*/
            
        }
        Vector2[] path = GeneratePathFromMesh();
        collider.SetPath(0,path);
    }
    private Vector2[] GeneratePathFromMesh()
    { //Conversion d'une mesh 3d en 2d
        // You would need to convert the mesh vertices into 2D if you want to use a PolygonCollider2D
        Mesh mesh = new Mesh();
        _brushRender.BakeMesh(mesh);
        Vector3[] vertices = mesh.vertices;

        // Convert 3D vertices into 2D for the collider
        Vector2[] path = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            path[i] = new Vector2(vertices[i].x, vertices[i].y);  // Choose the 2D axis
        }
        return path;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {      
        Debug.Log("Truc");
        if (collision.gameObject.CompareTag("CalligraphyPoint"))
        {
            Debug.Log(collision);
            collision.gameObject.SetActive(false);
        }

        int activeCircleCount = 0;
        foreach (GameObject traceArea in _traceArea)
        {
            if (traceArea.gameObject.activeSelf)
            {
                activeCircleCount++;
            }
        }
        Debug.Log(activeCircleCount);

        if (activeCircleCount == 0) {
            isLineComplete = true;
        }
    }
   


}
