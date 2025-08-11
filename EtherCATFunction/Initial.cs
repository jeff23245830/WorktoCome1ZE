using EtherCAT_DLL;
using EtherCAT_DLL_Err;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EtherCATFunction
{
    public class Initial
    {
        bool g_bInitialFlag = false;
        ushort g_uRet = 0;
        ushort g_nESCExistCards = 0, g_uESCCardNo = 0, g_uESCNodeID = 0, g_uESCSlotID;
        ushort[] g_uESCCardNoList = new ushort[32];

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



    }
}
