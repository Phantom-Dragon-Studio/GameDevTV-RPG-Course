using System;
using UnityEditor;

namespace Editor {
    public class FixEditorClipping : EditorWindow
    {
        private void Awake() {
            SceneView.currentDrawingSceneView.camera.farClipPlane = 10000000;
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
