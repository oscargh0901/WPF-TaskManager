# WPF Task Manager

Aplicación de escritorio para gestionar tareas, construida con WPF, .NET 9 y el patrón MVVM.

## Funcionalidad

- Añadir tareas nuevas (por botón o pulsando Enter).
- Marcar tareas como completadas.
- Eliminar tareas de la lista.

## Requisitos

- Windows con [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0).
- Visual Studio 2022 (17.14+) o el comando `dotnet` desde la terminal.

## Cómo ejecutarlo

```bash
dotnet run --project WPF-TaskManager.csproj
```

O abre `WPF-TaskManager.sln` en Visual Studio y ejecuta el proyecto con F5.

> Nota: WPF solo se ejecuta en Windows, por lo que el proyecto no compila en Linux/macOS.

## Estructura del proyecto

```
WPF-TaskManager/
├── Models/
│   └── TaskItem.cs        # Modelo de una tarea (título, estado de completado)
├── ViewModels/
│   ├── MainViewModel.cs    # Lógica de la vista principal (añadir/eliminar tareas)
│   └── RelayCommand.cs     # Implementación de ICommand para los comandos del ViewModel
├── MainWindow.xaml(.cs)    # Vista principal
└── App.xaml(.cs)           # Punto de entrada de la aplicación
```

## Estado del proyecto

Es una base funcional mínima (MVP): permite crear, completar y eliminar tareas en memoria, sin persistencia en disco todavía. Próximas mejoras posibles: guardar las tareas en un archivo o base de datos local, categorías/etiquetas y fechas de vencimiento.
