namespace Innovi.Models
{
    public class CountListData<T>
    {
        public List<T> ListData { get; set; }
        public int Count { get; set; }
        public CountListData(List<T> listData, int count)
        {
            ListData = listData;
            Count = count;
        }
    }
}
