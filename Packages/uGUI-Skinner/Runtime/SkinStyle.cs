using System.Collections.Generic;
using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// 見た目の大本データ
	/// <see cref="SkinParts"/>を統合管理する
	/// </summary>
	[System.Serializable]
	public sealed class SkinStyle
	{

		#region 変数

		[SerializeField] private string m_StyleKey = string.Empty;
		[SerializeField] private List<SkinParts> m_Parts;

		private ISkinStyleParent m_StyleParent;

		#endregion

		#region プロパティ

		/// <summary>
		/// スタイルを決める各要素
		/// </summary>
		public List<SkinParts> parts { get { return m_Parts; } }

		/// <summary>
		/// スタイルの文字列キー
		/// </summary>
		public string styleKey { get { return m_StyleKey; } }

		/// <summary>
		/// 親を基にしたスタイルのインデックス
		/// 親がないときまたは親に存在しないときは -1 を返す
		/// </summary>
		public int styleIndex {
			get {
				if (m_StyleParent == null) return -1;
				return m_StyleParent.GetStyleIndexInParent(this);
			}
		}

		#endregion

		#region コンストラクタ

		public SkinStyle()
		{
			m_Parts = new List<SkinParts>();
		}

		public SkinStyle(SkinStyle baseObject) : this()
		{
			foreach (SkinParts p in baseObject.m_Parts)
			{
				m_Parts.Add(p.Clone());
			}
		}

		public SkinStyle(int initCount) : this()
		{
			for (int i = 0; i < initCount; i++)
			{
				m_Parts.Add(new SkinParts());
			}
		}

		#endregion

		#region メソッド

		/// <summary>
		/// スキンスタイルの親を設定する
		/// </summary>
		/// <param name="styleParent">親</param>
		internal void SetStyleParent(ISkinStyleParent styleParent)
		{
			m_StyleParent = styleParent;
		}

		/// <summary>
		/// 見た目を反映
		/// </summary>
		public void Apply()
		{
			foreach (SkinParts cParts in m_Parts)
			{
				cParts.Apply(this);
			}
		}

		/// <summary>
		/// インスペクター変更時コールバック
		/// </summary>
		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		internal void OnValidate()
		{
			foreach (SkinParts cParts in m_Parts)
			{
				cParts.OnValidate();
			}
		}

		#endregion

	}

}
