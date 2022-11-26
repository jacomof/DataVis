using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generatePie : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var mr = GetComponent<MeshFilter>();
        mr.mesh = _constructMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Mesh _constructMesh()
    {

        Mesh _piePiece = new Mesh();

        Vector3[] _vertices = new Vector3[6];
        
        _vertices[0] = new Vector3(0,0,0);
        _vertices[1] = new Vector3(0,1f,0);
        _vertices[2] = new Vector3(1f,0,0);
        _vertices[3] = new Vector3(1f,1f,0);
        _vertices[4] = new Vector3(0.5f,0,1f);
        _vertices[5] = new Vector3(0.5f,1f,1f);


        int[] _triangles = new int[]{0,1,2,1,3,2,1,5,3,0,2,4,2,3,5,5,4,2,4,5,1,1,0,4};
        _piePiece.vertices = _vertices;
        _piePiece.triangles = _triangles;

        return _piePiece;

    }
}
