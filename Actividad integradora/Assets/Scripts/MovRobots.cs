using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovRobots : MonoBehaviour
{
    public GameObject robot;
    Vector3[] geometria;
    public Vector3 A;
    public Vector3 B;
    float t;
    public Vector3 Movement = new Vector3(-0.0001f,0,0);

    public Light SpotLight;
    public Light PointLight;

    Vector3[] ApplyTransform(float rY)
    {
        Matrix4x4 rm = Transformations.RotateM(rY, Transformations.AXIS.AX_Y);
        MeshFilter mf = robot.GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh;
        Vector3[] transform = new Vector3[mesh.vertices.Length];

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Vector3 v = mesh.vertices[i];
            Vector4 temp = new Vector4(v.x, v.y, v.z, 1);
            transform[i] = rm * temp;
        }
        return transform;
    }

    Vector3[] ApplyTransform2()
    {
        Vector3 pos = A + t * (B - A);
        //SpotLight.transform.position = new Vector3(pos.x,pos.y,pos.z);
        //PointLight.transform.position = new Vector3(pos.x,pos.y,pos.z);
        Matrix4x4 tm = Transformations.TranslateM(pos.x, pos.y, pos.z);
        Vector3[] transform = new Vector3[geometria.Length];

        for (int i = 0; i < geometria.Length; i++)
        {
            Vector3 v = geometria[i];
            Vector4 temp = new Vector4(v.x, v.y, v.z, 1);
            transform[i] = tm * temp;
        }
        return transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        //A = robot.transform.position;
        //SpotLight.transform.position = new Vector3(0,6.4f,0);
        MeshFilter mf = robot.GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh;
        geometria = mesh.vertices;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SpotLight.transform.position -= new Vector3 (-Movement.x,0,0) * Time.deltaTime;
        PointLight.transform.position -= new Vector3 (-Movement.x,0,0) * Time.deltaTime;
        MeshFilter mf = robot.GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh;
        t += 0.001f;
        mesh.vertices = ApplyTransform2();
    }
}
