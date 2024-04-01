### Unity Android Library Cleaner

## Purpose of this plugin
I Oftenly encounter many weird issues when building my game to android platform, and most the fixes was to remote the `Bee`folder in Library before build. This plugin does that automatically if you build the game and have the option to turn it on/off whenever you want.

### How to Use?
1. Create a script overriding the `BaseAndroidLibraryCleaner`
2. Override the `DATA_PATH` to where you want to store the config at
Example
```cs
public class AndroidLibraryCleaner : BaseAndroidLibraryCleaner
{
    public override string DATA_PATH => "Assets/Config";
}
```

3. Add the CreateAssetMenu path for our `AndroidLibraryCleaner`
```cs
[CreateAssetMenu(fileName = nameof(AndroidLibraryCleanerConfig), menuName = "LibraryCleaner/AndroidConfig")]
public class AndroidLibraryCleanerConfig : ScriptableObject
{
    [SerializeField] private bool _autoCleanPrebuild;

    public bool AutoCleanPreBuild => _autoCleanPrebuild;
}
```

4. Create a script overriding the `BaseAndroidLibraryCleanerWindow` and make sure the script is located inside `Editor` folder (anywhere)

5. Override the `Initialize()` and construct the `_cleaner` with our `AndroidLibraryCleaner`
```cs
public class AndroidLibraryCleanerWindow : BaseAndroidLibraryCleanerWindow
{
    public override void Initialize()
    {
        _cleaner = new AndroidLibraryCleaner();
    }
}
```

6. Add the ConstructWindow method

```cs
public class AndroidLibraryCleanerWindow : BaseAndroidLibraryCleanerWindow
{
    private const string MENU_ITEM_PATH = "Tools/Android Library Cleaner";

    [MenuItem(MENU_ITEM_PATH)]
    public static void ConstructWindow()
        => GetWindow<AndroidLibraryCleanerWindow>("Android Library Cleaner");

    public override void Initialize()
    {
       // ...
    }
}

```

### To-do
Better explanation on 'How to use'