namespace hinos.player {
    public abstract class Controller<TSource> {
        protected TSource source;

        public TSource Source {
            get => source;
            set => SetSource(value);
        }

        public Controller(TSource source) {
            this.source = source;
        }

        public void SetSource(TSource source) {
            this.source = source;
        }
    }
}
