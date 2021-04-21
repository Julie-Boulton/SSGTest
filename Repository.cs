using SSG.Interview.Contracts;
using System;
using System.Collections.Generic;

namespace SSG.Interview
{
	public class Repository<T> : IRepository<T> where T : IStoreable
	{
		private List<T> items;

		public Repository()
        {
			items = new List<T>();
        }

		/// <summary>
		/// Get all items in the Respository
		/// </summary>
		/// <returns>An IEnumerable of Items </returns>
		public IEnumerable<T> All()
		{
			return items;
		}

		/// <summary>
		/// Delete an item by its id
		/// </summary>
		/// <param name="id">Id of item to delete</param>
		public void Delete(IComparable id)
		{
			T item = items.Find(Match(id));

			if (item != null)
			{
				items.Remove(item);
			}
		}

		/// <summary>
		/// Save a new item
		/// </summary>
		/// <param name="item">item to Save</param>
		public void Save(T item)
		{
			try
			{
				// Try to delete the item first to stop getting duplicate entries
				Delete(item.Id);

				items.Add(item);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Find an item by its id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public T FindById(IComparable id)
		{
			return items.Find(Match(id));
		}

		/// <summary>
		/// Find the matching item by using its id
		/// </summary>
		/// <param name="id">IComparable id to match on</param>
		/// <returns>item that matches if found</returns>
		private Predicate<T> Match(IComparable id)
		{
			return item => item.Id.Equals(id);
		}
	}
}
