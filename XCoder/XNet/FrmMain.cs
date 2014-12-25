﻿using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using NewLife.Log;
using NewLife.Net;
using NewLife.Net.Sockets;
using XCoder;

namespace XNet
{
    public partial class FrmMain : Form
    {
        //TcpServer _TcpSever;
        //UdpServer _UdpServer;
        NetServer _Server;
        TcpSession _TcpSession;

        #region 窗体
        public FrmMain()
        {
            InitializeComponent();

            //var asmx = AssemblyX.Entry;
            //this.Text = asmx.Title;

            this.Icon = IcoHelper.GetIcon("网络");
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            txtReceive.UseWinFormControl();
            NetHelper.Debug = true;

            txtReceive.SetDefaultStyle(12);
            txtSend.SetDefaultStyle(12);
            numMutilSend.SetDefaultStyle(12);

            gbReceive.Tag = gbReceive.Text;
            gbSend.Tag = gbSend.Text;

            cbMode.DataSource = EnumHelper.GetDescriptions<WorkModes>().Select(kv => kv.Value).ToList();
            cbMode.SelectedIndex = 0;

            cbAddr.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAddr.DataSource = GetIPs();

            var config = NetConfig.Current;
            if (config.Port > 0) numPort.Value = config.Port;

            //var menu = spList.Menu;
            //txtReceive.ContextMenuStrip = menu;

            //// 添加清空
            //menu.Items.Insert(0, new ToolStripSeparator());
            ////var ti = menu.Items.Add("清空");
            //var ti = new ToolStripMenuItem("清空");
            //menu.Items.Insert(0, ti);
            //ti.Click += mi清空_Click;

            //ti = new ToolStripMenuItem("字体");
            //menu.Items.Add(ti);
            //ti.Click += mi字体_Click;

            //ti = new ToolStripMenuItem("前景色");
            //menu.Items.Add(ti);
            //ti.Click += mi前景色_Click;

            //ti = new ToolStripMenuItem("背景色");
            //menu.Items.Add(ti);
            //ti.Click += mi背景色_Click;

            // 加载保存的颜色
            var ui = UIConfig.Load();
            if (ui != null)
            {
                try
                {
                    txtReceive.Font = ui.Font;
                    txtReceive.BackColor = ui.BackColor;
                    txtReceive.ForeColor = ui.ForeColor;
                }
                catch { ui = null; }
            }
            if (ui == null)
            {
                ui = UIConfig.Current;
                ui.Font = txtReceive.Font;
                ui.BackColor = txtReceive.BackColor;
                ui.ForeColor = txtReceive.ForeColor;
                ui.Save();
            }
        }
        #endregion

        #region 收发数据
        void Connect()
        {
            _TcpSession = null;

            var port = (Int32)numPort.Value;

            var config = NetConfig.Current;
            config.Port = port;

            _Server = new NetServer();
            _Server.Log = XTrace.Log;
            _Server.Port = port;
            if (!cbAddr.Text.Contains("所有本地")) _Server.Local.Host = cbAddr.Text;
            _Server.Received += OnReceived;

            switch (GetMode())
            {
                case WorkModes.UDP_TCP:
                    _Server.Start();
                    break;
                case WorkModes.UDP:
                    _Server.ProtocolType = ProtocolType.Udp;
                    _Server.Start();
                    break;
                case WorkModes.TCP_Server:
                    _Server.ProtocolType = ProtocolType.Tcp;
                    _Server.Start();
                    break;
                case WorkModes.TCP_Client:
                    _Server = null;
                    _TcpSession = new TcpSession();
                    _TcpSession.Log = XTrace.Log;
                    _TcpSession.Received += OnReceived;
                    _TcpSession.Remote.Port = port;
                    _TcpSession.Remote.Host = cbAddr.Text;
                    _TcpSession.Open();

                    config.Address = cbAddr.Text;
                    break;
                default:
                    break;
            }

            pnlSetting.Enabled = false;
            btnConnect.Text = "关闭";

            config.Save();
        }

        void Disconnect()
        {
            if (_TcpSession != null)
            {
                _TcpSession.Dispose();
                _TcpSession = null;
            }
            if (_Server != null)
            {
                _Server.Dispose();
                _Server = null;
            }

            pnlSetting.Enabled = true;
            btnConnect.Text = "打开";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn.Text == "打开")
                Connect();
            else
                Disconnect();
        }

        void OnReceived(Object sender, ReceivedEventArgs e)
        {
            var session = sender as ISocketSession;
            if (session == null)
            {
                var ns = sender as INetSession;
                if (ns == null) return;
                session = ns.Session;
            }

            //var line = e.Stream.ToStr();
            var line = String.Format("{0} [{1}]: {2}\r\n", session.Remote, e.Length, e.Data.ToHex(0, e.Length));
            //XTrace.UseWinFormWriteLog(txtReceive, line, 100000);
            TextControlLog.WriteLog(txtReceive, line);
        }

        Int32 _pColor = 0;
        Int32 BytesOfReceived = 0;
        Int32 BytesOfSent = 0;
        Int32 lastReceive = 0;
        Int32 lastSend = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!pnlSetting.Enabled)
            {
                //    // 检查串口是否已断开，自动关闭已断开的串口，避免内存暴涨
                //    if (!spList.Enabled && btnConnect.Text == "打开")
                //    {
                //        Disconnect();
                //        return;
                //    }

                var rcount = BytesOfReceived;
                var tcount = BytesOfSent;
                if (rcount != lastReceive)
                {
                    gbReceive.Text = (gbReceive.Tag + "").Replace("0", rcount + "");
                    lastReceive = rcount;
                }
                if (tcount != lastSend)
                {
                    gbSend.Text = (gbSend.Tag + "").Replace("0", tcount + "");
                    lastSend = tcount;
                }

                txtReceive.ColourDefault(_pColor);
                _pColor = txtReceive.TextLength;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var str = txtSend.Text;
            if (String.IsNullOrEmpty(str))
            {
                MessageBox.Show("发送内容不能为空！", this.Text);
                txtSend.Focus();
                return;
            }

            // 多次发送
            var count = (Int32)numMutilSend.Value;
            for (int i = 0; i < count; i++)
            {
                //spList.Send(str);
                _TcpSession.Send(str);

                Thread.Sleep(100);
            }
        }
        #endregion

        #region 右键菜单
        private void mi清空_Click(object sender, EventArgs e)
        {
            txtReceive.Clear();
            //spList.ClearReceive();
        }

        private void mi清空2_Click(object sender, EventArgs e)
        {
            txtSend.Clear();
            //spList.ClearSend();
        }

        void mi字体_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = txtReceive.Font;
            if (fontDialog1.ShowDialog() != DialogResult.OK) return;

            txtReceive.Font = fontDialog1.Font;

            var ui = UIConfig.Current;
            ui.Font = txtReceive.Font;
            ui.Save();
        }

        void mi前景色_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = txtReceive.ForeColor;
            if (colorDialog1.ShowDialog() != DialogResult.OK) return;

            txtReceive.ForeColor = colorDialog1.Color;

            var ui = UIConfig.Current;
            ui.ForeColor = txtReceive.ForeColor;
            ui.Save();
        }

        void mi背景色_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = txtReceive.BackColor;
            if (colorDialog1.ShowDialog() != DialogResult.OK) return;

            txtReceive.BackColor = colorDialog1.Color;

            var ui = UIConfig.Current;
            ui.BackColor = txtReceive.BackColor;
            ui.Save();
        }
        #endregion

        private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var mode = GetMode();
            if ((Int32)mode == 0) return;

            switch (mode)
            {
                case WorkModes.UDP_TCP:
                case WorkModes.UDP:
                case WorkModes.TCP_Server:
                    cbAddr.DropDownStyle = ComboBoxStyle.DropDownList;
                    cbAddr.DataSource = GetIPs();
                    break;
                case WorkModes.TCP_Client:
                    cbAddr.DropDownStyle = ComboBoxStyle.DropDown;
                    cbAddr.DataSource = null;
                    cbAddr.Items.Clear();
                    cbAddr.Text = NetConfig.Current.Address;
                    break;
                default:
                    break;
            }
        }

        WorkModes GetMode()
        {
            var mode = cbMode.Text;
            if (String.IsNullOrEmpty(mode)) return (WorkModes)0;

            var wm = EnumHelper.GetDescriptions<WorkModes>().Where(kv => kv.Value == mode).Select(kv => kv.Key).First();
            return wm;
        }

        String[] GetIPs()
        {
            var list = NetHelper.GetIPs().Select(e => e.ToString()).ToList();
            list.Insert(0, "所有本地IPv4/IPv6");
            list.Insert(1, IPAddress.Any.ToString());
            list.Insert(2, IPAddress.IPv6Any.ToString());

            return list.ToArray();
        }
    }
}