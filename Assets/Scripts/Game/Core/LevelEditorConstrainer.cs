using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

[ExecuteInEditMode]
public class LevelEditorConstrainer : MonoBehaviour {


    private void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            var parent = transform.parent;
            var meshContainer = parent.Find("Mesh");
            
            var scale = meshContainer.lossyScale;
            parent.localScale = Vector3.one;
            meshContainer.localScale = scale;

            var position = meshContainer.position;
            parent.position = position;
            meshContainer.localPosition = Vector3.zero;
        }
#endif
    }


}
