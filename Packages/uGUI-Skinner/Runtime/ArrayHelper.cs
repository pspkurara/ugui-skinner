using System.Collections.Generic;
using System.Linq;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// 配列系のいろいろをサポートするヘルパークラス
	/// </summary>
	internal static class ArrayHelper
	{

		/// <summary>
		/// 一致チェック用メソッドデリゲート
		/// </summary>
		/// <typeparam name="T">要素の型</typeparam>
		/// <param name="a">1つ目の比較対象の要素</param>
		/// <param name="b">2つ目の比較対象の要素</param>
		/// <returns></returns>
		public delegate bool ElementComparer<T>(T a, T b);

		/// <summary>
		/// 配列の中身が一致しているかを調べる
		/// </summary>
		/// <typeparam name="T">配列の要素の型</typeparam>
		/// <param name="a">1つ目比較対象の配列</param>
		/// <param name="b">2つ目比較対象の配列</param>
		/// <param name="comparer">一致チェック用メソッド</param>
		/// <returns>一致する</returns>
		public static bool ArrayEquals<T>(IEnumerable<T> a, IEnumerable<T> b, ElementComparer<T> comparer)
		{
			if (a == null && b == null) return true; 
			if (a == null || b == null) return false;
			if (a.Count() != b.Count()) return false;
			for (int i = 0; i < a.Count(); i++)
			{
				if (!comparer.Invoke(a.ElementAt(i), b.ElementAt(i))) return false;
			}
			return true;
		}

		/// <summary>
		/// <see cref="foreach"/>をまわす
		/// </summary>
		/// <param name="arraySize">配列サイズ</param>
		/// <param name="elementAction">呼び出すメソッド</param>
		public static void ForEach<T>(int arraySize, System.Action<int> elementAction)
		{
			for (int i = 0; i < arraySize; i++)
			{
				elementAction.Invoke(i);
			}
		}

		/// <summary>
		/// <see cref="foreach"/>をまわす
		/// </summary>
		/// <param name="array">配列</param>
		/// <param name="elementAction">呼び出すメソッド</param>
		public static void ForEach<T>(IEnumerable<T> array, System.Action<T> elementAction)
		{
			if (array == null) return;
			for (int i = 0; i < array.Count(); i++)
			{
				elementAction.Invoke(array.ElementAt(i));
			}
		}

		/// <summary>
		/// パターンに沿った処理を行い結果を反映させる
		/// </summary>
		/// <param name="array">配列</param>
		/// <param name="elementAction">呼び出して結果を返すメソッド</param>
		public static void ToPatternArray<T>(IList<T> array, System.Func<T, int, T> elementAction)
		{
			if (array == null) return;
			for (int i = 0; i < array.Count; i++)
			{
				array[i] = elementAction.Invoke(array[i], i);
			}
		}

	}

}