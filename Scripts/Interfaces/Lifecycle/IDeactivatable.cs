namespace Foundation {
    public interface IDeactivatable {
        /// <summary>
        /// <c>Deactivate</c> is called when an object should be disabled without ending its lifecycle.  This is similar to Unity's <c>OnDisable</c>.
        /// </summary>
        void Deactivate();
    }
}