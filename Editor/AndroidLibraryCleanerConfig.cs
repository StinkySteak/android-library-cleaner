using UnityEngine;

namespace StinkySteak.LibraryCleaner.Android
{
    [CreateAssetMenu(fileName = nameof(AndroidLibraryCleanerConfig), menuName = "StinkySteak/LibraryCleaner/AndroidConfig")]
    public class AndroidLibraryCleanerConfig : ScriptableObject
    {
        [SerializeField] private bool _autoCleanPrebuild;

        public bool AutoCleanPreBuild => _autoCleanPrebuild;
    }
}