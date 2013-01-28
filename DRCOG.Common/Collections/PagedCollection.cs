using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DRCOG.Common.DesignByContract;

namespace DRCOG.Common.Collections
{
    /// <summary>
    /// Represents a collection of objects that have already been paged. 
    /// Pages displayed will be one-based indices and firstResults wiill be 0 based.
    /// This collection is a <see cref="Collection{T}"/>
    /// </summary>
    /// <typeparam name="T">The type of the contents of the collection</typeparam>
    public class PagedCollection<T> : Collection<T>, IPagedCollection<T>
    {
        public static Int32 GetFirstResult(Int32 pageNumber, Int32 pageSize)
        {
            return (pageNumber - 1) * pageSize;
        }

        /// <summary>
        /// Creates an instance of this class.
        /// </summary>
        /// <param name="list">The page of results</param>
        /// <param name="firstResult">The first result of this paged collection in the context of the entire recordset. Should be 0 based.</param>
        /// <param name="pageSize">The numbe rof records per page.</param>
        /// <param name="totalRecords"></param>
        public PagedCollection(IEnumerable<T> list, Int32 firstResult, Int32 pageSize, Int32 totalRecords)
        {
            foreach (T item in list)
            {
                this.Add(item);
            }
            Check.Require(pageSize > 0, "PageSize must be greater than zero");
            Check.Require(this.Count <= pageSize,
                "The list cannot be null and must be smaller or equal to the PageSize");
            FirstResult = firstResult;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }

        #region IPagedCollection<T> Members

        public Int32 FirstResult
        {
            get;
            private set;
        }

        private Int32 _currentPage = 1;
        public Int32 CurrentPage
        {
            get
            {
                if (_currentPage == 1)
                {
                    for (int p = 1; p <= TotalPages; p++)
                    {
                        if (FirstResult >= GetFirstResultOfPage(p) && FirstResult <= GetLastResultOfPage(p))
                        {
                            _currentPage = p;
                            break;
                        }
                    }
                }
                return _currentPage;
            }
        }

        public Int32 PageSize
        {
            get;
            private set;
        }

        public Int32 TotalRecords
        {
            get;
            private set;
        }

        public Int32 TotalPages
        {
            get
            {
                if (TotalRecords % PageSize != 0)
                {
                    return (TotalRecords / PageSize) + 1;
                }
                else
                {
                    return (TotalRecords / PageSize);
                }
            }
        }

        public Int32 GetFirstResultOfPage(Int32 page)
        {
            Check.Require(page <= TotalPages, "Cannot select a page outside the range of total pages. " +
                "Expected " + TotalPages.ToString() + " but was " + page.ToString());

            return GetFirstResult(page, PageSize);
        }

        public Int32 GetLastResultOfPage(Int32 page)
        {
            Int32 last;
            //check to see if its the last page
            if (page == TotalPages)
            {
                last = GetFirstResultOfPage(page) + this.Count - 1;
            }
            else
            {
                last = GetFirstResultOfPage(page) + PageSize - 1;
            }

            return last;
        }

        public Boolean HasPreviousPage
        {
            get 
            {
                return this.CurrentPage > 1;
            }
        }

        public Boolean HasNextPage
        {
            get
            {
                return this.CurrentPage < TotalPages;
            }
        }

        #endregion
    }
}
