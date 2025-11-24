using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sisusa.MVVM
{
    /// <summary>
    /// An object that can be bound to and notifies when its properties change.
    /// </summary>
    public abstract class BindableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// The event fired when a watched property changes its value.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Fires the PropertyChanged event for the given property name.
        /// </summary>
        /// <param name="propertyName">The name of the property whose value changed.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Changes the value of a property and notifies listeners if the value changed.
        /// </summary>
        /// <typeparam name="T">The data type of the property whose value is being being changed.</typeparam>
        /// <param name="backingField">The backing field of the property.</param>
        /// <param name="value">The new value of the property.</param>
        /// <param name="propertyName">The name of the property whose valeu is being changed.</param>
        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName]string propertyName = "")
        {
            if (!Equals(backingField, value))
            {
                backingField = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false; //equal no need to change or fire propertyChanged
        }
    }
}
