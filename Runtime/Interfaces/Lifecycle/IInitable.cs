namespace Foundation {
    public interface IInitable {
        /// <summary>
        /// <c>Init</c> is called at the beginning of an object's lifecycle.  This is similar to Unity's <c>Awake</c> or <c>Start</c>
        /// </summary>
        void Init();
    }
}