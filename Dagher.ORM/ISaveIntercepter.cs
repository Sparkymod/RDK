namespace Dagher.ORM
{
    public interface ISaveIntercepter
    {
        void BeforeSave(bool insert);
    }
}