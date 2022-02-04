namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// スキンパーツの種類
	/// スキンパーツが増えたら随時追加すること
	/// </summary>
	public enum SkinPartsType
	{
		ObjectsSetActives = 0,
		GraphicColor = 1,
		CanvasGroupAlpha = 2,
		ImageSprite = 3,
		RawImageTexture = 4,
		GraphicMaterial = 5,
		ShadowColor = 6,
		OutlineColor = 7,
		BaseMeshEffectEnable = 8,
		GraphicEnable = 9,
		CanvasEnable = 10,

		TransformRotation = 12,
		TransformScale = 13,

		AnimationSample = 20,

		SubSkinner = 99,
		ScriptableLogic = 100,
	}

}
