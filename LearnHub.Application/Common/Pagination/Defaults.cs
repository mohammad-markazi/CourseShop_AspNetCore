namespace LearnHub.Application.Common.Pagination
{
    public static class Defaults
    {
        /// <summary>
        /// 1
        /// </summary>
        public static int Index { get; set; }
        /// <summary>
        /// 10
        /// </summary>
        public static int PageLength { get; set; }

        static Defaults()
        {
            Index = 1;
            PageLength = 10;
        }
    }
}