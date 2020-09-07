using System.Collections.Generic;
using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// 細かいツール
	/// </summary>
	internal static class SkinnerUtility
	{

		/// <summary>
		/// オブジェクトを強制的にリロードする
		/// </summary>
		/// <param name="component">リロードしたいオブジェクトのコンポーネント</param>
		public static void ReloadGameObject(Component component)
		{
			if (!component) return;
			bool activeSelf = component.gameObject.activeSelf;
			if (!activeSelf) return;
			component.gameObject.SetActive(false);
			component.gameObject.SetActive(activeSelf);
		}

		/// <summary>
		/// Objectリストを指定数に合わせて正規化する
		/// </summary>
		/// <param name="resetList">初期化対象</param>
		/// <param name="count">設定したい要素数</param>
		public static void ResetObjectReference(List<Object> resetList, int count = 0)
		{
			ResetList(resetList, count, null);
		}

		/// <summary>
		/// boolリストを指定数に合わせて正規化する
		/// </summary>
		/// <param name="resetList">初期化対象</param>
		/// <param name="count">設定したい要素数</param>
		public static void ResetBoolean(List<bool> resetList, int count = 0)
		{
			ResetList(resetList, count, true);
		}

		/// <summary>
		/// Colorリストを指定数に合わせて正規化する
		/// </summary>
		/// <param name="resetList">初期化対象</param>
		/// <param name="count">設定したい要素数</param>
		public static void ResetColor(List<Color> resetList, int count = 0)
		{
			ResetList(resetList, count, Color.white);
		}

		/// <summary>
		/// floatリストを指定数に合わせて正規化する
		/// </summary>
		/// <param name="resetList">初期化対象</param>
		/// <param name="count">設定したい要素数</param>
		public static void ResetFloat(List<float> resetList, int count = 0)
		{
			ResetList(resetList, count, 1f);
		}

		/// <summary>
		/// intリストを指定数に合わせて正規化する
		/// </summary>
		/// <param name="resetList">初期化対象</param>
		/// <param name="count">設定したい要素数</param>
		public static void ResetInteger(List<int> resetList, int count = 0)
		{
			ResetList(resetList, count, 0);
		}

		/// <summary>
		/// Vector4リストを指定数に合わせて正規化する
		/// </summary>
		/// <param name="resetList">初期化対象</param>
		/// <param name="count">設定したい要素数</param>
		public static void ResetVector4(List<Vector4> resetList, int count = 0)
		{
			ResetList(resetList, count, Vector4.zero);
		}

		/// <summary>
		/// リストを指定数に合わせて正規化する
		/// </summary>
		/// <typeparam name="T">リストの種類</typeparam>
		/// <param name="resetList">初期化対象</param>
		/// <param name="count">設定したい要素数</param>
		/// <param name="defaultValue">新規追加の際の初期要素値</param>
		private static void ResetList<T>(List<T> resetList, int count, T defaultValue)
		{
			int listCount = resetList.Count;
			for (int i = listCount; i < count; i++)
			{
				resetList.Add(defaultValue);
			}
			resetList.RemoveRange(count, resetList.Count);
		}

	}

}
