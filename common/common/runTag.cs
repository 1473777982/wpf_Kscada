using common.helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public class runTag:baseRunTag
    {

        // public runTag(taginfo chinfo, protocol plugin)
        //          : base(chinfo, plugin)
        //      {
        //      }



        private object m_writeValue = null;
        public object WriteValue
        {
            get { return m_writeValue; }
        }
        /// <summary>
        /// 归属组
        /// </summary>
        IConnection group = null;
        /// <summary>
        /// 设置归属组
        /// </summary>
        /// <param name="g"></param>
        public void SetConnectGroup(IConnection g)
        {
            group = g;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        public override object value
        {
            get
            {
                return base.value;
            }
            set
            {
                if (string.IsNullOrEmpty(address))
                {
                    base.value = value;
                }
                else if (flagState)
                {
                    try
                    {
                        if (group != null)
                        {
                            value = ConvertFunction(value);//检查数据类型并转换  
                            m_writeValue = value;
                            group.WriteValue(this);
                        }
                    }
                    catch (Exception ex)
                    {
                        logHepler.addLog_common("runTag" + ex.Message);
                    }
                }
            }
        }



    }
}
