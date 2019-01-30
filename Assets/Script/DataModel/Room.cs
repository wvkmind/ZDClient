namespace DataModel
{
    public class Room
    {
        public int id;
        public string map_name;
        public string title;
        public string user_number;
        public int [] user_list;
        public  Room UnPack(MsgPack.MessagePackObject net_info){
            MsgPack.MessagePackObjectDictionary room_dic= net_info.AsDictionary();
            MsgPack.MessagePackObject temp;
            room_dic.TryGetValue("id",out temp);
            id = temp.AsInt32();
            room_dic.TryGetValue("map_name",out temp);
            map_name = temp.AsStringUtf8();
            room_dic.TryGetValue("title",out temp);
            title = temp.AsStringUtf8();
            room_dic.TryGetValue("user_number",out temp);
            user_number = temp.AsStringUtf8();

            return this;
        }
    }
}
