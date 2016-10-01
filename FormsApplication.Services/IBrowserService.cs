namespace Example.FormsApplication.Services
{
    public interface IBrowserService
    {
        string URL { get; }
        bool Closed { get; }
    }
}
