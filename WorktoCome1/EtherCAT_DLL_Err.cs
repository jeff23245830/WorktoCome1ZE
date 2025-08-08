using System;
using System.Text;
using System.Runtime.InteropServices;

namespace EtherCAT_DLL_Err
{
    public class CEtherCAT_DLL_Err
    {
        public const ushort ERR_ECAT_NO_ERROR = 0x0;
        public const ushort ERR_ECAT_HW_NO_INITIALIZE = 0x1;
        public const ushort ERR_ECAT_HW_PWM_INITIAL = 0x2;
        public const ushort ERR_ECAT_HW_HAS_INITIALIZED = 0x3;

        public const ushort ERR_ECAT_EEPROM_READ = 0x10;
        public const ushort ERR_ECAT_EEPROM_WRITE = 0x11;
        public const ushort ERR_ECAT_ENVIRONMENT_RECORD_DISABLE = 0x12;
        public const ushort ERR_ECAT_ENVIRONMENT_RECORD_NO_MATCH = 0x13;
        public const ushort ERR_ECAT_ENVIRONMENT_RECORD_FILE_OPEN = 0x14;
        public const ushort ERR_ECAT_ENVIRONMENT_RECORD_NOT_CREATE = 0x15;
        public const ushort ERR_ECAT_XML_FILE_PATH = 0x16;
        public const ushort ERR_ECAT_DEVICE_OPEN = 0x17;
        public const ushort ERR_ECAT_NO_DEVICE = 0x18;
        public const ushort ERR_ECAT_NO_MASTER = 0x19;
        public const ushort ERR_ECAT_NO_SLAVE = 0x1A;
        public const ushort ERR_ECAT_UNKNOWN_SLAVE = 0x1B;
        public const ushort ERR_ECAT_IST_CREATE = 0x1C;
        public const ushort ERR_ECAT_MASTER_CREATE = 0x1D;
        public const ushort ERR_ECAT_MASTER_REQUEST_STATE = 0x1E;
        public const ushort ERR_ECAT_MASTER_OPERATION_NOT_READY = 0x1F;
        public const ushort ERR_ECAT_DELTA_NODE_ID_ALIAS_READ = 0x20;
        public const ushort ERR_ECAT_MASTER_OPER_BUS = 0x20;
        
        public const ushort ERR_ECAT_MASTER_SIMULATION_XML = 0x26;
        public const ushort ERR_ECAT_MASTER_SIMULATION_NOT_FOUND = 0x27;
        public const ushort ERR_ECAT_MASTER_SIMULATION_FILE = 0x28;

        public const ushort ERR_ECAT_PIPELINE_CORE_TIMER_CREATE = 0x80;
        public const ushort ERR_ECAT_PIPELINE_CREATE = 0x81;
        public const ushort ERR_ECAT_COMMAND_ENQUEUE = 0x82;
        public const ushort ERR_ECAT_API_BUFFER_ENQUEUE = 0x83;

        public const ushort ERR_ECAT_NODE_ID = 0x100;
        public const ushort ERR_ECAT_SLOT_ID = 0x101;
        public const ushort ERR_ECAT_SDO_DOWNLOAD = 0x102;
        public const ushort ERR_ECAT_SDO_UPLOAD = 0x103;
        public const ushort ERR_ECAT_GET_PROCESS_DATA = 0x104;

        public const ushort ERR_ECAT_DIO_CHANNEL_INVALID = 0x200;
        public const ushort ERR_ECAT_ADDA_CHANNEL_INVALID = 0x201;
        public const ushort ERR_ECAT_MOTION_NOT_FINISHED = 0x202;
        public const ushort ERR_ECAT_SET_PULSE_MODE = 0x203;
        public const ushort ERR_ECAT_SET_SOFTLIMIT = 0x204;
        public const ushort ERR_ECAT_SET_POSITION = 0x205;
        public const ushort ERR_ECAT_GET_SPEED = 0x206;
        public const ushort ERR_ECAT_GET_MCDONE = 0x207;
        public const ushort ERR_ECAT_SET_HOME_CONFIG = 0x208;
        public const ushort ERR_ECAT_SET_P2P_CONFIG = 0x209;
        public const ushort ERR_ECAT_SET_PT_CONFIG = 0x20A;
        public const ushort ERR_ECAT_SET_PV_CONFIG = 0x20B;
        public const ushort ERR_ECAT_SET_CSP_CONFIG = 0x20C;
        public const ushort ERR_ECAT_SET_MULTI_AXES_LINE_CONFIG = 0x20D;
        public const ushort ERR_ECAT_SET_MULTI_AXES_ARC_CONFIG = 0x20E;

        public const ushort ERR_ECAT_MD1_SET_GEAR = 0x300;
        public const ushort ERR_ECAT_MD1_SET_P_CHANGE = 0x301;
        public const ushort ERR_ECAT_MD1_SET_V_CHANGE = 0x302;
        public const ushort ERR_ECAT_MD1_SET_SOFTLIMIT = 0x303;
        public const ushort ERR_ECAT_MD1_SET_SLD = 0x304;
        public const ushort ERR_ECAT_MD1_SET_HOME_CONFIG = 0x305;
        public const ushort ERR_ECAT_MD1_SET_P2P_CONFIG = 0x306;
        public const ushort ERR_ECAT_MD1_SET_V_MOVE_CONFIG = 0x307;
        public const ushort ERR_ECAT_MD1_SET_LINE_CONFIG = 0x308;
        public const ushort ERR_ECAT_MD1_SET_ARC_CONFIG = 0x309;

        public const ushort ERR_ECAT_PATH_NOT_SUPPORT = 0x400;
        public const ushort ERR_ECAT_PATH_AXIS_NUM = 0x401;
        public const ushort ERR_ECAT_PATH_AXIS_NO = 0x402;
        public const ushort ERR_ECAT_PATH_PARA = 0x403;
        public const ushort ERR_ECAT_PATH_ISR_FUNC_EVENT = 0x404;
        public const ushort ERR_ECAT_PATH_AXISNO_UNDER_GROUP = 0x405;

        public const ushort ERR_ESI_INITIAL = 0xF00;
        public const ushort ERR_ESI_OPEN_DEVICE = 0xF01;
        public const ushort ERR_ESI_CREATE_CANOPEN_OD_LIST = 0xF02;
        public const ushort ERR_ESI_NO_DATA_TYPE_INFO = 0xF03;
        public const ushort ERR_ESI_NO_OBJECT_INFO = 0xF04;
        public const ushort ERR_ESI_CREATE_SYNC_MANAGER = 0xF05;
        public const ushort ERR_ESI_CREATE_FMMU_CONTROL = 0xF06;
        public const ushort ERR_ESI_NO_PDO_CHANNEL = 0xF07;
        public const ushort ERR_ESI_NO_PDO_MAPPING = 0xF08;
        public const ushort ERR_ESI_PDO_MAPPING_INSERT = 0xF09;
        public const ushort ERR_ESI_PDO_MAPPING_DELETE = 0xF0A;
        public const ushort ERR_ESI_CREATE_DISTRIBUTED_CLOCK = 0xF0B;

        public const ushort ERR_ESI_ENI_INFORMATION_INITIAL = 0xFF0;
        public const ushort ERR_ESI_ENI_FILE_INITIAL = 0xFF1;
        public const ushort ERR_ESI_ENI_FILE_SAVE = 0xFF2;

        public const ushort ERR_ECAT_NO_SLAVE_FOUND = 0x1000;
        public const ushort ERR_ECAT_INITIAL_TIMEOUT = 0x1001;
        public const ushort ERR_ECAT_MODE_CHANGE_FAILED = 0x1002;
        public const ushort ERR_ECAT_SLAVE_ID = 0x1003;
        public const ushort ERR_ECAT_ALIAS_SLAVE_ID = 0x1004;

        public const ushort ERR_ECAT_NEED_INITIAL = 0x1100;
        public const ushort ERR_ECAT_NEED_RESET = 0x1101;
        public const ushort ERR_ECAT_NEED_CONNECT = 0x1102;
        public const ushort ERR_ECAT_NEED_DC_OP = 0x1103;
        public const ushort ERR_ECAT_NEED_RALM = 0x1104;
        public const ushort ERR_ECAT_NEED_SVON = 0x1105;
        public const ushort ERR_ECAT_NEED_HOMECONFIG = 0x1106;
        public const ushort ERR_ECAT_NEED_STOP = 0x1107;
        public const ushort ERR_ECAT_NEED_STOP_TOUCH_PROBE_HOMING = 0x1108;
        public const ushort ERR_ECAT_NEED_STOP_GANTRY = 0x1109;
        public const ushort ERR_ECAT_NEED_CLEAR_UNUSUAL = 0x110A;

        public const ushort ERR_ECAT_RING_BUFFER_FULL = 0x1200;
        public const ushort ERR_ECAT_API_PARAMETER = 0x1201;
        public const ushort ERR_ECAT_SLAVE_TYPE = 0x1202;
        public const ushort ERR_ECAT_TARGET_REACHED = 0x1203;
        public const ushort ERR_ECAT_MODE_NOT_SUPPORT = 0x1204;
        public const ushort ERR_ECAT_MOTION_TYPE = 0x1205;
        public const ushort ERR_ECAT_PDO_NOT_MAPPING = 0x1206;
        public const ushort ERR_ECAT_MODULE_REVISION = 0x1207;
        public const ushort ERR_ECAT_SPEED_CONTINUE_MODE = 0x1208;
        public const ushort ERR_ECAT_HOME_MODE = 0x1209;
        public const ushort ERR_ECAT_HOME_OFFSET = 0x120A;
        public const ushort ERR_ECAT_HOME_FIRST_SPEED = 0x120B;
        public const ushort ERR_ECAT_HOME_SECOND_SPEED = 0x120C;
        public const ushort ERR_ECAT_HOME_ACC = 0x120D;
        public const ushort ERR_ECAT_MRAM_INDEX = 0x120E;
        public const ushort ERR_ECAT_MRAM_INDEX_OUT_RANGE = 0x120F;
        public const ushort ERR_ECAT_REACH_SWLIMIT = 0x1210;
        public const ushort ERR_ECAT_NEED_ENABLE = 0x1211;
        public const ushort ERR_ECAT_DATA_SIZE_OVERFLOW = 0x1212;
        public const ushort ERR_ECAT_COOR_ALREADY_USED = 0x1213;
        public const ushort ERR_ECAT_COOR_AXIS_ALREADY_USED = 0x1214;
        public const ushort ERR_ECAT_MRAM_BMCGENERIC_ERROR = 0x1215;

        public const ushort ERR_ECAT_PDO_TX_FAILED = 0x1300;
        public const ushort ERR_ECAT_SDO_TIMEOUT = 0x1301;
        public const ushort ERR_ECAT_SDO_RETURN = 0x1302;
        public const ushort ERR_ECAT_PDO_RX_FAILED = 0x1303;
        public const ushort ERR_ECAT_MAILBOX = 0x1304;
        public const ushort ERR_ECAT_SDO_BUFFER_FULL = 0x1304;
        public const ushort ERR_ECAT_ESC_FAILED = 0x1304;
        
        public const ushort ERR_ECAT_GROUP_NUMBER = 0x1400;
        public const ushort ERR_ECAT_GROUP_ENABLE = 0x1401;
        public const ushort ERR_ECAT_GROUP_PAUSE = 0x1402;
        public const ushort ERR_ECAT_GROUP_SLAVE = 0x1403;
        public const ushort ERR_ECAT_GROUP_MODE = 0x1404;
        public const ushort ERR_ECAT_GROUP_ALREADY_USED = 0x1405;
        public const ushort ERR_ECAT_GROUP_TYPE = 0x1406;
        public const ushort ERR_ECAT_GROUP_SVON = 0x1407;
        public const ushort ERR_ECAT_GROUP_ALM = 0x1408;
        public const ushort ERR_ECAT_GROUP_DATA_BUFFER = 0x1409;
        public const ushort ERR_ECAT_GROUP_TIMEOUT = 0x140A;

        public const ushort ERR_ECAT_SERVO_PARA_EMPTY = 0x1500;
        public const ushort ERR_ECAT_SERVO_PARA_RO = 0x1501;
        public const ushort ERR_ECAT_SERVO_COMPARE_ENABLE = 0x1502;

        public const ushort ERR_ECAT_RECORD_TYPE = 0x1600;
        
        public const ushort ERR_ECAT_MPG_ENABLE = 0x1700;
        public const ushort ERR_ECAT_MPG_CONFIG = 0x1701;

        public const ushort ERR_ECAT_PATH_DLL_ERROR_CODE = 0x8000;
        public const ushort ERR_PATH_BOARD_INIIT = 0x8001;
        public const ushort ERR_PATH_BOARD_NUMBER = 0x8002;
        public const ushort ERR_PATH_INITIAL_BOARD_NUMBER = 0x8003;
        public const ushort ERR_PATH_BASE_ADDR_ERROR = 0x8004;
        public const ushort ERR_PATH_BASE_ADDR_CONFLICT = 0x8005;
        public const ushort ERR_PATH_DUPLICATE_BOARD_SETTING = 0x8006;
        public const ushort ERR_PATH_DUPLICATE_IRQ_SETTING = 0x8007;
        public const ushort ERR_PATH_ENC_NUMBER = 0x8008;
        public const ushort ERR_PATH_MODULE_NUMBER = 0x8009;
        public const ushort ERR_PATH_TIMER_VALUE = 0x800A;
        public const ushort ERR_PATH_ENABLE = 0x800B;
        public const ushort ERR_PATH_RANGE = 0x800C;
        public const ushort ERR_PATH_MEMORY_ALLOC = 0x800D;
        public const ushort ERR_PATH_MOTION_BUSY = 0x800E;
        public const ushort ERR_PATH_MOTION_NO_START = 0x800F;
        public const ushort ERR_PATH_WRONG_SPEED = 0x8010;
        public const ushort ERR_PATH_WRONG_ACCTIME = 0x8011;
        public const ushort ERR_PATH_IO_ALAM = 0x8012;
        public const ushort ERR_PATH_OPEN_FILE_FAILED = 0x8013;
        public const ushort ERR_PATH_MEMORY_ALLOCATE = 0x8014;
        public const ushort ERR_PATH_MEMORY_NOT_FREE = 0x8015;
        public const ushort ERR_PATH_OUTPUT_FILE_NOT_CLOSE = 0x8016;
        public const ushort ERR_PATH_MOVE_AXIS_NOT_MATCH = 0x8017;
        public const ushort ERR_PATH_PITCH_ZERO = 0x8018;
        public const ushort ERR_PATH_TIMEOUT = 0x8019;
        public const ushort ERR_PATH_PCI_BIOS_NOT_EXIST = 0x801A;
        public const ushort ERR_PATH_BUFFER_FULL = 0x801B;
        public const ushort ERR_PATH_ERROR = 0x801C;
        public const ushort ERR_PATH_NO_SUPPRT_MODE = 0x801D;
        
        public const ushort ERR_PATH_AXIS_CORRELATION = 0x801F;

        public const ushort ERR_PATH_SD_STOP_ON = 0x8021;
        public const ushort ERR_PATH_VELOCITY_CHANGE_SUPER = 0x8022;
        public const ushort ERR_PATH_DIR = 0x8023;
        
        public const ushort ERR_PATH_POS_CHANGE_MODE = 0x8028;
        public const ushort ERR_PATH_BUFFER_LENGTH = 0x8029;
        public const ushort ERR_PATH_2SEG_DISTANCE = 0x802A;
        public const ushort ERR_PATH_ARC_CENTER_POSITION = 0x802B;
        public const ushort ERR_PATH_ARC_END_POSITION = 0x802C;
        public const ushort ERR_PATH_ARC_ANGLE_CALC = 0x802D;
        public const ushort ERR_PATH_ARC_RADIUS_CALC = 0x802E;
        public const ushort ERR_PATH_GEAR_SETTING = 0x802F;
        public const ushort ERR_PATH_CAM_TABLE = 0x8030;
        public const ushort ERR_PATH_AXES_NUMBER = 0x8031;
        public const ushort ERR_PATH_SPIRAL_END_POSITION = 0x8032;
        public const ushort ERR_PATH_SPEED_MODE_SLAVE = 0x8033;
        public const ushort ERR_PATH_SPEED_MODE_SET_SLAVE = 0x8034;
        public const ushort ERR_PATH_VELOCITY_CHANGE = 0x8035;
        public const ushort ERR_PATH_BACKLASH_STEP = 0x8036;
        public const ushort ERR_PATH_BACKLASH_STATUS = 0x8037;
        public const ushort ERR_PATH_DIST_OVER = 0x8038;
       
        public const ushort ERR_PATH_PVT_DATA_ERROR = 0x803C;
        public const ushort ERR_PATH_PVT_MODE_ERROR = 0x803D;
        public const ushort ERR_PATH_PVT_CHANNEL = 0x803E;
        public const ushort ERR_PATH_TM_DATA_AXIS = 0x803F;
        public const ushort ERR_PATH_TM_CYCLE_DATA = 0x8040;
        public const ushort ERR_PATH_TM_DATA_SIZE = 0x8041;
        public const ushort ERR_PATH_AMF_NUM = 0x8042;
        public const ushort ERR_PATH_TM_IN_ACTIVE = 0x8043;
        public const ushort ERR_PATH_TM_BUFFER_FULL = 0x8044;
        
        public const ushort ERR_PATH_ECAM_DATA_NUM = 0x8045;
        public const ushort ERR_PATH_ECAM_LENGTH = 0x8046;
        public const ushort ERR_PATH_ECAM_INDEX = 0x8047;
        public const ushort ERR_PATH_ECAM_DISENGAGE = 0x8048;
        public const ushort ERR_PATH_ECAM_VELOCITY_PERCENT = 0x8049;
        public const ushort ERR_PATH_ECAM_CONSTRUCT_MODE = 0x804A;
        public const ushort ERR_PATH_ECAM_DISENGAGE_PARA = 0x804B;
        public const ushort ERR_PATH_ECAM_CUTLENGTH = 0x804C;
        public const ushort ERR_PATH_ECAM_REGION = 0x804D;
        public const ushort ERR_PATH_ECAM_VELOCITY_SCURVE = 0x804E;
        public const ushort ERR_PATH_ECAM_GEARNUM = 0x804F;
        public const ushort ERR_PATH_ECAM_KNIFENUM = 0x8050;
        public const ushort ERR_PATH_ECAM_MASTER_SOURCE = 0x8051;
        public const ushort ERR_PATH_ECAM_HOLDING_AREA = 0x8052;
        public const ushort ERR_PATH_ECAM_PARAMETERS = 0x8053;
        public const ushort ERR_PATH_ECAM_MASK_DISTANCE = 0x8054;
        public const ushort ERR_PATH_ECAM_COMPENSATE_POSITION = 0x8055;
        public const ushort ERR_PATH_ECAM_ENGAGED = 0x8056;
        public const ushort ERR_PATH_ECAM_TABLE_EMPTY = 0x8057;
        public const ushort ERR_PATH_ECAM_COMPENSATE_RANGE = 0x8058;
        
        public const ushort ERR_PATH_TP_CIRCLE_DATA = 0x805A;
        
        public const ushort ERR_PATH_AMF_ENABLE = 0x805D;
        public const ushort ERR_PATH_AMF_TIME = 0x805E;
        public const ushort ERR_PATH_SPEED_RATIO = 0x805F;
        public const ushort ERR_PATH_MOTION_DONE = 0x8060;
        public const ushort ERR_PATH_TM_SD_STOP = 0x8061;
        public const ushort ERR_PATH_TM_FEEDRATE_OVERWRITE = 0x8062;
        public const ushort ERR_PATH_IO_NUMBER = 0x8063;
        public const ushort ERR_PATH_PROFILE_MODE_ENABLE = 0x8064;
        public const ushort ERR_PATH_NOT_TM_CYCLE_MODE = 0x8065;
        public const ushort ERR_PATH_NONE_ANALYSIS = 0x8066;
        public const ushort ERR_PATH_OVER_TABLE_NUM = 0x8067;
        public const ushort ERR_PATH_ACCDEC_LIMIT = 0x8068;
        public const ushort ERR_PATH_SOFTLIMIT = 0x8069;
        public const ushort ERR_PATH_TM_ARCBIT = 0x806A;
        public const ushort ERR_PATH_TM_COLLINEAR = 0x806B;
        public const ushort ERR_PATH_TM_ARC_ANOTHER_AXIS_POS = 0x806C;
        public const ushort ERR_PATH_TM_PROFILE_PARAMETERS = 0x806D;
        public const ushort ERR_PATH_GANTRY_NUM = 0x806E;
        public const ushort ERR_PATH_GANTRY_PARAMETER = 0x806F;
        public const ushort ERR_PATH_REVERSE_TORQUE = 0x8070;
        public const ushort ERR_PATH_POSERROR_CONTROL_DISABLE = 0x8071;
        public const ushort ERR_PATH_IN_CHANGE_VEL = 0x8072;
        public const ushort ERR_PATH_IN_SUPERIMPOSED = 0x8073;
        public const ushort ERR_PATH_DEC_DIST_NOT_ENOUGHT = 0x8074;
        public const ushort ERR_PATH_ADJUSTMENT_AREA_LESS_ZERO = 0x8075;
        public const ushort ERR_PATH_RC_FS_NO_OVERRANGE = 0x8076;
        public const ushort ERR_PATH_PROFILE_DISABLE = 0x8077;
        public const ushort ERR_PATH_IN_TWO_STAGE_VEL = 0x8078;
        public const ushort ERR_PATH_NOT_PTP_MOTION = 0x8079;
        public const ushort ERR_PATH_ROTCUT_SYNC_POS = 0x807A;
        public const ushort ERR_PATH_NOT_IN_ECAM_MODE = 0x807B;
        public const ushort ERR_PATH_SWITCH_PERCENT = 0x807C;
        public const ushort ERR_PATH_AXIS_DIRECTION = 0x807D;
        public const ushort ERR_PATH_ECAM_CURVE_TYPE = 0x807E;
        public const ushort ERR_PATH_ECAM_CAMIN_PARAMETERS = 0x807F;
        public const ushort ERR_PATH_ECAM_PROFILE_PARAMETERS = 0x8080;
        public const ushort ERR_PATH_ECAM_TABLE_BEING_USED = 0x8081;
        public const ushort ERR_PATH_ECAM_TABLE_ORIGINAL_RANGE = 0x8082;
        public const ushort ERR_PATH_ECAM_FS_PARAMETERS = 0x8083;
        
        public const ushort ERR_PATH_GEAR_PARAMETERS = 0x8084;
        public const ushort ERR_PATH_ECAM_DATASIZE = 0x8085;
        public const ushort ERR_PATH_MB_FIFO_FULL = 0x8086;
        public const ushort ERR_PATH_MB_ITP_START = 0x8087;
        public const ushort ERR_PATH_FIFO_0_START = 0x8088;
        public const ushort ERR_PATH_GEAR_AXISNO = 0x8089;
        public const ushort ERR_PATH_GEAR_SYNCHRONIZE_NUM = 0x808A;
        public const ushort ERR_PATH_MB_IO_ADVANCE = 0x808B;
        public const ushort ERR_PATH_MB_NONE_INITIAL = 0x808C;
        public const ushort ERR_PATH_MB_CLEAR_BUFFER = 0x808D;
        public const ushort ERR_PATH_MB_ARC_PARAMETER = 0x808E;
        public const ushort ERR_PATH_MB_SETCOMMAND = 0x808F;
        public const ushort ERR_PATH_MB_GEAR_CONTROL = 0x8090;
        public const ushort ERR_PATH_PVT_TABLE_FULL = 0x8091;
        public const ushort ERR_PATH_PVT_DATA_NONE = 0x8092;
        
        public const ushort ERR_PATH_MULTIARC_INDEX = 0x8093;
        public const ushort ERR_PATH_MULTIARC_MODE = 0x8094;
        public const ushort ERR_PATH_MOTION_MODE = 0x8095;
        public const ushort ERR_PATH_GROUP_ERROR = 0x8096;
        
        public const ushort ERR_RTX_RTSS_LOAD = 0xD000;
        public const ushort ERR_RTX_CONNECT_LINK_FAILED = 0xD001;
        public const ushort ERR_RTX_EVENT_FAILED = 0xD002;
        public const ushort ERR_RTX_CONNECT_FAILED = 0xD003;
        public const ushort ERR_RTX_CONFIG_EDITED = 0xD004;
        public const ushort ERR_RTX_SECURITY_FAILED = 0xD005;
        public const ushort ERR_RTX_COMMANDING = 0xD006;
        
        public const ushort ERR_RTX_WIN32_SYSTEM_NOT_SUPPORT = 0xD100;
        public const ushort ERR_RTX_CALLBACK_CLOSE = 0xD101;
        public const ushort ERR_RTX_CALLBACK_FUNCTION = 0xD102;
        public const ushort ERR_RTX_CALLBACK_THREAD = 0xD103;

        public const ushort ERR_CARD_NO_FOUND = 0xE000;
        public const ushort ERR_CARD_NO_RESPONSE = 0xE001;
        public const ushort ERR_CARD_CONNECT_FAILED = 0xE002;
        public const ushort ERR_CARD_MEMORY_NOT_ENOUGH = 0xE003;
        public const ushort ERR_CARD_LOAD_AUTOCONFIG_FILE = 0xE004;
        public const ushort ERR_CARD_SECURITY_FAILED = 0xE005;
        public const ushort ERR_CARD_UPGRADE_CREATE_THREAD_FAILED = 0xE006;
        public const ushort ERR_CARD_UPGRADE_NO_RESPONSE = 0xE007;
        public const ushort ERR_CARD_UPGRADE_NO_RESOURSE = 0xE008;
        public const ushort ERR_CARD_UPGRADE_LOAD_RESOURSE = 0xE009;
        public const ushort ERR_CARD_UPGRADE_TIMEOUT = 0xE00A;
        public const ushort ERR_CARD_UPGRADE_FAILED = 0xE00B;
        public const ushort ERR_CARD_UPGRADE_CHECK_CRC_FAILED = 0xE00C;
        public const ushort ERR_CARD_NOT_SUPPORT = 0xE00D;
        public const ushort ERR_CARD_SHM_CREATE_FAIL = 0xE00E;
        public const ushort ERR_CARD_OS_TYPE = 0xE00F;
        public const ushort ERR_DSP_IO_CTRL = 0xE010;
        public const ushort ERR_DSP_WRITE_DATA = 0xE011;
        public const ushort ERR_DSP_DOWNLOAD_CODE = 0xE012;
        public const ushort ERR_DSP_BOOT_MODE = 0xE013;
        public const ushort ERR_DSP_NOT_RUNNING = 0xE014;
        public const ushort ERR_DSP_NOT_FOUND = 0xE015;
        public const ushort ERR_AMP_NOT_RUNNING = 0xE016;
        public const ushort ERR_AMP_LOAD_DRIVER = 0xE017;
        public const ushort ERR_AMP_CREATE_FILE = 0xE018;
        public const ushort ERR_AMP_WRONG_DRIVER = 0xE019;
        public const ushort ERR_AMP_MEMORY_MAPPING = 0xE01A;
        public const ushort ERR_AMP_CANT_STOP = 0xE01B;
        public const ushort ERR_AMP_DSP_Device_Not_Match = 0xE01C;
        public const ushort ERR_FPGA_NOT_FOUND = 0xE01D;
        public const ushort ERR_FPGA_UNLOCK_FAILED = 0xE01E;
        public const ushort ERR_CARD_FLASH_BAD = 0xE01F;
        //public const ushort ERR_CARD_FLASH_BAD = 0xE020;
        
        public const ushort ERR_ECAT_DLL_IS_USED = 0xF000;
        public const ushort ERR_ECAT_NO_DLL_FOUND = 0xF001;
        public const ushort ERR_ECAT_NO_RTSS_DLL_FOUND = 0xF002;
        public const ushort ERR_ECAT_NO_CARD_DLL_FOUND = 0xF003;
        public const ushort ERR_ECAT_NO_ESI_DLL_FOUND = 0xF004;
        public const ushort ERR_ECAT_SAME_CARD_NUMBER = 0xF005;
        public const ushort ERR_ECAT_CARDNO_ERROR = 0xF006;
        public const ushort ERR_ECAT_GET_DLL_PATH = 0xF007;
        public const ushort ERR_ECAT_GET_DLL_VERSION = 0xF008;
        public const ushort ERR_ECAT_NOT_SUPPORT = 0xF009;
        
        public const ushort ERR_ECAT_LOADLIB_EMPTY = 0xFFFF;
    }
}
