namespace ChatServer
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtIP = new System.Windows.Forms.TextBox();
            this.numPorta = new System.Windows.Forms.NumericUpDown();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.listaLog = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.numPorta)).BeginInit();
            this.SuspendLayout();
            // 
            // txtIP
            // 
            this.txtIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP.Location = new System.Drawing.Point(12, 12);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(343, 38);
            this.txtIP.TabIndex = 0;
            this.txtIP.Text = "127.0.0.1";
            // 
            // numPorta
            // 
            this.numPorta.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPorta.Location = new System.Drawing.Point(392, 12);
            this.numPorta.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numPorta.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numPorta.Name = "numPorta";
            this.numPorta.Size = new System.Drawing.Size(134, 38);
            this.numPorta.TabIndex = 1;
            this.numPorta.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // btnStartServer
            // 
            this.btnStartServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartServer.Location = new System.Drawing.Point(570, 12);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(195, 38);
            this.btnStartServer.TabIndex = 2;
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.UseVisualStyleBackColor = true;
            // 
            // listaLog
            // 
            this.listaLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaLog.FormattingEnabled = true;
            this.listaLog.ItemHeight = 29;
            this.listaLog.Location = new System.Drawing.Point(12, 63);
            this.listaLog.Name = "listaLog";
            this.listaLog.Size = new System.Drawing.Size(753, 323);
            this.listaLog.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listaLog);
            this.Controls.Add(this.btnStartServer);
            this.Controls.Add(this.numPorta);
            this.Controls.Add(this.txtIP);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numPorta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.NumericUpDown numPorta;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.ListBox listaLog;
    }
}

