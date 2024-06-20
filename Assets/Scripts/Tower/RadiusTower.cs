using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RadiusTower : MonoBehaviour
{
    [Range(0, 50)]
    public int segments = 50;
    [Range(0, 5)]
    public float xradius = 5;
    [Range(0, 5)]
    public float yradius = 5;
    LineRenderer line;
    PolygonCollider2D polygonCollider;
    Vector2[] _points;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();

        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
        polygonCollider.points = new Vector2[segments + 1];
        _points = polygonCollider.points;
        CreatePoints();
        
    }

    void CreatePoints()
    {
        float x;
        float y;

        float angle = 0f;

        for (int i = 0; i  < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            _points[i] = new Vector2(x, y);
            line.SetPosition(i, new Vector3(x, y, 0));
            

            angle += (360f / segments);
        }

        polygonCollider.points = _points;
    }

    public void ChangeScaleRadius()
    {
        xradius += .25f;
        yradius += .25f;

        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
        polygonCollider.points = new Vector2[segments + 1];
        _points = polygonCollider.points;
        CreatePoints();
    }

    public LineRenderer GetLine
    {
        get { return line; }
    }
}
