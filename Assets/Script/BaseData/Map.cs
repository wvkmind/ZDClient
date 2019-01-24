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
        public static string [] [] subMapName =
        {
            new [] {"ChickenHouse.spr","CowCage.spr","HenhouseBackGarden.spr","HenhouseInside.spr"} 
        };
        public static int GetMapIndex(string name){
            for(int i = 0;i<MapName.Length;i++){
                if(MapName[i].Equals(name))return i;
            }
            return -1;
        }
        public static int GetSubMapIndex(string main_name,string sub_name){
            int main_index = GetMapIndex(main_name);
            if(main_index==-1)return -1;
            for(int i = 0;i<subMapName[main_index].Length;i++){
                if(subMapName[main_index][i].Equals(sub_name))return i;
            }
            return -1;
        }
    }

}