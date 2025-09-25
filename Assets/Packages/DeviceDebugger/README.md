# Device Debugger
This library contains tools for on-device debugging.
This library contains:
- Graphy
- In-game debug console

## Installation
### Unity Package Manager
```
https://github.com/ThanhDang143/ThanhDV.DeviceDebugger.git?path=/Assets/Packages/DeviceDebugger
```

1. In Unity, open **Window** → **Package Manager**.
2. Press the **+** button, choose "**Add package from git URL...**"
3. Enter url above and press **Add**.

### Scoped Registry

1. In Unity, open **Project Settings** → **Package Manager** → **Add New Scoped Registry**
- ``Name`` ThanhDVs
- ``URL`` https://upm.thanhdv.icu
- ``Scope(s)`` thanhdv

2. In Unity, open **Window** → **Package Manager**.
- Press the **+** button, choose "**Add package by name...**" → ``thanhdv.devicedebugger``
- or
- Press the **Packages** button, choose "**My Registries**"

## How to use.
To add the debugger to your scene, use one of the following menu options:
- **Tools > Device Debugger > Add to Scene**
- **GameObject > Device Debugger**

⚠️ You only need one `DeviceDebugger` object in your project. It's best to place it in your main or starting scene.