# SwAddinManagerCli

![Icon](SwAddInManager.png)

> Cli tool that enable or disable solidworks addIns startup state.

# Install

[Nuget](https://www.nuget.org/packages/SwAddinManagerCli)

>  dotnet tool install --global SwAddinManagerCli

## List addins 

```
PS C:\Users\Administrator> swaddinmgr list
Solidworks installed:
┌─────────────────┬─────────────┐
│ Version         │ ServicePack │
├─────────────────┼─────────────┤
│ SOLIDWORKS 2024 │ 0           │
└─────────────────┴─────────────┘
AddIns installed:
┌──────────────────────────────────────┬────────────────────────┬───────────────────────────────────────────┬─────────┐
│ Id                                   │ Title                  │ Description                               │ Startup │
├──────────────────────────────────────┼────────────────────────┼───────────────────────────────────────────┼─────────┤
│ 057baacc-d022-42a6-973d-3033fba23678 │ Presentation Manager   │ Presentation Manager for MBD              │         │
│ 0d27d5d6-eb7f-4c0d-82ea-51017c236bdb │ Export to gltf or glb  │ SOLIDWORKS XRExporter                     │ False   │
│ 3dfb4fad-a719-3534-8491-2de6c8f2478a │ SolidWorks Lookup      │ Lookup SolidWorks Objects 0.0.4           │ False   │
│ 52b255ae-643f-4ac8-92de-dd213eede9cc │ 标记工具               │                                           │         │
│ 6877af96-14fa-4d34-986b-633320b01aa1 │ 俊鹏六贵电气平台       │                                           │ False   │
│ 81bf4582-ea69-4ff2-8b9f-2cbb332cacdc │ SVN for Solidworks     │                                           │ False   │
│ 898f63ef-5658-48fe-946e-d83ec7dc63b8 │ SOLIDWORKS XPS Driver  │ SOLIDWORKS XML Paper Specification Driver │         │
│ ea0a1266-013e-4f1f-bba2-47609207cddd │ DMS                    │                                           │         │
│ f88e0dc6-4359-4c9f-ab02-3124f8767997 │ TouchMDesign           │ SolidWorks Addin for Mainfold Design      │ False   │
└──────────────────────────────────────┴────────────────────────┴───────────────────────────────────────────┴─────────┘
```

```
USAGE:
    SwAddinManagerCli.dll [OPTIONS] <COMMAND>

OPTIONS:
    -h, --help    Prints help information

COMMANDS:
    list
    disable
    enable
    enableall
    disableall
```