using System;

namespace RDK.Core.Cache
{
    public sealed class ObjectValidator<T>
    {
        public event Action<ObjectValidator<T>> ObjectInvalidated;
        public TimeSpan? LifeTime { get; set; }

        private T Instance;
        private DateTime LastCreationDate;
        private bool Valid;
        private readonly object Sync = new();
        private readonly Func<T> Creator;

        /// <summary>
        /// Check if T is valid then return the same instance.
        /// </summary>
        /// <param name="creator">Same as T</param>
        /// <param name="lifeTime"></param>
        public ObjectValidator(Func<T> creator, TimeSpan? lifeTime = null)
        {
            Creator  = creator;
            LifeTime = lifeTime;
        }

        // This is where the operator validates the Type of the object and return it.
        public static implicit operator T(ObjectValidator<T> validator)
        {
            if (validator.IsValid) { return validator.Instance; };

            lock (validator.Sync)
            {
                if (validator.IsValid) { return validator.Instance; };
                validator.Instance = validator.Creator();
                validator.LastCreationDate = DateTime.Now;
                validator.Valid = true;
            }

            return validator.Instance;
        }

        public void Invalidate()
        {
            Valid = false;
            NotifyObjectInvalidated();
        }

        private void NotifyObjectInvalidated() => ObjectInvalidated?.Invoke(this);

        private bool IsValid => Valid && (LifeTime == null || DateTime.Now - LastCreationDate < LifeTime.Value); 
    }
}
