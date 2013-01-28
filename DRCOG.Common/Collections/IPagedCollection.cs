using System;
using System.Collections.Generic;

namespace DRCOG.Common.Collections
{
    public interface IPagedCollection<T> : IEnumerable<T>
    {
        Int32 GetFirstResultOfPage(Int32 page);

        Int32 Count { get; }

        Int32 FirstResult { get; }
        /// <summary>
        /// The current page expressed as a one-based index.
        /// </summary>
        Int32 CurrentPage { get; }
        /// <summary>
        /// The number of records displayed per page.
        /// </summary>
        Int32 PageSize { get; }
        /// <summary>
        /// The total number of records.
        /// </summary>
        Int32 TotalRecords { get; }
        /// <summary>
        /// The total pages based on a one-based index.
        /// </summary>
        Int32 TotalPages { get; }

        Int32 GetLastResultOfPage(Int32 page);

        Boolean HasPreviousPage { get; }

        Boolean HasNextPage { get; }
    }
}
