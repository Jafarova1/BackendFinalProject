namespace FinalProject.Helpers
{
    public class PaginateData<T>
    {
        public List<T> Datas { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPage { get; set; }

        public PaginateData(List<T> datas, int currentpage, int totalPage)
        {
            Datas = datas;
            CurrentPage = currentpage;
            TotalPage = totalPage;
        }
        public bool HasPrevious
        {
            get
            {
                return CurrentPage > 1;
            }
        }
        public bool HasNext
        {
            get
            {
                return CurrentPage < TotalPage;
            }
        }
    }
}
