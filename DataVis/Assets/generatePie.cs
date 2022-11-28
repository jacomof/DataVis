using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generatePie : MonoBehaviour
{
    // Start is called before the first frame update
    public float Height=1.0f;
    public float DeltaWidth=0.1f;
    public float Length = 1.5f;
    public bool rotate = false;
    void Start()
    {
        var mr = GetComponent<MeshFilter>();
        mr.mesh = _constructMesh();
    }

    // Update is called once per frame

    private Mesh _constructMesh()
    {

        Mesh _piePiece = new Mesh();
        int _numVerts = 8;
        Vector3[] _vertices = new Vector3[_numVerts];
        int[] _triangles = new int[_numVerts*3];
        
        /*_vertices[0] = new Vector3(0,0,0);
        _vertices[1] = new Vector3(0,Height,0);
        _vertices[2] = new Vector3(DeltaWidth,0,0);
        _vertices[3] = new Vector3(DeltaWidth,Height,0);
        _vertices[4] = new Vector3(DeltaWidth,0,Length);
        _vertices[5] = new Vector3(DeltaWidth,Height,Length);


        if(rotate){
            for(int i = 0; i < _vertices.Length; ++i){

                _vertices[i] = Quaternion.AngleAxis(-45, Vector3.up)*_vertices[i];
                
            }
        }

        int[] _triangles = new int[]{0,1,2,1,3,2,1,5,3,0,2,4,2,3,5,5,4,2,4,5,1,1,0,4};
        _piePiece.vertices = _vertices;
        _piePiece.triangles = _triangles;
        */
        
        _vertices[0] = new Vector3(0,0,0);
        _vertices[1] = new Vector3(0,Height,0);
        _vertices[2] = new Vector3(0,0,Length);
        _vertices[3] = new Vector3(0,Height,Length);
        
        //Cara inicial
        _triangles[0] = 0;
        _triangles[1] = 2;
        _triangles[2] = 1;
        _triangles[3] = 1;
        _triangles[4] = 2;
        _triangles[5] = 3;

        int last1 = 3;
        int last2 = 2;
        int _trianglediff = 6;
        for(int i = 0; i < 2; ++i){

            

            int new1 = last1+1;
            int new2 = last1+2;
            _vertices[new1] = Quaternion.AngleAxis(45, Vector3.up)* _vertices[last2];
            _vertices[new2] = Quaternion.AngleAxis(45, Vector3.up)* _vertices[last1];

            _triangles[6+_trianglediff] = last2;
            _triangles[7+_trianglediff] = new1;
            _triangles[8+_trianglediff] = new2;
            _triangles[9+_trianglediff] = new2;
            _triangles[10+_trianglediff] = last1;
            _triangles[11+_trianglediff] = last2;

            _trianglediff+=_trianglediff;
            last1=new2;
            last2=new1;
        }
        _piePiece.vertices = _vertices;
        _piePiece.triangles = _triangles;
        
        return _piePiece;

    }
}
