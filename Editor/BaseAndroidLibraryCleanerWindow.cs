using UnityEngine;
using UnityEditor;
using System;

namespace StinkySteak.LibraryCleaner.Android
{
    public class BaseAndroidLibraryCleanerWindow : EditorWindow
    {
        protected BaseAndroidLibraryCleaner _cleaner;

        public BaseAndroidLibraryCleanerWindow()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
            throw new NotImplementedException();
        }

        private void OnGUI()
        {
            DrawCleanCache();
            EditorGUILayout.Space(10);

            DrawValidateCleanerPath();
            DrawScanData();
        }

        private void DrawCleanCache()
        {
            string path = _cleaner.GetCacheFolderPath();

            EditorGUILayout.LabelField($"Directory Target: {path}");

            bool isPressed = GUILayout.Button("Clean Cache");

            if (isPressed)
            {
                _cleaner.CleanCache();
            }
        }

        private void DrawScanData()
        {
            if (GUILayout.Button("Validate Config"))
            {
                if (!_cleaner.ValidatePath()) return;

                _cleaner.TryGetData(out AndroidLibraryCleanerConfig data);
                Debug.Log($"[{nameof(BaseAndroidLibraryCleaner)}]: Data is exist: {data != null}");
            }
        }

        private void DrawValidateCleanerPath()
        {
            if (GUILayout.Button("Validate Cleaner Data Path"))
            {
                bool isValid = _cleaner.ValidatePath();

                if (isValid)
                {
                    Debug.Log($"[{nameof(BaseAndroidLibraryCleaner)}]: Path is valid");
                    return;
                }
                Debug.LogError($"[{nameof(BaseAndroidLibraryCleaner)}]: Path is invalid!");
            }
        }
    }
}