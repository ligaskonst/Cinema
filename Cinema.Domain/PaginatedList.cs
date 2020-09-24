using System;
using System.Collections.Generic;

namespace Cinema.Domain
{
    public class PaginatedList<T> : List<T>
    {
        private int _itemsDisplayed = 5; // todo move to configuration

        public int PageIndex { get; }

        public int TotalPage { get; }

        public int FirstItem {
            get
            {
                int firstItem = PageIndex - _itemsDisplayed;

                return firstItem < 1 ? 1 : firstItem;
            }
        }

        public int LastItem
        {
            get
            {
                int lastItem = PageIndex + _itemsDisplayed;

                return lastItem > TotalPage ? TotalPage : lastItem;
            }
        }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPage = (int) Math.Ceiling(count / (double) pageSize);
            AddRange(items);
        }

        public bool PreviousPage => PageIndex > 1;

        public bool NextPage => PageIndex < TotalPage;
    }
}
