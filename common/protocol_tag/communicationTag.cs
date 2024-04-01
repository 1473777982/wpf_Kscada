using common.helper;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace common.tag
{ 
    /// <summary>
    /// 从文件读取变量
    /// </summary>
    public class communicationTag 
    {
        public static communicationTag Current ;
        public static ObservableCollection<protocol> RunProtos;
        public static ObservableCollection<protocol_tag> Protos_Tags;
        public  ConcurrentDictionary<string, runTag> Dic_ranTags = new ConcurrentDictionary<string, runTag>();
        public  ConcurrentDictionary<string, runTag> Dic_ranTags_history = new ConcurrentDictionary<string, runTag>();
        public  ConcurrentDictionary<string, runTag> Dic_ranTags_logout = new ConcurrentDictionary<string, runTag>();
        public static ConcurrentDictionary<string, taginfo> Dic_taginfos = new ConcurrentDictionary<string, taginfo>();
        //public ConcurrentDictionary<protocol, ConcurrentDictionary<Device, ConcurrentDictionary<string, taginfo>>> tags;
        //public ConcurrentDictionary<protocol, ConcurrentDictionary<Device, ConcurrentDictionary<string, runTag>>> runTags;
        public void Load()
        {
            Current = this;
        }
        public  void LoadFromFile()
        {
            try
            {
                Protos_Tags = JsonConvert.DeserializeObject<ObservableCollection<protocol_tag>>(jsonFile.GetJsonFile(@"vars_json.json"));
                logHepler.addLog_common("communicationTag LoadFromFile loaded");
            }
            catch (Exception ex)
            {
                logHepler.addLog_common("communicationTag " + ex.Message);
            }
            
        }

        public IrunTag Get_runTag(string name)
        {
            runTag result = null;
            if (this.Dic_ranTags.TryGetValue(name, out result))
            {
                return result;
            }
            return null;
        }
        //  
        public  void CreateRuntimeTag()
        {
            RunProtos = new ObservableCollection<protocol>();
            
            
            if (Protos_Tags.Count>0)
            {
                try
                {  //将jsonchannel  同步到 runtag
                    for (int i = 0; i < Protos_Tags.Count; i++)
                    {
                        RunProtos.Add(new protocol()
                        {
                            ProtoName = Protos_Tags[i].ProtoName,
                            protoID = Protos_Tags[i].protoID,
                            Devices = new List<Device>()
                        });
                        for (int j = 0; j < Protos_Tags[i].Devices.Count; j++)
                        {
                            RunProtos[i].Devices.Add(new Device()
                            {
                                protoType = Protos_Tags[i].Devices[j].protoType,
                                Enable = Protos_Tags[i].Devices[j].Enable,
                                DeviceName = Protos_Tags[i].Devices[j].DeviceName,
                                IPAdress = Protos_Tags[i].Devices[j].IPAdress,
                                TCPPort = Protos_Tags[i].Devices[j].TCPPort,
                                Timeout = Protos_Tags[i].Devices[j].Timeout,
                                CycTime = Protos_Tags[i].Devices[j].CycTime,
                                CpuType = Protos_Tags[i].Devices[j].CpuType,
                                NetID = Protos_Tags[i].Devices[j].NetID,
                                ByteOrder = Protos_Tags[i].Devices[j].ByteOrder,
                                opcMonitor = Protos_Tags[i].Devices[j].opcMonitor,
                            });
                            for (int k = 0; k < Protos_Tags[i].Devices[j].infotags.Count; k++)
                            {
                                var tag = new runTag()
                                {
                                    protoType = Protos_Tags[i].Devices[j].infotags[k].protoType,
                                    DeviceName = Protos_Tags[i].Devices[j].infotags[k].DeviceName,
                                    name = Protos_Tags[i].Devices[j].infotags[k].name,
                                    tagType = Protos_Tags[i].Devices[j].infotags[k].tagType,
                                    address = Protos_Tags[i].Devices[j].infotags[k].address,
                                    defaultvalue = Protos_Tags[i].Devices[j].infotags[k].defaultvalue,
                                    limithigh = Protos_Tags[i].Devices[j].infotags[k].limithigh,
                                    limitlow = Protos_Tags[i].Devices[j].infotags[k].limitlow,
                                    alarmType = Protos_Tags[i].Devices[j].infotags[k].alarmType,
                                    alarmhigh = Protos_Tags[i].Devices[j].infotags[k].alarmhigh,
                                    alarmlow = Protos_Tags[i].Devices[j].infotags[k].alarmlow,
                                    archive = Protos_Tags[i].Devices[j].infotags[k].archive,
                                    logout = Protos_Tags[i].Devices[j].infotags[k].logout,
                                    description = Protos_Tags[i].Devices[j].infotags[k].description,
                                    unit = Protos_Tags[i].Devices[j].infotags[k].unit,
                                    //handle = Tags[i].Devices[j].infotags[k].handle,s
                                    flagState = Protos_Tags[i].Devices[j].infotags[k].flagState
                                };
                                RunProtos[i].Devices[j].runTags.Add(tag);
                                Dic_ranTags.TryAdd(tag.name, tag);
                                Dic_taginfos.TryAdd(tag.name, Protos_Tags[i].Devices[j].infotags[k]);
                                if (tag.logout)
                                {
                                    Dic_ranTags_logout.TryAdd(tag.name, tag);
                                }
                                if (tag.archive)
                                {
                                    Dic_ranTags_history.TryAdd(tag.name, tag);
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    logHepler.addLog_common("communicationTag " + ex.Message);
                }
              
            }
            else
            {
                logHepler.addLog_common("communicationTag CreateRuntimeTag 加载错误");
            }

        }

        public static void SaveToFile()
        {
            try
            {
                var js = JsonConvert.SerializeObject(Protos_Tags);
                jsonFile.WriteJsonFile(@"vars_json.json", js);
            }
            catch (Exception ex)
            {
                logHepler.addLog_common("communicationTag  SaveToFile " + ex.Message);
            }

        }
    }
}
