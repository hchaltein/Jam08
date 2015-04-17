using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GizmosTest))]
public class GizmosTestEditor : Editor {

    GizmosTest Target { get { return (GizmosTest)target; } }

    private void OnSceneGUI()
    {
        Handles.Slider2D(Target.transform.position, Vector3.right, Vector3.right, Vector3.right, 1, Handles.ArrowCap, 1);

        MyHandles.DragHandleResult dhResult;
        Vector3 newPosition = MyHandles.DragHandle(Target.transform.position, 1, Handles.ArrowCap, Color.red, out dhResult);

        switch (dhResult)
        {
            case MyHandles.DragHandleResult.LMBDrag:
                Target.transform.position = newPosition;
                break;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(Target);
        }
    }

}
