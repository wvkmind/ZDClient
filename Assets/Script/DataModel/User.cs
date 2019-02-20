namespace DataModel
{
    public class User
    {
        public int status = -1;
        public int id;
        public string name;
        public int type;
        public int tra_rate;
        public int phy_str_rate;
        public int exp_rate;
        public int level;
        public int zhanyang;
        public int buliang;
        public float cur_x;
        public float cur_y;
        public int direction;
        public float target_x;
        public float target_y;
        public int tilizhi;
        public int exp;
        public  User UnPack(MsgPack.MessagePackObject net_user){
            MsgPack.MessagePackObjectDictionary user_dic= net_user.AsDictionary();
            MsgPack.MessagePackObject temp;
            user_dic.TryGetValue("status",out temp);
            status = temp.AsInt32();
            user_dic.TryGetValue("id",out temp);
            id = temp.AsInt32();
            user_dic.TryGetValue("name",out temp);
            name = temp.AsStringUtf8();
            user_dic.TryGetValue("type",out temp);
            type = temp.AsInt32();
            user_dic.TryGetValue("tra_rate",out temp);
            tra_rate = temp.AsInt32();
            user_dic.TryGetValue("phy_str_rate",out temp);
            phy_str_rate = temp.AsInt32();
            user_dic.TryGetValue("exp_rate",out temp);
            exp_rate = temp.AsInt32();
            user_dic.TryGetValue("level",out temp);
            level = temp.AsInt32();
            user_dic.TryGetValue("zhanyang",out temp);
            zhanyang = temp.AsInt32();
            user_dic.TryGetValue("buliang",out temp);
            buliang = temp.AsInt32();
            user_dic.TryGetValue("room_pos",out temp);
            cur_x = (float)temp.AsList()[0].AsDouble();
            cur_y = (float)temp.AsList()[1].AsDouble();
            direction = (int)temp.AsList()[2].AsDouble();
            target_x = (float)temp.AsList()[3].AsDouble();
            target_y = (float)temp.AsList()[4].AsDouble();
            user_dic.TryGetValue("tilizhi",out temp);  
            tilizhi = temp.AsInt32();
            user_dic.TryGetValue("exp",out temp);  
            exp = temp.AsInt32();
            return this;
        }
    }
}
