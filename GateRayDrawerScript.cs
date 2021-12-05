using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateRayDrawerScript : MonoBehaviour {

   
    void OnDrawGizmos()
    {
        Color[] colorA = new Color[6];
        
        colorA[0] = Color.red;
        colorA[1] = Color.yellow;
        colorA[2] = Color.blue;
        colorA[3] = Color.cyan;
        colorA[4] = Color.green;
        colorA[5] = Color.white;
       

        Vector3 vec1 = new Vector3(365f, 5f, 0f);
        Vector3 vec2 = new Vector3(-365f, 5f, 0f);
        DrawRay(vec1, colorA[0]);
        DrawRay(vec2, colorA[0]);

        Vector3 vec3 = new Vector3(335f, 5f, 0f);
        Vector3 vec4 = new Vector3(-335f, 5f, 0f);
        DrawRay(vec3, colorA[1]);
        DrawRay(vec4, colorA[1]);

        Vector3 vec5 = new Vector3(305f, 5f, 0f);
        Vector3 vec6 = new Vector3(-305f, 5f, 0f);
        DrawRay(vec5, colorA[2]);
        DrawRay(vec6, colorA[2]);

        Vector3 vec7 = new Vector3(215f, 5f, 0f);
        Vector3 vec8 = new Vector3(-215f, 5f, 0f);
        DrawRay(vec7, colorA[3]);
        DrawRay(vec8, colorA[3]);

        Vector3 vec9 = new Vector3(125f, 5f, 0f);
        Vector3 vec10 = new Vector3(-125f, 5f, 0f);
        DrawRay(vec9, colorA[4]);
        DrawRay(vec10, colorA[4]);

        Vector3 vec11 = new Vector3(35f, 5f, 0f);
        Vector3 vec12 = new Vector3(-35f, 5f, 0f);
        DrawRay(vec11, colorA[5]);
        DrawRay(vec12, colorA[5]);

    }

    void DrawRay(Vector3 aVec, Color color)
    {
        Vector3 lineDrawRight = Vector3.right * 10f;
       
        Vector3 lineDrawLeft = Vector3.left * 10f;
        
        if (aVec.x < 0)
        {
            
            Gizmos.color = color;
            Gizmos.DrawRay(aVec, lineDrawRight);
        }
        else if (aVec.x > 0)
        {
            
            Gizmos.color = color;
            Gizmos.DrawRay(aVec, lineDrawLeft);
        }
        
    }
}
