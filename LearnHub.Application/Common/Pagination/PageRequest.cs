namespace LearnHub.Application.Common.Pagination
{
    public class PageRequest
    {
        public int Index { get; set; }
        public int Length { get; set; }

        public PageRequest()
        {
            Index = Defaults.Index;
            Length = Defaults.PageLength;
        }
    }
}