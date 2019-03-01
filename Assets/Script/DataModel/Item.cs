﻿using System.Collections.Generic;
using System.Collections;
namespace DataModel
{
    public class Item
    {
        public int id;
        public int type;
        public int pos;
        public int owner;
        public float energy;
        public float x;
        public float y;
        public ArrayList type_list = new ArrayList();
        public  Item UnPack(MsgPack.MessagePackObject net_info){
            MsgPack.MessagePackObjectDictionary item_dic= net_info.AsDictionary();
            MsgPack.MessagePackObject temp;
            item_dic.TryGetValue("id",out temp);
            id = temp.AsInt32();
            item_dic.TryGetValue("type",out temp);
            type = temp.AsInt32();
            item_dic.TryGetValue("pos",out temp);
            pos = temp.AsInt32();
            item_dic.TryGetValue("owner",out temp);
            owner = temp.AsInt32();
            item_dic.TryGetValue("energy",out temp);
            energy = (float)temp.AsDouble();
            item_dic.TryGetValue("x",out temp);
            x = (float)temp.AsDouble();
            item_dic.TryGetValue("y",out temp);
            y = (float)temp.AsDouble();
            return this;
        }
    }
}
