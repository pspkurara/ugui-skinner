# uGUI Skinner

It is a library that can easily switch the display pattern of UGUI.<br>
Switch the style by script from index and string.

[![](https://img.shields.io/npm/v/com.pspkurara.ugui-skinner?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.pspkurara.ugui-skinner/)
[![](https://img.shields.io/github/v/release/pspkurara/ugui-skinner)](https://github.com/pspkurara/ugui-skinner/releases/)
[![](https://img.shields.io/github/watchers/pspkurara/ugui-skinner?style=social)](https://github.com/pspkurara/ugui-skinner/subscription)

![](https://raw.githubusercontent.com/pspkurara/ugui-skinner/preview/.github/readme/top.gif)

## Usage

### Inspector work

1. Add "UI" => "Skinner" from Add Component in Game Object with Unity Inspector.
2. Press "Add New Skin Style" button.
3. Press "Add New Skin Parts" button.
4. Edit the added Skin Parts and specify the appearance you want to switch.
5. Press the "Add New Skin Parts" button to clone the previous skin part, Switch the dropdown and set the required skin parts.
6. Repeat 2 ~ 6 for as many skins as you need!
7. Switch the Current Select Style and check if each appearance is reflected correctly.

### Scripting work

1. Create a new script and add "using Pspkurara.UI".
2. Add a variable to the script. "[SerializedField] private UISkinner _variable = null;"
3. Call the "SetSkin" function and specify the required Skin Style index ("Current Select Style" number in Skinner inspector) as an argument.

```
using Pspkurara.UI;

public class SampleScript : MonoBehaviour
{
    [SerializedField] private UISkinner _yourSkinner = null;

    private void Start ()
    {
        int yourFirstApplySkinStyleIndex = 0;
        _yourSkinner.SetSkin (yourFirstApplySkinStyleIndex);
    }
    
    public void ChangeSkinStyle ()
    {
        int yourNextSkinStyleIndex = 0;
        _yourSkinner.SetSkin (yourNextSkinStyleIndex);
    }
    
}
```

### More advanced features

1. Finish Inspector work up to #6.
2. Specify a unique string for "Style Key" directly under "Skin ~" fold.
3. Finish Scripting work up to #2.
4. Call the "SetSkin" function and specify the required "Style Key" as an argument.

```
using Pspkurara.UI;

public class SampleScript : MonoBehaviour
{
    [SerializedField] private UISkinner _yourSkinner = null;

    private void Start ()
    {
        string yourFirstApplySkinStyleKey = "Default";
        _yourSkinner.SetSkin (yourFirstApplySkinStyleKey);
    }
    
    public void ChangeSkinStyle ()
    {
        string yourNextSkinStyleKey = "ChangedSkin";
        _yourSkinner.SetSkin (yourNextSkinStyleKey);
    }
    
}
```

## Installation
### Using Unity Package Manager (For Unity 2018.3 or later)
Find the manifest.json file in the Packages folder of your project and edit it to look like this:

```
{
  "dependencies": {
    "com.pspkurara.ugui-skinner": "https://github.com/pspkurara/ugui-skinner.git#upm",
    ...
  },
}
```

Requirement
Unity 2018.3 or later

## License

* [MIT](https://github.com/pspkurara/ugui-skinner/blob/master/Packages/uGUI-Skinner/LICENSE.md)

## Author

[pspkurara](https://github.com/pspkurara)
[![](https://img.shields.io/twitter/follow/pspkurara.svg?label=Follow&style=social)](https://twitter.com/intent/follow?screen_name=pspkurara) 

## See Also

* GitHub page : https://github.com/pspkurara/ugui-skinner
