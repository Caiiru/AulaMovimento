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
        GenerateSierpinski(left, right, top, startDepth);
    }

    void Update(){
        if(reDraw){
            reDraw = false;
            GenerateSierpinski(left, right, top, startDepth);
        }
    }
    void GenerateSierpinski(Vector2 left, Vector2 right, Vector2 top, int depth){
        if(depth <= 0){
            Debug.Log("Acabou");
            return;
        }
        Debug.Log("Gerando Triangulo");

        Vector2 leftTop = (top+left)/2;
        Vector2 rightTop = (top+right)/2;
        Vector2 leftRight = (left+right)/2;
        
        Debug.DrawLine(left,right, Color.blue, 1000f);
        Debug.DrawLine(right,top, Color.blue, 1000f);
        Debug.DrawLine(top,left, Color.blue, 1000f);
        

        Debug.DrawLine((leftTop), rightTop, Color.magenta, 3200f);
        Debug.DrawLine(rightTop, leftRight, Color.yellow, 3200f);
        Debug.DrawLine(leftRight, (leftTop), Color.green, 3200f);

        GenerateSierpinski(leftTop, rightTop, top, depth-1);
        GenerateSierpinski(left, leftRight, leftTop, depth-1);
        GenerateSierpinski(leftRight, right, rightTop, depth-1);

    }

   
}
