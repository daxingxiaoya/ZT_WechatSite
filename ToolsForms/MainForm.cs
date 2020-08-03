using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZTGJWechatUtils;

namespace ToolsForms
{
    public partial class MainForm : Form
    {
        List<string> datalist = new List<string>() {
            "wx_user","cust_reply","request_log","wx_relation","wx_warehouse","wx_his"
        };

        public MainForm()
        {
            InitializeComponent();
            this.pb_sync.Maximum = datalist.Count;//设置进度条最多值
        }

        /// <summary>
        /// 开始同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_start_Click(object sender, EventArgs e)
        {
            btn_start.Enabled = false;
            try
            {
                txt_msg.Text = "开始同步数据......";

                new Task(new Action(() =>
                {
                    for (int i = 0; i < datalist.Count; i++)
                    {
                        txt_msg.Text += "\r\n" + "===>正在同步数据表" + datalist[i];
                        Thread.Sleep(new Random().Next(300, 2000));
                        this.pb_sync.Value = i + 1;
                        txt_msg.Text += "\r\n" + "数据表" + datalist[i] + "同步完成";
                    }
                    txt_msg.Text += "\r\n" + "数据同步结束！";
                    btn_start.Enabled = true;
                })).Start();
            }
            catch (Exception ex)
            {
                string sre = ex.Message;
                LogHelper.ErrorLog(ex.Message + ",追踪" + ex.StackTrace);
                btn_start.Enabled = true;
            }
            
        }



    }
}
