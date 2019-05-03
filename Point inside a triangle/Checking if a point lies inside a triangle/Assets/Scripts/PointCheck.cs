using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCheck : MonoBehaviour {

    [SerializeField] private Transform side1, side2, side3;
    [SerializeField] private Transform pointerPrefab;
    private Transform pointer;
    private Vector2 A, B, C, P;
    private float area1;
    private string text;

    private void Start()
    {
        A = new Vector2(-4f, -2f);
        B = new Vector2(4f, -2f);
        C = new Vector2(0f, 4f);
        Vector2 AB = (A - B).normalized;
        Vector2 AC = (A - C).normalized;
        Vector2 BC = (B - C).normalized;
        side1.transform.up = AB;
        side1.transform.position = new Vector2(((A.x + B.x)/2),((A.y + B.y)/2));
        side2.transform.position = new Vector2(((A.x + C.x) / 2), ((A.y + C.y) / 2));
        side3.transform.position = new Vector2(((C.x + B.x) / 2), ((C.y + B.y) / 2));
        float temp = Mathf.Sqrt(Mathf.Abs((A.x - B.x)*(A.x - B.x) + (A.y - B.y)*(A.y - B.y)));
        side1.transform.localScale = new Vector3(0.5f, temp, 1);
        temp = Mathf.Sqrt(Mathf.Abs((A.x - C.x) * (A.x - C.x) + (A.y - C.y) * (A.y - C.y)));
        side2.transform.localScale = new Vector3(0.5f, temp, 1);
        temp = Mathf.Sqrt(Mathf.Abs((C.x - B.x) * (C.x - B.x) + (C.y - B.y) * (C.y - B.y)));
        side3.transform.localScale = new Vector3(0.5f, temp, 1);
        side2.transform.up = AC;
        side3.transform.up = BC;
        pointer = Instantiate(pointerPrefab, Vector3.zero, Quaternion.identity);
        area1 = Area(A, B, C);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(A, B);
        Gizmos.DrawLine(B, C);
        Gizmos.DrawLine(C, A);
    }

    float Area(Vector2 X, Vector2 Y, Vector2 Z)
    {
        float area = Mathf.Abs(((X.x) * (Y.y - Z.y) + (Y.x) * (Z.y - X.y) + (Z.x) * (X.y - Y.y)) / 2);
        return area;
    }

    private void Update()
    {
        P = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pointer.position = P;
        float area2 = Area(A, B, P);
        float area3 = Area(A, C, P);
        float area4 = Area(B, C, P);
        float areas = area2 + area3 + area4;
        if (area1 == areas)
            text = "INSIDE";
        else
            text = "OUTSIDE";
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), text);
    }
}
