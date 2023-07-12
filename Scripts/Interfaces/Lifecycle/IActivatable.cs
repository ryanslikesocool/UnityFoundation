namespace Foundation {
    public interface IActivatable {
        /// <summary>
        /// <c>Activate</c> is called when an object should be enable without restarting its lifecycle.  This is similar to Unity's <c>OnEnable</c>.
        /// </summary>
        void Activate();
    }
}