using common.helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace common.tag
{
    public sealed class tag_manager
    {
        //public Dictionary<string, IrunTag> channels = new Dictionary<string, IrunTag>(StringComparer.OrdinalIgnoreCase);
        public static tag_manager Current = new tag_manager();

        public bool setTagValue(string name, object obj)
        {
            IrunTag cc;
            cc = communicationTag.Current.Get_runTag(name);
            if (cc != null)
            {
                try
                {
                    switch (cc.tagType)//根据变量类型执行相应写入操作
                    {
                        case DataType.BOOL:
                            if (Convert.ToInt32(obj) == 1)
                            {
                                obj = true;
                            }
                            else if (Convert.ToInt32(obj) == 0)
                            {
                                obj = false;
                            }
                            else
                            {
                                obj = Convert.ToBoolean(obj);
                            }
                            break;
                        case DataType.BYTE:
                            obj = Convert.ToByte(obj);
                            break;
                        case DataType.WORD:
                            obj = Convert.ToUInt16(obj);
                            break;
                        case DataType.UINT:
                            obj = Convert.ToUInt16(obj);
                            break;
                        case DataType.INT:
                            obj = Convert.ToInt16(obj);
                            break;
                        case DataType.DINT:
                            obj = Convert.ToInt32(obj);
                            break;
                        case DataType.REAL:
                            obj = Convert.ToSingle(obj);
                            break;
                        case DataType.LREAL:
                            obj = Convert.ToDouble(obj);
                            break;
                        case DataType.STRING:
                            obj = Convert.ToString(obj);
                            break;
                    }
                }
                catch (Exception)
                {
                    logHepler.addLog_common("setTag ERR" + name + obj);
                    return false;
                }
                cc.value = obj;
                return true;
            }
            else
            {
                return false;
            }

        }
        public object getTagValue(string name)
        {
            IrunTag cc;
            cc = communicationTag.Current.Get_runTag(name);
            if (cc != null)
            {
                try
                {
                   return cc.value;
                }
                catch (Exception)
                {                    
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
        public int getTagbit(string name, int bit)
        {
            IrunTag cc;
            cc = communicationTag.Current.Get_runTag(name);
            if (cc != null)
            {
                int temp = -1;
                if (cc.tagType == DataType.BYTE)
                {
                    try
                    {
                        if (cc.value != null)
                        {
                            temp = GetBit((byte)cc.value, bit);
                        } 
                    }
                    catch (Exception)
                    {
                        //temp = - 1;
                    }
                }  
                return  temp;
            }
            else
            {
                return -1;
            }

        }

        public bool pulseTagValue(string varName, int time)
        {
            try
            {
                setTagValue(varName, true);
                //隔设定的时间在执行代码 
                //或者System.Threading.Tasks.Task.Run()
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(time);
                    //要执行的代码段　　　　　　　
                    setTagValue(varName, false);
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool setTagbit(string name, int bit ,int value)
        {
            IrunTag cc;
            cc = communicationTag.Current.Get_runTag(name);
           
            if (cc != null)
            {
                var temp = Convert.ToByte(cc.value);
                try
                {
                    if (cc.tagType == DataType.BYTE)
                    {
                        var re =  SetBit(temp, bit, value);
                        cc.value = re;
                    }
                }
                catch (Exception)
                {
                    logHepler.addLog_common("setTag ERR" + name + value);
                    return false;
                }
                
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool pulseTagbit(string varName, int bit , int time)
        {
            try
            {
                setTagbit(varName, bit , 1);
                //隔设定的时间在执行代码 
                //或者System.Threading.Tasks.Task.Run()
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(time);
                    //要执行的代码段　　　　　　　
                    setTagbit(varName, bit, 0);
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static int GetBit(byte data, int bitIndex)
        {
            if (bitIndex < 0 || bitIndex > 7)
            {
                throw new ArgumentOutOfRangeException(nameof(bitIndex), "位索引必须在 0 到 7 之间");
            }

            return (data & (1 << bitIndex)) != 0 ? 1 : 0; // 如果指定位为 true，则返回 1，否则返回 0
        }
        public static byte SetBit(byte b, int bitPosition, int value)
        {
            if (value != 0 && value != 1)
            {
                throw new ArgumentException("Value can only be 0 or 1");
            }

            if (bitPosition < 0 || bitPosition > 7)
            {
                throw new ArgumentException("Bit position must be between 0 and 7");
            }

            if (value == 1)
            {
                return (byte)(b | (1 << bitPosition));
            }
            else
            {
                return (byte)(b & ~(1 << bitPosition));
            }
        }
        //public IrunTag Get_runTag(string name)
        //{
        //    IrunTag result = null;
        //    if (this.channels.TryGetValue(name, out result))
        //    {
        //        return result;
        //    }
        //    return null;
        //}
        //public void Load()
        //{
        //    var js = jsonFile.GetJsonFile(@"vars_json.json");
        //    List<string> list = JsonConvert.DeserializeObject<List<string>>(js);
        //    if (channels.Count > 0)
        //    {
        //        this.channels.Clear();
        //    }
        //    if (list.Count > 0)
        //    {
        //        foreach (var item in list)
        //        {
        //            try
        //            {
        //                IrunTag channel = new baseRunTag();
        //                if (channel != null)
        //                {
        //                    string name = item;
        //                    if (!this.channels.ContainsKey(name))
        //                    {
        //                        this.channels.Add(name, channel);
        //                    }
        //                }
        //            }
        //            catch
        //            {
        //            }
        //        }
        //    }
        //    // Current = this;
        //}
    }
}
