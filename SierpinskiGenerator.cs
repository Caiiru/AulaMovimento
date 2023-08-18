using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SierpinskiGenerator : MonoBehaviour
{
    public int startDepth;
    public Vector2 left, right, top;
    public bool reDraw;
    void Start()
    {
        GenerateSierpinski(left, right, top, 1);
    }

    void Update(){
        if(reDraw){
            reDraw = false;
            GenerateSierpinski(left, right, top, 1);
        }
    }
    void GenerateSierpinski(Vector2 left, Vector2 right, Vector2 top, int depth){
        if(depth == 0){
            return;
        }

        Vector2 leftTop = (left-top)/2;
        Vector2 rightTop = (right-top)/2;
        Vector2 leftRight = (right-left)/2;
        
        Debug.DrawLine(left,right, Color.blue, 1000f);
        Debug.DrawLine(right,top, Color.blue, 1000f);
        Debug.DrawLine(top,left, Color.blue, 1000f);

        
        Debug.DrawLine(leftTop,leftRight, Color.red, 1000f);
        Debug.DrawLine(top,leftTop, Color.yellow,2300f);
        /*
        Debug.DrawLine(leftRight,rightTop, Color.red, 1000f);
        Debug.DrawLine(rightTop,leftTop, Color.red, 1000f);
        */


    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(left, 1f);
        Gizmos.DrawSphere(right, 1f);
        Gizmos.DrawSphere(top, 1f);
        
    }
}
