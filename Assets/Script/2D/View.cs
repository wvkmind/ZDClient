using UnityEngine;
namespace Utils2D
{
	public class View{
		public static float unit;
		public static float realScreenW;
		public static float realScreenH;
		public static float orthographicSize;
		public const float design_unit = 9.6f;
		public static float wdiff;
		public static void Init(Camera c){
			realScreenW = Screen.width;
			realScreenH = Screen.height;
			orthographicSize = c.orthographicSize;
			unit = realScreenH/2/orthographicSize;
			wdiff = (10.8f - realScreenW/unit)/2;
		}
	}
}

