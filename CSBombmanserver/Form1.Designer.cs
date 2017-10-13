using System.Drawing;

namespace CSBombmanServer
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (my_timer != null))
            {
                my_timer.Stop();
                my_timer.Dispose();
            }

            if (disposing && (Players != null))
            {
                Players.ForEach(p => p.Dispose());
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
			this.prev2 = new System.Windows.Forms.Button();
			this.prev = new System.Windows.Forms.Button();
			this.next = new System.Windows.Forms.Button();
			this.next2 = new System.Windows.Forms.Button();
			this.textArea = new System.Windows.Forms.TextBox();
			this.stop = new System.Windows.Forms.Button();
			this.play = new System.Windows.Forms.Button();
			this.fast = new System.Windows.Forms.Button();
			this.faster = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.infoArea = new System.Windows.Forms.TextBox();
			this.retry = new System.Windows.Forms.Button();
			this.field = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// prev2
			// 
			this.prev2.Location = new System.Drawing.Point(22, 12);
			this.prev2.Name = "prev2";
			this.prev2.Size = new System.Drawing.Size(39, 23);
			this.prev2.TabIndex = 0;
			this.prev2.Text = "<<";
			this.prev2.UseVisualStyleBackColor = true;
			this.prev2.Click += new System.EventHandler(this.prev2_Click);
			// 
			// prev
			// 
			this.prev.Location = new System.Drawing.Point(84, 12);
			this.prev.Name = "prev";
			this.prev.Size = new System.Drawing.Size(34, 23);
			this.prev.TabIndex = 1;
			this.prev.Text = "<";
			this.prev.UseVisualStyleBackColor = true;
			this.prev.Click += new System.EventHandler(this.prev_Click);
			// 
			// next
			// 
			this.next.Location = new System.Drawing.Point(143, 12);
			this.next.Name = "next";
			this.next.Size = new System.Drawing.Size(34, 23);
			this.next.TabIndex = 2;
			this.next.Text = ">";
			this.next.UseVisualStyleBackColor = true;
			this.next.Click += new System.EventHandler(this.next_Click);
			// 
			// next2
			// 
			this.next2.Location = new System.Drawing.Point(208, 12);
			this.next2.Name = "next2";
			this.next2.Size = new System.Drawing.Size(54, 23);
			this.next2.TabIndex = 3;
			this.next2.Text = ">>";
			this.next2.UseVisualStyleBackColor = true;
			this.next2.Click += new System.EventHandler(this.next2_Click);
			// 
			// textArea
			// 
			this.textArea.Location = new System.Drawing.Point(2, 435);
			this.textArea.Multiline = true;
			this.textArea.Name = "textArea";
			this.textArea.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textArea.Size = new System.Drawing.Size(477, 114);
			this.textArea.TabIndex = 5;
			// 
			// stop
			// 
			this.stop.Location = new System.Drawing.Point(22, 41);
			this.stop.Name = "stop";
			this.stop.Size = new System.Drawing.Size(75, 23);
			this.stop.TabIndex = 6;
			this.stop.Text = "stop";
			this.stop.UseVisualStyleBackColor = true;
			this.stop.Click += new System.EventHandler(this.stop_Click);
			// 
			// play
			// 
			this.play.Location = new System.Drawing.Point(112, 41);
			this.play.Name = "play";
			this.play.Size = new System.Drawing.Size(75, 23);
			this.play.TabIndex = 7;
			this.play.Text = "play";
			this.play.UseVisualStyleBackColor = true;
			this.play.Click += new System.EventHandler(this.play_Click);
			// 
			// fast
			// 
			this.fast.Location = new System.Drawing.Point(208, 41);
			this.fast.Name = "fast";
			this.fast.Size = new System.Drawing.Size(75, 23);
			this.fast.TabIndex = 8;
			this.fast.Text = "fast";
			this.fast.UseVisualStyleBackColor = true;
			this.fast.Click += new System.EventHandler(this.fast_Click);
			// 
			// faster
			// 
			this.faster.Location = new System.Drawing.Point(299, 41);
			this.faster.Name = "faster";
			this.faster.Size = new System.Drawing.Size(75, 23);
			this.faster.TabIndex = 9;
			this.faster.Text = "faster";
			this.faster.UseVisualStyleBackColor = true;
			this.faster.Click += new System.EventHandler(this.faster_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(279, 16);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(122, 16);
			this.checkBox1.TabIndex = 10;
			this.checkBox1.Text = "Stop At Settlement";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// infoArea
			// 
			this.infoArea.Location = new System.Drawing.Point(338, 70);
			this.infoArea.Multiline = true;
			this.infoArea.Name = "infoArea";
			this.infoArea.ReadOnly = true;
			this.infoArea.Size = new System.Drawing.Size(141, 359);
			this.infoArea.TabIndex = 11;
			// 
			// retry
			// 
			this.retry.Location = new System.Drawing.Point(397, 41);
			this.retry.Name = "retry";
			this.retry.Size = new System.Drawing.Size(75, 23);
			this.retry.TabIndex = 12;
			this.retry.Text = "retry";
			this.retry.UseVisualStyleBackColor = true;
			this.retry.Click += new System.EventHandler(this.retry_Click);
			// 
			// field
			// 
			this.field.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.field.Location = new System.Drawing.Point(2, 70);
			this.field.Name = "field";
			this.field.ReadOnly = true;
			this.field.Size = new System.Drawing.Size(330, 358);
			this.field.TabIndex = 13;
			this.field.Text = "";
			this.field.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Field_KeyDown);
			this.field.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Field_KeyUp);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 561);
			this.Controls.Add(this.field);
			this.Controls.Add(this.retry);
			this.Controls.Add(this.infoArea);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.faster);
			this.Controls.Add(this.fast);
			this.Controls.Add(this.play);
			this.Controls.Add(this.stop);
			this.Controls.Add(this.textArea);
			this.Controls.Add(this.next2);
			this.Controls.Add(this.next);
			this.Controls.Add(this.prev);
			this.Controls.Add(this.prev2);
			this.Name = "Form1";
			this.Text = "C# ボムマン ${VERSION}";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button prev2;
        private System.Windows.Forms.Button prev;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Button next2;
        private System.Windows.Forms.TextBox textArea;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.Button play;
        private System.Windows.Forms.Button fast;
        private System.Windows.Forms.Button faster;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox infoArea;
        private System.Windows.Forms.Button retry;
        private System.Windows.Forms.RichTextBox field;
    }
}

