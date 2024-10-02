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
�������������������������������������Щ���������������������������
�� Version         �� ServicePack ��
�������������������������������������੤��������������������������
�� SOLIDWORKS 2024 �� 0           ��
�������������������������������������ة���������������������������
AddIns installed:
�������������������������������������������������������������������������������Щ������������������������������������������������Щ��������������������������������������������������������������������������������������Щ�������������������
�� Id                                   �� Title                  �� Description                               �� Startup ��
�������������������������������������������������������������������������������੤�����������������������������������������������੤�������������������������������������������������������������������������������������੤������������������
�� 057baacc-d022-42a6-973d-3033fba23678 �� Presentation Manager   �� Presentation Manager for MBD              ��         ��
�� 898f63ef-5658-48fe-946e-d83ec7dc63b8 �� SOLIDWORKS XPS Driver  �� SOLIDWORKS XML Paper Specification Driver ��         ��
�������������������������������������������������������������������������������ة������������������������������������������������ة��������������������������������������������������������������������������������������ة�������������������
```

# Sample usage

> Add post build event to visual studio project, so that solidworks only load addin that develop currently.

```xml
  <Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Exec Command="swaddinmgr enable [AddinName] --only" />
  </Target>
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