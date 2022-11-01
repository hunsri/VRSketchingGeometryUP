# Example Scenes for the Sketching Geometry Framework

**The provided Scenes are intended to provide an overview over the Sketching Geometry Framework and its functionalities.**<br>

**IMPORTANT**<br>
Before you start the scenes make sure you also have the `Shared Assets` imported.</br>
The scenes are designed to work independently from the input system.
To interact with a scene at runtime you can use the buttons provided in the inspector of the `SceneScripts` GameObject within each scene.

![BrushExample](https://user-images.githubusercontent.com/51961152/195395639-a0ab97dc-4f84-4e89-b9bb-d3c28ae27885.png)

## LineExample

### Shows
- Creation of line objects with the command invoker
- Grouping of objects
- Undo and Redo actions

### Interaction
- Undo actions
- Redo actions

### Notes
- lines drawn with e.g. a controller should use the ´AddControlPointContinuousCommand´ instead

## BrushExample

### Shows
- Creation of brushes
- Adding of materials to line objects
- Adding of colors to line objects

### Interaction
- modified line creation

***
## Still Missing Examples
More examples like these should be added in the future, to replace the Legacy example.
The goal is to show all functionalities of the Modelling Kernel in detail.

### Missing Scenes
- Active line creation (with AddControlPointContinuousCommand instead of AddControlPointCommand)
- Object selection
- Object serialization/deserialization
- RibbonSketchObject
- PatchSketchObject
- OBJ export of Sketches
