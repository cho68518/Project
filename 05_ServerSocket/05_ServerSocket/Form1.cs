using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// 네임스페이스 추가부분
using System.Net;
using System.Net.Sockets;

namespace _05_ServerSocket
{
    public partial class Form1 : Form
    {
        // 변수 선언
        string server_ip = null;                    // 서버 주소

        Socket m_srvSock = null;                    // 서버 소켓

        // callback 정의
        AsyncCallback m_fnRecvHandler;              // 데이터를 전송받을 때, 이벤트 처리 
        AsyncCallback m_fnSendHandler;              // 데이터를 전송할 때, 이벤트 처리
        AsyncCallback m_fnAcceptHandler;            // 클라이언트와 연결시 이벤트 처리

        public class AsyncObject
        {
            public Byte[] Buffer;
            public Socket WorkingSocket;
            public AsyncObject(Int32 bufferSize)
            {
                this.Buffer = new Byte[bufferSize];
            }
        }

        delegate void SetTextCallBack(string str);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 컴퓨터 IP 어드레스 가져오기
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            server_ip = string.Empty;
            for (int i = 0; i < host.AddressList.Length; i++)
                server_ip = host.AddressList[i].ToString();

            // 내부 IP 주소 설정 : 컴퓨터 IP 대신에 테스트를 위해서 내부 IP "127.0.0.1"을 사용
            server_ip = "127.0.0.1";

            // IP 어드레스 표시하기
            maskedTextBox_ip.Mask = server_ip;          // 마스크를 현재 IP 어드레스에 맞추기
            maskedTextBox_ip.Text = server_ip;          // 데이터를 쓰기

            // PORT 번호 : Default 값표시
            textBox_portnum.Text = "1234";
        }

        private void button_socket_Click(object sender, EventArgs e)
        {
            // 핸들 생성 : 데이터 수신 / 데이터 전송 / 클라이언트 연결
            m_fnRecvHandler = new AsyncCallback(HdlDataReceive);
            m_fnSendHandler = new AsyncCallback(HdlDataSend);
            m_fnAcceptHandler = new AsyncCallback(HdlClientConnectionRequest);

            // TCP/IP 소켓 생성
            // AddressFamily 열거형은 Socket 인스턴스가 주소를 해석 하기 위해 사용하는 주소 스키마를 나타낸다. 몇 가지 값들은 아래와 같다.
            // AddressFamily.InterNetwork : IP 버전 4 주소
            // AddressFamily.InterNetwork V6: IP 버전 6 주소
            // AddressFamily.Ipx : IPX / SPX 주소
            // AddressFamily.NetBios: NetBios 주소
            m_srvSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            textBox_msg.Text += "서버 소켓이 생성되었습니다. !!\r\n";
        }
        private void button_listen_Click(object sender, EventArgs e)
        {
            if (m_srvSock != null)      // 소켓이 생성되었는지 검사
            {
                // IP 주소 변환 : 
                IPAddress m_ipaddress = IPAddress.Parse(server_ip);

                // 특정 포트에서 모든 주소로부터 들어오는 연결을 받기 위해 포트를 바인딩합니다.
                // 사용한 포트: textBox.portnum의 문자을 int형으로 변환함
                int portnum = Convert.ToInt32(textBox_portnum.Text);
                m_srvSock.Bind(new IPEndPoint(IPAddress.Any, portnum));

                // 연결 요청을 받기 시작합니다.
                m_srvSock.Listen(5);

                // BeginAccept 메서드를 이용해 들어오는 연결 요청을 비동기적으로 처리합니다.
                // 연결 요청을 처리하는 함수는 handleClientConnectionRequest 입니다.
                m_srvSock.BeginAccept(m_fnAcceptHandler, null);
            }
            else
                MessageBox.Show("소켓이 생성되지 않았습니다. 먼저 소켓을 생성해주세요 !!");

            textBox_msg.Text += "서버 Listen 동작 중 !!  클라이언트 접속 대기중. !!\r\n";
        }

        private void button_sendtxt_Click(object sender, EventArgs e)
        {
            AsyncObject ao = new AsyncObject(1);
            // 문자열을 바이트 배열으로 변환
            ao.Buffer = Encoding.Unicode.GetBytes(textBox_sendtext.Text);

            // 사용된 소켓을 저장
            ao.WorkingSocket = m_srvSock;

            // 전송 시작!
            m_srvSock.BeginSend(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, m_fnSendHandler, ao);
            textBox_msg.Text += "Send : " + textBox_sendtext.Text + "\r\n";
        }

        private void button_server_end_Click(object sender, EventArgs e)
        {
            m_srvSock.Close();
        }

        private void HdlClientConnectionRequest(IAsyncResult ar)
        {
            // 클라이언트의 연결 요청을 수락합니다.
            Socket sockClient = m_srvSock.EndAccept(ar);

            IPEndPoint client_ip = (IPEndPoint)sockClient.RemoteEndPoint;
            string str = "클라이언트와 연결됨 : " + client_ip + "\r\n";

            this.BeginInvoke(new SetTextCallBack(display_data), new object[] { str });

            // 4096 바이트의 크기를 갖는 바이트 배열을 가진 AsyncObject 클래스 생성
            AsyncObject ao = new AsyncObject(4096);

            // 작업 중인 소켓을 저장하기 위해 sockClient 할당
            ao.WorkingSocket = sockClient;

            // 비동기적으로 들어오는 자료를 수신하기 위해 BeginReceive 메서드 사용!
            sockClient.BeginReceive(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, m_fnRecvHandler, ao);
        }       

        private void HdlDataReceive(IAsyncResult ar)
        {

            // 넘겨진 추가 정보를 가져옵니다.
            AsyncObject ao = (AsyncObject)ar.AsyncState;

            // 자료를 수신하고, 수신받은 바이트를 가져옵니다.
            Int32 recvBytes = ao.WorkingSocket.EndReceive(ar);

            // 수신받은 자료의 크기가 1 이상일 때에만 자료 처리
            if (recvBytes > 0)
            {
                Byte[] msgByte = new Byte[recvBytes];
                Array.Copy(ao.Buffer, msgByte, recvBytes);

                string str = "메시지 받음 : " + Encoding.Unicode.GetString(msgByte) + "\r\n";
                this.BeginInvoke(new SetTextCallBack(display_data), new object[] { str });
            }

            // 수신 대기
            ao.WorkingSocket.BeginReceive(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, m_fnRecvHandler, ao);
        }

        private void HdlDataSend(IAsyncResult ar)
        {
            // 넘겨진 추가 정보를 가져옵니다.
            AsyncObject ao = (AsyncObject)ar.AsyncState;

            // 자료를 전송하고, 전송한 바이트를 가져옵니다.
            Int32 sentBytes = ao.WorkingSocket.EndSend(ar);

            try
            {
                // 자료를 전송하고, 전송한 바이트를 가져옵니다.
                sentBytes = ao.WorkingSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                // 예외가 발생하면 예외 정보 출력 후 함수를 종료한다.
                string str = "자료 송신 도중 오류 발생  !!\r\n";
                this.BeginInvoke(new SetTextCallBack(display_data), new object[] { str });
                return;
            }

            if (sentBytes > 0)
            {
                Byte[] msgByte = new Byte[sentBytes];
                Array.Copy(ao.Buffer, msgByte, sentBytes);

                string str = "메세지 보냄 : " + Encoding.Unicode.GetString(msgByte) + "\r\n";
                this.BeginInvoke(new SetTextCallBack(display_data), new object[] { str });
            }
        }

        private void display_data(string str)
        {
            this.textBox_msg.Text += str;
        }
    }
}
