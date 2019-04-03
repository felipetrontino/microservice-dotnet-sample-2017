namespace Framework.Test.Data
{
    public interface IMockRepository
    {
        void Add<T>(T e) where T : class;
    }
}
