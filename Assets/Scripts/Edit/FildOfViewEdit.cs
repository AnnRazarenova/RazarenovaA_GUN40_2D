using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FildOfView))]
public class FildOfViewEdit : Editor
{
    private void OnSceneGUI()
    {
        FildOfView fov = (FildOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.Radius);

        Vector3 viewAngleLeft = DitactionFromAngle(fov.transform.eulerAngles.y, -fov.Angle / 2);
        Vector3 viewAngleRight = DitactionFromAngle(fov.transform.eulerAngles.y, fov.Angle / 2);

        Handles.color = Color.yellow;

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleLeft * fov.Radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleRight * fov.Radius);

        if(fov.CanSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.Player.transform.position);
        }
    }

    private Vector3 DitactionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
