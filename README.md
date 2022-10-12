# VRSketchingGeometry
Work in progress. Features may not be complete and the API may change.

This is a framework for developing 3D sketching applications in Unity.

## Overview

The Unity project that holds the package and the project configuration lays on the `master` branch.</br>
The version of the package that can be used to import it into Unity lays on the `upm` branch.

## Installation and user guide

To import this package into Unity the import via URL can be used.</br>
For example: `https://github.com/hunsri/VRSketchingGeometryUP.git#<version>` </br>

For further details, please refer to [this readme](./Assets/VRSketchingGeometryPackage/README.md) inside the package.

## Developing for this package

This repository uses a subtree for the Unity Package.
That way the valid Unity Package and the development Unity project that contains it can be placed into one repository.

The development process stays the same, while the release of a new version of the package takes place on the `upm` branch.
You can find further details [here](https://www.patreon.com/posts/25070968)

### Pushing the Unity package to the upm branch

**IMPORTANT**</br>
Check that the `Samples` folder located under `Assets/` is renamed to `Samples~` before creating a new release!
Otherwise Unity will throw an error stating that the contained scripts can't be compiled.
This practice follows the Unity guidelines for creating the package structure.

Note that `"version"` needs to be replaced by the version number that you want to release.
```
git subtree split --prefix=UnityGLTFPackage/Assets/VRSketchingGeometry --branch upm
git tag "version" upm
git push origin upm --tags
```

To delete a wrong tag:
```
git tag -d tagName
```
If the wrong tag is already pushed:
```
git push origin :tagName
```

## Tests

There are unit tests using Unity Testing Framework. (https://docs.unity3d.com/Packages/com.unity.test-framework@1.1/manual/index.html)
They mostly cover the undoable commands and the generation and applying of data objects.
Coverage of the unit tests should be expanded.

**Before you can run the tests, you have to make sure the `CommandTestScene` is added in the Build Settings**</br>
The tests are located at `Assets/VRSketchingGeometryPackage/Tests`.</br>
To do that go to "File>Build Settings..." and check if `VRSKetchingGeometryPackage/Tests/Scenes/CommandTestScene` is present there.</br>
If that's not the case you have to add it.</br>

![BuildSettings](https://user-images.githubusercontent.com/51961152/195391439-bf552078-04a4-4722-aa1f-a12d7c8d21d0.png)


**To run the tests you have to open the Test Runner**</br>
You can find it under "Window>General>Test Runner".</br>
Once the Test Runner is open, click on "Play Mode".</br>
You can then select and perform the tests of your choice!</br>

![TestRunner](https://user-images.githubusercontent.com/51961152/195391886-76177d36-d95f-46c4-beba-3a93c37cb2f8.png)
![UnitTestSelection](https://user-images.githubusercontent.com/51961152/195391910-92307dfc-6cba-44df-b87e-50a8dfba7976.png)
