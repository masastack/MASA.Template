# Masa.Template
## Masa.Framework
### Install
#### Windows
At the root directory, open windows terminal and run commands:
```ps
> dotnet pack .\Masa.Framework --output nupkgs
> dotnet new -i .\nupkgs\Masa.Framework.0.1.0.nupkg

-- 0.1.0 need to replace real version
```
---
### Create Solution
#### CLI
Open windows terminal and run commands.
* Get Help
  ```ps
  > dotnet new masafx -h
  ```
* Create .net 6 project, it's the default.
  ```ps
  > dotnet new masafx -n Masa.EShop --DotNetFramework net6.0
  ```
* Rename the web project name and service project name.
  ```ps
  > dotnet new masafx -n Masa.EShop --WebProjectName Client --ServiceProjectName API
  ```

#### VS
create project in vs (!!!NOT Recommend)
### Uninstall
#### Windows
Open windows terminal and run commands:
* dotnet new --uninstall Masa.Framework
