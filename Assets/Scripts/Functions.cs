using UnityEngine;

public static class Functions
{

    public static void DrawCircle(Vector3 position, float radius, int segments, Color color)
    {
        // If either radius or number of segments are less or equal to 0, skip drawing
        if (radius <= 0.0f || segments <= 0)
        {
            return;
        }

        // Single segment of the circle covers (360 / number of segments) degrees
        float angleStep = (360.0f / segments);

        // Result is multiplied by Mathf.Deg2Rad constant which transforms degrees to radians
        // which are required by Unity's Mathf class trigonometry methods

        angleStep *= Mathf.Deg2Rad;

        // lineStart and lineEnd variables are declared outside of the following for loop
        Vector3 lineStart = Vector3.zero;
        Vector3 lineEnd = Vector3.zero;

        for (int i = 0; i < segments; i++)
        {
            // Line start is defined as starting angle of the current segment (i)
            lineStart.x = Mathf.Cos(angleStep * i);
            lineStart.y = Mathf.Sin(angleStep * i);

            // Line end is defined by the angle of the next segment (i+1)
            lineEnd.x = Mathf.Cos(angleStep * (i + 1));
            lineEnd.y = Mathf.Sin(angleStep * (i + 1));

            // Results are multiplied so they match the desired radius
            lineStart *= radius;
            lineEnd *= radius;

            // Results are offset by the desired position/origin 
            lineStart += position;
            lineEnd += position;

            // Points are connected using DrawLine method and using the passed color
            Debug.DrawLine(lineStart, lineEnd, color);
        }
    }
    public static bool InsideCol(Collider2D mycol, Collider2D other)
    {
        if (other.bounds.Contains(mycol.bounds.min)
             && other.bounds.Contains(mycol.bounds.max))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool CheckIntersection(EdgeCollider2D edgeCollider1, EdgeCollider2D edgeCollider2)
    {
        Vector2[] points1 = edgeCollider1.points;
        Vector2[] points2 = edgeCollider2.points;
        Vector2 offset1 = new Vector2(edgeCollider1.transform.position.x, edgeCollider1.transform.position.y);
        Vector2 offset2 = new Vector2(edgeCollider2.transform.position.x, edgeCollider2.transform.position.y);
        float scale1 = edgeCollider1.transform.localScale.x;
        float scale2 = edgeCollider2.transform.localScale.x;
        for (int i = 0; i < points1.Length - 1; i++)
        {
            for (int j = 0; j < points2.Length - 1; j++)
            {
                Debug.DrawLine(points1[i] * scale1 + offset1, points1[i + 1] * scale1 + offset1);
                Debug.DrawLine(points2[j] * scale2 + offset2, points2[j + 1] * scale2 + offset2);
                if (DoLineSegmentsIntersect(points1[i] * scale1 + offset1, points1[i + 1] * scale1 + offset1, points2[j] * scale2 + offset2, points2[j + 1] * scale2 + offset2))
                {

                    return true;
                }
            }
        }

        return false;
    }
    static bool DoLineSegmentsIntersect(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        float denominator = ((p2.x - p1.x) * (p4.y - p3.y)) - ((p2.y - p1.y) * (p4.x - p3.x));
        float numerator1 = ((p1.y - p3.y) * (p4.x - p3.x)) - ((p1.x - p3.x) * (p4.y - p3.y));
        float numerator2 = ((p1.y - p3.y) * (p2.x - p1.x)) - ((p1.x - p3.x) * (p2.y - p1.y));

        // Detect coincident lines
        if (denominator == 0)
        {
            return numerator1 == 0 && numerator2 == 0;
        }

        float r = numerator1 / denominator;
        float s = numerator2 / denominator;

        return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);
    }

    public static float DeltaTimeLerp(float value)
    {
        //ovo ti radi stvar, tako da lerp zavisi od vremena, a ne da vraca samo fiksnu vrednost svakog frejma
        return 1 - Mathf.Pow(1 - value, Time.deltaTime * 60);
    }
    public static void DrawBox(Vector3 pos, Quaternion rot, Vector3 scale, Color c)
    {
        // create matrix
        Matrix4x4 m = new Matrix4x4();
        m.SetTRS(pos, rot, scale);

        var point1 = m.MultiplyPoint(new Vector3(-0.5f, -0.5f, 0.5f));
        var point2 = m.MultiplyPoint(new Vector3(0.5f, -0.5f, 0.5f));
        var point3 = m.MultiplyPoint(new Vector3(0.5f, -0.5f, -0.5f));
        var point4 = m.MultiplyPoint(new Vector3(-0.5f, -0.5f, -0.5f));

        var point5 = m.MultiplyPoint(new Vector3(-0.5f, 0.5f, 0.5f));
        var point6 = m.MultiplyPoint(new Vector3(0.5f, 0.5f, 0.5f));
        var point7 = m.MultiplyPoint(new Vector3(0.5f, 0.5f, -0.5f));
        var point8 = m.MultiplyPoint(new Vector3(-0.5f, 0.5f, -0.5f));

        Debug.DrawLine(point1, point2, c);
        Debug.DrawLine(point2, point3, c);
        Debug.DrawLine(point3, point4, c);
        Debug.DrawLine(point4, point1, c);

        Debug.DrawLine(point5, point6, c);
        Debug.DrawLine(point6, point7, c);
        Debug.DrawLine(point7, point8, c);
        Debug.DrawLine(point8, point5, c);

        Debug.DrawLine(point1, point5, c);
        Debug.DrawLine(point2, point6, c);
        Debug.DrawLine(point3, point7, c);
        Debug.DrawLine(point4, point8, c);


    }

}