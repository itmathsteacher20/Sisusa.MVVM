# Sisusa.MVVM
A lightweight .NET library for implementing the MVVM (Model-View-ViewModel) design pattern in applications. 
Sisusa.MVVM provides a set of base classes and utilities to facilitate the development of maintainable and testable user interfaces.

> This is not a full-featured MVVM framework, but rather a simple library to help you get started with MVVM in your .NET applications.

## Features
- Base classes for ViewModel and Model
- Property change notification support
- Command implementation for handling user interactions
- Lightweight and easy to integrate into existing projects

## Installation
You can install Sisusa.MVVM via NuGet Package Manager. Run the following command in the Package Manager Console:
```
Install-Package Sisusa.MVVM
```
## Usage
To use Sisusa.MVVM in your project, follow these steps:
1. Create a ViewModel class that inherits from `ViewModelBase`.
2. Implement properties and commands in your ViewModel.
3. Bind your ViewModel to your View (e.g., WPF, WinUI, MAUI).
4. Use data binding to connect your View to the ViewModel properties and commands.
5. Run your application and enjoy the benefits of the MVVM pattern!

## Example
```csharp
using Sisusa.MVVM;
using System.Windows.Input;

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
