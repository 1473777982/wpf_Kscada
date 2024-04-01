using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public enum DataType : int
    {
        BOOL = 0,
        BYTE = 1,
        WORD = 2,
        INT = 3,
        UINT = 4,
        DINT = 5,
        REAL = 6,
        LREAL = 7,
        STRING = 8
    }
    public enum AlarmType : int
    {
        NoAlarm = 0,
        OnAlarm = 1,
        OffAlarm = 2,
        HighAlarm = 3,
        LowAlarm = 4,
    }
    public enum ProtoType : int
    {
        ADS = 0,
        INOVANCE = 1,
        MODBUS = 2,
        OPCUA = 3,
    }
    public enum SoftElemType 
    {
        //AM600
        ELEM_QX = 0,     //QX元件
        ELEM_MW = 1,     //MW元件
        ELEM_X = 2,      //X元件(对应QX200~QX300)
        ELEM_Y = 3,      //Y元件(对应QX300~QX400)
        
        //区分数据类型
        AM_QX = 10,
        AM_QB = 11,
        AM_QW = 12,
        AM_QD = 13,
        AM_MX = 14,
        AM_MB = 15,
        AM_MW = 16,
        AM_MD = 17,
        AM_ML = 18,

        //H3U
        REGI_H3U_Y = 0x20,       //Y元件的定义	
        REGI_H3U_X = 0x21,      //X元件的定义							
        REGI_H3U_S = 0x22,      //S元件的定义				
        REGI_H3U_M = 0x23,      //M元件的定义							
        REGI_H3U_TB = 0x24,     //T位元件的定义				
        REGI_H3U_TW = 0x25,     //T字元件的定义				
        REGI_H3U_CB = 0x26,     //C位元件的定义				
        REGI_H3U_CW = 0x27,     //C字元件的定义				
        REGI_H3U_DW = 0x28,     //D字元件的定义				
        REGI_H3U_CW2 = 0x29,    //C双字元件的定义
        REGI_H3U_SM = 0x2a,     //SM
        REGI_H3U_SD = 0x2b,     //
        REGI_H3U_R = 0x2c,      //
                                //H5u
        REGI_H5U_Y = 0x30,       //Y元件的定义	
        REGI_H5U_X = 0x31,      //X元件的定义							
        REGI_H5U_S = 0x32,      //S元件的定义				
        REGI_H5U_M = 0x33,      //M元件的定义	
        REGI_H5U_B = 0x34,       //B元件的定义
        REGI_H5U_D = 0x35,       //D字元件的定义
        REGI_H5U_R = 0x36,       //R字元件的定义

    }

    public enum ByteOrder : int
    {
        ABCD = 0,
        CDAB = 1,
        BADC = 2,
        DCBA = 3,
    }
}
