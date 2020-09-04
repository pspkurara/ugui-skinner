using System.Collections.Generic;
using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// プロパティパック
	/// </summary>
	[System.Serializable]
	public sealed class SkinPartsPropertry : SkinPartsPropertryWithoutObjectReference
	{

		#region 変数

		/// <summary>
		/// <see cref="UnityEngine.Object"/>系を保存
		/// </summary>
		[SerializeField] private List<Object> m_ObjectReferenceValues = null;

		#endregion

		#region プロパティ

		/// <summary>
		/// Unityオブジェクト
		/// </summary>
		public List<Object> objectReferenceValues { get { return m_ObjectReferenceValues; } }

		#endregion

		#region コンストラクタ

		/// <summary>
		/// <see cref="SkinPartsPropertry"/>を初期化して生成
		/// </summary>
		public SkinPartsPropertry() :base()
		{
			m_ObjectReferenceValues = new List<Object>();
		}

		/// <summary>
		/// <see cref="SkinPartsPropertry"/>を初期化して生成
		/// </summary>
		/// <param name="base">複製元となるオブジェクト</param>
		public SkinPartsPropertry(SkinPartsPropertry @base) :base(@base)
		{
			m_ObjectReferenceValues.AddRange(@base.m_ObjectReferenceValues);
		}

		#endregion

	}

	/// <summary>
	/// プロパティパック
	/// オブジェクト参照は除く
	/// </summary>
	[System.Serializable]
	public class SkinPartsPropertryWithoutObjectReference
	{

		#region 変数

		/// <summary>
		/// フラグを保存
		/// </summary>
		[SerializeField] private List<bool> m_BoolValues = null;

		/// <summary>
		/// 色を保存
		/// </summary>
		[SerializeField] private List<Color> m_ColorValues = null;

		/// <summary>
		/// 少数を保存
		/// </summary>
		[SerializeField] private List<float> m_FloatValues = null;

		/// <summary>
		/// 整数を保存
		/// </summary>
		[SerializeField] private List<int> m_IntValues = null;

		#endregion

		#region プロパティ

		/// <summary>
		/// フラグ
		/// </summary>
		public List<bool> boolValues { get { return m_BoolValues; } }

		/// <summary>
		/// 色
		/// </summary>
		public List<Color> colorValues { get { return m_ColorValues; } }

		/// <summary>
		/// 少数値
		/// </summary>
		public List<float> floatValues { get { return m_FloatValues; } }

		/// <summary>
		/// 整数値
		/// </summary>
		public List<int> intValues { get { return m_IntValues; } }

		#endregion

		#region コンストラクタ

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference"/>を初期化して生成
		/// </summary>
		public SkinPartsPropertryWithoutObjectReference()
		{
			m_BoolValues = new List<bool>();
			m_ColorValues = new List<Color>();
			m_FloatValues = new List<float>();
			m_IntValues = new List<int>();
		}

		/// <summary>
		/// <see cref="SkinPartsPropertryWithoutObjectReference"/>を初期化して生成
		/// </summary>
		/// <param name="base">複製元となるオブジェクト</param>
		public SkinPartsPropertryWithoutObjectReference(SkinPartsPropertryWithoutObjectReference @base) :this()
		{
			m_BoolValues.AddRange(@base.m_BoolValues);
			m_ColorValues.AddRange(@base.m_ColorValues);
			m_FloatValues.AddRange(@base.m_FloatValues);
			m_IntValues.AddRange(@base.m_IntValues);
		}

		#endregion

	}

}
