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
		public static float force_height_wdiff_witch_pixel;
		public static void Init(Camera c){
			realScreenW = Screen.width;
			realScreenH = Screen.height;
			orthographicSize = c.orthographicSize;
			unit = realScreenH/2/orthographicSize;
			wdiff = (10.8f - realScreenW/unit)/2;
			force_height_wdiff_witch_pixel = 1080.0f - Screen.height/1920.0f*1080.0f;
		}
	}
}

