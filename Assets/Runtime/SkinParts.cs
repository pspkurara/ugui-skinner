using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// 見た目の個別データ
	/// </summary>
	[System.Serializable]
	public sealed class SkinParts
	{

		#region 変数

		/// <summary>
		/// 見た目を反映させる仕組みの種類
		/// </summary>
		[SerializeField] private int m_Type;

		/// <summary>
		/// 見た目を反映されるときに参照される値
		/// </summary>
		[SerializeField] private SkinPartsPropertry m_Property;

		private ISkinLogic applySkinFunction;

		#endregion

		#region プロパティ

		/// <summary>
		/// 見た目を反映させる仕組みの種類
		/// </summary>
		public int type { get { return m_Type; } internal set { m_Type = value; } }

		/// <summary>
		/// 見た目を反映されるときに参照される値
		/// </summary>
		public SkinPartsPropertry property { get { return m_Property; }}

		#endregion

		#region コンストラクタ

		/// <summary>
		/// <see cref="SkinParts"/>を初期化して生成
		/// </summary>
		public SkinParts()
		{
			m_Type = (int)default(SkinPartsType);
			m_Property = new SkinPartsPropertry();
			applySkinFunction = null;
		}

		/// <summary>
		/// <see cref="SkinParts"/>を初期化して生成
		/// </summary>
		/// <param name="base">複製対象となるデータ</param>
		public SkinParts(SkinParts @base) : this()
		{
			m_Type = @base.m_Type;
			m_Property = new SkinPartsPropertry(@base.m_Property);
		}

		#endregion

		/// <summary>
		/// 自身の複製を作る
		/// </summary>
		/// <returns>複製</returns>
		public SkinParts Clone()
		{
			return new SkinParts(this);
		}

		/// <summary>
		/// 見た目を反映する
		/// </summary>
		public void Apply()
		{
			//初回はタイプによって状態を変える
			if (applySkinFunction == null)
			{
				applySkinFunction = SkinPartsAccess.CreateSkinLogicInstance(type);
			}
			//値を適応する
			applySkinFunction.SetValues(property);
		}

		/// <summary>
		/// インスペクター変更時コールバック
		/// </summary>
		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		internal void OnValidate()
		{
			//空にすることでタイプが変わっても正常に動作するようにする
			applySkinFunction = null;
		}

	}

}
