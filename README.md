# Inventory

Example of a frontend and backend system using protobufs and unity

## Pre-alpha
This is repo still very much in active development, and silly errors are currently tolerated since this is just a fun project for me


## Goals
1. Simple flow for updating data format
2. Strong enforcement of data version based on data format changes
3. Simple editor flow for updating data format and testing server functionality
4. Build system previously not used
5. CI using systems previously not used
6. Using a logging container on both server and client
7. Use Unity Packages for potentially sharing among multiple unity apps. Ideally also have an ability to publish nuget packages for the same reason

### Decision Status
1. Proto directories are going to be symlinked inside the unity folder
    * This requires `build.ps1` to be run before running, as we are not going to trust the git symlink option for now
    * There are a ton of git options and symlinks is yet another optional piece that can go wrong in a developer's setup. Having lfs and all these other bits can trip up clean development
    * Consequences:
        1. Server dll can get out of sync from unity version since unity auto-compiles and the server piece would need to compile the dll then itself
        2. Working solution to the above compensate with a daemon-style approach that would re-launch the server once compilation is done
2. Strong enforcement comes from using an md5 of all source proto files across both platforms
    * This requires that line endings are always consistent between platforms to make sure the md5 sum is the same for in-editor flows as ci flows
    * Probably should flag mixed line endings as an error or automatically run `dos2unix` or `unix2dos` with a C# filewatcher daemon background process
    * Could also just warn with a linter or something
    * Probably worth considering all of the above. linters are good, and making sure that temporary hashes/etc are the same seems good
    * When in a 'production' mode, also stamp the dll with a git hash and verify against that. Maybe also a product version
3. Will write Background C# daemon that uses a Filewatcher to rebuild server in the unity editor
    * Will likely play around with grpc to see if this can simplify the process of setting up trivial CRUD style calls on both client and server
    * Daemon should use consistent process to rebuild dll, likely the same build system as below
4. Using [Nuke build](https://nuke.build/) 
5. [Travis](https://travis-ci.org/github/hardcoded2/Inventory) Since this is popular, and seems to be a newcomer to both windows and unity development. Still very much in progress as the packages are 
6. Still early - [Microsoft.Extensions.Logging](https://docs.microsoft.com/en-us/archive/msdn-magazine/2016/april/essential-net-logging-with-net-core) for the logging controller and for implementation options:
    * [nlog unity](https://github.com/sschmid/NLog) under the hood 
    * [log4net unity](https://github.com/HolyShovelSoft/log4net.unity) 
    * Ultimately whichever one has the nice eidtor window for configuring it. Both solutions require an option to use a [Filewatcher](https://docs.microsoft.com/en-us/dotnet/api/system.io.filesystemwatcher?view=netcore-3.1) to re-read the config xml file to re-load the logging options at runtime, which is almost a requirement if you want to be able to adjust logging options/masks on device
7. Still open to this goal, but wanted to play with the new transitive dependencies in the 2020 unity package manager
    * C# protos package depends on -> Google protobuf dlls package
    * standard .NET nuget package should be easy to bang out in nukebuild
    * Open question - what is the best way to publish and stamp with release numbers on github

### Structure of Current Implementation
* Backend - backend code for running a service also using the same protobuf definitions and algorighm for determining "same-ness"
    * Not a lot of thought has gone here yet. Mostly tests of what is proclaimed to be ASP.Net best practices and standard logging/IoC patterns, along with ways of setting up flexible routes for the web endpoints
* _build - [Nuke build](https://nuke.build/) C# project for building the project, and also to be called by daemon process to rebuild client
* ExampleCustomProtoBufStructure
     * `protos` folder has the source protobufs
     * `gen` output C# files
     * `ExampleCustomProtoBufStructure.csproj` solution to build the protos as a dll. not sure if I want this approach, but want it as an option for now
* Frontend - Unity project
    * After switching to having this symlink to the protos folder, likely in-editor protobuf editing will only be avaiable after running the default build ie `build.ps1` in the root directory. Likely the unity editor will try to spin up the C# process watching for file changess that should call the build process to rebuild the protobuf dlls for server, and regenerate files for use in the unity editor
    * C# unity files are used raw in the editor to allow for in-editor use of partial classes. If another flow can be arranged that feels good, like using local packages and/or easy in-editor dll flow
    * Open questions:
        * How to migrate data - `Assets/Scripts/Protobufs/Migrations/ColorMigration.cs` is a trivial example where an alpha channel was added to protobufs, and we want the default value to be opaque (`1f`) rather than transparent, which is the C# default value `0f`
        * The best way to view assets serialzed using protobufs in the editor, current thinking is to use [Odin inspector](https://odininspector.com/) since getting arbitrary unity POCOs to serialize/be viewable in the editor is quirky. Could use some property drawers on the internet for some partial solutions and code up the rest, but not sure about that
        *  How to rely on a subset of items, `RequiredItemDefinitionSO.cs` is an attempt to put thought into requiring a specific item serialized in the edtor. but it is *really* bare. More questions than answers there -- Is this managing the deserializing of the protobuf (ala ISerialzationcallback) and using a `byte[]` backingstore not currently present in the class? `ItemDefintionFromJsonExample` did something similar to this, and perhaps a `[Serialized]` POCO could contain this, and have an interface for the `ISerialzationCallback` to call into. This would allow serialzation time stuff to be somewhat decoupled, but leaves open the issue of how those classes are configured
        * IoC container? Zenject is a likely candidate for use again here. Not sure the best practices for setting up a context for use at the time of serialization, which can be in the editor or at runtime, or if I want a different setup for those serialized classes than the rest of the unity application.

### Prerequisites:
* Unity `2019.3.0f5` or greater unil unity setup scripts are fleshed out
* On mac and linux, make sure to install the `protoc` package and their dependencies. This was an unexpected requirement since I thought the protoc.exe file was a C# executable file, but it is in fact just a windows binary.
    * Mac - run `brew install protobuf`
    * Linux/Ubuntu - run `apt-get install protobuf`
* Other requirements should be kept up to date in the .travis.yml file
