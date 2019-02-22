using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BaseData
{

    public class PhoneDevice
    {
        private string type ;
        public Notch notch ;
        public static Dictionary<string,Notch>  NotchInfo = new Dictionary<string, Notch>{
            {"iPhone10,3",new Notch(61,47)},
            {"iPhone10,6",new Notch(61,47)},
            {"iPhone11,8",new Notch(61,47)},
            {"iPhone11,2",new Notch(61,47)},
            {"iPhone11,6",new Notch(67,52)}
        };
        public PhoneDevice(){
            type = UnityEngine.SystemInfo.deviceModel.ToString();
            NotchInfo.TryGetValue(type,out notch);
            if(notch==null){
                notch = new Notch(0,0);
            }
        }
    }
}
