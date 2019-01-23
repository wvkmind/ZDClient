namespace BaseData
{
	public class Map  
    {
        public static string  MapSmallPath(int i){
            return "Image/Map/ChatMap/ChatMapSmall/ChatMap_"+i;
        } 
        
        public static string [] MapName = {
            "ChickenHouse.spr"
        };

        public static int GetMapIndex(string name){
            for(int i = 0;i<MapName.Length;i++){
                if(MapName[i].Equals(name))return i;
            }
            return -1;
        }
    }

}