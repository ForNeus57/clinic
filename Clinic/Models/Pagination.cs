using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Models;

public class PaginatedList<T>
{
	public int PageIndex { get; private set; }
	public int TotalPages { get; private set; }

	public bool HasPreviousPage => PageIndex > 1;

	public bool HasNextPage => PageIndex < TotalPages;

	public IEnumerable<T> Items { get; set; }

	public PaginatedList(IQueryable<T> source, int pageIndex, int pageSize)
	{
		var count = source.Count();
		var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

		PageIndex = pageIndex;
		TotalPages = (int) Math.Ceiling(count / (double) pageSize);
		Items = items;
	}

}