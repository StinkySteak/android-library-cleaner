using System.Drawing.Printing;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace StinkySteak.LibraryCleaner.Android
{
    public class BaseAndroidLibraryCleaner : IPreprocessBuildWithReport
    {
        public int callbackOrder => -1;

        public virtual string DATA_PATH => string.Empty;

        public virtual void OnPreprocessBuild(BuildReport report)
        {
            
        }

        public virtual void ProcessCleanCache() 
        {
            Debug.Log($"[{nameof(BaseAndroidLibraryCleaner)}] Pre-processing build...");

            if (!ValidatePath()) return;

            if (!TryGetData(out AndroidLibraryCleanerConfig data))
            {
                Debug.LogError($"[{nameof(BaseAndroidLibraryCleaner)}]: {nameof(AndroidLibraryCleanerConfig)} Not Found!");
                return;
            }

            if (!data.AutoCleanPreBuild) return;

            CleanCache();
        }

        public void CleanCache()
        {
            if (!Directory.Exists(GetCacheFolderPath()))
            {
                Debug.LogError($"[{nameof(BaseAndroidLibraryCleaner)}]: Directory is not exist");
                return;
            }

            Directory.Delete(GetCacheFolderPath(), true);

            Debug.Log($"[{nameof(BaseAndroidLibraryCleaner)}]: Directory Cleaned!");
        }

        public string GetCacheFolderPath()
        {
            string path = Path.GetDirectoryName(Application.dataPath) + "/Library/Bee/Android";
            return path;
        }

        public bool ValidatePath()
        {
            if (string.IsNullOrEmpty(DATA_PATH))
            {
                Debug.LogError($"[{nameof(BaseAndroidLibraryCleaner)}]: {nameof(DATA_PATH)} is not implemented! Override the path to use.");
                return false;
            }

            return true;
        }

        public bool TryGetData(out AndroidLibraryCleanerConfig data)
        {
            data = null;

            string[] folders = new string[] { DATA_PATH };
            string[] guids = AssetDatabase.FindAssets(string.Empty, folders);

            foreach (var guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                object obj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(AndroidLibraryCleanerConfig));

                if (obj is AndroidLibraryCleanerConfig dataCandidate)
                {
                    data = dataCandidate;
                }
            }

            return data != null;
        }
    }
}