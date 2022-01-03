namespace Code.Items
{
    public interface IItem
    {
        public  int Id { get; set; }
        ItemInfo Info { get; set; }
    }
}