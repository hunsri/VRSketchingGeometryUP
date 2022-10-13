# VRSketchingGeometry
Work in progress. Features may not be complete and the API may change.

This is a framework for developing 3D sketching applications in Unity.

## Features:
- smoothly interpolated lines
- patch surfaces
- ribbon shaped lines
- organisation of sketch objects with groups
- undo and redo
- serialization of sketches
- OBJ export of sketches

## Installation

### Manually from a local folder
- open the Package Manager
- click on "+"
- choose "Add package from disk..."
- locate the downloaded package
- select the package.json file within the package

### Automatically from URL
- open the Package Manager
- click on "+"
- choose "Add package from git URL..."
- enter the URL 
``` 
https://github.com/hunsri/VRSketchingGeometryUP.git#0.1.0
```

## Import the examples
- open the Package Manager
- locate the installed package
- click on "Samples"
- click on "Import"
- files will be imported under `Assets/Samples/`

## [API documentation](https://tterpi.github.io/VRSketchingGeometry/)
Read the [developer guide](https://tterpi.github.io/VRSketchingGeometry/articles/intro.html) and [API documentation](https://tterpi.github.io/VRSketchingGeometry/api/index.html) at the github pages site.

## Quick start
The following example script shows how to create a new line sketch object and add few control points to it using a command invoker.</br>
At the end one command is undone.</br></br>
You will have to reference a `DefaultReference` Scriptable Object in the inspector.</br>
An example can be found under `SharedAssets/Assets/DefaultReferences.asset`.</br>
To access `SharedAssets` you have to import it in the Package Manager (see [Import the examples](./README.md#import-the-examples))</br>

See [the example script](./Samples~/LegacyExample/Scripts/VRSketchingExample.cs) for a more comprehensive demonstration.

```C#
    using UnityEngine;
    using VRSketchingGeometry.SketchObjectManagement;
    using VRSketchingGeometry;
    using VRSketchingGeometry.Commands;
    using VRSketchingGeometry.Commands.Line;

    public class CreateLineSketchObject : MonoBehaviour
    {
        public DefaultReferences Defaults;
        private LineSketchObject LineSketchObject;
        private SketchWorld SketchWorld;
        private CommandInvoker Invoker;

        void Start()
        {
            SketchWorld = Instantiate(Defaults.SketchWorldPrefab).GetComponent<SketchWorld>();
            LineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            Invoker = new CommandInvoker();
            Invoker.ExecuteCommand(new AddObjectToSketchWorldRootCommand(LineSketchObject, SketchWorld));
            Invoker.ExecuteCommand(new AddControlPointCommand(this.LineSketchObject, new Vector3(1, 2, 3)));
            Invoker.ExecuteCommand(new AddControlPointCommand(this.LineSketchObject, new Vector3(1, 4, 2)));
            Invoker.ExecuteCommand(new AddControlPointCommand(this.LineSketchObject, new Vector3(1, 5, 3)));
            Invoker.ExecuteCommand(new AddControlPointCommand(this.LineSketchObject, new Vector3(1, 5, 2)));
            Invoker.Undo();
        }
    }
```

## Workflow
1. Instantiate a sketch world prefab. Easy access to prefabs is provided through the DefaultReferences asset at `SharedAssets/Assets/DefaultReferences.asset`.
2. Create sketch objects and groups from prefabs and add them to the sketch object world. Execute commands using a CommandInvoker object for undo and redo functionality. All scripts are in the VRSketchingGeometry namespace.
4. Serialize or export using methods of the sketch world script.
5. Load serialized sketch world from the serialized xml file for further editing.

An [example script](./Samples~/LegacyExample/Scripts/VRSketchingExample.cs) was created to show this process in practice.

## Samples

Various examples are provided under `VRSketchingGeometryPackage/Samples`.
These can be imported via the Unity Package Manager.
(see [Import the examples](./README.md#import-the-examples))

### Shared Assets
Contains all the assets required to run the examples.
Can be used as a reference for the creation of own assets.

### Example Scenes
Contains various examples. For further details please refer to [this readme](./Samples~/ExampleScenes/Scenes/README.md).

### Legacy Example
Contains a static scene with various messy test scripts and corresponding game objects.
![sampleScene](https://user-images.githubusercontent.com/51961152/192534926-2c6406b1-4556-4808-baf7-9f8eeab0bc5f.png)

## Notes
This is a conversion from the plugin version (https://github.com/tterpi/VRSketchingGeometry) to a package version.
Originally based on code from: https://github.com/bittermanq/KochanekBartelsSplines
