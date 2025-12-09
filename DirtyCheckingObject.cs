namespace Sisusa.MVVM
{
    /// <summary>
    /// An object that tracks whether its properties/values have been changed since creation or since the last reset.
    /// </summary>
    public abstract class DirtyCheckingObject
    {

        //private Queue<Action> _undoActions;

        /// <summary>
        /// Flag tracking whether the object has had its values/properties changed or not.
        /// </summary>
        public bool IsDirty { get; protected set;  }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingField"></param>
        /// <param name="newValue"></param>
        protected void ChangeProperty<T>(ref T backingField, T newValue)
        {
            if (!Equals(backingField, newValue))
            {
                var oldValue = backingField;
                backingField = newValue;
                if (!IsDirty)
                    IsDirty = true;
            }
        } 
    }
}
