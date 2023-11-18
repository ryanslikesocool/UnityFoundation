namespace Foundation {
    public interface IDeinitable {
        /// <summary>
        /// <c>Deinit</c> is called at the end of an object's lifecycle.  This is similar to Unity's <c>OnDestroy</c>.
        /// </summary>
        void Deinit();
    }
}