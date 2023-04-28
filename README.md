# Masa.Template

> For information on how to use `dotnet new` CLI, visit the [official documentation](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new).

After installing the template, you can quickly create the following items:

* masablazor-server: MASA Blazor Server Template
* masablazor-wasm: MASA Blazor WebAssembly Template
* masablazor-empty-server: MASA Blazor Server Empty Template
* masablazor-empty-wasm: MASA Blazor WebAssembly Empty Template
* masablazor-pro-server: MASA Blazor Pro Server Template
* masablazor-pro-wasm: MASA Blazor Pro WebAssembly Template
* masablazor-maui: MASA Blazor MAUI Template
* masafx: MASA Framework Full Template(Cqrs、Ddd、Repository、Dapr、Auth、Minimal Api)
* masafx-service: MASA Framework Pure Template (Minimal Api)
* masafx-service-cqrs: MASA Framework CQRS Template(Minimal Api、CQRS)

## Install template

```shell
dotnet new install Masa.Template
```

## 

### Install specific version or prerelease version

```shell
dotnet new install Masa.Template::1.0.0-rc.1
```

## Uninstall template

```shell
dotnet new uninstall Masa.Template
```

## How to use

Take the masa framework template(masafx) as an example, run commands:

### Get help

```shell
dotnet new masafx -h
```

### Create project

```shell
dotnet new masafx -n Masa.EShop
```

## Develop or install locally

1. Clone the repository

   ```shell
   git clone https://github.com/masastack/MASA.Template.git
   ```

2. Install Template

   ```shell
   cd MASA.Template
   dotnet new install .\src\ --force
   ```

3. Uninstall Template

   ```shell
   dotnet new uninstall  .\src\ 
   ```