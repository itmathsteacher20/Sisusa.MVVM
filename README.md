# Sisusa.MVVM

A lightweight, flexible MVVM toolkit for .NET UI applications (WPF, WinUI, Avalonia). Sisusa.MVVM provides minimal scaffolding to implement MVVM, while keeping your architecture modular and testable. You can adopt as little or as much as you need.

> This is NOT a framework. Simple collection of utility classes for the MVVM pattern - pick and choose but remember these are not tested for 'prod'.
---

## Table of Contents

1. Overview
2. Installation
3. Core MVVM Features
4. Interaction Services
5. Coordinators
6. Use Cases
7. Recommendations
8. License

---

## Overview

Sisusa.MVVM provides:

* Base classes for ViewModels and Models
* Property change notifications for data-binding
* RelayCommand utilities for user interactions
* Interaction services to decouple ViewModels from UI dialogs
* Coordinator pattern support for optional navigation/lifecycle management

The library is unopinionated: you can use the parts you need without adopting a heavy framework.

Separating View, ViewModel, and Model improves testability, maintainability, and scalability of your UI applications.

---

## Installation

Install via NuGet:

```powershell
Install-Package Sisusa.MVVM
```

Include the namespaces you need:

```csharp
using Sisusa.MVVM;
using Sisusa.MVVM.Interactions;
```

---

## Core MVVM Features

### ViewModelBase

Provides property change notification, so bindings update automatically.

```csharp
public class MainViewModel : ViewModelBase
{
    private string _greeting;
    public string Greeting
    {
        get => _greeting;
        set => SetProperty(ref _greeting, value);
    }

    public ICommand SayHelloCommand { get; }

    public MainViewModel()
    {
        SayHelloCommand = CreateCommand(SayHello);
    }

    private void SayHello()
    {
        Greeting = "Hello, MVVM!";
    }
}
```

### IErrorHandler
A simple interface implemented by error handlers.
Handling an error can include:
- Logging
- Reporting
- Marshaling upwards

The `IErrorHandler` interface has one method `Handle(Exception)` .


### RelayCommand

Simplifies binding of user actions (buttons, menu items) to ViewModel methods.

```csharp
public ICommand SubmitCommand => CreateCommand(OnSubmit);

private void OnSubmit()
{
    // Handle submission logic
}
```

### AsyncRelayCommand

Simplifies binding of user actions (buttons, menu items) to asynchronous ViewModel methods.
Forces all async methods to take a `CancellationToken`. Ideally, such methods must make use of the Token and not 
simply accept it for signature compliance. This makes it possible to cancel the command action by calling
`AsyncRelayCommand.Cancel` if needed.

```csharp
private readonly IErrorHandler errorHandler;

.
.
.

public ICommand SubmitCommand => CreateAsyncCommand(OnSubmitAsync, errorHandler, CanSubmit);

private Task OnSubmitAsync(CancellationToken token)
{
   //submission logic that say, calls an api
}
```

---

## Interaction Services

Keep your ViewModels UI-agnostic while still requesting user interactions.

### Available Interactions

* PickFile
  To allow the user to choose a file from the file system.

*  PickFolder
  To help the user choose a folder from the file system.

* PickColor / PickFont
  To help the user pick a color or font.

* Confirm
  To ask the user for confirmation of an action.

* Inform
 To notify/inform the user of something that has happened, status of an action, etc.

* Prompt
To ask the user to provide some input `Task<string?> PrompAsync(string description)` or `Task<T?> PromptAsync<T?>(string)` depending on whether you
require `string` or `T` input from the user. 

NB: All these methods are `Task` returning methods to allow for async operation.

### Example

```csharp
public class MainViewModel : ViewModelBase
{
    private readonly IInteractionService _interaction;

    public MainViewModel(IInteractionService interaction)
    {
        _interaction = interaction;
    }

    public async Task PickAndConfirm()
    {
        var file = await _interaction.PickFile();
        if (!string.IsNullOrEmpty(file))
        {
            await _interaction.Inform($"You picked: {file}");
        }
    }
}
```

Your ViewModel remains fully testable, with no direct dependency on concrete UI dialogs.

---

## Coordinators

Optional pattern for managing navigation and ViewModel lifecycles.

* `CoordinatorBase` is a base class to implement coordinators.
* Coordinators can create or receive ViewModels via dependency injection (`IViewModelService`).
* Useful for app startup, multi-window management, or complex navigation flows.

### Example Coordinator

```csharp
public class AppCoordinator : CoordinatorBase
{
    private readonly IViewModelService _vmService;

    public AppCoordinator(IViewModelService vmService)
    {
        _vmService = vmService;
    }

    public void Start()
    {
        var mainVm = _vmService.Create<MainViewModel>();
        ShowWindow(mainVm);
    }

    private void ShowWindow(ViewModelBase vm)
    {
        // Implement showing the View bound to vm
    }
}
```

Coordinators provide structure without enforcing it — optional scaffolding for navigation and ViewModel lifecycle management.

---

## Use Cases

### When to Use

* Small-to-medium .NET UI applications
* Developers who want MVVM without heavy frameworks
* Projects needing decoupled UI logic and testable ViewModels
* Gradual adoption of MVVM, starting simple and adding complexity later

### When You Might Skip / Limit Usage

* Very simple UI (few controls)
* Advanced navigation or lifecycle requirements beyond basic Coordinator scope
* Non-binding UI frameworks or projects where MVVM is unnecessary

---

## Recommendations

* Start minimal: ViewModelBase + RelayCommand
* Add Interaction Services for dialog abstractions
* Introduce Coordinators for multi-window or complex navigation scenarios
* Keep Models lean and separate
* Write unit tests for ViewModels where possible

---

## License

Apache2.0 License — free and open-source.
