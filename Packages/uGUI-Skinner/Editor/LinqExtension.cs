using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// <see cref="System.Linq"/>のように使える拡張関数
	/// </summary>
	internal static class LinqExtension
	{

		/// <summary>
		/// <see cref="IList.Insert(int, object)"/>を行い返す
		/// </summary>
		/// <param name="index">追加する場所</param>
		/// <param name="element">追加する要素</param>
		public static IList<T> InsertThen<T>(this IList<T> list, int index,  T element)
		{
			list.Insert(index, element);
			return list;
		}

		/// <summary>
		/// 指定された配列番号の対象要素を指定されたものに置き換えて返す
		/// </summary>
		/// <param name="index">変更対象の場所</param>
		/// <param name="element">変更後の要素</param>
		public static IList<T> ReplaceThen<T>(this IList<T> list, int index, T element)
		{
			list[index] = element;
			return list;
		}

	}

}
