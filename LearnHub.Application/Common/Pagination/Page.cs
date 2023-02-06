namespace LearnHub.Application.Common.Pagination
{
    public class Page<TModel>
    {
        public IEnumerable<TModel> Items { get; set; }
        public int Index { get; set; }
        public int Length { get; set; }
        public int Count { get; set; }
    }
}