# Documentation Reference

**IMPORTANT** </br>
All provided commands have to be executed from the root of the project. </br>
To make the commands easier to read the path to the executable gets replaced with the command name.</br>
For example: </br>
`Packages/docfx.console.2.59.4/tools/docfx.exe` gets replaced with `docfx` </br>
**So in case you want to use the bundled docfx:** </br>
instead of a command like `docfx help` use `Packages/docfx.console.2.59.4/tools/docfx.exe help`

## General

**The documentation for this project is made with [DocFx](https://dotnet.github.io/docfx/)** </br>
DocFx is bundled as a NuGet package development dependency within this project. </br>

Your IDE should automatically pick up the dependency under `packages.json` and install the DocFx tool for you. </br>
Afterwards the tool can be found under `Packages/docfx.console.2.59.4`

However, in case the tool does not get installed you can also install it manually from [here](https://dotnet.github.io/docfx/). </br>
It is advised to use the same version used within this project.</br>
You can also install it via a package manager like [Chocolatey](https://community.chocolatey.org/packages/docfx). </br>
In that case you can also use the provided commands as is.

## How to build the documentation

**To build the projects documentation execute**
```docfx docfx.json```
This will create the `obj` and `_site` directories at the root of the project, which hold cache files and the built documentation. </br>

**Afterwards, to host and view the documentation locally append the `serve` flag**
```docfx docfx.json --serve```

**Troubleshooting** </br>
Sometimes it can be necessary to clear the generated files before creating new ones. </br>
This can be due to conflicting metadata, but can easily be resolved by deleting the `obj` directory.

## How to create a remote public documentation (e.g. for GitHub pages)

The documentation can also be remotely hosted via GitHub pages. </br>
Before you start, make sure that there are no documentation files under directories with a `~`. </br>
That breaks links on GH-pages.

Once a new release has been made, the remote public documentation should be updated as well. </br>
For that we create a new branch.
```git checkout -b doc/yourVersionHere```

Next, generate the documentation in a new `docs` directory, instead of the standard `_site`. </br>
To do that, you have to change the destination in `docfx.json`. </br>
Change `"dest": "_site",` into `"dest": "docs",` 

Finally **add and commit the changes** to git and push the changes onto the branch that you created previously.
```git push origin doc/yourVersionHere```

Once you are done, you can return to developing on another branch. </br>
Also you can now set the newly pushed branch as the base of your GH-pages. </br>
Or alternatively, merge it with a `docs` branch that holds the latest version of the documentation.

Note that you will have to specify that the `docs` folder should be used as the source of the documentation.

**IMPORTANT** </br>
The docs folder should never be pushed to the `master` or `upm` branch.
Ideally it should only exist on the `docs` branch.