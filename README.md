# WPF Task Manager

![Build](https://github.com/oscargh0901/WPF-TaskManager/actions/workflows/build.yml/badge.svg)

Aplicación de escritorio para gestionar tareas, construida con WPF, .NET 9 y el patrón MVVM.

## Funcionalidad

- Añadir tareas nuevas (por botón o pulsando Enter).
- Marcar tareas como completadas.
- Eliminar tareas de la lista.
- Contador de tareas completadas frente al total.
- Las tareas se guardan automáticamente en disco y se recuperan al volver a abrir la app.

## Requisitos

- Windows con [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0).
- Visual Studio 2022 (17.14+) o el comando `dotnet` desde la terminal.

## Cómo ejecutarlo

```bash
dotnet run --project WPF-TaskManager.csproj
```

O abre `WPF-TaskManager.sln` en Visual Studio y ejecuta el proyecto con F5.

> Nota: WPF solo se ejecuta en Windows, por lo que el proyecto no compila en Linux/macOS.

## Cómo ejecutar los tests

```bash
dotnet test WPF-TaskManager.sln
```

El proyecto `WPF-TaskManager.Tests` contiene pruebas unitarias (xUnit) del `MainViewModel`, usando una implementación de `ITaskStorageService` en memoria para no depender del sistema de archivos.

## Persistencia

Las tareas se guardan en formato JSON en `%AppData%\WPF-TaskManager\tasks.json`. El acceso a este archivo está aislado detrás de la interfaz `ITaskStorageService`, lo que permite sustituirlo (por ejemplo, por una base de datos) sin tocar el ViewModel.

## Estructura del proyecto

```
WPF-TaskManager/
├── Models/
│   └── TaskItem.cs                 # Modelo de una tarea (título, estado de completado)
├── Services/
│   ├── ITaskStorageService.cs      # Contrato de persistencia
│   └── JsonTaskStorageService.cs   # Implementación con un archivo JSON local
├── ViewModels/
│   ├── MainViewModel.cs            # Lógica de la vista principal (añadir/eliminar/persistir tareas)
│   └── RelayCommand.cs             # Implementación de ICommand para los comandos del ViewModel
├── WPF-TaskManager.Tests/          # Pruebas unitarias (xUnit) del ViewModel
├── MainWindow.xaml(.cs)            # Vista principal
└── App.xaml(.cs)                   # Punto de entrada de la aplicación
```

## Integración continua

Cada push y pull request a `main` dispara un workflow de GitHub Actions (`.github/workflows/build.yml`) que compila la solución y ejecuta los tests en `windows-latest`.

## Próximas mejoras posibles

- Editar el título de una tarea existente.
- Categorías o etiquetas y fechas de vencimiento.
- Persistencia en una base de datos local (SQLite) en lugar de un archivo JSON.
