namespace Code.Analitics
{
    public interface IAnaliticsTools
    {
        void SendMessage(string nameEvent);
        void SendMessage(string nameEvent, (string key, object value) data);
    }
}