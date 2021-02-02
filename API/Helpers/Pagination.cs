using System.Collections.Generic;

namespace API.Helpers
{
    public class Pagination<T> where T : class
    {
       //we want to count items after the filters have been applied
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        //može i enumerable, on je stavio ovo for consistency
        public IReadOnlyList<T> Data { get; set; }
    }
}