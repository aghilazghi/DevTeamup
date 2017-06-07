using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevTeamup.Infrastructure
{
    public class PagedData<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _currentCollections;

        public int TotalCount { get; private set; }

        public int Page { get; private set; }

        public int PerPage { get; private set; }

        public int TotalPages { get; private set; }

        public bool HasNextPage { get; private set; }

        public bool HasPreviousPage { get; private set; }

        public int NextPage
        {
            get
            {
                if (!HasNextPage)
                    throw new InvalidOperationException();
                return Page + 1;
            }
        }

        public int PreviousPage
        {
            get
            {
                if(!HasPreviousPage)
                    throw new InvalidOperationException();
                return Page - 1;
            }
        }

        public PagedData(IEnumerable<T> currentCollections, int totalCount, int page, int perPage)
        {
            _currentCollections = currentCollections;
            TotalCount = totalCount;
            Page = page;
            PerPage = perPage;
            TotalPages = (int) Math.Ceiling((float) TotalCount / perPage);
            HasNextPage = Page < TotalPages;
            HasPreviousPage = Page > 1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _currentCollections.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}