using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace NsMvVisionControlSDK
{
    class CMvVisionControlSDK
    {
        ////////////////////////////////////Constant definition///////////////////////////
        // Definition of error code
        // Definition of correct code
        public const Int32 MV_OK = unchecked((Int32)0x00000000);     ///< Successed, no error

        // Definition of General Error Codes: Range from 0x80000000-0x800000FF
        public const Int32 MV_E_HANDLE = unchecked((Int32)0x80000000);     ///< Error or invalid handle
        public const Int32 MV_E_SUPPORT = unchecked((Int32)0x80000001);     ///< Not supported function
        public const Int32 MV_E_BUFOVER = unchecked((Int32)0x80000002);     ///< Cache is full
        public const Int32 MV_E_CALLORDER = unchecked((Int32)0x80000003);     ///< Function calling order error
        public const Int32 MV_E_PARAMETER = unchecked((Int32)0x80000004);     ///< Incorrect parameter
        public const Int32 MV_E_RESOURCE = unchecked((Int32)0x80000006);     ///< Applying resource failed
        public const Int32 MV_E_NODATA = unchecked((Int32)0x80000007);     ///< No data
        public const Int32 MV_E_PRECONDITION = unchecked((Int32)0x80000008);     ///< Precondition error, or running environment changed
        public const Int32 MV_E_VERSION = unchecked((Int32)0x80000009);     ///< Version mismatches
        public const Int32 MV_E_NOENOUGH_BUF = unchecked((Int32)0x8000000A);     ///< Insufficient memory
        public const Int32 MV_E_UNKNOW = unchecked((Int32)0x800000FF);     ///< Unknown error

        //  GenICam Series Error Codes: Range from 0x80000100 to 0x800001FF
        public const Int32 MV_E_GC_GENERIC = unchecked((Int32)0x80000100);     ///< General error
        public const Int32 MV_E_GC_ARGUMENT = unchecked((Int32)0x80000101);     ///< Illegal parameters
        public const Int32 MV_E_GC_RANGE = unchecked((Int32)0x80000102);     ///< The value is out of range
        public const Int32 MV_E_GC_PROPERTY = unchecked((Int32)0x80000103);     ///< Property
        public const Int32 MV_E_GC_RUNTIME = unchecked((Int32)0x80000104);     ///< Running environment error
        public const Int32 MV_E_GC_LOGICAL = unchecked((Int32)0x80000105);     ///< Logical error
        public const Int32 MV_E_GC_ACCESS = unchecked((Int32)0x80000106);     ///< Node accessing condition error
        public const Int32 MV_E_GC_TIMEOUT = unchecked((Int32)0x80000107);     ///< Timeout
        public const Int32 MV_E_GC_DYNAMICCAST = unchecked((Int32)0x80000108);     ///< Transformation exception
        public const Int32 MV_E_GC_UNKNOW = unchecked((Int32)0x800001FF);     ///< GenICam unknown error

        // GigE_STATUS Error Codes: Range from 0x80000200 to 0x800002FF
        public const Int32 MV_E_NOT_IMPLEMENTED = unchecked((Int32)0x80000200);     ///< The command is not supported by device
        public const Int32 MV_E_INVALID_ADDRESS = unchecked((Int32)0x80000201);     ///< The target address being accessed does not exist
        public const Int32 MV_E_WRITE_PROTECT = unchecked((Int32)0x80000202);     ///< The target address is not writable
        public const Int32 MV_E_ACCESS_DENIED = unchecked((Int32)0x80000203);     ///< No permission
        public const Int32 MV_E_BUSY = unchecked((Int32)0x80000204);     ///< Device is busy, or network disconnected
        public const Int32 MV_E_PACKET = unchecked((Int32)0x80000205);     ///< Network data packet error
        public const Int32 MV_E_NETER = unchecked((Int32)0x80000206);     ///< Network error

        // USB_STATUS Error Codes: Range from 0x80000300 to 0x800003FF
        public const Int32 MV_E_USB_READ = unchecked((Int32)0x80000300);     ///< Reading USB error
        public const Int32 MV_E_USB_WRITE = unchecked((Int32)0x80000301);     ///< Writing USB error
        public const Int32 MV_E_USB_DEVICE = unchecked((Int32)0x80000302);     ///< Device exception
        public const Int32 MV_E_USB_GENICAM = unchecked((Int32)0x80000303);     ///< GenICam error
        public const Int32 MV_E_USB_BANDWIDTH = unchecked((Int32)0x80000304);     ///< Insufficient bandwidth, this error code is newly added
        public const Int32 MV_E_USB_UNKNOW = unchecked((Int32)0x800003FF);     ///< USB unknown error

        //Upgrade Error Codes: Range from 0x80000400 to 0x800004FF
        public const Int32 MV_E_UPG_FILE_MISMATCH = unchecked((Int32)0x80000400);     ///< Firmware mismatches
        public const Int32 MV_E_UPG_LANGUSGE_MISMATCH = unchecked((Int32)0x80000401);     ///< Firmware language mismatches
        public const Int32 MV_E_UPG_CONFLICT = unchecked((Int32)0x80000402);     ///< Upgrading conflicted (repeated upgrading requests during device upgrade)
        public const Int32 MV_E_UPG_INNER_ERR = unchecked((Int32)0x80000403);     ///< Camera internal error during upgrade
        public const Int32 MV_E_UPG_UNKNOW = unchecked((Int32)0x800004FF);     ///< Unknown error during upgrade

        public const Int32 MV_SC_MAX_DEVICE_NUM = 64;
        public const Int32 MV_SC_EXCEPTION_DEV_DISCONNECT = 0x8001;
        public const Int32 MV_SC_MAX_XML_STRVALUE_LEN = 64;
        public const Int32 MV_SC_MAX_BCR_CODE_LEN = 256;
        public const Int32 MAX_SC_BCR_COUNT = 32;
        public const Int32 MV_SC_MAX_RESULT_SIZE = (1024 * 16);
        public const Int32 MV_IP_CFG_STATIC = 0x5000000;
        public const Int32 MV_IP_CFG_DHCP = 0x6000000;
        public const Int32 MV_IP_CFG_LLA = 0x4000000;
        public const Int32 MV_NET_TRANS_DRIVER = 0x1;
        public const Int32 MV_NET_TRANS_SOCKET = 0x2;
        public const Int32 MV_MATCH_TYPE_NET_DETECT = 0x1;
        public const Int32 MV_MAX_XML_SYMBOLIC_NUM = 64;
        public const Int32 CommuPtlSmartSDK = 1;
        public const Int32 CommuPtlTcpIP = 2;
        public const Int32 CommuPtlSerial = 3;

        // Baudrate Macro Definition
        public enum MV_VC_BAUDRATE
        {
            MV_VC_BAUD_RATE_UNKNOWN = -1,
            MV_VC_BAUD_RATE_110     = 110,
            MV_VC_BAUD_RATE_300     = 300,
            MV_VC_BAUD_RATE_600     = 600,
            MV_VC_BAUD_RATE_1200    = 1200,
            MV_VC_BAUD_RATE_2400    = 2400,
            MV_VC_BAUD_RATE_4800    = 4800,
            MV_VC_BAUD_RATE_9600    = 9600,
            MV_VC_BAUD_RATE_14400   = 14400,
            MV_VC_BAUD_RATE_19200   = 19200,
            MV_VC_BAUD_RATE_38400   = 38400,
            MV_VC_BAUD_RATE_56000   = 56000,
            MV_VC_BAUD_RATE_57600   = 57600,
            MV_VC_BAUD_RATE_115200  = 115200,
            MV_VC_BAUD_RATE_128000  = 128000,
            MV_VC_BAUD_RATE_256000  = 256000,
        };

        // Databits Macro Definition
        public enum MV_VC_DATABITS
        {
            MV_VC_DATA_BITS_UNKNOWN = -1,
            MV_VC_DATA_BITS_5       =  5,
            MV_VC_DATA_BITS_6       =  6,
            MV_VC_DATA_BITS_7       =  7,
            MV_VC_DATA_BITS_8       =  8,
        };

        // Parity Scheme definition
        public enum MV_VC_PARITY_SCHEME
        {
            MV_VC_PARITY_SCHEME_UNKNOWN = -1,
            MV_VC_PARITY_SCHEME_NONE    = 0,
            MV_VC_PARITY_SCHEME_ODD     = 1,
            MV_VC_PARITY_SCHEME_EVEN    = 2,
            MV_VC_PARITY_SCHEME_MARK    = 3,
            MV_VC_PARITY_SCHEME_SPACE   = 4,
        };

        // Stop bits definition
        public enum MV_VC_STOPBITS
        {
            MV_VC_STOP_BITS_UNKNOWN = -1,
            MV_VC_STOP_BITS_1       = 0,
            MV_VC_STOP_BITS_1_5     = 1,
            MV_VC_STOP_BITS_2       = 2,
        };

        public enum MV_VC_PATTERN
        {
            MV_VC_PATTERN_ALL       = 0x00,
            MV_VC_PATTERN_I_O       = 0x01,
            MV_VC_PATTERN_SERIAL    = 0x02,
            MV_VC_PATTERN_LIGHT     = 0x03,
        };

        // Port definition
        public enum MV_VC_PORT_NUMBER
        {
            MV_VC_PORT_1 = 0x1,
            MV_VC_PORT_2 = 0x2,
            MV_VC_PORT_3 = 0x4,
            MV_VC_PORT_4 = 0x8,
        };

        // Rising and Falling Edge Definition
        public enum MV_VC_EDGE_TYPE
        {
            MV_VC_EDGE_FALLING    = 0x02,
            MV_VC_EDGE_RISING     = 0x01,
        };

        // Enable Start and End Definition
        public enum MV_VC_ENABLE_TYPE
        {
            MV_VC_ENABLE_START    = 0x00,
            MV_VC_ENABLE_END      = 0x01,
        };

        // High and Low Level definition
        public enum MV_VC_LEVEL
        {
            MV_VC_LEVEL_HIGH  = 0x0001,
            MV_VC_LEVEL_LOW   = 0x0000,
        };


        public struct MV_VC_SERIAL
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public String strComName;
            public MV_VC_BAUDRATE enBaudRate;
            public MV_VC_DATABITS enDataBits;
            public MV_VC_PARITY_SCHEME enParityScheme;
            public MV_VC_STOPBITS enStopBits;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public UInt32[] nReserved;
        };

        public struct MV_VC_PATTERN_SELECT
        {
            public Byte nPatternSelect;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public UInt32[] nReserved;
        };

        public struct MV_VC_INPUT
        {
            public Byte nPortNumber;
            public Byte nEdgeType;
            public Byte nAssociatedOutPort;
            public UInt32 nDelayTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public UInt32[] nReserved;
        };

        public struct MV_VC_PWM_PARAM
        {
            public Byte nPortNumber;
            public UInt16 nPulseWidth;    // Pulse Width(ms)
            public UInt16 nCycleTime;     // Cycle Time(ms)
            public UInt32 nDurationTime;  // Duration Time(ms)
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public UInt32[] nReserved;
        };

        public struct MV_VC_PWM_ENABLE
        {
            public Byte nPortNumber;
            public Byte nType;          // 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public UInt32[] nReserved;
        };

        public struct MV_VC_SINGLE_PARAM
        {
            public Byte nPortNumber;
            public UInt16 nValidLevel;
            public UInt16 nDurationTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public UInt32[] nReserved;
        };

        public struct MV_VC_SINGLE_ENABLE
        {
            public Byte nPortNumber;
            public Byte nType;          // 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public UInt32[] nReserved;
        };

        public struct MV_VC_LIGHT_PARAM
        {
            public Byte nPortNumber;
            public UInt16 nLightValue;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public UInt32[] nReserved;
        };

        public struct MV_VC_LIGHT_ENABLE
        {
            public Byte nPortNumber;
            public Byte nType;          // 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public UInt32[] nReserved;
        };

        public struct MV_VC_READ_PARAM
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public Byte[] pData;
            public UInt32 nTimeOut;
            public UInt32 nReadLen;
        }


        [DllImport("MvVisionControl.dll", EntryPoint = "MV_VC_Open")]
        private static extern Int32 MV_VC_Open(ref MV_VC_SERIAL pstSerial);

        [DllImport("MvVisionControl.dll", EntryPoint = "MV_VC_PATTERN_Select")]
        private static extern Int32 MV_VC_PATTERN_Select(ref MV_VC_PATTERN_SELECT pstPatternSelect);

        [DllImport("MvVisionControl.dll", EntryPoint = "MV_VC_INPUT_Command")]
        private static extern Int32 MV_VC_INPUT_Command(ref MV_VC_INPUT pstInput);

        [DllImport("MvVisionControl.dll", EntryPoint = "MV_VC_PWM_SetParam")]
        private static extern Int32 MV_VC_PWM_SetParam(ref MV_VC_PWM_PARAM pstPwmParam);

        [DllImport("MvVisionControl.dll", EntryPoint = "MV_VC_PWM_Enable")]
        private static extern Int32 MV_VC_PWM_Enable(ref MV_VC_PWM_ENABLE pstPwmEnable);

        [DllImport("MvVisionControl.dll", EntryPoint = "MV_VC_SINGLE_SetParam")]
        private static extern Int32 MV_VC_SINGLE_SetParam(ref MV_VC_SINGLE_PARAM pstSingleParam);

        [DllImport("MvVisionControl.dll", EntryPoint = "MV_VC_SINGLE_Enable")]
        private static extern Int32 MV_VC_SINGLE_Enable(ref MV_VC_SINGLE_ENABLE pstSingleEnable);

        [DllImport("MvVisionControl.dll", EntryPoint = "MV_VC_LIGHT_SetParam")]
        private static extern Int32 MV_VC_LIGHT_SetParam(ref MV_VC_LIGHT_PARAM pstLightParam);

        [DllImport("MvVisionControl.dll", EntryPoint = "MV_VC_LIGHT_Enable")]
        private static extern Int32 MV_VC_LIGHT_Enable(ref MV_VC_LIGHT_ENABLE pstLightEnable);

        [DllImport("MvVisionControl.dll", EntryPoint = "MV_VC_Close")]
        private static extern void MV_VC_Close();

        [DllImport("MvVisionControl.dll", EntryPoint = "MV_VC_SetDebouncing")]
        private static extern Int32 MV_VC_SetDebouncing(UInt32 nDebouncing);

        [DllImport("MvVisionControl.dll", EntryPoint = "MV_VC_Write")]
        private static extern Int32 MV_VC_Write(Byte[] pData, UInt32 nDataLen);

        [DllImport("MvVisionControl.dll", EntryPoint = "MV_VC_Read")]
        private static extern Int32 MV_VC_Read(ref MV_VC_READ_PARAM pstReadParam);


        public static Int32 MV_VC_Open_CS(ref MV_VC_SERIAL pstSerial)
        {
            return MV_VC_Open(ref pstSerial);
        }

        public static Int32 MV_VC_PATTERN_Select_CS(ref MV_VC_PATTERN_SELECT pstPatternSelect)
        {
            return MV_VC_PATTERN_Select(ref pstPatternSelect);
        }

        public static Int32 MV_VC_INPUT_Command_CS(ref MV_VC_INPUT pstInput)
        {
            return MV_VC_INPUT_Command(ref pstInput);
        }

        public static Int32 MV_VC_PWM_SetParam_CS(ref MV_VC_PWM_PARAM pstPwmParam)
        {
            return MV_VC_PWM_SetParam(ref pstPwmParam);
        }
        public static Int32 MV_VC_PWM_Enable_CS(ref MV_VC_PWM_ENABLE pstPwmEnable)
        {
            return MV_VC_PWM_Enable(ref pstPwmEnable);
        }
        public static Int32 MV_VC_SINGLE_SetParam_CS(ref MV_VC_SINGLE_PARAM pstSingleParam)
        {
            return MV_VC_SINGLE_SetParam(ref pstSingleParam);
        }
        public static Int32 MV_VC_SINGLE_Enable_CS(ref MV_VC_SINGLE_ENABLE pstSingleEnable)
        {
            return MV_VC_SINGLE_Enable(ref pstSingleEnable);
        }
        public static Int32 MV_VC_LIGHT_SetParam_CS(ref MV_VC_LIGHT_PARAM pstLightParam)
        {
            return MV_VC_LIGHT_SetParam(ref pstLightParam);
        }
        public static Int32 MV_VC_LIGHT_Enable_CS(ref MV_VC_LIGHT_ENABLE pstLightEnable)
        {
            return MV_VC_LIGHT_Enable(ref pstLightEnable);
        }
        public static void MV_VC_Close_CS()
        {
            MV_VC_Close();
        }
        public static Int32 MV_VC_SetDebouncing_CS(UInt32 nDebouncing)
        {
            return MV_VC_SetDebouncing(nDebouncing);
        }
        public static Int32 MV_VC_Write_CS(Byte[] pData, UInt32 nDataLen)
        {
            return MV_VC_Write(pData, nDataLen);
        }
        public static Int32 MV_VC_Read_CS(ref MV_VC_READ_PARAM pstReadParam)
        {
            return MV_VC_Read(ref pstReadParam);
        }

    }
}
