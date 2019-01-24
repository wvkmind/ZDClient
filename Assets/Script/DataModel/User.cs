namespace DataModel
{
    public class User
    {
        public int status;
        public int id;
        public string name;
        public int type;
        public int tra_rate;
        public int phy_str_rate;
        public int exp_rate;
        public int level;
        public int zhanyang;
        public int buliang;
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
            return this;
        }
    }
}
