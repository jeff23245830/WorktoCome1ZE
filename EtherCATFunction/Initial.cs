using EtherCAT_DLL;
using EtherCAT_DLL_Err;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace EtherCATFunction
{
    public class SlaveInfo
    {
        public ushort NodeID { get; set; }
        public ushort SlotID { get; set; }
        public string Description { get; set; }
    }
    public class Initial
    {
        bool g_bInitialFlag = false;
        ushort g_uRet = 0;
        ushort g_nESCExistCards = 0, g_uESCCardNo = 0, g_uESCNodeID = 0, g_uESCSlotID;
        ushort[] g_uESCCardNoList = new ushort[32];
        public List<SlaveInfo> FoundSlaves { get; private set; } = new List<SlaveInfo>();

        //屬性
        public ushort CardCount => g_nESCExistCards;
        public ushort g_ESCNodeID_u => g_uESCNodeID;
        public ushort g_ESCSlotID_u => g_uESCSlotID;
        public bool Initial_Card() {
            ushort uCount = 0;
            ushort uCardNo = 0;

            // 1. 抓取所有卡片，取得存在的卡片數量
            g_uRet = CEtherCAT_DLL.CS_ECAT_Master_Open(ref g_nESCExistCards);

            // 如果找不到任何卡片，或開啟失敗，就直接回傳 false
            if (g_uRet != 0 || g_nESCExistCards == 0)
            {
               
                return false;
            }

            // 2. 針對每一張找到的卡片進行初始化
            bool allCardsInitialized = true;
            for (uCount = 0; uCount < g_nESCExistCards; uCount++)
            {
                // 取得卡片序列號
                g_uRet = CEtherCAT_DLL.CS_ECAT_Master_Get_CardSeq(uCount, ref uCardNo);
                if (g_uRet != 0)
                {
                    // 如果取得序列號失敗，也視為初始化失敗
                    allCardsInitialized = false;
                    continue; // 跳過這張卡片，嘗試下一張
                }

                // 初始化卡片
                g_uRet = CEtherCAT_DLL.CS_ECAT_Master_Initial(uCardNo);
                if (g_uRet != 0)
                {
                    // 如果初始化失敗，記錄下來
                    allCardsInitialized = false;
                } 
            }

            // 如果 g_bInitialFlag 這個變數一定要用
            g_bInitialFlag = allCardsInitialized;

            // 3. 回傳最終的初始化狀態
            // 這裡回傳的是「所有卡片都成功初始化」的狀態
            return allCardsInitialized;

        }

        public bool Card_FindSlave()
        {
            short nSID = 0, Cnt = 0;
            ushort uNID = 0, uSlaveNum = 0, uReMapNodeID = 0;
            uint uVendorID = 0, uProductCode = 0, uRevisionNo = 0, uSlaveDCTime = 0;


            string TxtSlaveNum, CmbNodeID, CmbSlotID;

            TxtSlaveNum = "0";

            CmbNodeID = "0";

            CmbSlotID = "0";

            g_uRet = CEtherCAT_DLL.CS_ECAT_Master_Get_SlaveNum(g_uESCCardNo, ref uSlaveNum);

            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                //AddErrMsg("_ECAT_Master_Get_SlaveNum, ErrorCode = " + g_uRet.ToString(), true);
                return false;
            }
            else
            {
                if (uSlaveNum == 0)
                { 
                    //AddErrMsg("Card NO: " + g_uESCCardNo.ToString() + " No slave found!", true);
                    return false;
                }
                else
                {
                    
                     
                    TxtSlaveNum = uSlaveNum.ToString();
                    for (uNID = 0; uNID < uSlaveNum; uNID++)
                    {
                        g_uRet = CEtherCAT_DLL.CS_ECAT_Master_Get_Slave_Info(g_uESCCardNo, uNID, ref uReMapNodeID, ref uVendorID, ref uProductCode, ref uRevisionNo, ref uSlaveDCTime);

                        if ((uVendorID == 0x1A05 || uVendorID == 0x1DD) && (uProductCode == 0x7062 || uProductCode == 0x70A2 || uProductCode == 0x71A2)) //Ec16Out
                        {
                            nSID = 0;
                            //CmbNode.Items.Add("NodeID:" + uNID + " - SlotID:" + nSID + "-Ec16DI");
                            FoundSlaves.Add(new SlaveInfo { NodeID = uNID, SlotID = (ushort)nSID, Description = "Ec16DI" });

                            Cnt++;
                        }

                        
                        else if ((uVendorID == 0x1A05 || uVendorID == 0x1DD) && uProductCode == 0x5621) //EcAxis
                        {
                            nSID = 0;
                            FoundSlaves.Add(new SlaveInfo { NodeID = uNID, SlotID = (ushort)nSID, Description = "EcAxis" });

                            Cnt++;
                        }
                        if ((uVendorID == 0x1A05 || uVendorID == 0x1DD) && (uProductCode == 0x6002 || uProductCode == 0x6022 || uProductCode == 0x6032 || uProductCode == 0x6142)) //Ec16DI
                        {
                            nSID = 0;
                            FoundSlaves.Add(new SlaveInfo { NodeID = uNID, SlotID = (ushort)nSID, Description = "Ec16DI" }); 
                             
                            Cnt++;
                        }

                    }
                    /*
                    if (Cnt > 0)
                    {
                        CmbNode.SelectedIndex = 0;
                        CmbNodeID.SelectedIndex = 0;
                        CmbSlotID.SelectedIndex = 0;
                    }
                    */
                    //只有一個的話
                    int nSelectNode = 0;
                    

                    if (FoundSlaves.Count > nSelectNode)
                    {
                        g_uESCNodeID = FoundSlaves[nSelectNode].NodeID;
                        g_uESCSlotID = FoundSlaves[nSelectNode].SlotID;
                    }
                    return true; // 找到至少一個符合條件的從站
                }
            }
        }

    }
}
