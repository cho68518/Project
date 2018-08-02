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
using System.IO;


namespace _05_ClientSocket
{
    public partial class Form1 : Form
    {
        // 변수 선언
        string server_ip = null;                    // 서버 IP 주소
        Boolean isConnected = false;

        Socket m_cliSocket = null;
        AsyncCallback m_fnRecvHandler;
        AsyncCallback m_fnSendHandler;

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
            // Default IP 주소 설정 : 
            server_ip = "127.0.0.1";

            // 서버 IP 주소에 표시
            maskedTextBox_ip.Mask = server_ip;          // 마스크 수정
            maskedTextBox_ip.Text = server_ip;          // IP 주소 값을 넣음

            // Default port 번호
            textBox_portnum.Text = "1234";
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            m_fnRecvHandler = new AsyncCallback(handleDataReceive);
            m_fnSendHandler = new AsyncCallback(handleDataSend);

            // 소켓 생성
            m_cliSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            isConnected = false;
            try
            {

                int portnum = Convert.ToInt32(textBox_portnum.Text);
                IPAddress srv_ip = IPAddress.Parse(server_ip);
                m_cliSocket.Connect(srv_ip, portnum);
                isConnected = true;
            }
            catch
            {
                isConnected = false;
            }

            if (isConnected)
            {
                // 4096 바이트의 크기를 갖는 바이트 배열을 가진 AsyncObject 클래스 생성
                AsyncObject ao = new AsyncObject(4096);

                // 작업 중인 소켓을 저장하기 위해 sockClient 할당
                ao.WorkingSocket = m_cliSocket;

                // 비동기적으로 들어오는 자료를 수신하기 위해 BeginReceive 메서드 사용!
                m_cliSocket.BeginReceive(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, m_fnRecvHandler, ao);

                textBox_msg.Text += "서버 연결 성공 !!\r\n";
            }
            else
            {
                textBox_msg.Text += "서버 연결 실패 !!\r\n";
            }            
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            m_cliSocket.Disconnect(true);
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            // 최소한의 크기르 배열을 초기화합니다.
            AsyncObject ao = new AsyncObject(1);

            // 문자열을 바이트 배열으로 변환
            ao.Buffer = Encoding.Unicode.GetBytes(textBox_sendtxt.Text);

            ao.WorkingSocket = m_cliSocket;

            // 전송 시작!
            try
            {
                m_cliSocket.BeginSend(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, m_fnSendHandler, ao);
            }
            catch (Exception ex)
            {
                textBox_msg.Text +="전송 중 오류 발생 !! \r\n";
            }
        }

        private void handleDataReceive(IAsyncResult ar)
        {
            AsyncObject ao = (AsyncObject)ar.AsyncState;

            // 받은 바이트 수 저장할 변수 선언
            Int32 recvBytes;

            try
            {
                // 자료를 수신하고, 수신받은 바이트를 가져옵니다.
                recvBytes = ao.WorkingSocket.EndReceive(ar);
            }
            catch
            {
                // 예외가 발생하면 함수 종료!
                return;
            }

            // 수신받은 자료의 크기가 1 이상일 때에만 자료 처리
            if (recvBytes > 0)
            {
                // 공백 문자들이 많이 발생할 수 있으므로, 받은 바이트 수 만큼 배열을 선언하고 복사한다.
                Byte[] msgByte = new Byte[recvBytes];
                Array.Copy(ao.Buffer, msgByte, recvBytes);

                // 받은 메세지를 출력
                string str = "메세지 받음: " + Convert.ToString(msgByte) + "\r\n";
                this.BeginInvoke(new SetTextCallBack(display_data), new object[] { str });
            }

            try
            {
                // 자료 처리가 끝났으면~
                // 이제 다시 데이터를 수신받기 위해서 수신 대기를 해야 합니다.
                // Begin~~ 메서드를 이용해 비동기적으로 작업을 대기했다면
                // 반드시 대리자 함수에서 End~~ 메서드를 이용해 비동기 작업이 끝났다고 알려줘야 합니다!
                ao.WorkingSocket.BeginReceive(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, m_fnRecvHandler, ao);
            }
            catch (Exception ex)
            {
                // 예외가 발생하면 예외 정보 출력 후 함수를 종료한다.
                string str = "자료 수신 대기 도중 오류 발생! !\r\n";
                this.BeginInvoke(new SetTextCallBack(display_data), new object[] { str });
                return;
            }
        }
        private void handleDataSend(IAsyncResult ar)
        {

            // 넘겨진 추가 정보를 가져옵니다.
            AsyncObject ao = (AsyncObject)ar.AsyncState;

            // 보낸 바이트 수를 저장할 변수 선언
            Int32 sentBytes;

            try
            {
                // 자료를 전송하고, 전송한 바이트를 가져옵니다.
                sentBytes = ao.WorkingSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                // 예외가 발생하면 예외 정보 출력 후 함수를 종료한다.
                string str = "자료 송신 도중 오류 발생!\r\n";
                this.BeginInvoke(new SetTextCallBack(display_data), new object[] { str });

                return;
            }

            if (sentBytes > 0)
            {
                // 여기도 마찬가지로 보낸 바이트 수 만큼 배열 선언 후 복사한다.
                Byte[] msgByte = new Byte[sentBytes];
                Array.Copy(ao.Buffer, msgByte, sentBytes);

                string str = "메세지 보냄: " + textBox_sendtxt.Text + "\r\n";
                this.BeginInvoke(new SetTextCallBack(display_data), new object[] { str });
            }
        }       

        private void display_data(string str)
        {
            this.textBox_msg.Text += str;
        }
    }
}
