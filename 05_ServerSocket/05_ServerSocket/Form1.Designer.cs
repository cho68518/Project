namespace _05_ServerSocket
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.maskedTextBox_ip = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_portnum = new System.Windows.Forms.TextBox();
            this.button_socket = new System.Windows.Forms.Button();
            this.button_listen = new System.Windows.Forms.Button();
            this.button_server_end = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_sendtext = new System.Windows.Forms.TextBox();
            this.button_sendtxt = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_msg = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "서버 IP 어드레스 : ";
            // 
            // maskedTextBox_ip
            // 
            this.maskedTextBox_ip.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.maskedTextBox_ip.Location = new System.Drawing.Point(160, 10);
            this.maskedTextBox_ip.Mask = "000.000.000.000";
            this.maskedTextBox_ip.Name = "maskedTextBox_ip";
            this.maskedTextBox_ip.Size = new System.Drawing.Size(195, 26);
            this.maskedTextBox_ip.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(33, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "연결포트번호 :";
            // 
            // textBox_portnum
            // 
            this.textBox_portnum.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_portnum.Location = new System.Drawing.Point(160, 52);
            this.textBox_portnum.Name = "textBox_portnum";
            this.textBox_portnum.Size = new System.Drawing.Size(195, 26);
            this.textBox_portnum.TabIndex = 3;
            // 
            // button_socket
            // 
            this.button_socket.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_socket.Location = new System.Drawing.Point(387, 6);
            this.button_socket.Name = "button_socket";
            this.button_socket.Size = new System.Drawing.Size(144, 34);
            this.button_socket.TabIndex = 4;
            this.button_socket.Text = "소켓생성";
            this.button_socket.UseVisualStyleBackColor = true;
            this.button_socket.Click += new System.EventHandler(this.button_socket_Click);
            // 
            // button_listen
            // 
            this.button_listen.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_listen.Location = new System.Drawing.Point(387, 46);
            this.button_listen.Name = "button_listen";
            this.button_listen.Size = new System.Drawing.Size(144, 34);
            this.button_listen.TabIndex = 5;
            this.button_listen.Text = "Listen";
            this.button_listen.UseVisualStyleBackColor = true;
            this.button_listen.Click += new System.EventHandler(this.button_listen_Click);
            // 
            // button_server_end
            // 
            this.button_server_end.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_server_end.Location = new System.Drawing.Point(537, 6);
            this.button_server_end.Name = "button_server_end";
            this.button_server_end.Size = new System.Drawing.Size(90, 72);
            this.button_server_end.TabIndex = 6;
            this.button_server_end.Text = "서버종료";
            this.button_server_end.UseVisualStyleBackColor = true;
            this.button_server_end.Click += new System.EventHandler(this.button_server_end_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(12, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "전송문자 :";
            // 
            // textBox_sendtext
            // 
            this.textBox_sendtext.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_sendtext.Location = new System.Drawing.Point(100, 96);
            this.textBox_sendtext.Name = "textBox_sendtext";
            this.textBox_sendtext.Size = new System.Drawing.Size(577, 22);
            this.textBox_sendtext.TabIndex = 8;
            // 
            // button_sendtxt
            // 
            this.button_sendtxt.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_sendtxt.Location = new System.Drawing.Point(683, 89);
            this.button_sendtxt.Name = "button_sendtxt";
            this.button_sendtxt.Size = new System.Drawing.Size(105, 37);
            this.button_sendtxt.TabIndex = 9;
            this.button_sendtxt.Text = "전송";
            this.button_sendtxt.UseVisualStyleBackColor = true;
            this.button_sendtxt.Click += new System.EventHandler(this.button_sendtxt_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(13, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "메시지 :";
            // 
            // textBox_msg
            // 
            this.textBox_msg.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_msg.Location = new System.Drawing.Point(16, 150);
            this.textBox_msg.Multiline = true;
            this.textBox_msg.Name = "textBox_msg";
            this.textBox_msg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_msg.Size = new System.Drawing.Size(772, 288);
            this.textBox_msg.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox_msg);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_sendtxt);
            this.Controls.Add(this.textBox_sendtext);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_server_end);
            this.Controls.Add(this.button_listen);
            this.Controls.Add(this.button_socket);
            this.Controls.Add(this.textBox_portnum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.maskedTextBox_ip);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "소켓서버프로그램";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_ip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_portnum;
        private System.Windows.Forms.Button button_socket;
        private System.Windows.Forms.Button button_listen;
        private System.Windows.Forms.Button button_server_end;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_sendtext;
        private System.Windows.Forms.Button button_sendtxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_msg;
    }
}

