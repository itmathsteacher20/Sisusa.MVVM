using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sisusa.MVVM
{
    /// <summary>
    /// A class that coordinates between different view models or components.
    /// </summary>
    public abstract class CoordinatorBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected IServiceProvider? _serviceProvider;

        protected bool OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        protected bool SetProperty<T>(ref T backingField, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (!Equals(backingField, newValue))
            {
                backingField = newValue;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        private ViewModelBase _currentViewModel = null!;

        /// <summary>
        /// The current or active view model.
        /// </summary>
        public ViewModelBase ActiveViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        private Dictionary<string, ViewModelBase> _viewModels = new();

        /// <summary>
        /// Navigates to the view model of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of view model to navigate to.</typeparam>
        public void NavigateTo<T>() where T : ViewModelBase, new()
        {
            if (ActiveViewModel != null)
            {
                var typeName = ActiveViewModel.GetType().FullName!;
                if (!_viewModels.ContainsKey(typeName))
                {
                    _viewModels[typeName] = ActiveViewModel;
                }
            }
            ActiveViewModel = Get<T>();
        }


        /// <summary>
        /// Loads the view model of the specified type.
        /// </summary>
        /// <typeparam name="T">The specific type of ViewModel to load.</typeparam>
        /// <returns>The found viewModel</returns>
        /// <exception cref="InvalidOperationException">If no viewModel matching type could be found.</exception>
        protected T Get<T>() where T : ViewModelBase
        {
            var typeName = typeof(T).FullName!;
            if (_viewModels.ContainsKey(typeName))
            {
                return (T)_viewModels[typeName];
            }
            var fromService = _serviceProvider?.GetService(typeof(T));
            if (fromService is T vm)
            {
                _viewModels[typeName] = vm;
                return vm;
            }
            throw new InvalidOperationException($"ViewModel of type {typeName} not found.");
        }

        public void Dispose()
        {
            foreach (var vm in _viewModels.Values)
            {
                vm.Dispose();
            }
            _viewModels.Clear();
            if (PropertyChanged != null)
            {
                foreach (var d in PropertyChanged.GetInvocationList())
                {
                    PropertyChanged -= (PropertyChangedEventHandler)d;
                }
            }
        }
    }
}
